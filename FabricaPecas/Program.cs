using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;

namespace FabricaPecas {
    class Program {
        static void Main(string[] args) {


            int resp1, contador=0;
            string resp2, respDirect="...";
            bool erro=false;

            //area de add

            string comentario, nome;
            int numeroserie;
            double preco;

            //-------------

            

            do {

                Console.Clear();

                Console.WriteLine("--- Painel de Ferramentas da Fabrica XL ---");
                Console.WriteLine("- [1] - Carregar Ferramentas -");
                Console.WriteLine("- [2] - Adicionar Ferramenta -");
                Console.WriteLine("- [3] - Visualizar todas as Ferramentas -");
                Console.WriteLine("- [4] - Mostrar detalhes da Ferramenta -");
                Console.WriteLine("- [5] - Salvar Ferramentas -");
                Console.Write("Digite o que deseja fazer:");
                resp1 = Convert.ToInt32(Console.ReadLine());

                switch(resp1) {
                    case 1:

                        Console.Clear();

                        Console.WriteLine("-- Abrir Dados -- [Ultimo nome usado:"+respDirect+"] --");
                        Console.Write("Digite o nome do arquivo:");
                        string resp = Console.ReadLine();
                        resp += ".txt";

                        if(File.Exists(resp)) {

                            Console.WriteLine("Deseja manter apenas os dados do arquivo ou deseja somar com os já criados?");
                            Console.WriteLine("[1] - Manter apenas os dados do arquivo.");
                            Console.WriteLine("[2] - Somar com os já criados.");
                            Console.Write("Digite uma das opções:");
                            int opc = Convert.ToInt32(Console.ReadLine());                                                     

                            if(opc == 1) {
                                Banco.DeleteFerramentas();
                                Console.WriteLine("Ferramentas apagadas da memória.");
                            }else if(opc == 2) {
                                Console.WriteLine("Dados somados com os da memória.");
                            } else {
                                Console.WriteLine("Opção inválida.");
                                erro = true;
                            }

                            StreamReader ler = new StreamReader(resp);
                            string[] txt = ler.ReadToEnd().Split('\n');
                            int qtd = System.IO.File.ReadAllLines(resp).Length;
                            qtd = qtd / 4;                           

                            if(erro == false) {

                                int i = 0;

                                for(int x = 0;x < qtd;x++) {

                                    Ferramenta Ferramentas = new Ferramenta();

                                    Ferramentas.SetNome(txt[i]);
                                    i++;
                                    Ferramentas.SetNumeroSerie(Convert.ToInt32(txt[i]));
                                    i++;
                                    Ferramentas.SetPreco(Convert.ToInt32(txt[i]));
                                    i++;
                                    Ferramentas.SetComentario(txt[i]);
                                    i++;

                                    Banco.SetFerramentas(Ferramentas);
                                }
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Dados carregados com sucesso.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            
                            ler.Close();

                        } else {
                            Console.WriteLine("Arquivo inexistente.");
                        }

                        break;
                    case 2:

                        Console.Clear();

                        Ferramenta fr = new Ferramenta();

                        Console.WriteLine("-- Adicionar Ferramentas --");

                        Console.Write("Digite o nome da ferramenta:");
                        nome = Console.ReadLine();

                        Console.Write("Digite o número de série da ferramenta:");
                        numeroserie = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Digite um comentário sobre a ferramenta:");
                        comentario = Console.ReadLine();

                        Console.Write("Digite o preço da ferramenta:");
                        preco = Convert.ToDouble(Console.ReadLine());

                        fr.SetNome(nome);
                        fr.SetNumeroSerie(numeroserie);
                        fr.SetPreco(preco);
                        fr.SetComentario(comentario);

                        Banco.SetFerramentas(fr);

                        contador++;

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Dados adicionados com sucesso!");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case 3:

                        List<Ferramenta> fr2 = Banco.GetFerramentas();

                        for(int x = 0;x < fr2.Count;x++) {

                            Console.WriteLine("----------------------");
                            Console.WriteLine();
                            Console.WriteLine("Nome:" + fr2[x].GetNome());
                            Console.WriteLine();
                            Console.WriteLine("----------------------");
                        }
                        break;
                    case 4:

                        Console.Clear();

                        List<Ferramenta> fr3 = Banco.GetFerramentas();

                        for(int x = 0;x < fr3.Count;x++) {

                            Console.WriteLine("----------------------");
                            Console.WriteLine();
                            Console.WriteLine("Nome:" + fr3[x].GetNome());
                            Console.WriteLine();
                            Console.WriteLine("Numero de Série:" + fr3[x].GetNumeroSerie());
                            Console.WriteLine();
                            Console.WriteLine("Preço:" + fr3[x].GetPreco());
                            Console.WriteLine();
                            Console.WriteLine("Comentário:" + fr3[x].GetComentario());
                            Console.WriteLine();
                            Console.WriteLine("----------------------");
                        }
                        break;
                    case 5:

                        Console.Clear();

                        List<Ferramenta> fr4 = Banco.GetFerramentas();
                        
                        Console.Write("Digite um nome para seu arquivo de texto:");
                        respDirect = Console.ReadLine();
                        respDirect += ".txt";

                        StreamWriter escrever = new StreamWriter(respDirect);

                        for(int x = 0;x < fr4.Count;x++) {
                            escrever.WriteLine(fr4[x].GetNome());
                            escrever.WriteLine(fr4[x].GetNumeroSerie());
                            escrever.WriteLine(fr4[x].GetPreco());
                            escrever.WriteLine(fr4[x].GetComentario());
                        }

                        escrever.Close();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Dados salvos com sucesso!");
                        Console.ForegroundColor = ConsoleColor.White;

                        break;
                }
                Console.Write("Deseja continuar? S/N: ");
                resp2 = Console.ReadLine();
            } while(resp2 != "N");

            Console.ReadLine();
        }
    }
    class Ferramenta {

        private int _NumeroSerie;
        private double _Preco;
        private string _Nome;
        private string _Comentario;

        public void SetNumeroSerie(int num) {
            _NumeroSerie = num;
        }
        public void SetPreco(double preco) {
            _Preco = preco;
        }
        public void SetNome(string nome) {
            _Nome = nome;
        }
        public void SetComentario(string comentario) {
            _Comentario = comentario;
        }
        public int GetNumeroSerie() {
            return _NumeroSerie;
        }
        public double GetPreco() {
            return _Preco;
        }
        public string GetNome() {
            return _Nome;
        }
        public string GetComentario() {
            return _Comentario;
        }
    }

    class Banco {
        private static List<Ferramenta> Ferramentas = new List<Ferramenta>();
        public static void SetFerramentas(Ferramenta fr) {
            Ferramentas.Add(fr);
        }
        public static List<Ferramenta> GetFerramentas() {
            return Ferramentas;
        }
        public static void DeleteFerramentas() {
            Ferramentas.Clear();
        }
    }
}
