namespace NewCenturyTest.Models
{
    public class VendasModel
    {
        public string codVenda { get; set; }
        public double valorVenda { get; set; }
        public VendedorModel vendedor { get; set; }
    }
}