using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DynVis.Core;
using DynVis.Core.About;
using DynVis.Core.About.Update;
using DynVis.Core.IO;
using DynVis.Core.Plugin;
using DynVis.Core.PropertyEditor;
using DynVis.Core.UserCallback;
using DynVis.CurrentPointInformation;
using DynVis.EventsLog;
using DynVis.Geometry;
using DynVis.Path;
using DynVis.Points;
using DynVis.Surface;

namespace DynVis
{
    internal partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private ReactionData ReactionData;

        private readonly FormSurface FormSurface = new FormSurface();
        private readonly FormGeometry FormGeometry = new FormGeometry();
        private readonly FormPath FormPath = new FormPath();
        private readonly FormCurrentPointInformation FormCurrentPointInformation = new FormCurrentPointInformation();
        private readonly FormPoints FormPoints = new FormPoints();

        private readonly FormEventLogs FormEventLogs = new FormEventLogs();


        private void FormMain_Load(object sender, EventArgs e)
        {
            Text = Core.About.Version.NameWithVersion;

            LoadPlugin();

            Updater.CheckNewVersion();
        }

        private bool AllowCreateGeometry
        {
            get { return ReactionData != null && ReactionData.AllowCreateGeometry; }
        }

        private bool AllowCreateReactionPath
        {
            get { return ReactionData != null && ReactionData.AllowCalcPath; }
        }

        private bool AllowCreatePoints
        {
            get { return ReactionData != null && ReactionData.AllowCalcPoints; }
        }

        private bool SurfaceView
        {
            get { return ReactionData != null && ReactionData.Surface != null;}
        }

        private bool GeomView
        {
            get { return ReactionData != null && ReactionData.Geometry != null; }
        }

        private bool PathView
        {
            get { return ReactionData != null; }
        }

        private bool PointsView
        {
            get { return ReactionData != null; }
        }

        private void SetReactionData(ReactionData rd)
        {
            ReactionData = rd;
            FormSurface.Hide();
            FormPath.Hide();
            FormGeometry.Hide();
            FormCurrentPointInformation.Hide();
            FormPoints.Hide();

            FormPoints.ReactionData = FormCurrentPointInformation.ReactionData =
                FormSurface.ReactionData = FormPath.ReactionData = FormGeometry.ReactionData = ReactionData;
        }

        private void ToolStripMenuItemGeomImport_Paint(object sender, PaintEventArgs e)
        {
            ToolStripMenuItemGeomImport.Enabled = AllowCreateGeometry;
        }

        private void ToolStripMenuItemPath_Paint(object sender, PaintEventArgs e)
        {
            if (sender is ToolStripItem)
            {
                ((ToolStripItem)sender).Enabled = AllowCreateReactionPath; 
            }
        }

        private void ToolStripMenuItemPoints_Paint(object sender, PaintEventArgs e)
        {
            if (sender is ToolStripItem)
            {
                ((ToolStripItem)sender).Enabled = AllowCreatePoints;
            }
        }

        private void ToolStripMenuItemViewSurface_Paint(object sender, PaintEventArgs e)
        {
            ToolStripMenuItemViewSurface.Enabled = SurfaceView;
            ToolStripMenuItemViewSurface.Checked = FormSurface.Visible;
        }

        private void ToolStripMenuItemViewPath_Paint(object sender, PaintEventArgs e)
        {
            ToolStripMenuItemViewPath.Enabled = PathView;
            ToolStripMenuItemViewPath.Checked = FormPath.Visible;
        }

        private void ToolStripMenuItemViewGeom_Paint(object sender, PaintEventArgs e)
        {
            ToolStripMenuItemViewGeom.Enabled = GeomView;
            ToolStripMenuItemViewGeom.Checked = FormGeometry.Visible;
        }

        private void ToolStripMenuItemSurfacePointsView_Paint(object sender, PaintEventArgs e)
        {
            ToolStripMenuItemSurfacePointsView.Enabled = PointsView;
            ToolStripMenuItemSurfacePointsView.Checked = FormPoints.Visible;
        }

