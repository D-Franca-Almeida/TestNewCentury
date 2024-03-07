namespace NewCenturyTest.Models
{
    public class CarneModel
    {   
        public string TipoDeCarne { get; set; }
        public double Preco {  get; set; }
        public double PrecoEspecial { get; set; }

        public CarneModel() { }
        public CarneModel(string tipoDeCarne, double preco, double precoEspecial)
        {
            TipoDeCarne = tipoDeCarne;
            Preco = preco;
            PrecoEspecial = precoEspecial;
        }
    }
}
