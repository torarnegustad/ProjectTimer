using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

/* TODO for v2:
 * 
 *  1. OK: Knapp for "Pending project", kan samle tid mens et nytt prosjekt opprettes eller inntil riktig prosjekt er kjent
 *      Når nytt prosjekt aktiveres overføres all tiden fra "unknown" over til dette.
 *      
 *  2. OK: Rapport skriver ut CSV, kolonne to inneholder dagens dato (20.11.2009), kolonne tre 
 *      inneholder antall timer (rundet opp til nærmeste halvtime)
 *  
 *  3. OK: Fri posisjonering av knapper, f.eks at right click tar tak i knappen. Posisjon lagres som koordinater i hver entry.
 *      Dette vil si at flowlayout fjernes? Sorteringsknappene angir "høyden", dvs Z-posisjon i vinduet.
 * 
 *  4. Knapp for å flytte aktiv timer til pending timer (f.eks en [<] øverst til høyre).
 *  
 *  5. OK: Sette default priority på programmet til High.
 *  
 *  6. Støtte &Hotkeys for knappene.
 * 
 *  7. OK Hvis PC er idle i over x minutter, bytt til pending timer.
 */

namespace ProjectTimer
{
    public partial class ProjectTimer : Form
    {
        #region *** Variables ***

        /// <summary>
        /// Allowed system idle time before activating pending timer.
        /// </summary>
        private static readonly int MaxIdleTimeInSeconds = 5 * 60;

        private String m_configFileXML = null;

        private CustomStopwatch m_stopWatch = new CustomStopwatch();
        private ProjectList m_projectList = new ProjectList();

        private Button m_activeButton = null;
        private Button m_editButton = null;
        private Button m_dragButton = null;

        private Boolean m_clickResizing = false;
        private Boolean m_buttonDragged = false;

        private Size m_clickLocation;

        #endregion

        #region *** Project Entry class ***

        [XmlRoot("ProjectEntry")]
        public class ProjectEntry
        {
            private String m_name = "";
            private long m_elapsed = 0;
            private int m_color = 0;
            private Size m_size = new Size(200, 80);
            private Point m_position = new Point(0, 0);
            private Boolean m_ignore = false;
            private Boolean m_idle = false;

            [XmlElement("Name")]
            public String Name
            {
                get { return (m_name); }
                set { m_name = value; }
            }

            [XmlElement("Ignore")]
            public Boolean Ignore
            {
                get { return (m_ignore); }
                set { m_ignore = value; }
            }

            [XmlElement("Color")]
            public int Color
            {
                get { return (m_color); }
                set { m_color = value; }
            }

            [XmlElement("Elapsed")]
            public long Elapsed
            {
                get { return (m_elapsed); }
                set { m_elapsed = value; }
            }

            [XmlElement("Size")]
            public Size Size
            {
                get { return (m_size); }
                set { m_size = value; }
            }

            [XmlElement("Location")]
            public Point Location
            {
                get { return (m_position); }
                set { m_position = value; }
            }

            [XmlElement("Idle")]
            public bool Idle
            {
                get { return (m_idle); }
                set { m_idle = value; }
            }
        }

        [XmlRoot("ProjectList")]
        public class ProjectList : List<ProjectEntry> { }

        #endregion

        #region *** Constructor ***

        public ProjectTimer()
        {
            InitializeComponent();

            m_configFileXML = Environment.GetFolderPath(Environment.SpecialFolder.Personal).ToString() + "\\" + Properties.Resources.r_XmlFile;

            // setup tray icon
            trayNotifyIcon.Icon = this.Icon;
            trayNotifyIcon.Text = Properties.Resources.r_Title + " v" + Properties.Resources.r_Version;

            // set high priority task
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
        }

        #endregion

        #region *** Functions ***

        private void ShowHide()
        {
            this.Visible = !this.Visible;
        }

        private Color PT_GetStateColor(bool active, bool edit)
        {
            if (active && edit) return (Color.DarkOrange);
            if (active) return (Color.Yellow);
            if (edit) return (Color.LightYellow);
            return (Button.DefaultBackColor);
        }

        private String PT_GetElapsedTime(long msec)
        {
            TimeSpan span = TimeSpan.FromMilliseconds(msec);
            return (String.Format("{0:00}:{1:00}:{2:00}",
                span.Hours, span.Minutes, span.Seconds));
        }

