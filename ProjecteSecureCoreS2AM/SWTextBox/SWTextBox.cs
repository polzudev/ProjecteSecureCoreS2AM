using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjecteSecureCoreS2AM
{
    /// <summary>
    /// TextBox personalitzat per a Secure Core que incorpora metadades de la base de dades,
    /// validació de format i canvi de color quan rep el focus.
    /// </summary>
    public class SWTextBox : TextBox
    {
        private const string PatronNumero = @"^[0-9]+([.,][0-9]+)?$";
        private const string PatronCodigo = @"^[AEIOU][A-Z]{3}-[0-9]{2}[13579]$";

        private Color colorOriginal;

        /// <summary>Tipus de dada que valida el control: text lliure, número o codi SecureCore.</summary>
        [Category("Secure Core"), Description("Tipus de dada que conté: text, número o codi")]
        public TipoDato Tipo { get; set; }

        /// <summary>Nom de la columna de la base de dades a la qual s'enllaça el control per al DataBinding.</summary>
        [Category("Secure Core"), Description("Nom del camp de la base de dades al que s'enllaça el control")]
        public string CampoBD { get; set; }

        /// <summary>Indica si el camp pot quedar buit sense que la validació el marqui com a incorrecte.</summary>
        [Category("Secure Core"), Description("Indica si el camp es pot deixar buit")]
        public bool PuedeEstarVacio { get; set; }

        /// <summary>Indica que el camp és una clau forana; els formularis de manteniment l'exclouen de les insercions directes.</summary>
        [Category("Secure Core"), Description("Indica si el camp és una clau forana")]
        public bool EsClaveForanea { get; set; }

        /// <summary>Color de fons del control mentre té el focus de teclat.</summary>
        [Category("Secure Core"), Description("Color de fons mentre el control té el focus")]
        public Color ColorFoco { get; set; } = Color.FromArgb(45, 62, 76);

        /// <inheritdoc/>
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            colorOriginal = BackColor;
            BackColor = ColorFoco;
        }

        /// <inheritdoc/>
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            BackColor = colorOriginal;
        }

        /// <summary>
        /// Valida el contingut del control segons el seu <see cref="Tipo"/> i la propietat
        /// <see cref="PuedeEstarVacio"/>.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el valor és vàlid; <c>false</c> si no compleix el format o és buit
        /// quan no ho permet.
        /// </returns>
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