        private void ToolStripMenuItemCurrentPointInformation_Paint(object sender, PaintEventArgs e)
        {
            ToolStripMenuItemCurrentPointInformation.Enabled = SurfaceView;
            ToolStripMenuItemCurrentPointInformation.Checked = FormCurrentPointInformation.Visible;
        }

        private void ToolStripMenuItemGeomImport_Click(object sender, EventArgs e)
        {
            if (ReactionData != null)
            {
                if (ReactionData.CreateGeometry(this))
                {
                    ShowForm(FormGeometry);
                }
            }
        }

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ToolStripMenuItemViewGeom_Click(object sender, EventArgs e)
        {
            if (FormGeometry.Visible)
            {
                FormGeometry.Hide();
            } 
            else
            {
                ShowForm(FormGeometry);
            }
        }

        private void ToolStripMenuItemSurfacePointsView_Click(object sender, EventArgs e)
        {
            if (FormPoints.Visible)
            {
                FormPoints.Hide();
            }
            else
            {
                ShowForm(FormPoints);
            }
        }

        private void ToolStripMenuItemLogEvents_Click(object sender, EventArgs e)
        {
            if (!FormGeometry.Visible)
            {
                if (!FormEventLogs.Visible) FormEventLogs.Show(this);
            }
        }

        private void ToolStripMenuItemAboutPrograms_Click(object sender, EventArgs e)
        {
            var FormAbout = new FormAbout();
            FormAbout.ShowDialog(this);
        }

        private void LoadPlugin()
        {
            Plugin.Load();

            UpdateSurfaceCalculationModule(Plugin.SurfacePluginList);

            UpdatePathCalculationModule(Plugin.PathPluginList);

            UpdatePointsCalculationModule(Plugin.PointsPluginList);
        }

        private void UpdateSurfaceCalculationModule(IEnumerable<IDynVisSurfacePlugin> modules)
        {
            ToolStripMenuItemSurface.DropDownItems.Clear();
            foreach (var module in modules)
            {
                var newToolStrip = new ToolStripMenuItem(module.CaptionText, module.CaptionImage, ToolStripMenuItemCalculationSurface_Click)
                                       {Tag = module};

                ToolStripMenuItemSurface.DropDownItems.Add(newToolStrip);
            }

            ToolStripMenuItemSurface.Visible = ToolStripMenuItemSurface.DropDownItems.Count != 0;
        }

        private void UpdatePathCalculationModule(IEnumerable<IDynVisPathPlugin> modules)
        {
            ToolStripMenuItemPath.DropDownItems.Clear();
            foreach (var module in modules)
            {
                var newToolStrip = new ToolStripMenuItem(module.CaptionText, module.CaptionImage, ToolStripMenuItemCalculationPath_Click) { Tag = module };

                newToolStrip.Paint += ToolStripMenuItemPath_Paint;

                ToolStripMenuItemPath.DropDownItems.Add(newToolStrip);
            }

            ToolStripMenuItemPath.Visible = ToolStripMenuItemPath.DropDownItems.Count != 0;
        }

        private void UpdatePointsCalculationModule(IEnumerable<IDynVisPointsPlugin> modules)
        {
            ToolStripMenuItemSurfacePoints.DropDownItems.Clear();
            foreach (var module in modules)
            {
                var newToolStrip = new ToolStripMenuItem(module.CaptionText, module.CaptionImage, ToolStripMenuItemCalculationPoints_Click) { Tag = module };

                newToolStrip.Paint += ToolStripMenuItemPoints_Paint;

                ToolStripMenuItemSurfacePoints.DropDownItems.Add(newToolStrip);
            }

            ToolStripMenuItemSurfacePoints.Visible = ToolStripMenuItemSurfacePoints.DropDownItems.Count != 0;
        }

