using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NewCenturyTest.Models;
using NewCenturyTest.Models.Q5Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Reflection;

namespace NewCenturyTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _dataFilePath;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "HistoricoTentativas.json");
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Q1()
        {
            return View();
        }
        public IActionResult Q2()
        {
            return View();
        }
        public IActionResult Q3()
        {

            List<VendedorModel> vendedores = VendedoresData();
            int[] intervalos = CalcularIntervalos(vendedores);
            string[] dados = new string[intervalos.Length];

            for (int i = 0; i < intervalos.Length; i++)
            {
                dados[i] = $"{classificacao(i)}: {intervalos[i]}";
            }

            ViewBag.Intervalos = dados;

            return View();

        }

        private string classificacao(int index)
        {
            switch (index)
            {
                case 0:
                    return "$200 - $299";
                case 1:
                    return "$300 - $399";
                case 2:
                    return "$400 - $499";
                case 3:
                    return "$500 - $599";
                case 4:
                    return "$600 - $699";
                case 5:
                    return "$700 - $799";
                case 6:
                    return "$800 - $899";
                case 7:
                    return "$900 - $999";
                case 8:
                    return "$1000 em diante";
                default:
                    return "Intervalo Inválido";
            }
        }

        private List<VendedorModel> VendedoresData()
        {
            
            return new List<VendedorModel>
            {
                new VendedorModel { nome = "Joana", salario = 200, vendas = new List<VendasModel> { new VendasModel { codVenda = "1", valorVenda = 3000 } } },
                new VendedorModel { nome = "Miranda", salario = 200, vendas = new List<VendasModel> { new VendasModel { codVenda = "2", valorVenda = 500 } } },
                new VendedorModel { nome= "Jonas", salario = 200, vendas = new List<VendasModel> { new VendasModel { codVenda = "3", valorVenda = 6000 } } },
                new VendedorModel { nome = "Carla", salario = 200, vendas = new List<VendasModel> { new VendasModel { codVenda = "4", valorVenda = 7000 } } },
                new VendedorModel { nome = "Marcos", salario = 200, vendas = new List<VendasModel> { new VendasModel { codVenda = "5", valorVenda = 2000 } } },
                new VendedorModel { nome= "Bruna", salario = 200, vendas = new List<VendasModel> { new VendasModel { codVenda = "6", valorVenda = 2500 } } },
                new VendedorModel { nome = "Fernanda", salario = 200, vendas = new List<VendasModel> { new VendasModel { codVenda = "7", valorVenda = 3500 } } },
                new VendedorModel { nome = "Franco", salario = 200, vendas = new List<VendasModel> { new VendasModel { codVenda = "8", valorVenda = 8877 } } },
                new VendedorModel { nome= "Jorge", salario = 200, vendas = new List<VendasModel> { new VendasModel { codVenda = "9", valorVenda = 15000 } } },
                new VendedorModel { nome = "João", salario = 200, vendas = new List<VendasModel> { new VendasModel { codVenda = "10", valorVenda = 30000 } } },
                new VendedorModel { nome = "Bryan", salario = 200, vendas = new List<VendasModel> { new VendasModel { codVenda = "8", valorVenda = 4500 } } },
                new VendedorModel { nome= "Marcio", salario = 200, vendas = new List<VendasModel> { new VendasModel { codVenda = "9", valorVenda = 6777 } } },
                new VendedorModel { nome = "Iglecias", salario = 200, vendas = new List<VendasModel> { new VendasModel { codVenda = "10", valorVenda = 5500 } } }



            };
        }
        private int[] CalcularIntervalos(List<VendedorModel> vendedores)
        {
            int[] intervalos = new int[9]; 

            foreach (var vendedor in vendedores)
            {
                double salarioTotal = vendedor.salario + (0.09 * vendedor.vendas.Sum(v => v.valorVenda));
                int index = (int)(salarioTotal / 100) - 2; 

                
                if (index >= 0 && index < intervalos.Length)
                {
                    intervalos[index]++;
                }
                else
                {
                    intervalos[intervalos.Length - 1]++; 
                }
            }

            return intervalos;
        }
        public IActionResult Q4()
        {
            return View();
        }
        public IActionResult Q5()
        {
            List<Jogador> modelo = GerarListaAleatoriaJogadores(10);
            try
            {
                var dados = LerDados();
                dados.AddRange(modelo);

                SalvarDados(dados);

                //return Ok("Dados adicionados com sucesso");
                return View();
            }
            catch (Exception ex)
            {
                //return StatusCode(500, $"Erro ao adicionar dados: {ex.Message}");
                return View();
            }
            //return View();
        }

        private List<Jogador> LerDados()
        {
            try
            {
                if (!System.IO.File.Exists(_dataFilePath))
                {
                    return new List<Jogador>();
                }

                var json = System.IO.File.ReadAllText(_dataFilePath);
                return JsonConvert.DeserializeObject<List<Jogador>>(json);
            }
            catch (Exception ex)
            {
                // Log do erro para diagnóstico
                Console.WriteLine($"Erro ao ler dados do arquivo JSON: {ex}");
                throw; // Re-throw para sinalizar que ocorreu um erro
            }
        }

        private void SalvarDados(List<Jogador> dados)
        {
            try {
                var json = JsonConvert.SerializeObject(dados, Formatting.Indented);
                var linhas = new[] { json }; // Coloca o JSON em um array de uma única linha
                System.IO.File.WriteAllLines(_dataFilePath, linhas);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erro ao ler dados do arquivo JSON: {ex}");
                throw; // Re-throw para sinalizar que ocorreu um erro
            }
        }

        static List<Jogador> GerarListaAleatoriaJogadores(int quantidade)
        {
            var nomes = new[] { "João", "Maria", "Pedro", "Ana", "José", "Carla", "Lucas", "Fernanda", "Marcos", "Amanda" };
            var rand = new Random();
            var listaJogadores = new List<Jogador>();

            for (int i = 1; i <= quantidade; i++)
            {
                var nomeAleatorio = nomes[rand.Next(nomes.Length)];
                var jogador = new Jogador
                {
                    Id = i,
                    Name = nomeAleatorio,
                    ContadorVitorias = rand.Next(0, 11), // Vitórias aleatórias entre 0 e 10
                    ContadorDerrotas = rand.Next(0, 11) // Derrotas aleatórias entre 0 e 10
                };
                listaJogadores.Add(jogador);
            }

            return listaJogadores;
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region POST

        [HttpPost]
        public IActionResult CupomFiscal(string tipo, double quantidade,string tipoPagamento, bool cartaoTabajara)
        {
            CupomFiscalModel model = new CupomFiscalModel();
            double valorDesconto = 0;
            if(cartaoTabajara == true)
            {
                valorDesconto = (RetornaPreco(tipo, quantidade)) * 0.05;
            }
            
            model.TipoCarne = tipo;
            model.Quantidade = quantidade;
            model.PrecoTotal = Math.Round(RetornaPreco(tipo, quantidade), 2);
            model.TipoPagamento = tipoPagamento;
            model.ValorDesconto = Math.Round(valorDesconto, 2);
            model.ValorPago = Math.Round(RetornaPreco(tipo, quantidade) - valorDesconto, 2);

            return View(model);
        }

        private double RetornaPreco(string tipo, double quantidade)
        {
            double precoTotal = 0;
            double carneExtra = 0;
            double carne = 0; 

            if(quantidade > 5)
            {
                carneExtra = quantidade -5;
                carne = 5;
            }
            else
            {
                carne = quantidade;
            }
            switch (tipo)
            {
                case "FileDuplo":
                    return carne * 4.9 + carneExtra * 5.8;
                case "Alcatra":
                    return carne * 5.9 + carneExtra * 6.8;
                case "Picanha":
                    return carne * 6.9 + carneExtra * 7.8;
                default:
                    return 10000000000000; // valor que entendiram que existe um erro. 
            }
        }

        #endregion
    }
}
