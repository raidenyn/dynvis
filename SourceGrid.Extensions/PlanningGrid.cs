#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevAge.Drawing;
using SourceGrid.Cells.Controllers;
using SourceGrid.Cells.Views;
using Cell=SourceGrid.Cells.Cell;
using Header=SourceGrid.Cells.Header;
using RowHeader=SourceGrid.Cells.RowHeader;

#endregion

namespace SourceGrid.Planning
{
    /// <summary>
    /// Summary description for PlanningGrid.
    /// </summary>
    public class PlanningGrid : UserControl
    {
        #region Delegates

        public delegate void AppointmentEventHandler(object sender, AppointmentEventArgs e);

        #endregion

        private const int c_ColumnsHeader = 2;
        private const int c_RowsHeader = 2;
        private readonly AppointmentCollection m_Appointments = new AppointmentCollection();

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        private Grid grid;

        private DateTime m_DateTimeEnd;
        private DateTime m_DateTimeStart;
        private int m_MinAppointmentLength;

        public PlanningGrid()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            grid.Selection.FocusStyle = FocusStyle.None;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AppointmentCollection Appointments
        {
            get { return m_Appointments; }
        }

        /// <summary>
        /// The grid used internally to display the planning (note that you usually don't need to access directly this class).
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Grid Grid
        {
            get { return grid; }
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            grid.Controller.AddController(new AppointmentController(this));
        }

        /// <summary>
        /// Load the grid using the parameters specified
        /// </summary>
        /// <param name="dateTimeStart"></param>
        /// <param name="dateTimeEnd"></param>
        /// <param name="minAppointmentLength"></param>
        public void LoadPlanning(DateTime dateTimeStart, DateTime dateTimeEnd, int minAppointmentLength)
        {
            m_DateTimeStart = dateTimeStart;
            m_DateTimeEnd = dateTimeEnd;
            m_MinAppointmentLength = minAppointmentLength;

            if (dateTimeStart >= dateTimeEnd)
                throw new ApplicationException("Invalid Planning Range");
            if (dateTimeStart.TimeOfDay >= dateTimeEnd.TimeOfDay)
                throw new ApplicationException("Invalid Plannnin Range");
            if (dateTimeStart.TimeOfDay.Minutes != 0 ||
                dateTimeEnd.TimeOfDay.Minutes != 0)
                throw new ApplicationException("Invalid Start or End hours must be with 0 minutes");
            if (minAppointmentLength <= 0 || minAppointmentLength > 60)
                throw new ApplicationException("Invalid Minimum Appointment Length");
            if (60%minAppointmentLength != 0)
                throw new ApplicationException("Invalid Minimum Appointment Length must be multiple of 60");

            TimeSpan dayInterval = dateTimeEnd - dateTimeStart;
            TimeSpan timeInterval = dateTimeEnd.TimeOfDay - dateTimeStart.TimeOfDay;
            int partsForHour = 60/minAppointmentLength;

            if (dayInterval.TotalDays > 30)
                throw new ApplicationException("Range too big");
            if (timeInterval.TotalMinutes < minAppointmentLength)
                throw new ApplicationException("Invalid Minimum Appointment Length for current Planning Range");

            //Redim Grid
            grid.Redim((int) ((timeInterval.TotalHours + 1)*partsForHour + c_RowsHeader),
                       (int) (dayInterval.TotalDays + 1 + c_ColumnsHeader));

            //Load Header
            grid[0, 0] = new Header00(null);
            grid[0, 0].RowSpan = 2;
            grid[0, 0].ColumnSpan = 2;
            //create day caption
            DateTime captionDate = dateTimeStart;
            for (int c = c_ColumnsHeader; c < grid.ColumnsCount; c++)
            {
                grid[0, c] = new HeaderDay1(captionDate.ToShortDateString());
                grid[1, c] = new HeaderDay2(captionDate.ToString("dddd"));

                captionDate = captionDate.AddDays(1);
            }

            //create hour caption
            int hours = dateTimeStart.Hour;
            for (int r = c_RowsHeader; r < grid.RowsCount; r += partsForHour)
            {
                grid[r, 0] = new HeaderHour1(hours);
                grid[r, 0].RowSpan = partsForHour;

                int minutes = 0;
                for (int rs = r; rs < (r + partsForHour); rs++)
                {
                    grid[rs, 1] = new HeaderHour2(minutes);
                    minutes += minAppointmentLength;
                }
                hours++;
            }

            grid.FixedColumns = c_ColumnsHeader;
            grid.FixedRows = c_RowsHeader;

            //Fix the width of the first 2 columns
            grid.Columns[0].Width = 40;
            grid.Columns[0].AutoSizeMode = SourceGrid.AutoSizeMode.None;
            grid.Columns[1].Width = 40;
            grid.Columns[1].AutoSizeMode = SourceGrid.AutoSizeMode.None;

            grid.AutoStretchColumnsToFitWidth = true;
            grid.AutoStretchRowsToFitHeight = true;
            grid.AutoSizeCells();


            //Create Appointment Cells
            //Days
            for (int c = c_ColumnsHeader; c < grid.ColumnsCount; c++)
            {
                DateTime currentTime = dateTimeStart.AddDays(c - c_ColumnsHeader);
                int indexAppointment = -1;
                Cell appointmentCell = null;
                //Hours
                for (int r = c_RowsHeader; r < grid.RowsCount; r += partsForHour)
                {
                    //Minutes
                    for (int rs = r; rs < (r + partsForHour); rs++)
                    {
                        bool l_bFound = false;
                        //Appointments
                        for (int i = 0; i < m_Appointments.Count; i++)
                        {
                            if (m_Appointments[i].ContainsDateTime(currentTime))
                            {
                                l_bFound = true;

                                if (indexAppointment != i)
                                {
                                    appointmentCell = new CellAppointment(m_Appointments[i]);
                                    appointmentCell.View = m_Appointments[i].View;
                                    if (m_Appointments[i].Controller != null)
                                        appointmentCell.AddController(m_Appointments[i].Controller);
                                    grid[rs, c] = appointmentCell;
                                    indexAppointment = i;
                                }
                                else
                                {
                                    grid[rs, c] = null;
                                    appointmentCell.RowSpan++;
                                }

                                break;
                            }
                        }
                        if (l_bFound)
                        {
                        }
                        else
                        {
                            grid[rs, c] = new CellEmpty(currentTime, currentTime.AddMinutes(minAppointmentLength));
                            indexAppointment = -1;
                            appointmentCell = null;
                        }

                        currentTime = currentTime.AddMinutes(minAppointmentLength);
                    }
                }
            }
        }

