namespace NewCenturyTest.Models.Q5Models
{
    public class Jogador
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ContadorVitorias { get; set; }
        public int ContadorDerrotas { get; set; }


        public Jogador() { }
        public Jogador(string name)
        {
            Name = name;
            ContadorVitorias = 0;
            ContadorDerrotas = 0;
        }
        
        public void addVitoria()
        {
            ContadorVitorias ++;
        }
        public void addDerrota()
        {
            ContadorDerrotas ++;
        }
    }
}
