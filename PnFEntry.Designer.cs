namespace PnF_MktAssessment._2017.App
{
    partial class PnFEntry
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dtSymbolsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tASds2 = new PnF_MktAssessment._2017.App.TASds2();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.rbPrimary = new System.Windows.Forms.RadioButton();
            this.rbSectors = new System.Windows.Forms.RadioButton();
            this.fillByPrimaryIndicatorsToolStrip = new System.Windows.Forms.ToolStrip();
            this.fillByPrimaryIndicatorsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.dtSymbolsTableAdapter = new PnF_MktAssessment._2017.App.TASds2TableAdapters.dtSymbolsTableAdapter();
            this.symbolDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Indicator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsBreakout = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsPullback = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsRedZone = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.HEATScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SymbolID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSymbolsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tASds2)).BeginInit();
            this.fillByPrimaryIndicatorsToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.symbolDataGridViewTextBoxColumn,
            this.Indicator,
            this.IsBreakout,
            this.IsPullback,
            this.IsRedZone,
            this.HEATScore,
            this.Notes,
            this.SymbolID});
            this.dataGridView1.DataSource = this.dtSymbolsBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 67);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(863, 414);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.CkIndicatorCell);
            this.dataGridView1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.CaptureRow);
            // 
            // dtSymbolsBindingSource
            // 
            this.dtSymbolsBindingSource.AllowNew = false;
            this.dtSymbolsBindingSource.DataMember = "dtSymbols";
            this.dtSymbolsBindingSource.DataSource = this.tASds2;
            // 
            // tASds2
            // 
            this.tASds2.DataSetName = "TASds2";
            this.tASds2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(426, 41);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(551, 500);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.SavePnFData);
            // 
            // rbPrimary
            // 
            this.rbPrimary.AutoSize = true;
            this.rbPrimary.Checked = true;
            this.rbPrimary.Location = new System.Drawing.Point(15, 22);
            this.rbPrimary.Name = "rbPrimary";
            this.rbPrimary.Size = new System.Drawing.Size(108, 17);
            this.rbPrimary.TabIndex = 3;
            this.rbPrimary.TabStop = true;
            this.rbPrimary.Text = "Primary Indicators";
            this.rbPrimary.UseVisualStyleBackColor = true;
            this.rbPrimary.CheckedChanged += new System.EventHandler(this.ShowPrimaryIndicators);
            // 
            // rbSectors
            // 
            this.rbSectors.AutoSize = true;
            this.rbSectors.Location = new System.Drawing.Point(16, 40);
            this.rbSectors.Name = "rbSectors";
            this.rbSectors.Size = new System.Drawing.Size(61, 17);
            this.rbSectors.TabIndex = 4;
            this.rbSectors.Text = "Sectors";
            this.rbSectors.UseVisualStyleBackColor = true;
            this.rbSectors.CheckedChanged += new System.EventHandler(this.ShowSectorIndicators);
            // 
            // fillByPrimaryIndicatorsToolStrip
            // 
            this.fillByPrimaryIndicatorsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fillByPrimaryIndicatorsToolStripButton});
            this.fillByPrimaryIndicatorsToolStrip.Location = new System.Drawing.Point(0, 0);
            this.fillByPrimaryIndicatorsToolStrip.Name = "fillByPrimaryIndicatorsToolStrip";
            this.fillByPrimaryIndicatorsToolStrip.Size = new System.Drawing.Size(905, 25);
            this.fillByPrimaryIndicatorsToolStrip.TabIndex = 5;
            this.fillByPrimaryIndicatorsToolStrip.Text = "fillByPrimaryIndicatorsToolStrip";
            // 
            // fillByPrimaryIndicatorsToolStripButton
            // 
            this.fillByPrimaryIndicatorsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fillByPrimaryIndicatorsToolStripButton.Name = "fillByPrimaryIndicatorsToolStripButton";
            this.fillByPrimaryIndicatorsToolStripButton.Size = new System.Drawing.Size(132, 22);
            this.fillByPrimaryIndicatorsToolStripButton.Text = "FillByPrimaryIndicators";
            this.fillByPrimaryIndicatorsToolStripButton.Click += new System.EventHandler(this.fillByPrimaryIndicatorsToolStripButton_Click);
            // 
            // dtSymbolsTableAdapter
            // 
            this.dtSymbolsTableAdapter.ClearBeforeFill = true;
            // 
            // symbolDataGridViewTextBoxColumn
            // 
            this.symbolDataGridViewTextBoxColumn.DataPropertyName = "Symbol";
            this.symbolDataGridViewTextBoxColumn.HeaderText = "Symbol";
            this.symbolDataGridViewTextBoxColumn.Name = "symbolDataGridViewTextBoxColumn";
            this.symbolDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Indicator
            // 
            this.Indicator.HeaderText = "Indicator";
            this.Indicator.Name = "Indicator";
            // 
            // IsBreakout
            // 
            this.IsBreakout.FalseValue = "1";
            this.IsBreakout.HeaderText = "Is BrkOut";
            this.IsBreakout.IndeterminateValue = "-1";
            this.IsBreakout.Name = "IsBreakout";
            this.IsBreakout.TrueValue = "0";
            // 
            // IsPullback
            // 
            this.IsPullback.FalseValue = "1";
            this.IsPullback.HeaderText = "Is Pullback";
            this.IsPullback.Name = "IsPullback";
            this.IsPullback.TrueValue = "0";
            // 
            // IsRedZone
            // 
            this.IsRedZone.FalseValue = "1";
            this.IsRedZone.HeaderText = "Is In Red Zone";
            this.IsRedZone.Name = "IsRedZone";
            this.IsRedZone.TrueValue = "0";
            // 
            // HEATScore
            // 
            this.HEATScore.HeaderText = "HEAT Score";
            this.HEATScore.Name = "HEATScore";
            // 
            // Notes
            // 
            this.Notes.HeaderText = "Notes";
            this.Notes.Name = "Notes";
            // 
            // SymbolID
            // 
            this.SymbolID.DataPropertyName = "SymbolID";
            this.SymbolID.HeaderText = "SymbolID";
            this.SymbolID.Name = "SymbolID";
            this.SymbolID.ReadOnly = true;
            this.SymbolID.Visible = false;
            // 
            // PnFEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 543);
            this.Controls.Add(this.fillByPrimaryIndicatorsToolStrip);
            this.Controls.Add(this.rbSectors);
            this.Controls.Add(this.rbPrimary);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "PnFEntry";
            this.Text = "PnFEntry";
            this.Load += new System.EventHandler(this.PnFEntry_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSymbolsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tASds2)).EndInit();
            this.fillByPrimaryIndicatorsToolStrip.ResumeLayout(false);
            this.fillByPrimaryIndicatorsToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private TASds2 tASds2;
        private System.Windows.Forms.BindingSource dtSymbolsBindingSource;
        private TASds2TableAdapters.dtSymbolsTableAdapter dtSymbolsTableAdapter;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RadioButton rbPrimary;
        private System.Windows.Forms.RadioButton rbSectors;
        private System.Windows.Forms.ToolStrip fillByPrimaryIndicatorsToolStrip;
        private System.Windows.Forms.ToolStripButton fillByPrimaryIndicatorsToolStripButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn symbolDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Indicator;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsBreakout;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsPullback;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsRedZone;
        private System.Windows.Forms.DataGridViewTextBoxColumn HEATScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notes;
        private System.Windows.Forms.DataGridViewTextBoxColumn SymbolID;

    }
}