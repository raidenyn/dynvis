using System;
using System.ComponentModel;

namespace DynVis.Core.Progress
{
    /// <summary>
    /// Компонент для работы с формой отображения прогресса
    /// </summary>
    public partial class ProgressNotifier : Component
    {
        public ProgressNotifier()
        {
            InitializeComponent();
        }

        public ProgressNotifier(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public ProgressNotifier(string comment): this ()
        {
            Comment = comment;
        }



        private readonly FormProgressBar Form = new FormProgressBar();
        private DateTime BeginProgressTime;

        private int _UnvisibleSecond = 1;
        
        [DefaultValue(1)]
        [Description("Число секунд, которые форма отсается невидимой")]
        [Category("Time")]
        public int UnvisibleSecond
        {
            get { return _UnvisibleSecond; }
            set { _UnvisibleSecond = value > 0 ? value : 0; }
        }

        [DefaultValue("")]
        [Description("Комментарий прогресса на форме")]
        [Category("Visible")]
        public string Comment
        {
            get { return Form.Comment; }
            set { Form.Comment = value;}
        }

        /// <summary>
        /// Команда начала прогресса
        /// </summary>
        public void BeginProgress()
        {
            BeginProgressTime = DateTime.Now;
            oldPersent = 0;
            Form.SetProgress(oldPersent);
            ApplicationExtension.BlockAllOpenedForms();
        }

        /// <summary>
        /// Команда заврешения прогресса
        /// </summary>
        public void CompliteProgress()
        {
            Form.Hide();
            ApplicationExtension.UnblockAllOpenedForms();
        }


        private int oldPersent;
        /// <summary>
        /// Изменение прогресса
        /// </summary>
        /// <param name="Persent">Возращает флаг выхода из операции</param>
        public bool ProgressChanged(int Persent)
        {
            if (Persent != oldPersent)
            {
                if (DateTime.Now.Subtract(BeginProgressTime).TotalSeconds > UnvisibleSecond)
                {
                    if (!Form.Visible)
                    {
                        Form.Show(ApplicationExtension.GlobalOwner());
                    }

                    return Form.SetProgress(Persent);
                }
                oldPersent = Persent;
            }
            return false;
        }

    }

    /// <summary>
    /// Функция сообщения изменения прогресса
    /// </summary>
    /// <param name="Persent">Прогресс в процентах от 0 до 100</param>
    /// <returns>Флаг выхода из операции</returns>
    public delegate bool ProgressChanged(int Persent);
}
