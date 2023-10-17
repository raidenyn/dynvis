#region

using System;
using System.Collections.Generic;
using DevAge.Drawing;
using DevAge.Drawing.VisualElements;

#endregion

namespace SourceGrid.Cells.Views
{
    /// <summary>
    /// Summary description for VisualModelCheckBox.
    /// </summary>
    [Serializable]
    public class MultiImages : Cell
    {
        #region Constructors

        /// <summary>
        /// Use default setting
        /// </summary>
        public MultiImages()
        {
            ElementsDrawMode = ElementsDrawMode.Covering;
        }

        /// <summary>
        /// Copy constructor.  This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
        /// </summary>
        /// <param name="other"></param>
        public MultiImages(MultiImages other)
            : base(other)
        {
            mImages = (VisualElementList) other.mImages.Clone();
        }

        #endregion

        private readonly VisualElementList mImages = new VisualElementList();

        /// <summary>
        /// Images of the cells
        /// </summary>
        public VisualElementList SubImages
        {
            get { return mImages; }
        }

        protected override IEnumerable<IVisualElement> GetElements()
        {
            foreach (IVisualElement v in GetBaseElements())
                yield return v;

            foreach (IVisualElement v in SubImages)
                yield return v;
        }

        private IEnumerable<IVisualElement> GetBaseElements()
        {
            return base.GetElements();
        }

        #region Clone

        /// <summary>
        /// Clone this object. This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new MultiImages(this);
        }

        #endregion
    }
}