        private void PT_SetActiveButton(Button btn)
        {
            ProjectEntry entry = null;

            if (null == m_activeButton)
                updateTimer.Start();

            if (null != m_activeButton)
            {
                m_stopWatch.Stop();

                if (btn == this.m_activeButton)
                    btn = null;

                if (m_activeButton == btnPending)
                {
                    m_activeButton.Text = PT_GetElapsedTime(0);
                    m_activeButton.ForeColor = Color.RoyalBlue;
                }
                else
                {
                    entry = (ProjectEntry)m_activeButton.Tag;
                    entry.Elapsed += m_stopWatch.ElapsedMilliseconds;
                    m_stopWatch.Reset();

                    m_activeButton.ForeColor = Color.FromArgb(entry.Color);
                    m_activeButton.Text = GetActiveButtonText(entry);
                }

                m_activeButton.BackColor = PT_GetStateColor(false, m_activeButton == this.m_editButton);
            }

            m_activeButton = btn;

            if (null != m_activeButton)
            {
                m_stopWatch.Start();
                m_activeButton.ForeColor = Color.Black;
                m_activeButton.BackColor = PT_GetStateColor(true, m_activeButton == this.m_editButton);
            }
            else
                m_stopWatch.Reset();
        }

        private void PT_SetEditButton(Button btn)
        {
            ProjectEntry entry = null;

            if (btn == this.btnPending)
                return;

            if (null != this.m_editButton)
            {
                if (btn == this.m_editButton)
                    btn = null;

                this.m_editButton.BackColor = PT_GetStateColor(m_activeButton == this.m_editButton, false);
            }

            this.m_editButton = btn;
            if (this.m_editButton != null)
            {
                this.m_editButton.BackColor = PT_GetStateColor(m_activeButton == this.m_editButton, true);

                entry = (ProjectEntry)this.m_editButton.Tag;

                this.btnColor.BackColor = Color.FromArgb(entry.Color);
                this.txtName.Text = entry.Name;               
                this.cbInclude.Checked = !entry.Ignore;
                this.cbIdle.Checked = entry.Idle;

                this.pnlEdit.Visible = true;

                this.txtName.Select();
                this.txtName.SelectAll();
            }
            else
            {
                this.txtName.Text = "";
                this.btnColor.BackColor = Color.Gray;
                this.pnlEdit.Visible = false;
            }
        }

        private void PT_SetDragButton(Button btn, Point loc)
        {
            if (btn == this.btnPending) return;

            if (null != btn)
            {
                Size size = Properties.Resources.Resize.Size + new Size(4, 4);

                // check whether user has clicked in the "resize" area
                this.m_clickResizing = ((btn.Width - loc.X < size.Width) && (btn.Height - loc.Y < size.Height));

                // start dragging or resizing the button
                if (this.m_clickResizing)
                {
                    this.m_clickLocation = new Size(btn.Width - loc.X, btn.Height - loc.Y);
                }
                else
                {
                    this.m_clickLocation = new Size(loc.X, loc.Y);
                }
            }

            // assign the drag button
            this.m_dragButton = btn;
        }

        private Button PT_AddProjectButton(ProjectEntry entry)
        {
            Button btn = new Button();

            btn.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btn.BackColor = Button.DefaultBackColor;
            btn.TabStop = false;
            btn.MinimumSize = new Size(120, 40);
            btn.ImageAlign = ContentAlignment.BottomRight;
            btn.Image = Properties.Resources.Resize;

            btn.Text = String.Format("{0}\n{1}", entry.Name, PT_GetElapsedTime(entry.Elapsed));
            btn.ForeColor = Color.FromArgb(255, Color.FromArgb(entry.Color));
            btn.Size = entry.Size;
            btn.Tag = entry;
            btn.AutoEllipsis = true;

            btn.MouseDown += new MouseEventHandler(btn_MouseDown);
            btn.MouseMove += new MouseEventHandler(btn_MouseMove);
            btn.MouseUp += new MouseEventHandler(btn_MouseUp);
//            btn.Click += new EventHandler(btn_Click);

            this.panelButtons.Controls.Add(btn);
            btn.Location = entry.Location;
            btn.BringToFront();

            return (btn);
        }

        #endregion

        #region *** Form events ***

        private void ProjectTimer_Load(object sender, EventArgs e)
        {
            // set application title
            this.Text = Properties.Resources.r_Title + " v" + Properties.Resources.r_Version + " " + Properties.Resources.r_Info;

            // load window size and location
            if (Properties.Settings.Default.Saved)
            {
                this.Location = Properties.Settings.Default.Location;
                this.Size = Properties.Settings.Default.Size;
            }

            TextReader tr = null;
            try
            {
                // try to load serialized XML data from file
                XmlSerializer xs = new XmlSerializer(typeof(ProjectList));
                tr = new StreamReader(m_configFileXML);
                m_projectList = (ProjectList)xs.Deserialize(tr);
            }
            catch { }
            finally
            {
                if (tr != null) tr.Close();

                // add a button for each project
                foreach (ProjectEntry proj in m_projectList)
                {
                    this.PT_AddProjectButton(proj);
                }
            }
        }

