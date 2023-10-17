using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;

namespace DevAge.Windows.Forms
{
	/// <summary>
	/// A TextBoxTypedButton that uase the UITypeEditor associated with the type.
	/// </summary>
	public class TextBoxUITypeEditor : DevAgeTextBoxButton, System.Windows.Forms.Design.IWindowsFormsEditorService, ITypeDescriptorContext
	{
		private IContainer components;

		public TextBoxUITypeEditor()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		public override void ShowDialog()
		{
			try
			{
				OnDialogOpen(EventArgs.Empty);
				if (m_UITypeEditor != null)
				{
					UITypeEditorEditStyle style = m_UITypeEditor.GetEditStyle();
                    if (style == UITypeEditorEditStyle.DropDown ||
                        style == UITypeEditorEditStyle.Modal)
					{
						object editObject;
                        //Try to read the actual value, if the function failed I edit the default value
                        if (IsValidValue(out editObject) == false)
                        {
                            editObject = Validator != null ? Validator.DefaultValue : null;
                        }

                        object tmp = m_UITypeEditor.EditValue(this, this, editObject);
						Value = tmp;
					}
				}

				OnDialogClosed(EventArgs.Empty);
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message,"Error");
			}
		}

		private UITypeEditor m_UITypeEditor;

        /// <summary>
        /// Gets or sets the UITypeEditor to use. If you have specified a validator the TypeDescriptor.GetEditor method is used based on the Validator.ValueType.
        /// </summary>
        [DefaultValue(null), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public UITypeEditor UITypeEditor
		{
			get{return m_UITypeEditor;}
			set{m_UITypeEditor = value;}
		}

        //public bool ShouldSerializeUITypeEditor()
        //{
        //    return m_UITypeEditor != m_DefaultUITypeEditor;
        //}

        protected override void ApplyValidatorRules()
        {
            base.ApplyValidatorRules();

            if (m_UITypeEditor == null && Validator != null)
            {
                var tmp = TypeDescriptor.GetEditor(Validator.ValueType, typeof(UITypeEditor));
                if (tmp is UITypeEditor)
                    m_UITypeEditor = (UITypeEditor)tmp;
            }
        }

		#region IServiceProvider Members
		Object IServiceProvider.GetService ( Type serviceType )
		{
			//modal
			if (serviceType == typeof(System.Windows.Forms.Design.IWindowsFormsEditorService))
				return this;

			return null;
		}
		#endregion

		#region System.Windows.Forms.Design.IWindowsFormsEditorService
		private DropDown m_dropDown;
		public virtual void CloseDropDown ()
		{
			if (m_dropDown != null)
			{
				m_dropDown.CloseDropDown();
			}
		}

		public virtual void DropDownControl ( Control control )
		{
            using (m_dropDown = new DropDown(control, this, ParentForm))
            {
                m_dropDown.DropDownFlags = DropDownFlags.CloseOnEscape;

                m_dropDown.ShowDropDown();

                m_dropDown.Close();
            }
            m_dropDown = null;
        }

		public virtual DialogResult ShowDialog ( Form dialog )
		{
			return dialog.ShowDialog(this);
		}
		#endregion

		#region ITypeDescriptorContext Members

		void ITypeDescriptorContext.OnComponentChanged()
		{
			
		}

		IContainer ITypeDescriptorContext.Container
		{
			get
			{
				return Container;
			}
		}

		bool ITypeDescriptorContext.OnComponentChanging()
		{
			return true;
		}

		object ITypeDescriptorContext.Instance
		{
			get
			{
				return Value;
			}
		}

		PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor
		{
			get
			{
				return null;
			}
		}

		#endregion
	}
}

