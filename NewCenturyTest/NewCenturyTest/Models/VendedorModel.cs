namespace NewCenturyTest.Models
{
    public class VendedorModel
    {
        public string nome { get; set; }
        public double salario { get; set; }
        public  List<VendasModel> vendas = new List<VendasModel>();
        


    }

   
}
