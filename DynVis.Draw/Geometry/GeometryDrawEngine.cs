using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DynVis.Core;
using DynVis.Core.Elements;
using DynVis.Core.Geometry;
using DynVis.Core.PropertyEditor;
using DynVis.Draw.Properties;
using DynVis.Math;
using Tao.OpenGl;

namespace DynVis.Draw.Geometry
{
    public class GeometryDrawEngine : BaseEngine
    {
        public GeometryDrawEngine(Control control)
            : base(control)
        {
            SetProperties();
        }

        private const int BUFSIZE = 512;

        public int ObjectSliceCount = GlobalParams.GetInt("ObjectSliceCount", 20);
        public int ObjectStacksCount = GlobalParams.GetInt("ObjectStacksCount", 20);

        [TransactingEditable]
        [SavableProperty]
        public double BondRadius { get; set; }
        [TransactingEditable]
        [SavableProperty]
        public Color SelectedAtomsColor { get; set; }
        [TransactingEditable]
        [SavableProperty]
        public float ScaleTextOnAtom { get; set; }


        private void SetProperties()
        {
            BondRadius = 0.05;

            SelectedAtomsColor = Color.FromArgb(255, 255, 255);

            ScaleTextOnAtom = 0.3f;
        }


        public void DrawAtom(IAtom atom)
        {
            DrawAtomByColor(atom, atom.Color());
        }

        public void DrawAtomByColor(IAtom atom, Color color)
        {
            var GluObj = CreateNewObject();

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glPushMatrix();

            //Переносим центр на координату атома
            Gl.glTranslated(atom.Coordinate.X, atom.Coordinate.Y, atom.Coordinate.Z);

            SetColor(color); //Устанавливаем цвет атома
            Glu.gluSphere(GluObj, atom.Radius(), ObjectSliceCount, ObjectStacksCount); //Рисуем сферу атома

            DeleteObject(GluObj);

            Gl.glPopMatrix();
        }

        /// <summary>
        /// Выводит текст на атоме
        /// </summary>
        private void DrawTextOnCurrentAtom(IAtom atom, string text)
        {
            Gl.glPushMatrix();
            //Переносим центр на координату атома
            Gl.glTranslated(atom.Coordinate.X, atom.Coordinate.Y, atom.Coordinate.Z);

            DirectAxesZToCamera(); //Направляем ось Z на камеру
            Gl.glTranslated(0, 0, atom.Radius()); //Подвигаем координаты ближе к зрителю, чтобы буквы не сидели внутри сферы
            Gl.glScalef(ScaleTextOnAtom, ScaleTextOnAtom, ScaleTextOnAtom); //Масштабируем, чтобы буквы не были слишком большие

            //Color(atom.Color().ReverseColor()); //Устанавливаем обратный цвет для контрастности
            SetColor(Color.Black);

            var enablingLighting = Gl.glIsEnabled(Gl.GL_LIGHTING);
            Gl.glDisable(Gl.GL_LIGHTING); //Выключам осфещение, чтобы не бликовало
            PrintString(text, 0, 0, 0);
            if (enablingLighting != 0) Gl.glEnable(Gl.GL_LIGHTING); //Включаем осфещение, если было включено

            Gl.glPopMatrix();
        }

        public void DrawBond(IBond bond)
        {
            var GluObj = CreateNewObject();

            SetColor(bond.Color());

            Gl.glPushMatrix();

            var atom1 = bond.Atom1;
            var atom2 = bond.Atom2;

            TranslateDirectAxesZToPoint(atom1.Coordinate, atom2.Coordinate);

            Glu.gluCylinder(GluObj, BondRadius, BondRadius, atom1.Coordinate.DistanceTo(atom2.Coordinate), ObjectSliceCount, ObjectStacksCount);
            Gl.glPopMatrix();

            DeleteObject(GluObj);
        }

        public void DrawAtoms(IAtomStructure atoms, List<IAtom> selectionAtoms)
        {
            foreach (IAtom atom in atoms)
            {
                if (selectionAtoms.Contains(atom))
                {
                    DrawAtomByColor(atom, SelectedAtomsColor);
                } 
                else
                {
                    DrawAtom(atom);
                }
                

                DrawTextOnCurrentAtom(atom, atom.Symbol());
            }
        }

        private void DrawNamedAtoms(IAtomStructure atoms)
        {
            var atomNumber = 0;
            foreach (IAtom atom in atoms)
            {
                Gl.glPushName(atomNumber);
                DrawAtom(atom);
                Gl.glPopName();
                atomNumber++;
            }
        }

        public void DrawBonds(IBondList bonds, ICollection<IBond> manualBonds)
        {
            var listBond = BondList.JoinVisibleBonds(bonds, manualBonds);

            foreach (var bond in listBond)
            {
                DrawBond(bond);
            }
        }

        public void DrawStructures(IAtomStructure atoms, List<IAtom> selectionAtoms, ICollection<IBond> manualBonds)
        {
            DrawAtoms(atoms, selectionAtoms);

            DrawBonds(atoms.BondList, manualBonds);
        }


        
        public IAtom[] SelectAtoms(IAtomStructure atoms, int x, int y)
        {
            var selectBuffer = new int[BUFSIZE];
            var viewport = new int[4];

            if (BeginScene())
            {
                Gl.glGetIntegerv(Gl.GL_VIEWPORT, viewport);

                var Width = viewport[2];
                var Height = viewport[3];

                Gl.glSelectBuffer(BUFSIZE, selectBuffer);
                Gl.glRenderMode(Gl.GL_SELECT);

                Gl.glInitNames();

                Gl.glMatrixMode(Gl.GL_PROJECTION);
                Gl.glPushMatrix();
                Gl.glLoadIdentity();
                // create 5x5 pixel picking region near cursor location
                Glu.gluPickMatrix(x, (Height - y), 1.0, 1.0, viewport);
                Glu.gluPerspective(45, Width / (double)Height, Near, Fare);

                DrawNamedAtoms(atoms);

                Gl.glMatrixMode(Gl.GL_PROJECTION);
                Gl.glPopMatrix();
                Gl.glFlush();

                int hits = Gl.glRenderMode(Gl.GL_RENDER);


                return hits > 0 ? new[] { atoms[FindTopObject(selectBuffer, hits)] } : new IAtom[0];
            }

            return new IAtom[0];
        }



        protected override void BeginSceneFunction()
        {
            ClearColor(BackgroundColor);

            EnableColor();
            EnableLighting(Light1PositionPoint, Light2PositionPoint);

            TranslateToCenter();
            RotateAlongCenter();


        }

        protected override void EndSceneFunction()
        {
        }
    }
}
