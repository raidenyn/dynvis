using System;
using System.Windows.Forms;

namespace DynVis.Core.Progress
{
    /// <summary>
    /// Форма прогресса
    /// </summary>
    internal partial class FormProgressBar : Form
    {
        public FormProgressBar()
        {
            InitializeComponent();
        }

        public FormProgressBar(string comment):this()
        {
            Comment = comment;
        }

        private const int CS_NOCLOSE = 0x200;

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CS_NOCLOSE;
                return cp;
            }
        }

        /// <summary>
        /// Флаг выхода
        /// </summary>
        private bool Cancel;

        private string _Comment;
        /// <summary>
        /// Коментарий у полосы прокрутки
        /// </summary>
        public string Comment
        {
            get
            {
                return _Comment;
            }
            set
            {
                _Comment = value;
                UpdateCommentText();
            }
        }

        /// <summary>
        /// Текущий прогресс
        /// </summary>
        public int ProgressPersent
        {
            get
            {
                return (int)(progressBar.Value/(double)(progressBar.Maximum - progressBar.Minimum) * 100);
            }
        }

        /// <summary>
        /// Изменение прогресса
        /// </summary>
        /// <param name="persent"></param>
        public bool SetProgress(int persent)
        {
            progressBar.Value = persent * (progressBar.Maximum - progressBar.Minimum) / 100;
            UpdateCommentText();

            return Cancel;
        }

        private void UpdateCommentText()
        {
            labelText.Text = String.Format("{0}: {1}%", Comment, ProgressPersent);
            Application.DoEvents();
        }

        private void FormProgressBar_Load(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
        }

        private void FormProgressBar_Deactivate(object sender, EventArgs e)
        {
            Application.UseWaitCursor = false;
        }

        private void FormProgressBar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Cancel = true;
            }
        }
    }
}
