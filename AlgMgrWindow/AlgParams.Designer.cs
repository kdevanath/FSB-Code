namespace AlgMgrWindow
{
    partial class AlgParams
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.AlgParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKEW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WIDTH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MA1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MA2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MA1_LENGTH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MA2_LENGTH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPD_THRESH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CXL_WORSE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BASE_WORK_SIZE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CXL_BETTER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RANDOMIZE_BASE = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BASE_TAKE_SIZE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HEDGE_TAKE_SIZE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HEDGE_PREMIUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRADE_THRESH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BETA_MULTI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FX_MULTI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIZE_ADJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MIN_HDG_SIZE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAX_POSITION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAX_PENDING = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRICE_LIMIT = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.MIX_RATIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DELTA_THRESH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BUMP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SKEW_UNIT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLOSE_TARGET = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AlgParamName,
            this.SKEW,
            this.WIDTH,
            this.MA1,
            this.MA2,
            this.MA1_LENGTH,
            this.MA2_LENGTH,
            this.UPD_THRESH,
            this.CXL_WORSE,
            this.BASE_WORK_SIZE,
            this.CXL_BETTER,
            this.RANDOMIZE_BASE,
            this.BASE_TAKE_SIZE,
            this.HEDGE_TAKE_SIZE,
            this.HEDGE_PREMIUM,
            this.TRADE_THRESH,
            this.BETA_MULTI,
            this.FX_MULTI,
            this.SIZE_ADJ,
            this.MIN_HDG_SIZE,
            this.MAX_POSITION,
            this.MAX_PENDING,
            this.PRICE_LIMIT,
            this.MIX_RATIO,
            this.DELTA_THRESH,
            this.BUMP,
            this.SKEW_UNIT,
            this.CLOSE_TARGET});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(775, 175);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // AlgParamName
            // 
            this.AlgParamName.HeaderText = "NAME";
            this.AlgParamName.Name = "AlgParamName";
            this.AlgParamName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AlgParamName.Width = 32;
            // 
            // SKEW
            // 
            this.SKEW.HeaderText = "SKEW";
            this.SKEW.Name = "SKEW";
            this.SKEW.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SKEW.Width = 32;
            // 
            // WIDTH
            // 
            this.WIDTH.HeaderText = "WIDTH";
            this.WIDTH.Name = "WIDTH";
            this.WIDTH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WIDTH.Width = 32;
            // 
            // MA1
            // 
            this.MA1.HeaderText = "MA1";
            this.MA1.Name = "MA1";
            this.MA1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MA1.Width = 31;
            // 
            // MA2
            // 
            this.MA2.HeaderText = "MA2";
            this.MA2.Name = "MA2";
            this.MA2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MA1_LENGTH
            // 
            this.MA1_LENGTH.HeaderText = "MA1 LENGTH";
            this.MA1_LENGTH.Name = "MA1_LENGTH";
            this.MA1_LENGTH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MA2_LENGTH
            // 
            this.MA2_LENGTH.HeaderText = "MA2 LENGTH";
            this.MA2_LENGTH.Name = "MA2_LENGTH";
            this.MA2_LENGTH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UPD_THRESH
            // 
            this.UPD_THRESH.HeaderText = "UPD. THRESH";
            this.UPD_THRESH.Name = "UPD_THRESH";
            this.UPD_THRESH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPD_THRESH.Width = 32;
            // 
            // CXL_WORSE
            // 
            this.CXL_WORSE.HeaderText = "CXL WORSE";
            this.CXL_WORSE.Name = "CXL_WORSE";
            this.CXL_WORSE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CXL_WORSE.Width = 32;
            // 
            // BASE_WORK_SIZE
            // 
            this.BASE_WORK_SIZE.HeaderText = "BASE WORK SZ.";
            this.BASE_WORK_SIZE.Name = "BASE_WORK_SIZE";
            this.BASE_WORK_SIZE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BASE_WORK_SIZE.Width = 32;
            // 
            // CXL_BETTER
            // 
            this.CXL_BETTER.HeaderText = "CXL BETTER";
            this.CXL_BETTER.Name = "CXL_BETTER";
            this.CXL_BETTER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CXL_BETTER.Width = 32;
            // 
            // RANDOMIZE_BASE
            // 
            this.RANDOMIZE_BASE.HeaderText = "RAND. BASE";
            this.RANDOMIZE_BASE.Name = "RANDOMIZE_BASE";
            this.RANDOMIZE_BASE.Width = 32;
            // 
            // BASE_TAKE_SIZE
            // 
            this.BASE_TAKE_SIZE.HeaderText = "BASE TK. SZ.";
            this.BASE_TAKE_SIZE.Name = "BASE_TAKE_SIZE";
            this.BASE_TAKE_SIZE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BASE_TAKE_SIZE.Width = 31;
            // 
            // HEDGE_TAKE_SIZE
            // 
            this.HEDGE_TAKE_SIZE.HeaderText = "HDG. TAKE SZ.";
            this.HEDGE_TAKE_SIZE.Name = "HEDGE_TAKE_SIZE";
            this.HEDGE_TAKE_SIZE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.HEDGE_TAKE_SIZE.Width = 32;
            // 
            // HEDGE_PREMIUM
            // 
            this.HEDGE_PREMIUM.HeaderText = "HDG. PREMIUM";
            this.HEDGE_PREMIUM.Name = "HEDGE_PREMIUM";
            this.HEDGE_PREMIUM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.HEDGE_PREMIUM.Width = 32;
            // 
            // TRADE_THRESH
            // 
            this.TRADE_THRESH.HeaderText = "TRADE THRESH.";
            this.TRADE_THRESH.Name = "TRADE_THRESH";
            this.TRADE_THRESH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TRADE_THRESH.Width = 32;
            // 
            // BETA_MULTI
            // 
            this.BETA_MULTI.HeaderText = "BETA MULT.";
            this.BETA_MULTI.Name = "BETA_MULTI";
            this.BETA_MULTI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BETA_MULTI.Width = 32;
            // 
            // FX_MULTI
            // 
            this.FX_MULTI.HeaderText = "FX. MULT";
            this.FX_MULTI.Name = "FX_MULTI";
            this.FX_MULTI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FX_MULTI.Width = 32;
            // 
            // SIZE_ADJ
            // 
            this.SIZE_ADJ.HeaderText = "SIZE ADUST";
            this.SIZE_ADJ.Name = "SIZE_ADJ";
            this.SIZE_ADJ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SIZE_ADJ.Width = 31;
            // 
            // MIN_HDG_SIZE
            // 
            this.MIN_HDG_SIZE.HeaderText = "MIN. HDG. SIZE";
            this.MIN_HDG_SIZE.Name = "MIN_HDG_SIZE";
            this.MIN_HDG_SIZE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MIN_HDG_SIZE.Width = 32;
            // 
            // MAX_POSITION
            // 
            this.MAX_POSITION.HeaderText = "MAX. POSITION";
            this.MAX_POSITION.Name = "MAX_POSITION";
            this.MAX_POSITION.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MAX_POSITION.Width = 32;
            // 
            // MAX_PENDING
            // 
            this.MAX_PENDING.HeaderText = "MAX. PENDING";
            this.MAX_PENDING.Name = "MAX_PENDING";
            this.MAX_PENDING.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MAX_PENDING.Width = 32;
            // 
            // PRICE_LIMIT
            // 
            this.PRICE_LIMIT.HeaderText = "PRICE LIMIT";
            this.PRICE_LIMIT.Items.AddRange(new object[] {
            "JOIN",
            "CROSS",
            "SQZ",
            "ASQZ",
            "MOC"});
            this.PRICE_LIMIT.Name = "PRICE_LIMIT";
            this.PRICE_LIMIT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // MIX_RATIO
            // 
            this.MIX_RATIO.HeaderText = "MIX RATIO";
            this.MIX_RATIO.Name = "MIX_RATIO";
            // 
            // DELTA_THRESH
            // 
            this.DELTA_THRESH.HeaderText = "DELTA THRESHOLD";
            this.DELTA_THRESH.Name = "DELTA_THRESH";
            this.DELTA_THRESH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BUMP
            // 
            this.BUMP.HeaderText = "BUMP";
            this.BUMP.Name = "BUMP";
            this.BUMP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SKEW_UNIT
            // 
            this.SKEW_UNIT.HeaderText = "SKEW. UNIT";
            this.SKEW_UNIT.Name = "SKEW_UNIT";
            this.SKEW_UNIT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CLOSE_TARGET
            // 
            this.CLOSE_TARGET.HeaderText = "TGT POSITION";
            this.CLOSE_TARGET.Name = "CLOSE_TARGET";
            // 
            // AlgParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 175);
            this.Controls.Add(this.dataGridView1);
            this.Name = "AlgParams";
            this.Text = "STP PARAMS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlgParams_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlgParamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKEW;
        private System.Windows.Forms.DataGridViewTextBoxColumn WIDTH;
        private System.Windows.Forms.DataGridViewTextBoxColumn MA1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MA2;
        private System.Windows.Forms.DataGridViewTextBoxColumn MA1_LENGTH;
        private System.Windows.Forms.DataGridViewTextBoxColumn MA2_LENGTH;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPD_THRESH;
        private System.Windows.Forms.DataGridViewTextBoxColumn CXL_WORSE;
        private System.Windows.Forms.DataGridViewTextBoxColumn BASE_WORK_SIZE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CXL_BETTER;
        private System.Windows.Forms.DataGridViewCheckBoxColumn RANDOMIZE_BASE;
        private System.Windows.Forms.DataGridViewTextBoxColumn BASE_TAKE_SIZE;
        private System.Windows.Forms.DataGridViewTextBoxColumn HEDGE_TAKE_SIZE;
        private System.Windows.Forms.DataGridViewTextBoxColumn HEDGE_PREMIUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRADE_THRESH;
        private System.Windows.Forms.DataGridViewTextBoxColumn BETA_MULTI;
        private System.Windows.Forms.DataGridViewTextBoxColumn FX_MULTI;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIZE_ADJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn MIN_HDG_SIZE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAX_POSITION;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAX_PENDING;
        private System.Windows.Forms.DataGridViewComboBoxColumn PRICE_LIMIT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MIX_RATIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DELTA_THRESH;
        private System.Windows.Forms.DataGridViewTextBoxColumn BUMP;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKEW_UNIT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLOSE_TARGET;


    }
}