        private void ProjectTimer_FormClosing(object sender, FormClosingEventArgs e)
        {
            // don't close, move to tray
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; this.Hide(); return;
            }

            this.m_stopWatch.Stop();
            this.PT_SetActiveButton(null);

            TextWriter tw = null;
            String backupFile = m_configFileXML + ".bak";
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(ProjectList));
                if (File.Exists(m_configFileXML))
                {
                    if (File.Exists(backupFile))
                        File.Delete(backupFile);
                    File.Move(m_configFileXML, backupFile);
                }
                tw = new StreamWriter(m_configFileXML);
                xs.Serialize(tw, m_projectList);
            }
            catch
            {
                // an error occurred, try to restore backup:
                if (File.Exists(backupFile))
                {
                    if (File.Exists(m_configFileXML))
                        File.Delete(m_configFileXML);
                    File.Move(backupFile, m_configFileXML);
                }
            }
            finally
            {
                if (tw != null) 
                { 
                    tw.Close(); tw.Dispose(); tw = null; 
                }

                if( File.Exists(backupFile) )
                    System.IO.File.Delete(backupFile);

                // save window size and location
                Properties.Settings.Default.Saved = true;
                Properties.Settings.Default.Location = this.Location;
                Properties.Settings.Default.Size = this.Size;
                Properties.Settings.Default.Save();
            }
        }

        #endregion

        #region *** Other events ***

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (m_stopWatch.IsRunning)
            {
                if (m_activeButton != btnPending)
                {
                    // check if the entry allows idling
                    ProjectEntry entry = (ProjectEntry)m_activeButton.Tag;
                    if (!entry.Idle)
                    {
                        // check how long the computer has been idle
                        var userIdleSeconds = User32.GetLastInputTime();
                        if (userIdleSeconds >= MaxIdleTimeInSeconds)
                        {
                            m_stopWatch.Stop();

                            // transfer (elapsed time - idle time) to active button
                            m_stopWatch.Reset(m_stopWatch.ElapsedMilliseconds - userIdleSeconds * 1000);

                            // switch to pending button
                            this.PT_SetActiveButton(btnPending);

                            // reset stopwatch to idle time
                            m_stopWatch.Reset(userIdleSeconds * 1000);
                            m_stopWatch.Start();
                        }
                    }
                }

                if (m_activeButton == btnPending)
                {
                    m_activeButton.Text = PT_GetElapsedTime(m_stopWatch.ElapsedMilliseconds);
                }
                else
                {
                    ProjectEntry entry = (ProjectEntry)m_activeButton.Tag;
                    m_activeButton.Text = GetActiveButtonText(entry);
                }
            }
        }

        private string GetActiveButtonText(ProjectEntry entry) => string.Format("{0}\n{1}", entry.Name, PT_GetElapsedTime(m_stopWatch.ElapsedMilliseconds + entry.Elapsed));

        /*
       private void btn_Click(object sender, EventArgs e)
       {
           this.PT_SetActiveButton((Button)sender);
       }
*/
        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    this.PT_SetActiveButton((Button)sender);
                    break;

                case MouseButtons.Right:
                    this.m_buttonDragged = false;
                    this.PT_SetDragButton((Button)sender, e.Location);
                    break;
            }
        }

        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    this.PT_SetDragButton(null, e.Location);
                    if (!this.m_buttonDragged)
                        this.PT_SetEditButton((Button)sender);
                    break;
            }
        }

        private void btn_MouseMove(object sender, MouseEventArgs e)
        {
            if (null != this.m_dragButton)
            {
                // move button to front
                this.m_dragButton.BringToFront();
                
                // move entry to front of list (for correct order when loaded)
                ProjectEntry entry = (ProjectEntry)this.m_dragButton.Tag;
                int index = this.m_projectList.IndexOf(entry);
                this.m_projectList.RemoveAt(index);
                this.m_projectList.Add(entry);

                if (this.m_clickResizing)
                {
                    this.m_dragButton.Size = new Size(e.X, e.Y) + this.m_clickLocation;
                    entry.Size = this.m_dragButton.Size;
                }
                else
                {
                    this.m_dragButton.Location = this.panelButtons.PointToClient(
                        this.m_dragButton.PointToScreen(e.Location)) - this.m_clickLocation;
                    entry.Location = this.m_dragButton.Location;
                }

                this.m_buttonDragged = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProjectEntry entry = new ProjectEntry();
            entry.Name = "new project";
            this.m_projectList.Add(entry);

            this.PT_SetEditButton(this.PT_AddProjectButton(entry));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (null == this.m_editButton) return;

            ProjectEntry entry = (ProjectEntry)this.m_editButton.Tag;
            if (entry.Elapsed > 0 && MessageBox.Show(this, "This button has elapsed time.\n\nAre you sure you want to delete it?", "Comfirm button delete", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            this.panelButtons.Controls.Remove(this.m_editButton);
            this.m_projectList.Remove(entry);

            this.PT_SetEditButton(null);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (null == this.m_editButton) return;

            if (MessageBox.Show(this, "Are you sure you want to reset this timer?", "Comfirm timer reset", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            if (this.m_editButton == this.m_activeButton)
            {
                this.m_stopWatch.Reset();
                this.m_stopWatch.Start();
            }

            ProjectEntry entry = (ProjectEntry)this.m_editButton.Tag;
            entry.Elapsed = 0;
            this.m_editButton.Text = String.Format("{0}\n{1}", entry.Name, PT_GetElapsedTime(entry.Elapsed));
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (null == this.m_editButton) return;

            if (this.colorPicker.ShowDialog() == DialogResult.OK)
            {
                ProjectEntry entry = (ProjectEntry)this.m_editButton.Tag;
                entry.Color = this.colorPicker.Color.ToArgb();

                this.btnColor.BackColor = this.colorPicker.Color;
                if (this.m_editButton != this.m_activeButton)
                {
                    this.m_editButton.ForeColor = this.colorPicker.Color;
                }
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (null == this.m_editButton) return;
            if (!this.pnlEdit.Enabled) return;

            ProjectEntry entry = (ProjectEntry)this.m_editButton.Tag;
            entry.Name = this.txtName.Text;

            if (this.m_editButton != this.m_activeButton)
            {
                this.m_editButton.Text = String.Format("{0}\n{1}", entry.Name, PT_GetElapsedTime(entry.Elapsed));
            }

            entry.Name = txtName.Text;
        }

        private void cbInclude_CheckedChanged(object sender, EventArgs e)
        {
            if (null == this.m_editButton) return;
            if (!this.pnlEdit.Enabled) return;

            ProjectEntry entry = (ProjectEntry)this.m_editButton.Tag;
            entry.Ignore = !cbInclude.Checked;
        }

        private void cbIdle_CheckedChanged(object sender, EventArgs e)
        {
            if (null == this.m_editButton) return;
            if (!this.pnlEdit.Enabled) return;

            ProjectEntry entry = (ProjectEntry)this.m_editButton.Tag;
            entry.Idle = cbIdle.Checked;
        }

        private void btnEndDay_Click(object sender, EventArgs e)
        {
            if (this.m_stopWatch.IsRunning)
            {
                ProjectEntry entry = (ProjectEntry)m_activeButton.Tag;
                if( entry != null )
                    entry.Elapsed += m_stopWatch.ElapsedMilliseconds;

                this.m_stopWatch.Stop();
                this.m_stopWatch.Reset();

                this.m_activeButton.BackColor = PT_GetStateColor(false, m_activeButton == this.m_editButton);
                this.m_activeButton = null;
            }

            String report = "";
            String date = DateTime.Now.ToString("dd.MM.yyyy");
            foreach (ProjectEntry entry in this.m_projectList)
            {
                if (!entry.Ignore)
                {
                    TimeSpan span = TimeSpan.FromMilliseconds(entry.Elapsed);
                    double time = span.Hours + (span.Minutes >= 5 ? 0.5 : 0.0) + (span.Minutes >= 35 ? 0.5 : 0.0);
                    if (time > 0.0) report += $"{entry.Name}\t{date}\t{time:#.0}\n";
                }
            }

            if( !String.IsNullOrEmpty(report) )
                Clipboard.SetText(report);
        }

        private void btnResetAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to reset all timers?", "Confirm timer reset", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.m_stopWatch.Stop();
                this.m_stopWatch.Reset();

                this.PT_SetActiveButton(null);

                foreach (Button btn in this.panelButtons.Controls)
                {
                    ProjectEntry entry = (ProjectEntry)btn.Tag;
                    entry.Elapsed = 0;
                    btn.Text = String.Format("{0}\n{1}", entry.Name, PT_GetElapsedTime(entry.Elapsed));
                    btn.BackColor = PT_GetStateColor(false, btn == this.m_editButton);
                }
            }
        }


        private void trayContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == showTrayToolStripMenuItem)
                ShowHide();

            if (e.ClickedItem == closeTrayToolStripMenuItem)
                Application.Exit();

            if (e.ClickedItem == aboutTrayToolStripMenuItem)
                MessageBox.Show(Properties.Resources.r_About, Properties.Resources.r_Title + " v" + Properties.Resources.r_Version);
        }

        private void trayNotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                ShowHide();
        }

        #endregion


    }
}
