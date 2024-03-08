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
        private int NumeroDoJogo = 0;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //Para ver o histórico sendo alterado, você pode alterar pelo arquivo no seu endereço de máquina como exemplo abaixo; 
            // _dataFilePath = @"C:\Users\User\Desktop\TestNewCentury\NewCenturyTest\NewCenturyTest\Data\HistoricoTentativas.json";
            _dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "DATA", "HistoricoTentativas.json");
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
        public IActionResult Q5Create(string CodJogador, string NivelDoJogo, int NumTentativa, int NumeroDigitado, int NumeroDoJogo, int NumeroAleatorio)
        {
            try{

                //Verifica se o número e tentativas de jogo são permitidos, se não reseta para um novo jogo
                var resultado = (NumeroDigitado == NumeroAleatorio) == true ? Resultado.SUCCESS : Resultado.WRONG;
                var dados = LerDados();
                //Adiciona o novo jogo ao banco de dados; 
                var novoHistorico = new HistoricoTentativas
                {
                        CodJogador = CodJogador,
                        NivelDoJogo = NivelDoJogo,
                        NumTentativa = NumTentativa,
                        NumeroDigitado = NumeroDigitado,
                        NumeroDoJogo = NumeroDoJogo,
                        DataHoraTentativa = DateTime.Now,
                        Resultado = resultado,
                        NumeroAleatorio = NumeroAleatorio
                };
                //Adicionando a nova tentativa no histórico; 
                dados.Add(novoHistorico);
                var ultimoJogo = dados.OrderByDescending(d => d.DataHoraTentativa).FirstOrDefault();
                var controleUltimoJogo = ultimoJogo;
                //if(ultimoJogo.Resultado == Resultado.SUCCESS)
                //{
                //    ultimoJogo.NumeroDoJogo++;
                //}
                //Pegando o último jogo;
                ViewBag.UltimoJogo = ultimoJogo.NumeroDoJogo;

                //Numero de tentativas
                var tentativas = dados.Where(d => d.NumeroDoJogo == NumeroDoJogo && d.CodJogador == CodJogador);
               
                //Garante que o jogo começa no 0 se necessário; 
                if (ultimoJogo.NumeroDoJogo == controleUltimoJogo.NumeroDoJogo)
                {
                    ViewBag.Num_Tentativas = tentativas.Count();
                }
                else
                {
                    ViewBag.Num_Tentativas = 0;
                }

                //Numeros jogados
                var numerosJogados = dados
                .Where(d => d.NumeroDoJogo == NumeroDoJogo)
                .Select(d => d.NumeroDigitado)
                .ToList();
                ViewBag.NumerosJogados = numerosJogados;

                //Numero de Nivel do jogo            
                ViewBag.NivelDoJogo = NivelDoJogo;

                // Salvo os novos dados no banco de dados;
                SalvarDadosJogador(dados);

            return RedirectToAction("Q5");
            }catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar dados: {ex.Message}");
            }          
        }
              
        public IActionResult Q5()
        {
            var dados = LerDados();
            
            var ultimoJogo = dados.OrderByDescending(d => d.DataHoraTentativa).FirstOrDefault();
            var controleUltimoJogo = ultimoJogo.NumeroDoJogo;
            if (ultimoJogo.Resultado == Resultado.SUCCESS)
            {
                ultimoJogo.NumeroDoJogo++;
            }
            // Pega o último jogo para colocar na página; 
            ViewBag.UltimoJogo = ultimoJogo.NumeroDoJogo;
            //Numero de tentativas
            var tentativas = dados.Where(d => d.NumeroDoJogo == ultimoJogo.NumeroDoJogo && d.CodJogador == ultimoJogo.CodJogador);
            //Garante que as tentativas novas vão ser iniciadas do 0; 
            if(ultimoJogo.NumeroDoJogo == controleUltimoJogo)
            {
                ViewBag.Num_Tentativas = tentativas.Count();
            }
            else
            {
                ViewBag.Num_Tentativas = 0;
            }
            

            //Garantindo o array para verificar quantos numeros foram jogados
            var numerosJogados = dados
            .Where(d => d.NumeroDoJogo == ultimoJogo.NumeroDoJogo)
            .Select(d => d.NumeroDigitado)
            .ToList();
            ViewBag.NumerosJogados = numerosJogados;

            //Numero de Nivel do jogo            
            ViewBag.NivelDoJogo = ultimoJogo.NivelDoJogo;

            
            return View();
        }

        //Lê os dados que estão salvos no banco de tentativas; 
        private List<HistoricoTentativas> LerDados()
        {
            try
            {
                if (!System.IO.File.Exists(_dataFilePath))
                {
                    return new List<HistoricoTentativas>();
                }

                var json = System.IO.File.ReadAllText(_dataFilePath);
                return JsonConvert.DeserializeObject<List<HistoricoTentativas>>(json);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Erro ao ler dados do arquivo JSON: {ex}");
                throw;
            }
        }

        // Salva os dados no banco de dados e garante que não tem duplicidade de eventos que já ocorreram.
        private void SalvarDadosJogador(List<HistoricoTentativas> dados)
        {
            try
            {
             
                var json = JsonConvert.SerializeObject(dados, Formatting.Indented);

                // Escreve os dados JSON no arquivo (substituindo o conteúdo existente)
                System.IO.File.WriteAllText(_dataFilePath, json);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Erro ao ler dados do arquivo JSON: {ex}");
                throw;
            }
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
