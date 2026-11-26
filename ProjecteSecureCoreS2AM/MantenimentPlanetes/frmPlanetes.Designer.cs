namespace ProjecteSecureCoreS2AM.MantenimentPlanetes
{
    partial class frmPlanetes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
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
            this.cmbSector.Location = new System.Drawing.Point(66, 78);
            this.cmbSector.Name = "cmbSector";
            this.cmbSector.Size = new System.Drawing.Size(180, 24);
            this.cmbSector.TabIndex = 55;
            //
            // cmbFiliation
            //
            this.cmbFiliation.FormattingEnabled = true;
            this.cmbFiliation.Location = new System.Drawing.Point(66, 109);
            this.cmbFiliation.Name = "cmbFiliation";
            this.cmbFiliation.Size = new System.Drawing.Size(180, 24);
            this.cmbFiliation.TabIndex = 56;
            //
            // cmbSpecie
            //
            this.cmbSpecie.FormattingEnabled = true;
            this.cmbSpecie.Location = new System.Drawing.Point(66, 139);
            this.cmbSpecie.Name = "cmbSpecie";
            this.cmbSpecie.Size = new System.Drawing.Size(180, 24);
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

        private System.Windows.Forms.ComboBox cmbSector;
        private System.Windows.Forms.ComboBox cmbFiliation;
        private System.Windows.Forms.ComboBox cmbSpecie;
    }
}
