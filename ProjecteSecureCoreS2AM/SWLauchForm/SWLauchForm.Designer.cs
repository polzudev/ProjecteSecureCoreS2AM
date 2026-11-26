
namespace ProjecteSecureCoreS2AM
{
    partial class SWLauchForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlBase = new System.Windows.Forms.Panel();
            this.picIcono = new System.Windows.Forms.PictureBox();
            this.lblTexto = new System.Windows.Forms.Label();
            this.lblClave = new System.Windows.Forms.Label();
            this.pnlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcono)).BeginInit();
            this.SuspendLayout();
            //
            // pnlBase
            //
            this.pnlBase.BackColor = System.Drawing.Color.Transparent;
            this.pnlBase.Controls.Add(this.picIcono);
            this.pnlBase.Controls.Add(this.lblTexto);
            this.pnlBase.Controls.Add(this.lblClave);
            this.pnlBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBase.Location = new System.Drawing.Point(0, 0);
            this.pnlBase.Name = "pnlBase";
            this.pnlBase.Padding = new System.Windows.Forms.Padding(8, 10, 8, 6);
            this.pnlBase.Size = new System.Drawing.Size(190, 145);
            this.pnlBase.TabIndex = 0;
            this.pnlBase.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBase_Paint);
            //
            // picIcono
            //
            this.picIcono.BackColor = System.Drawing.Color.Transparent;
            this.picIcono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picIcono.Location = new System.Drawing.Point(8, 10);
            this.picIcono.Name = "picIcono";
            this.picIcono.Size = new System.Drawing.Size(174, 103);
            this.picIcono.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIcono.TabIndex = 0;
            this.picIcono.TabStop = false;
            //
            // lblTexto
            //
            this.lblTexto.BackColor = System.Drawing.Color.Transparent;
            this.lblTexto.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTexto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTexto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(241)))), ((int)(((byte)(248)))));
            this.lblTexto.Location = new System.Drawing.Point(8, 113);
            this.lblTexto.Name = "lblTexto";
            this.lblTexto.Size = new System.Drawing.Size(174, 26);
            this.lblTexto.TabIndex = 1;
            this.lblTexto.Text = "Opció";
            this.lblTexto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblClave
            //
            this.lblClave.BackColor = System.Drawing.Color.Transparent;
            this.lblClave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblClave.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.lblClave.Location = new System.Drawing.Point(8, 119);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(174, 20);
            this.lblClave.TabIndex = 2;
            this.lblClave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblClave.Visible = false;
            //
            // SWLauchForm
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            this.Controls.Add(this.pnlBase);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(10);
            this.Name = "SWLauchForm";
            this.Size = new System.Drawing.Size(190, 145);
            this.pnlBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picIcono)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBase;
        private System.Windows.Forms.PictureBox picIcono;
        private System.Windows.Forms.Label lblTexto;
        private System.Windows.Forms.Label lblClave;
    }
}
