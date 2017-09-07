namespace ProjectTimer
{
    partial class ProjectTimer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectTimer));
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.pnlLower = new System.Windows.Forms.Panel();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.cbIdle = new System.Windows.Forms.CheckBox();
            this.cbInclude = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnColor = new System.Windows.Forms.Button();
            this.btnNewDay = new System.Windows.Forms.Button();
            this.btnEndDay = new System.Windows.Forms.Button();
            this.colorPicker = new System.Windows.Forms.ColorDialog();
            this.trayNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aboutTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnPending = new System.Windows.Forms.Button();
            this.ptTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlLower.SuspendLayout();
            this.pnlEdit.SuspendLayout();
            this.trayContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 500;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(429, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(89, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add Project";
            this.ptTooltip.SetToolTip(this.btnAdd, "Create a new project timer button");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(45, 8);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 20);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // pnlLower
            // 
            this.pnlLower.Controls.Add(this.btnAdd);
            this.pnlLower.Controls.Add(this.pnlEdit);
            this.pnlLower.Controls.Add(this.btnNewDay);
            this.pnlLower.Controls.Add(this.btnEndDay);
            this.pnlLower.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLower.Location = new System.Drawing.Point(0, 337);
            this.pnlLower.Name = "pnlLower";
            this.pnlLower.Size = new System.Drawing.Size(792, 34);
            this.pnlLower.TabIndex = 3;
            // 
            // pnlEdit
            // 
            this.pnlEdit.Controls.Add(this.cbIdle);
            this.pnlEdit.Controls.Add(this.cbInclude);
            this.pnlEdit.Controls.Add(this.btnReset);
            this.pnlEdit.Controls.Add(this.txtName);
            this.pnlEdit.Controls.Add(this.btnDelete);
            this.pnlEdit.Controls.Add(this.btnColor);
            this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlEdit.Location = new System.Drawing.Point(0, 0);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(411, 34);
            this.pnlEdit.TabIndex = 0;
            this.pnlEdit.Visible = false;
            // 
            // cbIdle
            // 
            this.cbIdle.AutoSize = true;
            this.cbIdle.Location = new System.Drawing.Point(26, 11);
            this.cbIdle.Name = "cbIdle";
            this.cbIdle.Size = new System.Drawing.Size(15, 14);
            this.cbIdle.TabIndex = 4;
            this.ptTooltip.SetToolTip(this.cbIdle, "Allow idle system\r\n\r\nIf unchecked the pending timer will be\r\nautomatically activa" +
        "ted when the system\r\nhas been idle for a time.\r\n\r\nTypically used for meeting act" +
        "ivities.");
            this.cbIdle.UseVisualStyleBackColor = true;
            this.cbIdle.CheckedChanged += new System.EventHandler(this.cbIdle_CheckedChanged);
            // 
            // cbInclude
            // 
            this.cbInclude.AutoSize = true;
            this.cbInclude.Location = new System.Drawing.Point(7, 11);
            this.cbInclude.Name = "cbInclude";
            this.cbInclude.Size = new System.Drawing.Size(15, 14);
            this.cbInclude.TabIndex = 3;
            this.ptTooltip.SetToolTip(this.cbInclude, "Include in report\r\n\r\nIf checked, this project will be included\r\nwhen a report is " +
        "copied to the clipboard.\r\n\r\nTypically unchecked for your \"Lunch\" button.");
            this.cbInclude.UseVisualStyleBackColor = true;
            this.cbInclude.CheckedChanged += new System.EventHandler(this.cbInclude_CheckedChanged);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(347, 6);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(57, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.ptTooltip.SetToolTip(this.btnReset, "Reset elapsed time for this button");
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(284, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(57, 23);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "Delete";
            this.ptTooltip.SetToolTip(this.btnDelete, "Delete this button");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnColor
            // 
            this.btnColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColor.Location = new System.Drawing.Point(253, 6);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(25, 23);
            this.btnColor.TabIndex = 0;
            this.ptTooltip.SetToolTip(this.btnColor, "Project text color");
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnNewDay
            // 
            this.btnNewDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewDay.BackColor = System.Drawing.SystemColors.Control;
            this.btnNewDay.Location = new System.Drawing.Point(655, 6);
            this.btnNewDay.Name = "btnNewDay";
            this.btnNewDay.Size = new System.Drawing.Size(125, 23);
            this.btnNewDay.TabIndex = 0;
            this.btnNewDay.Text = "Reset all timers";
            this.ptTooltip.SetToolTip(this.btnNewDay, "Reset all project timers");
            this.btnNewDay.UseVisualStyleBackColor = false;
            this.btnNewDay.Click += new System.EventHandler(this.btnResetAll_Click);
            // 
            // btnEndDay
            // 
            this.btnEndDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEndDay.BackColor = System.Drawing.SystemColors.Control;
            this.btnEndDay.Location = new System.Drawing.Point(524, 6);
            this.btnEndDay.Name = "btnEndDay";
            this.btnEndDay.Size = new System.Drawing.Size(125, 23);
            this.btnEndDay.TabIndex = 0;
            this.btnEndDay.Text = "Stop / Get report";
            this.ptTooltip.SetToolTip(this.btnEndDay, "Stop all timers and write a report to the clipboard\r\n\r\nThe report can be pasted i" +
        "nto Excel\r\nor simply the text editor of your choice.");
            this.btnEndDay.UseVisualStyleBackColor = false;
            this.btnEndDay.Click += new System.EventHandler(this.btnEndDay_Click);
            // 
            // colorPicker
            // 
            this.colorPicker.Color = System.Drawing.SystemColors.ButtonFace;
            // 
            // trayNotifyIcon
            // 
            this.trayNotifyIcon.ContextMenuStrip = this.trayContextMenu;
            this.trayNotifyIcon.Text = "Project timer 2";
            this.trayNotifyIcon.Visible = true;
            this.trayNotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.trayNotifyIcon_MouseClick);
            // 
            // trayContextMenu
            // 
            this.trayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutTrayToolStripMenuItem,
            this.showTrayToolStripMenuItem,
            this.closeTrayToolStripMenuItem});
            this.trayContextMenu.Name = "trayContextMenu";
            this.trayContextMenu.Size = new System.Drawing.Size(134, 70);
            this.trayContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.trayContextMenu_ItemClicked);
            // 
            // aboutTrayToolStripMenuItem
            // 
            this.aboutTrayToolStripMenuItem.Name = "aboutTrayToolStripMenuItem";
            this.aboutTrayToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.aboutTrayToolStripMenuItem.Text = "About";
            // 
            // showTrayToolStripMenuItem
            // 
            this.showTrayToolStripMenuItem.Name = "showTrayToolStripMenuItem";
            this.showTrayToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.showTrayToolStripMenuItem.Text = "Show/Hide";
            // 
            // closeTrayToolStripMenuItem
            // 
            this.closeTrayToolStripMenuItem.Name = "closeTrayToolStripMenuItem";
            this.closeTrayToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.closeTrayToolStripMenuItem.Text = "Close";
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(0, 27);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(792, 310);
            this.panelButtons.TabIndex = 6;
            // 
            // btnPending
            // 
            this.btnPending.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPending.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPending.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnPending.Location = new System.Drawing.Point(0, 0);
            this.btnPending.Name = "btnPending";
            this.btnPending.Size = new System.Drawing.Size(792, 27);
            this.btnPending.TabIndex = 5;
            this.btnPending.Text = "00:00:00";
            this.ptTooltip.SetToolTip(this.btnPending, "Pending timer\r\n\r\nClick on a project button to transfer \r\nall pending time to that" +
        " project.");
            this.btnPending.UseMnemonic = false;
            this.btnPending.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            // 
            // ProjectTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 371);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.pnlLower);
            this.Controls.Add(this.btnPending);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "ProjectTimer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProjectTimer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProjectTimer_FormClosing);
            this.Load += new System.EventHandler(this.ProjectTimer_Load);
            this.pnlLower.ResumeLayout(false);
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            this.trayContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Panel pnlLower;
        private System.Windows.Forms.Button btnNewDay;
        private System.Windows.Forms.Button btnEndDay;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ColorDialog colorPicker;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.NotifyIcon trayNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip trayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem aboutTrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeTrayToolStripMenuItem;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox cbInclude;
        private System.Windows.Forms.Button btnPending;
        private System.Windows.Forms.CheckBox cbIdle;
        private System.Windows.Forms.ToolTip ptTooltip;
    }
}

