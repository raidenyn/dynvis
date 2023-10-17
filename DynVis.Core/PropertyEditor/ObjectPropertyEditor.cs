using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using DynVis.Core.Properties;

namespace DynVis.Core.PropertyEditor
{
    internal partial class ObjectPropertyEditor : UserControl
    {
        public ObjectPropertyEditor()
        {
            InitializeComponent();

            PropertiesObjects = new CollectionWithCurrentItems<IPropertiesObject>();
            PropertiesObjects.CurrentItemChanged += EditableObjects_CurrentItemChanged;
            PropertiesObjects.CurrentItemChanged += CurrentEditableObjectChanged;
            PropertiesObjects.CountItemChanged += EditableObjects_CountItemChanged;
            PropertiesObjects.CountItemChanged += EditableObjectsCountChanged;
        }
        [DefaultValue(true)]
        [Description("Видимость дерева объектов")]
        [Category("Appearance")]
        public bool ObjectsTreeView
        {
            get { return !splitContainer.Panel1Collapsed; }
            set { splitContainer.Panel1Collapsed = !value;}
        }

        [DefaultValue(null)]
        [Description("Выбранный объект")]
        [Category("Appearance")]
        public IPropertiesObject SelectedObject
        {
            get
            {
                return PropertiesObjects.CurrentItem;
            }
            set { PropertiesObjects.CurrentItem = value; }
        }

        [Description("Объекты редактирования")]
        [Category("Appearance")]
        public CollectionWithCurrentItems<IPropertiesObject> PropertiesObjects
        {
            get;
            private set;
        }

        
        public event EventHandler EditableObjectsCountChanged;
        public event EventHandler CurrentEditableObjectChanged;
        public event EventHandler CaptionChanged;
        public event EventHandler EditableObjectsValueChanged;

        private void treeViewObjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                SelectedObject = e.Node != null ? e.Node.Tag as IPropertiesObject : null;
            } 
            else
            {
                treeViewObjects.SelectedNode = e.Node.Nodes.Count > 0 ? e.Node.Nodes[0] : null;
            }
        }
        
        void EditableObjects_CurrentItemChanged(object sender, EventArgs e)
        {
            SetCurrentItem(PropertiesObjects.CurrentItem);
        }

        private IPropertyEditorControl CurrentEditor;

        private readonly Dictionary<IPropertiesObject, IPropertyEditorControl> CashedControls = new Dictionary<IPropertiesObject,IPropertyEditorControl>();

        void SetCurrentItem(IPropertiesObject obj)
        {
            HideControl(CurrentEditor);

            CurrentEditor = GetControl(obj);
            
            ShowControl(CurrentEditor);

            var objectNode = FindObjectNode(obj);

            if (treeViewObjects.SelectedNode != objectNode)
            {
                treeViewObjects.SelectedNode = objectNode;
            }
        }

        private void ShowControl(IPropertyEditorControl editor)
        {
            if (editor != null)
            {
                splitContainer.Panel2.SuspendLayout();
                editor.Control.Visible = true;
                editor.Control.Location = new Point(0, 0);
                editor.Control.Dock = DockStyle.Fill;
                splitContainer.Panel2.Controls.Add(editor.Control);
                splitContainer.Panel2.ResumeLayout(true);
            }
        }

        private void HideControl(IPropertyEditorControl editor)
        {
            if (editor != null)
            {
                splitContainer.Panel2.SuspendLayout();
                splitContainer.Panel2.Controls.Remove(editor.Control);
                splitContainer.Panel2.ResumeLayout(true);
            }
        }

        private IPropertyEditorControl GetControl(IPropertiesObject obj)
        {
            IPropertyEditorControl result; 
            if (CashedControls.ContainsKey(obj))
            {
                result = CashedControls[obj];
            }
            else
            {
                result = obj != null ? obj.GetPropertiesEditor() : null;
                if (obj != null && result != null)
                {
                    CashedControls.Add(obj, result);
                    result.EditingObjectValueChanged += EditableObjectsValueChanged;
                }
            }
            return result;
        }


        private  void EditableObjects_CountItemChanged(object sender, EventArgs e)
        {
            UpdateTree();
        }

        private void UpdateTree()
        {
            treeViewObjects.Nodes.Clear();
            foreach (var obj in PropertiesObjects)
            {
                if (obj != null)
                {
                    var sectionNode = GetSectionNode(obj.SectionName);

                    var node = new TreeNode {Tag = obj, Text = obj.PropertiesObjectName};
                    sectionNode.Nodes.Add(node);

                    if (SelectedObject == null) SelectedObject = obj;
                }
            }

            ObjectsTreeView = PropertiesObjects.Count > 1;

            Caption = PropertiesObjects.Count > 1 || SelectedObject == null ? LangGeneral.CustomizationPproperties : SelectedObject.PropertiesObjectName;
        }

        private TreeNode FindSection(string name)
        {
            foreach (TreeNode node in treeViewObjects.Nodes)
            {
                if (node.Text.CompareTo(name) == 0)
                {
                    return node;
                }
            }
            return null;
        }

        private TreeNode GetSectionNode(string name)
        {
            var result = FindSection(name);
            if (result == null)
            {
                result = new TreeNode { Text = name };
                treeViewObjects.Nodes.Add(result);
            }
            return result;
        }

        private TreeNode FindObjectNode(IPropertiesObject obj)
        {
            foreach (TreeNode sectionNode in treeViewObjects.Nodes)
            {
                foreach (TreeNode objectNode in sectionNode.Nodes)
                {
                    if (objectNode.Tag == obj) return objectNode;
                }
            }
            return null;
        }

        public string Caption
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                if (CaptionChanged != null) CaptionChanged(this, new EventArgs());
            }
        }

        public void SaveChanges()
        {
            foreach (var obj in CashedControls)
            {
                obj.Value.SaveChanges();
            }
        }

        public void DiscartChanges()
        {
            foreach (var obj in CashedControls)
            {
                obj.Value.DiscartChanges();
            }
        }

        public void ClearCash()
        {
            CashedControls.Clear();
        }
    }
}