        private void ToolStripMenuItemCalculationSurface_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem)
            {
                var module = ((ToolStripItem)sender).Tag as IDynVisSurfacePlugin;
                if (module != null)
                {
                    SetReactionData(module.CreateReactionData());
                    if (ReactionData.CreateSurface(this))
                    {
                        ShowForm(FormSurface);
                    }
                }
            }
        }

        private void ToolStripMenuItemCalculationPath_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem)
            {
                var module = ((ToolStripItem)sender).Tag as IDynVisPathPlugin;
                if (module != null)
                {
                    if (ReactionData.CalcPath(module.CalculationPath))
                    {
                        ShowForm(FormPath);
                    }
                }
            }
        }

        private void ToolStripMenuItemCalculationPoints_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem)
            {
                var module = ((ToolStripItem)sender).Tag as IDynVisPointsPlugin;
                if (module != null)
                {
                    if (ReactionData.CalcPoints(module.CalculationPoints))
                    {
                        ShowForm(FormPoints);
                    }
                }
            }
        }

        

        private void ShowForm(Form form)
        {
            if (form.Visible)
            {
                form.Refresh();
            }
            else
            {
                form.Show(this);
            }
        }

        private void ToolStripMenuItemViewPath_Click(object sender, EventArgs e)
        {
            if (FormPath.Visible)
            {
                FormPath.Hide();
            }
            else
            {
                ShowForm(FormPath);
            }
        }

        private void ToolStripMenuItemViewSurface_Click(object sender, EventArgs e)
        {
            if (FormSurface.Visible)
            {
                FormSurface.Hide();
            }
            else
            {
                ShowForm(FormSurface);
            }
        }

        private void ToolStripMenuItemCurrentPointInformation_Click(object sender, EventArgs e)
        {
            if (FormCurrentPointInformation.Visible)
            {
                FormCurrentPointInformation.Hide();
            }
            else
            {
                ShowForm(FormCurrentPointInformation);
            }
        }

        private void ToolStripMenuItemSaveSurface_Click(object sender, EventArgs e)
        {
            if (ReactionData != null && ReactionData.Surface != null)
            {
                IOFileDialog.SaveToFile(ReactionData.Surface);
            }
            
        }

        private void ToolStripMenuItemSaveGeom_Click(object sender, EventArgs e)
        {
            if (ReactionData != null && ReactionData.Geometry != null)
            {
                IOFileDialog.SaveToFile(ReactionData.Geometry);
            }
        }

        private void ToolStripMenuItemSavePath_Click(object sender, EventArgs e)
        {
            if (ReactionData != null)
            {
                ReactionData.SavePathDialog();
            }
        }

        private void ToolStripMenuItemSavePoints_Click(object sender, EventArgs e)
        {
            if (ReactionData != null && ReactionData.Points != null)
            {
                IOFileDialog.SaveToFile(ReactionData.Points);
            }

        }


        private void SetFormPosition()
        {
            var top = Top + Height;
            var left = Left;
            var height = (Screen.PrimaryScreen.WorkingArea.Height - Top);
            var width = Width;

            FormSurface.Top = top;
            FormSurface.Left = left;
            FormSurface.Height = height / 2;

            FormSurface.SetProportion();

            FormGeometry.Top = top;
            FormGeometry.Left = left + FormSurface.Width;
            FormGeometry.Width = width - FormSurface.Width;
            FormGeometry.Height = height / 2;

            FormPath.Top = FormSurface.Top + FormSurface.Height;
            FormPath.Left = left;
            FormPath.Width = width/2;
            FormPath.Height = (Screen.PrimaryScreen.WorkingArea.Height - FormPath.Top);

            FormPoints.Top = FormPath.Top;
            FormPoints.Left = FormPath.Left + FormPath.Width;
            FormPoints.Width = width - FormPath.Width;
            FormPoints.Height = FormPath.Height;
        }

        private void ToolStripMenuItemSetFormsPositions_Click(object sender, EventArgs e)
        {
            SetFormPosition();
        }

        private void ToolStripMenuItemCheckUpdates_Click(object sender, EventArgs e)
        {
            Updater.CheckNewVersionDialog();
        }

        private void ToolStripMenuItemSendCallback_Click(object sender, EventArgs e)
        {
            Callback.SendCallbackDialog();
        }

        private void ToolStripMenuItemOptions_Click(object sender, EventArgs e)
        {
            GlobalProperiesEditor.ShowEditor(this);
        }
    }
}
