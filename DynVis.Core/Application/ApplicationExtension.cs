using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DynVis.Core
{
    /// <summary>
    /// Расширение функций уровня приложения
    /// </summary>
    public static class ApplicationExtension
    {
        /// <summary>
        /// Возращает одну из форм приложения, которое будет служить владельцом дочерних форм 
        /// </summary>
        /// <returns></returns>
        public static IWin32Window GlobalOwner()
        {
            return Application.OpenForms[0];
        }

        
        #region Bloking Forms

        /// <summary>
        /// Список блокированных форм
        /// </summary>
        private static readonly List<Form> BlockedForm = new List<Form>();

        /// <summary>
        /// Блокирует все открытые формы в приложении
        /// </summary>
        public static void BlockAllOpenedForms()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.Enabled)
                {
                    BlockedForm.Add(form);
                    form.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Разблокирует все формы в приложении, заблокированных ранее функцией BlockAllOpenedForms()
        /// </summary>
        public static void UnblockAllOpenedForms()
        {
            foreach (Form form in BlockedForm)
            {
                form.Enabled = true;
                
            }
            BlockedForm.Clear();
        }

        /// <summary>
        /// Ищет в открытых окнах заданный тип окна
        /// </summary>
        /// <param name="formType"></param>
        /// <returns></returns>
        public static Form FindOpenForm(Type formType)
        {
            var allForms = Application.OpenForms;
            foreach (Form form in allForms)
            {
                if (form.GetType() == formType)return form;
            }
            return null;
        }

        #endregion
    }
}
