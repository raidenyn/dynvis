#region

using System;
using System.Drawing;
using DevAge.Drawing;
using DevAge.Drawing.VisualElements;
using ContentAlignment=DevAge.Drawing.ContentAlignment;

#endregion

namespace SourceGrid.Cells.Views
{
    /// <summary>
    /// Base abstract class to manage the visual aspect of a cell. This class can be shared beetween multiple cells.
    /// </summary>
    [Serializable]
    public abstract class ViewBase : ContainerBase, IView
    {
        public static ContentAlignment DefaultAlignment = ContentAlignment.MiddleLeft;
        public static Color DefaultBackColor = Color.FromKnownColor(KnownColor.Window);

        /// <summary>
        /// A default RectangleBorder instance with a 1 pixed LightGray border on bottom and right side
        /// </summary>
        public static RectangleBorder DefaultBorder = new RectangleBorder(new BorderLine(Color.LightGray, 1),
                                                                          new BorderLine(Color.LightGray, 1));

        public static Color DefaultForeColor = Color.FromKnownColor(KnownColor.WindowText);
        public static Padding DefaultPadding = new Padding(2);

        #region Constructors

        /// <summary>
        /// Use default setting
        /// </summary>
        public ViewBase()
        {
            Background = new BackgroundSolid();


            Padding = DefaultPadding;
            ForeColor = DefaultForeColor;
            BackColor = DefaultBackColor;
            Border = DefaultBorder;
            TextAlignment = DefaultAlignment;
            ImageAlignment = DefaultAlignment;
        }

        /// <summary>
        /// Copy constructor.  This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
        /// </summary>
        /// <param name="p_Source"></param>
        public ViewBase(ViewBase p_Source) : base(p_Source)
        {
            ForeColor = p_Source.ForeColor;

            BackColor = p_Source.BackColor;
            Border = p_Source.Border;
            Padding = p_Source.Padding;

            //Duplicate the reference fields
            Font tmpFont = null;
            if (p_Source.Font != null)
                tmpFont = (Font) p_Source.Font.Clone();

            Font = tmpFont;

            WordWrap = p_Source.WordWrap;
            TextAlignment = p_Source.TextAlignment;
            TrimmingMode = p_Source.TrimmingMode;
            ImageAlignment = p_Source.ImageAlignment;
            ImageStretch = p_Source.ImageStretch;
        }

        #endregion

        #region Format

        private TrimmingMode mTrimmingMode = TrimmingMode.Char;

        /// <summary>
        /// True to stretch the image to all the cell
        /// </summary>
        public bool ImageStretch { get; set; }

        /// <summary>
        /// Image Alignment
        /// </summary>
        public ContentAlignment ImageAlignment { get; set; }

        /// <summary>
        /// TrimmingMode, default Char.
        /// </summary>
        public TrimmingMode TrimmingMode
        {
            get { return mTrimmingMode; }
            set { mTrimmingMode = value; }
        }

        /// <summary>
        /// The padding of the cell. Usually it is 2 pixel on all sides.
        /// </summary>
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        /// <summary>
        /// Gets or sets a property that specify how to draw the elements.
        /// </summary>
        public new ElementsDrawMode ElementsDrawMode
        {
            get { return base.ElementsDrawMode; }
            set { base.ElementsDrawMode = value; }
        }

        /// <summary>
        /// If null the grid font is used
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// ForeColor of the cell
        /// </summary>
        public Color ForeColor { get; set; }

        /// <summary>
        /// Word Wrap, default false.
        /// </summary>
        public bool WordWrap { get; set; }

        /// <summary>
        /// Text Alignment.
        /// </summary>
        public ContentAlignment TextAlignment { get; set; }

        /// <summary>
        /// Get the font of the cell, check if the current font is null and in this case return the grid font
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public virtual Font GetDrawingFont(GridVirtual grid)
        {
            if (Font == null)
                return grid.Font;
            else
                return Font;
        }

        /// <summary>
        /// Returns the color of the Background. If the Background it isn't an instance of BackgroundSolid then returns DefaultBackColor
        /// </summary>
        public Color BackColor
        {
            get
            {
                if (Background is BackgroundSolid)
                    return ((BackgroundSolid) Background).BackColor;
                else
                    return DefaultBackColor;
            }
            set
            {
                if (Background is BackgroundSolid)
                    ((BackgroundSolid) Background).BackColor = value;
            }
        }

        /// <summary>
        /// The border of the Cell. Usually it is an instance of the DevAge.Drawing.RectangleBorder structure.
        /// </summary>
        public new IBorder Border
        {
            get { return base.Border; }
            set { base.Border = value; }
        }

        #endregion

        #region DrawCell

        /// <summary>
        /// Draw the cell specified
        /// </summary>
        /// <param name="cellContext"></param>
        /// <param name="graphics">Paint arguments</param>
        /// <param name="p_ClientRectangle">Rectangle where draw the current cell</param>
        public void DrawCell(CellContext cellContext,
                             GraphicsCache graphics,
                             RectangleF rectangle)
        {
            PrepareView(cellContext);

            Draw(graphics, rectangle);
        }

        /// <summary>
        /// Prepare the current View before drawing. Override this method for customize the specials VisualModel that you need to create. Always calls the base PrepareView.
        /// </summary>
        /// <param name="context">Current context. Cell to draw. This property is set before drawing. Only inside the PrepareView you can access this property.</param>
        protected virtual void PrepareView(CellContext context)
        {
        }

        #endregion

        #region Measure (GetRequiredSize)

        /// <summary>
        /// Returns the minimum required size of the current cell, calculating using the current DisplayString, Image and Borders informations.
        /// </summary>
        /// <param name="cellContext"></param>
        /// <param name="maxLayoutArea">SizeF structure that specifies the maximum layout area for the text. If width or height are zero the value is set to a default maximum value.</param>
        /// <returns></returns>
        public Size Measure(CellContext cellContext,
                            Size maxLayoutArea)
        {
            using (var measure = new MeasureHelper(cellContext.Grid))
            {
                PrepareView(cellContext);

                SizeF measureSize = Measure(measure, SizeF.Empty, maxLayoutArea);
                return Size.Ceiling(measureSize);
            }
        }

        #endregion

        /// <summary>
        /// Background of the cell. Usually it is an instance of BackgroundSolid
        /// </summary>
        public new IVisualElement Background
        {
            get { return base.Background; }
            set { base.Background = value; }
        }
    }
}