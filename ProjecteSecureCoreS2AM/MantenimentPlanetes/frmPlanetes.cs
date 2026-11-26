using ProjecteSecureCoreS2AM;

namespace ProjecteSecureCoreS2AM.MantenimentPlanetes
{
    public partial class frmPlanetes : frmBase
    {
        private System.Windows.Forms.ComboBox cmbSector;
        private System.Windows.Forms.ComboBox cmbFiliation;
        private System.Windows.Forms.ComboBox cmbSpecie;

        public frmPlanetes()
        {
            InitializeComponent();
            Tabla = "Planets";
        }

        protected override void ConfigurarDGV()
        {
            base.ConfigurarDGV();
            ConfigurarFK(cmbSector,    "Sectors",    "idSector",    "DescSector",     "idSector");
            ConfigurarFK(cmbFiliation, "Filiations", "idFiliation", "DescFiliations", "idFiliation");
            ConfigurarFK(cmbSpecie,    "Species",    "idSpecie",    "DescSpecie",     "idNatives");
        }

        private void InitializeComponent()
        {
            this.cmbSector = new System.Windows.Forms.ComboBox();
            this.cmbFiliation = new System.Windows.Forms.ComboBox();
            this.cmbSpecie = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbSector
            // 
            this.cmbSector.FormattingEnabled = true;
            this.cmbSector.Location = new System.Drawing.Point(80, 76);
            this.cmbSector.Name = "cmbSector";
            this.cmbSector.Size = new System.Drawing.Size(121, 24);
            this.cmbSector.TabIndex = 55;
            // 
            // cmbFiliation
            // 
            this.cmbFiliation.FormattingEnabled = true;
            this.cmbFiliation.Location = new System.Drawing.Point(80, 106);
            this.cmbFiliation.Name = "cmbFiliation";
            this.cmbFiliation.Size = new System.Drawing.Size(121, 24);
            this.cmbFiliation.TabIndex = 56;
            // 
            // cmbSpecie
            // 
            this.cmbSpecie.FormattingEnabled = true;
            this.cmbSpecie.Location = new System.Drawing.Point(80, 137);
            this.cmbSpecie.Name = "cmbSpecie";
            this.cmbSpecie.Size = new System.Drawing.Size(121, 24);
            this.cmbSpecie.TabIndex = 57;
            // 
            // frmPlanetes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(900, 620);
            this.Controls.Add(this.cmbSpecie);
            this.Controls.Add(this.cmbFiliation);
            this.Controls.Add(this.cmbSector);
            this.Name = "frmPlanetes";
            this.Controls.SetChildIndex(this.cmbSector, 0);
            this.Controls.SetChildIndex(this.cmbFiliation, 0);
            this.Controls.SetChildIndex(this.cmbSpecie, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
