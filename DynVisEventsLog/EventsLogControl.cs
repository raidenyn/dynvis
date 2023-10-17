using System.Windows.Forms;

namespace DynVis.EventsLog
{
    public partial class EventsLogControl : UserControl
    {
        public EventsLogControl()
        {
            InitializeComponent();

            LoadEvents();
            NoteToEvent();
        }

        private void NoteToEvent()
        {
            Log.OnNewEvent += EventsLog_OnNewEvent;
        }

        private void LoadEvents()
        {
            foreach (var e in Log.Events)
            {
                AddEventToList(e);
            }
        }

        private const int NormalImageIndex = 0;
        private const int ErrorImageIndex = 1;

        private void EventsLog_OnNewEvent(EventEventArgs e)
        {
            AddEventToList(e.Event);
        }

        private void AddEventToList(Event e)
        {
            var NoteItem = new ListViewItem();

            if (e.EventType == EventType.Error || (e.EventType == EventType.Result && e.Result == false))
            {
                NoteItem.ImageIndex = ErrorImageIndex;
            }
            else
            {
                NoteItem.ImageIndex = NormalImageIndex;
            }

            NoteItem.SubItems.Add(new ListViewItem.ListViewSubItem(NoteItem, e.CreateTime.ToString()));
            NoteItem.SubItems.Add(new ListViewItem.ListViewSubItem(NoteItem, e.Message));

            if (e.EventType == EventType.Result)
            {
                NoteItem.SubItems.Add(new ListViewItem.ListViewSubItem(NoteItem, e.Result == true ? "OK" : "FAILE"));
            }

            listViewLog.Items.Add(NoteItem);

            SelectLast();
        }

        private void SelectLast()
        {
            if (listViewLog.Items.Count > 0)
                listViewLog.Items[listViewLog.Items.Count - 1].Selected = true;
        }


    }
}
