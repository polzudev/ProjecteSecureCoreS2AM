
namespace ProjecteSecureCoreS2AM
{
    partial class frmPrincipal
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mnuPrincipal = new System.Windows.Forms.MenuStrip();
            this.mnuSecureCore = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInfoUsuario = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSeparador = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCerrarSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpciones = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlCabecera = new System.Windows.Forms.Panel();
            this.lblRango = new System.Windows.Forms.Label();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblSeccion = new System.Windows.Forms.Label();
            this.flpOpciones = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlPie = new System.Windows.Forms.Panel();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblHora = new System.Windows.Forms.Label();
            this.tmrReloj = new System.Windows.Forms.Timer(this.components);
            this.mnuPrincipal.SuspendLayout();
            this.pnlCabecera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlPie.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuPrincipal
            // 
            this.mnuPrincipal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            this.mnuPrincipal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuPrincipal.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSecureCore,
            this.mnuOpciones});
            this.mnuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.mnuPrincipal.Name = "mnuPrincipal";
            this.mnuPrincipal.Size = new System.Drawing.Size(1100, 31);
            this.mnuPrincipal.TabIndex = 0;
            // 
            // mnuSecureCore
            // 
            this.mnuSecureCore.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInfoUsuario,
            this.mnuSeparador,
            this.mnuCerrarSesion,
            this.mnuSalir});
            this.mnuSecureCore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(194)))), ((int)(((byte)(48)))));
            this.mnuSecureCore.Name = "mnuSecureCore";
            this.mnuSecureCore.Size = new System.Drawing.Size(116, 27);
            this.mnuSecureCore.Text = "Secure Core";
            // 
            // mnuInfoUsuario
            // 
            this.mnuInfoUsuario.Name = "mnuInfoUsuario";
            this.mnuInfoUsuario.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuInfoUsuario.Size = new System.Drawing.Size(286, 28);
            this.mnuInfoUsuario.Text = "Informació de l\'usuari";
            this.mnuInfoUsuario.Click += new System.EventHandler(this.mnuInfoUsuario_Click);
            // 
            // mnuSeparador
            // 
            this.mnuSeparador.Name = "mnuSeparador";
            this.mnuSeparador.Size = new System.Drawing.Size(283, 6);
            // 
            // mnuCerrarSesion
            // 
            this.mnuCerrarSesion.Name = "mnuCerrarSesion";
            this.mnuCerrarSesion.Size = new System.Drawing.Size(286, 28);
            this.mnuCerrarSesion.Text = "Tancar sessió";
            this.mnuCerrarSesion.Click += new System.EventHandler(this.mnuCerrarSesion_Click);
            // 
            // mnuSalir
            // 
            this.mnuSalir.Name = "mnuSalir";
            this.mnuSalir.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mnuSalir.Size = new System.Drawing.Size(286, 28);
            this.mnuSalir.Text = "Sortir";
            this.mnuSalir.Click += new System.EventHandler(this.mnuSalir_Click);
            // 
            // mnuOpciones
            // 
            this.mnuOpciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(241)))), ((int)(((byte)(248)))));
            this.mnuOpciones.Name = "mnuOpciones";
            this.mnuOpciones.Size = new System.Drawing.Size(86, 27);
            this.mnuOpciones.Text = "Opcions";
            // 
            // pnlCabecera
            // 
            this.pnlCabecera.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(33)))), ((int)(((byte)(41)))));
            this.pnlCabecera.Controls.Add(this.lblRango);
            this.pnlCabecera.Controls.Add(this.lblCategoria);
            this.pnlCabecera.Controls.Add(this.lblUsuario);
            this.pnlCabecera.Controls.Add(this.lblSubtitulo);
            this.pnlCabecera.Controls.Add(this.lblTitulo);
            this.pnlCabecera.Controls.Add(this.picLogo);
            this.pnlCabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCabecera.Location = new System.Drawing.Point(0, 31);
            this.pnlCabecera.Name = "pnlCabecera";
            this.pnlCabecera.Size = new System.Drawing.Size(1100, 110);
            this.pnlCabecera.TabIndex = 1;
            this.pnlCabecera.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCabecera_Paint);
            // 
            // lblRango
            // 
            this.lblRango.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRango.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRango.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.lblRango.Location = new System.Drawing.Point(660, 74);
            this.lblRango.Name = "lblRango";
            this.lblRango.Size = new System.Drawing.Size(420, 22);
            this.lblRango.TabIndex = 5;
            this.lblRango.Text = "Rang";
            this.lblRango.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCategoria
            // 
            this.lblCategoria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCategoria.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(194)))), ((int)(((byte)(48)))));
            this.lblCategoria.Location = new System.Drawing.Point(660, 50);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(420, 22);
            this.lblCategoria.TabIndex = 4;
            this.lblCategoria.Text = "Categoria · Nivell d\'accés";
            this.lblCategoria.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(241)))), ((int)(((byte)(248)))));
            this.lblUsuario.Location = new System.Drawing.Point(660, 14);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(420, 34);
            this.lblUsuario.TabIndex = 3;
            this.lblUsuario.Text = "Usuari";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(184, 64);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(217, 20);
            this.lblSubtitulo.TabIndex = 2;
            this.lblSubtitulo.Text = "Rebel Alliance System Upgrade";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(194)))), ((int)(((byte)(48)))));
            this.lblTitulo.Location = new System.Drawing.Point(178, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(262, 50);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "SECURE CORE";
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Image = global::ProjecteSecureCoreS2AM.Properties.Resources.logo_CommitJedi_fondo_transparente;
            this.picLogo.Location = new System.Drawing.Point(16, 8);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(156, 94);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // lblSeccion
            // 
            this.lblSeccion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSeccion.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(194)))), ((int)(((byte)(48)))));
            this.lblSeccion.Location = new System.Drawing.Point(0, 141);
            this.lblSeccion.Name = "lblSeccion";
            this.lblSeccion.Padding = new System.Windows.Forms.Padding(26, 0, 0, 0);
            this.lblSeccion.Size = new System.Drawing.Size(1100, 50);
            this.lblSeccion.TabIndex = 2;
            this.lblSeccion.Text = "OPCIONS DISPONIBLES";
            this.lblSeccion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSeccion.Click += new System.EventHandler(this.lblSeccion_Click);
            // 
            // flpOpciones
            // 
            this.flpOpciones.AutoScroll = true;
            this.flpOpciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpOpciones.Location = new System.Drawing.Point(0, 191);
            this.flpOpciones.Name = "flpOpciones";
            this.flpOpciones.Padding = new System.Windows.Forms.Padding(16, 0, 16, 16);
            this.flpOpciones.Size = new System.Drawing.Size(1100, 475);
            this.flpOpciones.TabIndex = 3;
            // 
            // pnlPie
            // 
            this.pnlPie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            this.pnlPie.Controls.Add(this.lblEstado);
            this.pnlPie.Controls.Add(this.lblHora);
            this.pnlPie.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPie.Location = new System.Drawing.Point(0, 666);
            this.pnlPie.Name = "pnlPie";
            this.pnlPie.Size = new System.Drawing.Size(1100, 34);
            this.pnlPie.TabIndex = 4;
            // 
            // lblEstado
            // 
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.lblEstado.Location = new System.Drawing.Point(0, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.lblEstado.Size = new System.Drawing.Size(900, 34);
            this.lblEstado.TabIndex = 0;
            this.lblEstado.Text = "Sessió iniciada";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHora
            // 
            this.lblHora.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblHora.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHora.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(194)))), ((int)(((byte)(48)))));
            this.lblHora.Location = new System.Drawing.Point(900, 0);
            this.lblHora.Name = "lblHora";
            this.lblHora.Padding = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.lblHora.Size = new System.Drawing.Size(200, 34);
            this.lblHora.TabIndex = 1;
            this.lblHora.Text = "00:00:00";
            this.lblHora.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmrReloj
            // 
            this.tmrReloj.Interval = 1000;
            this.tmrReloj.Tick += new System.EventHandler(this.tmrReloj_Tick);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(27)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.flpOpciones);
            this.Controls.Add(this.lblSeccion);
            this.Controls.Add(this.pnlPie);
            this.Controls.Add(this.pnlCabecera);
            this.Controls.Add(this.mnuPrincipal);
            this.MainMenuStrip = this.mnuPrincipal;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Secure Core";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPrincipal_FormClosed);
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.mnuPrincipal.ResumeLayout(false);
            this.mnuPrincipal.PerformLayout();
            this.pnlCabecera.ResumeLayout(false);
            this.pnlCabecera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlPie.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem mnuSecureCore;
        private System.Windows.Forms.ToolStripMenuItem mnuInfoUsuario;
        private System.Windows.Forms.ToolStripSeparator mnuSeparador;
        private System.Windows.Forms.ToolStripMenuItem mnuCerrarSesion;
        private System.Windows.Forms.ToolStripMenuItem mnuSalir;
        private System.Windows.Forms.ToolStripMenuItem mnuOpciones;
        private System.Windows.Forms.Panel pnlCabecera;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Label lblRango;
        private System.Windows.Forms.Label lblSeccion;
        private System.Windows.Forms.FlowLayoutPanel flpOpciones;
        private System.Windows.Forms.Panel pnlPie;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Timer tmrReloj;
    }
}
