namespace AlgMgrWindow
{
    partial class AlgMgrForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlgMgrForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AlgName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mfr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TakeLiq = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.WorkLiq = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CloseOnly = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BSFLAG = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DELTA_CLOSE = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BASE_HTB = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.HEDGE_HTB = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Save = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Start_Stop = new System.Windows.Forms.DataGridViewButtonColumn();
            this.DP0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MA1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MA2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OURBID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OURASK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TBID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TASK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.Black;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(3, -7);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel1.Margin = new System.Windows.Forms.Padding(3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.RoyalBlue;
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel2.Margin = new System.Windows.Forms.Padding(1);
            this.splitContainer1.Size = new System.Drawing.Size(798, 194);
            this.splitContainer1.SplitterDistance = 157;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SeaShell;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Black;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AlgName,
            this.Mfr,
            this.TakeLiq,
            this.WorkLiq,
            this.CloseOnly,
            this.BSFLAG,
            this.DELTA_CLOSE,
            this.BASE_HTB,
            this.HEDGE_HTB,
            this.Save,
            this.Start_Stop,
            this.DP0,
            this.MA1,
            this.MA2,
            this.OURBID,
            this.OURASK,
            this.BID,
            this.ASK,
            this.TBID,
            this.TASK});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.Black;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(794, 153);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.setupFefaultValues);
            this.dataGridView1.RowDefaultCellStyleChanged += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_RowDefaultCellStyleChanged);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
            // 
            // statusStrip2
            // 
            this.statusStrip2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.statusStrip2.BackColor = System.Drawing.Color.RoyalBlue;
            this.statusStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2});
            this.statusStrip2.Location = new System.Drawing.Point(628, 1);
            this.statusStrip2.MinimumSize = new System.Drawing.Size(130, 7);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(166, 22);
            this.statusStrip2.TabIndex = 8;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.Color.RoyalBlue;
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(149, 17);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.RoyalBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(324, -2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 22);
            this.button1.TabIndex = 5;
            this.button1.Text = "STP View";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.statusStrip1.BackColor = System.Drawing.Color.RoyalBlue;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(147, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ActiveLinkColor = System.Drawing.Color.WhiteSmoke;
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(130, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(809, 181);
            this.panel1.TabIndex = 1;
            // 
            // AlgName
            // 
            this.AlgName.HeaderText = "NAME";
            this.AlgName.Name = "AlgName";
            this.AlgName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AlgName.Width = 42;
            // 
            // Mfr
            // 
            this.Mfr.HeaderText = "MFR";
            this.Mfr.Name = "Mfr";
            this.Mfr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Mfr.ToolTipText = "MessageToFillRatio";
            this.Mfr.Width = 41;
            // 
            // TakeLiq
            // 
            this.TakeLiq.HeaderText = "TAKE LIQ.";
            this.TakeLiq.Name = "TakeLiq";
            this.TakeLiq.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TakeLiq.Width = 42;
            // 
            // WorkLiq
            // 
            this.WorkLiq.HeaderText = "WORK LIQ.";
            this.WorkLiq.Name = "WorkLiq";
            this.WorkLiq.Width = 42;
            // 
            // CloseOnly
            // 
            this.CloseOnly.HeaderText = "CLOSE ONLY";
            this.CloseOnly.Name = "CloseOnly";
            this.CloseOnly.Width = 42;
            // 
            // BSFLAG
            // 
            this.BSFLAG.HeaderText = "B/S FLAG";
            this.BSFLAG.Name = "BSFLAG";
            this.BSFLAG.Width = 41;
            // 
            // DELTA_CLOSE
            // 
            this.DELTA_CLOSE.HeaderText = "DELTA";
            this.DELTA_CLOSE.Name = "DELTA_CLOSE";
            this.DELTA_CLOSE.Width = 42;
            // 
            // BASE_HTB
            // 
            this.BASE_HTB.HeaderText = "BASE HTB";
            this.BASE_HTB.Name = "BASE_HTB";
            this.BASE_HTB.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BASE_HTB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // HEDGE_HTB
            // 
            this.HEDGE_HTB.HeaderText = "HEDGE HTB";
            this.HEDGE_HTB.Name = "HEDGE_HTB";
            this.HEDGE_HTB.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.HEDGE_HTB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Save
            // 
            this.Save.HeaderText = "SAVE";
            this.Save.Name = "Save";
            this.Save.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Save.Text = "SAVE";
            this.Save.UseColumnTextForButtonValue = true;
            this.Save.Width = 42;
            // 
            // Start_Stop
            // 
            this.Start_Stop.HeaderText = "START STOP";
            this.Start_Stop.Name = "Start_Stop";
            this.Start_Stop.Text = "STOP";
            this.Start_Stop.Width = 41;
            // 
            // DP0
            // 
            this.DP0.HeaderText = "DP0";
            this.DP0.Name = "DP0";
            this.DP0.ReadOnly = true;
            this.DP0.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DP0.Width = 42;
            // 
            // MA1
            // 
            this.MA1.HeaderText = "MA1";
            this.MA1.Name = "MA1";
            this.MA1.ReadOnly = true;
            this.MA1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MA1.Width = 42;
            // 
            // MA2
            // 
            this.MA2.HeaderText = "MA2";
            this.MA2.Name = "MA2";
            this.MA2.ReadOnly = true;
            this.MA2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MA2.Width = 42;
            // 
            // OURBID
            // 
            this.OURBID.HeaderText = "OURBID";
            this.OURBID.Name = "OURBID";
            this.OURBID.ReadOnly = true;
            this.OURBID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OURBID.Width = 41;
            // 
            // OURASK
            // 
            this.OURASK.HeaderText = "OURASK";
            this.OURASK.Name = "OURASK";
            this.OURASK.ReadOnly = true;
            this.OURASK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OURASK.Width = 42;
            // 
            // BID
            // 
            this.BID.HeaderText = "BID";
            this.BID.Name = "BID";
            this.BID.ReadOnly = true;
            this.BID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BID.Width = 42;
            // 
            // ASK
            // 
            this.ASK.HeaderText = "ASK";
            this.ASK.Name = "ASK";
            this.ASK.ReadOnly = true;
            this.ASK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ASK.Width = 42;
            // 
            // TBID
            // 
            this.TBID.HeaderText = "TBID";
            this.TBID.Name = "TBID";
            this.TBID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TBID.Width = 41;
            // 
            // TASK
            // 
            this.TASK.HeaderText = "TASK";
            this.TASK.Name = "TASK";
            this.TASK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TASK.Width = 42;
            // 
            // AlgMgrForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(829, 201);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AlgMgrForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "STP MANAGER";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlgMgrForm_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlgName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mfr;
        private System.Windows.Forms.DataGridViewCheckBoxColumn TakeLiq;
        private System.Windows.Forms.DataGridViewCheckBoxColumn WorkLiq;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CloseOnly;
        private System.Windows.Forms.DataGridViewCheckBoxColumn BSFLAG;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DELTA_CLOSE;
        private System.Windows.Forms.DataGridViewCheckBoxColumn BASE_HTB;
        private System.Windows.Forms.DataGridViewCheckBoxColumn HEDGE_HTB;
        private System.Windows.Forms.DataGridViewButtonColumn Save;
        private System.Windows.Forms.DataGridViewButtonColumn Start_Stop;
        private System.Windows.Forms.DataGridViewTextBoxColumn DP0;
        private System.Windows.Forms.DataGridViewTextBoxColumn MA1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MA2;
        private System.Windows.Forms.DataGridViewTextBoxColumn OURBID;
        private System.Windows.Forms.DataGridViewTextBoxColumn OURASK;
        private System.Windows.Forms.DataGridViewTextBoxColumn BID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASK;
        private System.Windows.Forms.DataGridViewTextBoxColumn TBID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TASK;




    }
}

