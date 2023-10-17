using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DynVis.Core;
using DynVis.Core.Elements;
using DynVis.Core.Geometry;
using DynVis.Core.PropertyEditor;
using DynVis.Draw.Geomtery.Properties;
using DynVis.Math;

namespace DynVis.Draw.Geometry
{
    internal class GeometryDrawProvider: DrawProvider
    {
        private readonly GeometryDrawEngine GeometryDrawEngine;

        protected override BaseEngine BaseEngine
        {
            get { return GeometryDrawEngine; }
        }

        public GeometryDrawProvider(Control control)
        {
            GeometryDrawEngine = new GeometryDrawEngine(control);
            GeometryProperty = new GeometryProviderProperty(GeometryDrawEngine, Draw);
            GlobalProperiesEditor.RegisterEditableObject(GeometryProperty);
        }

        private IAtomStructure _CurrentAtomStructure;

        private Point3D CashedGeometryCenter;

        public IAtomStructure CurrentAtomStructure
        {
            get
            {
                return _CurrentAtomStructure;
            }
            set
            {
                AllocateEvents(CurrentAtomStructure, value);
                _CurrentAtomStructure = value;
                SelectionAtoms.Clear();
                ManualBonds.Clear();
                Draw();
            }
        }

        


        public void AllocateEvents(IAtomStructure oldAtomStructure, IAtomStructure newAtomStructure)
        {
            if (oldAtomStructure != null)
            {
                oldAtomStructure.GeometryChanged -= GeomentryChanged;
            }

            if (newAtomStructure != null)
            {
                newAtomStructure.GeometryChanged += GeomentryChanged;
            }
        }

        void GeomentryChanged(object sender, EventArgs e)
        {
            CashedGeometryCenter = CurrentAtomStructure.ToPointEnumerable().GeometryCenter();
        }



        public void Draw()
        {
            PreperCalculation();

            if (GeometryDrawEngine.BeginScene())
            {
                if (CurrentAtomStructure != null)
                {
                    GeometryDrawEngine.DrawStructures(CurrentAtomStructure, SelectionAtoms, ManualBonds);
                }
                GeometryDrawEngine.EndScene();
            }
        }

        private void PreperCalculation()
        {
            if (AutoUpdateRotateCenter && CurrentAtomStructure != null && CashedGeometryCenter != null)
            {
                GeometryDrawEngine.RotateCenterPoint = CashedGeometryCenter;
            }
        }

        #region Bonds
        
        private readonly BondList ManualBonds = new BondList { AllowNoneBond = true };

        public void SetBondOnCurrentAtom()
        {
            if (SelectionAtoms.Count == 2)
            {
                var Atom1 = SelectionAtoms[0];
                var Atom2 = SelectionAtoms[1];

                var bond = ManualBonds.GetBond(Atom1, Atom2) ?? CurrentAtomStructure.BondList.GetBond(Atom1, Atom2);

                if (bond == null || bond.BondType == BondType.None)
                {
                    ManualBonds.Add(new Bond(Atom1, Atom2, BondType.Single));
                }
                else
                {
                    ManualBonds.Add(new Bond(Atom1, Atom2, BondType.None));
                }
            }
        }
        #endregion

        #region Selection
        public readonly List<IAtom> SelectionAtoms = new List<IAtom>();
        
        public void SelectAtoms(int x, int y)
        {
            
            if (CurrentAtomStructure != null)
            {
                var currSelection = GeometryDrawEngine.SelectAtoms(CurrentAtomStructure, x, y);

                SelectAtom(currSelection);
                
                Draw();
            }
        }

        private void SelectAtom(IEnumerable<IAtom> atoms)
        {
            foreach (var atom in atoms)
            {
                if (SelectionAtoms.Contains(atom)) SelectionAtoms.Remove(atom); else SelectionAtoms.Add(atom);
            }
            RiseSelectionChanged();
        }

        public void UnselectAllAtom()
        {
            SelectionAtoms.Clear();
            RiseSelectionChanged();
        }

        public string SelectedAtomsValuesString
        {
            get
            {
                switch (SelectionAtoms.Count)
                {
                    case 1:
                        return String.Format(LangResource.ViewCoordinate, SelectionAtoms[0].Coordinate);
                    case 2:
                        return String.Format(LangResource.ViewDistance, SelectionAtoms[0].Coordinate.DistanceTo(SelectionAtoms[1].Coordinate));
                    case 3:
                        return String.Format(LangResource.ViewValentAngle, MathExtended.RadToDeg(PointsFunc.AnglePoint3D(SelectionAtoms[0].Coordinate, SelectionAtoms[1].Coordinate, SelectionAtoms[2].Coordinate)));
                    case 4:
                        return String.Format(LangResource.ViewDihedralAngel, MathExtended.RadToDeg(PointsFunc.DiedralAnglePoint3D(SelectionAtoms[0].Coordinate, SelectionAtoms[1].Coordinate, SelectionAtoms[2].Coordinate, SelectionAtoms[3].Coordinate)));
                    default:
                        return String.Empty;
                }
            }
        }

        public event EventHandler SelectionChanged;
        private void RiseSelectionChanged()
        {
            if (SelectionChanged != null) SelectionChanged(this, new EventArgs());
        }
        #endregion

        #region Property Editor
        public void ShowPropertiesDialog()
        {
            GlobalProperiesEditor.ShowEditor(ApplicationExtension.GlobalOwner(), GeometryProperty);
        }

        private readonly GeometryProviderProperty GeometryProperty;

        class GeometryProviderProperty : IPropertiesObject
        {
            public readonly GeometryDrawEngine GeometryDrawEngine;

            public readonly Proc UpdateViewProc;

            public GeometryProviderProperty(GeometryDrawEngine geometryDrawEngine)
            {
                GeometryDrawEngine = geometryDrawEngine;
            }

            public GeometryProviderProperty(GeometryDrawEngine geometryDrawEngine, Proc updateViewProc)
                : this(geometryDrawEngine)
            {
                UpdateViewProc = updateViewProc;
            }

            public object EditableObject
            {
                get { return GeometryDrawEngine; }
            }

            public string PropertiesObjectName
            {
                get { return LangResource.StructurePropertiesCustomization; }
            }

            public string SectionName
            {
                get { return LangResource.SructureSection; }
            }

            public IPropertyEditorControl GetPropertiesEditor()
            {
                return new GeometryDrawParamsEditor { EditingObject = EditableObject, UpdateViewObject = UpdateViewProc };
            }
        }
        #endregion
    }
}
