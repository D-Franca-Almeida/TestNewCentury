namespace NewCenturyTest.Models
{
    public class CupomFiscalModel
    {
        public string TipoCarne { get; set; }
        public double Quantidade{ get; set; }
        public double PrecoTotal{ get; set; }
        public string TipoPagamento{ get; set; }
        public double ValorDesconto { get; set; }
        public double ValorPago {  get; set; }
        public CupomFiscalModel() { }

        public CupomFiscalModel(string tipoCarne, double quantidade, double precoTotal, string tipoPagamento, double valorDesconto, double valorPago)
        {
            TipoCarne = tipoCarne;
            Quantidade = quantidade;
            PrecoTotal = precoTotal;
            TipoPagamento = tipoPagamento;
            ValorDesconto = valorDesconto;
            ValorPago = valorPago;
        }
    }
}