        public void UnLoadPlanning()
        {
            grid.Redim(0, 0);
        }

        public event AppointmentEventHandler AppointmentClick;

        protected virtual void OnAppointmentClick(AppointmentEventArgs e)
        {
            if (AppointmentClick != null)
                AppointmentClick(this, e);
        }

        public event AppointmentEventHandler AppointmentDoubleClick;

        protected virtual void OnAppointmentDoubleClick(AppointmentEventArgs e)
        {
            if (AppointmentDoubleClick != null)
                AppointmentDoubleClick(this, e);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grid = new Grid();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AutoStretchColumnsToFitWidth = false;
            this.grid.AutoStretchRowsToFitHeight = false;
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(360, 340);
            this.grid.SpecialKeys = GridSpecialKeys.Default;
            this.grid.TabIndex = 0;
            // 
            // PlanningGrid
            // 
            this.Controls.Add(this.grid);
            this.Name = "PlanningGrid";
            this.Size = new System.Drawing.Size(360, 340);
            this.ResumeLayout(false);
        }

        #endregion

        #region Nested type: AppointmentController

        private class AppointmentController : ControllerBase
        {
            private readonly PlanningGrid mPlanningGrid;

            public AppointmentController(PlanningGrid planningGrid)
            {
                mPlanningGrid = planningGrid;
            }

            public override void OnClick(CellContext sender, EventArgs e)
            {
                base.OnClick(sender, e);

                if (sender.Cell is CellAppointment)
                {
                    var cell = (CellAppointment) sender.Cell;
                    mPlanningGrid.OnAppointmentClick(new AppointmentEventArgs(cell.Appointment.DateTimeStart,
                                                                              cell.Appointment.DateTimeEnd,
                                                                              cell.Appointment));
                }
                else if (sender.Cell is CellEmpty)
                {
                    var cell = (CellEmpty) sender.Cell;
                    mPlanningGrid.OnAppointmentClick(new AppointmentEventArgs(cell.Start, cell.End, null));
                }
            }

