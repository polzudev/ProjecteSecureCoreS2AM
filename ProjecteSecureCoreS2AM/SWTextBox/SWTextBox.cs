using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjecteSecureCoreS2AM
{
    public class SWTextBox : TextBox
    {
        private const string PatronNumero = @"^[0-9]+([.,][0-9]+)?$";
        private const string PatronCodigo = @"^[AEIOU][A-Z]{3}-[0-9]{2}[13579]$";

        private Color colorOriginal;

        [Category("Secure Core"), Description("Tipus de dada que conté: text, número o codi")]
        public TipoDato Tipo { get; set; }

        [Category("Secure Core"), Description("Nom del camp de la base de dades al que s'enllaça el control")]
        public string CampoBD { get; set; }

        [Category("Secure Core"), Description("Indica si el camp es pot deixar buit")]
        public bool PuedeEstarVacio { get; set; }

        [Category("Secure Core"), Description("Indica si el camp és una clau forana")]
        public bool EsClaveForanea { get; set; }

        [Category("Secure Core"), Description("Color de fons mentre el control té el focus")]
        public Color ColorFoco { get; set; } = Color.FromArgb(45, 62, 76);

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            colorOriginal = BackColor;
            BackColor = ColorFoco;
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            BackColor = colorOriginal;
        }

        public bool ValidateData()
        {
            bool correcto;
            if (Text == "")
            {
                correcto = PuedeEstarVacio;
            }
            else if (Tipo == TipoDato.Numero)
            {
                correcto = Regex.IsMatch(Text, PatronNumero);
            }
            else if (Tipo == TipoDato.Codigo)
            {
                correcto = Regex.IsMatch(Text, PatronCodigo);
            }
            else
            {
                correcto = true;
            }
            return correcto;
        }
    }
}
