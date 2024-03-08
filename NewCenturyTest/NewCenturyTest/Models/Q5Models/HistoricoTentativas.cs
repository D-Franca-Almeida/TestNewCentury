namespace NewCenturyTest.Models.Q5Models
{
    public class HistoricoTentativas
    {
        public string? CodJogador { get; set; }
        public int? NumTentativa { get; set; }
        public int? NumeroDigitado { get; set; }
        public DateTime? DataHoraTentativa { get; set; }
        public Resultado? Resultado { get; set; }
        public string? NivelDoJogo {  get; set; }
        public int? NumeroDoJogo { get; set; }
        public int? NumeroAleatorio { get; set; }


        public HistoricoTentativas() { }
        public HistoricoTentativas(string codJogador, int numTentativa, int numeroDigitado, DateTime dataHoraTentativa, Resultado resultado, string? nivelDoJogo, int numeroDoJogo, int numeroAleatorio)
        {
            CodJogador = codJogador;
            NumTentativa = numTentativa;
            NumeroDigitado = numeroDigitado;
            DataHoraTentativa = dataHoraTentativa;
            Resultado = resultado;
            NivelDoJogo = nivelDoJogo;
            NumeroDoJogo = numeroDoJogo;
            NumeroAleatorio = numeroAleatorio;
        }       
    }
}