            public override void OnDoubleClick(CellContext sender, EventArgs e)
            {
                base.OnDoubleClick(sender, e);

                if (sender.Cell is CellAppointment)
                {
                    var cell = (CellAppointment) sender.Cell;
                    mPlanningGrid.OnAppointmentDoubleClick(new AppointmentEventArgs(cell.Appointment.DateTimeStart,
                                                                                    cell.Appointment.DateTimeEnd,
                                                                                    cell.Appointment));
                }
                else if (sender.Cell is CellEmpty)
                {
                    var cell = (CellEmpty) sender.Cell;
                    mPlanningGrid.OnAppointmentDoubleClick(new AppointmentEventArgs(cell.Start, cell.End, null));
                }
            }
        }

        #endregion
    }

    public class CellAppointment : Cell
    {
        private readonly IAppointment m_Appointment;

        public CellAppointment(IAppointment appointment) : base(appointment.Title)
        {
            m_Appointment = appointment;
        }

        public IAppointment Appointment
        {
            get { return m_Appointment; }
        }
    }

    public class CellEmpty : Cell
    {
        private readonly DateTime m_End;
        private readonly DateTime m_Start;

        public CellEmpty(DateTime start, DateTime end) : base(null)
        {
            m_Start = start;
            m_End = end;
        }

        public DateTime Start
        {
            get { return m_Start; }
        }

        public DateTime End
        {
            get { return m_End; }
        }
    }

    public class Header00 : Header
    {
        public Header00(object val) : base(val)
        {
        }
    }

    public class HeaderDay1 : Header
    {
        public HeaderDay1(object val) : base(val)
        {
        }
    }

    public class HeaderDay2 : Header
    {
        public HeaderDay2(object val) : base(val)
        {
        }
    }

    public class HeaderHour1 : RowHeader
    {
        public HeaderHour1(object val) : base(val)
        {
        }
    }

    public class HeaderHour2 : RowHeader
    {
        public HeaderHour2(object val) : base(val)
        {
        }
    }

    public class AppointmentEventArgs : EventArgs
    {
        private readonly IAppointment m_Appointment;
        private readonly DateTime m_DateTimeEnd;
        private readonly DateTime m_DateTimeStart;

        public AppointmentEventArgs(DateTime start, DateTime end, IAppointment appointment)
        {
            m_DateTimeEnd = end;
            m_DateTimeStart = start;
            m_Appointment = appointment;
        }

        public DateTime DateTimeStart
        {
            get { return m_DateTimeStart; }
        }

        public DateTime DateTimeEnd
        {
            get { return m_DateTimeEnd; }
        }

        public IAppointment Appointment
        {
            get { return m_Appointment; }
        }
    }

    public interface IAppointment
    {
        string Title { get; }

        IView View { get; }

        DateTime DateTimeStart { get; }
        DateTime DateTimeEnd { get; }

        IController Controller { get; set; }
        bool ContainsDateTime(DateTime p_DateTime);
    }

    public class AppointmentBase : IAppointment
    {
        private DateTime m_DateTimeEnd;
        private DateTime m_DateTimeStart;
        private string m_Title;
        private IView m_View;

        public AppointmentBase(string title, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            m_Title = title;
            m_DateTimeEnd = dateTimeEnd;
            m_DateTimeStart = dateTimeStart;

            m_View = new Cells.Views.Cell();
            m_View.Border = RectangleBorder.RectangleBlack1Width;
        }

        public AppointmentBase() : this("", DateTime.Now, DateTime.Now)
        {
        }

        #region IAppointment Members

        public DateTime DateTimeStart
        {
            get { return m_DateTimeStart; }
            set { m_DateTimeStart = value; }
        }

        public DateTime DateTimeEnd
        {
            get { return m_DateTimeEnd; }
            set { m_DateTimeEnd = value; }
        }

        public virtual string Title
        {
            get { return m_Title; }
            set { m_Title = value; }
        }

        [Browsable(false)]
        public virtual IView View
        {
            get { return m_View; }
            set { m_View = value; }
        }

        public bool ContainsDateTime(DateTime p_DateTime)
        {
            return (m_DateTimeStart <= p_DateTime && m_DateTimeEnd > p_DateTime);
        }

        [Browsable(false)]
        public IController Controller { get; set; }

        #endregion
    }

    /// <summary>
    /// A collection of elements of type IAppointment
    /// </summary>
    public class AppointmentCollection : List<IAppointment>
    {
    }
}