namespace ProjecteSecureCoreS2AM
{
    /// <summary>
    /// Especifica la validació que aplica un <see cref="SWTextBox"/> al seu contingut.
    /// </summary>
    public enum TipoDato
    {
        /// <summary>Accepta qualsevol cadena de text; no s'aplica cap patró.</summary>
        Texto,
        /// <summary>Requereix un valor numèric enters o decimal (separador punt o coma).</summary>
        Numero,
        /// <summary>Requereix el format de codi SecureCore: vocal + 3 majúscules + guió + 2 xifres + dígit imparell.</summary>
        Codigo
    }
}
