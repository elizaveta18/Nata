using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1Класс.Симплекс_метод;

namespace _1Класс
{
    class Program
    {
        static void Main(string[] args)
        {
            int otv = 1;
            while (otv != 0)
            {
                Console.WriteLine("Выберите задачу:");
                Console.WriteLine(" 1 - симплекс-метод; \n 2 - метод Джонсона; \n 3 - метод потенциалов; \n 4 - кратчайший путь (Коммивояжёра); \n 5 - кратчайший путь; \n 6 - кртический путь.");
                int nom = Convert.ToInt32(Console.ReadLine());
                if (nom == 1)
                {
                    Vvod vz = new Vvod();
                    vz.simplexBol();
                }
                else if (nom == 2)
                {    
                    Class1 c1 = new Class1();
                    c1.Jonson("jo.csv", "out.csv");
                    Console.Read();
                }
                else if (nom == 3)
                {
                    
                }
                else if (nom == 4)
                {
                    Сommivoyageur commivoyageur = new Сommivoyageur("Kom.csv", "VivodKom.txt");
                    commivoyageur.Calculate();
                }
                else if (nom == 5)
                {
                    //CrtPth Cp = new CrtPth("vvodKritic.csv");
                }
                else if (nom == 6)
                {
                    CrtPth kr = new CrtPth("vvodKritic.csv");                  
                }
                else
                {
                    Console.WriteLine("Неверный ввод!");
                }
                Console.WriteLine("Продолжить работу? да - 1, нет - 0");
                otv = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            }
            Console.ReadKey();
            //Class3.Kom();
            //Метод Джонсона          
            //Class1 c1 = new Class1();
            //c1.Jonson("jo.csv", "out.csv");
            //Console.Read();

            //Class2 c2 = new Class2();
            //c2.Poten("po.csv", "ou.csv");
            //Console.Read();

            //IEnumerable<string> result = File.ReadLines("po.csv").Skip(0).Take(1);
            //foreach (string str in result)
            //{
            //    Console.WriteLine(str);
            //}
            //IEnumerable<string> line = File.ReadLines("po.csv").Skip(1).Take(3);
            //string[,] file = new string[1000, 3];
            //using (StreamReader s = new StreamReader("po.csv"))
            //{
            //    int i = 0;
            //    string input;
            //    while ((input = s.ReadLine()) != null)
            //    {
            //        string[] m = input.Split(new char[] { ' ' });
            //        for (int k = 0, j = 0; k < m.Length; k++)
            //        {
            //            if (m[k].Trim() != "")
            //            {
            //                file[i, j] = m[k];
            //                j++;
            //                Console.WriteLine(file[i, j]);
            //            }
            //        }
            //        i++;
            //    }

            //}
            //{

            //    if (line == null)
            //    {

            //    }
            //    else { }
            //    Console.WriteLine(line);
            //}
            //if ( == ' ') break;
            //Console.ReadKey();

            //var g = new Graph();
            ////добавление вершин
            //g.AddVertex("A");
            //g.AddVertex("B");
            //g.AddVertex("C");
            //g.AddVertex("D");
            //g.AddVertex("E");
            //g.AddVertex("F");
            //g.AddVertex("G");
            ////добавление ребер
            //g.AddEdge("A", "B", 22);
            //g.AddEdge("A", "C", 33);
            //g.AddEdge("A", "D", 61);
            //g.AddEdge("B", "C", 47);
            //g.AddEdge("B", "E", 93);
            //g.AddEdge("C", "D", 11);
            //g.AddEdge("C", "E", 79);
            //g.AddEdge("C", "F", 63);
            //g.AddEdge("D", "F", 41);
            //g.AddEdge("E", "F", 17);
            //g.AddEdge("E", "G", 58);
            //g.AddEdge("F", "G", 84);
            //var dijkstra = new Dijkstra(g);
            //var path = dijkstra.FindShortestPath("A", "G");
            //Console.WriteLine(path);
            //Console.ReadLine();
        }
    }
}
//private static void Main(string[] args)
//{
//Метод Джонсона
//Model m = new Model();
//m.Jonson("mas.csv", "out.csv");
//Console.Read();
//Конец

//Коммивояжёра
//Class3 c3 = new Class3();
//Конец

//Критический путь
//var g = new Graph();
////добавление вершин
//g.AddVertex("A");
//g.AddVertex("B");
//g.AddVertex("C");
//g.AddVertex("D");
//g.AddVertex("E");
//g.AddVertex("F");
//g.AddVertex("G");
////добавление ребер
//g.AddEdge("A", "B", 22);
//g.AddEdge("A", "C", 33);
//g.AddEdge("A", "D", 61);
//g.AddEdge("B", "C", 47);
//g.AddEdge("B", "E", 93);
//g.AddEdge("C", "D", 11);
//g.AddEdge("C", "E", 79);
//g.AddEdge("C", "F", 63);
//g.AddEdge("D", "F", 41);
//g.AddEdge("E", "F", 17);
//g.AddEdge("E", "G", 58);
//g.AddEdge("F", "G", 84);
//var dijkstra = new Dijkstra(g);
//var path = dijkstra.FindShortestPath("A", "G");
//Console.WriteLine(path);
//Console.ReadLine();
//Конец

//Считывание
//    struct User
//{
//    public int i, a, b;
//    public void jo()
//    {
//        Console.WriteLine("{0} {1} {2}", i, a, b);
//    }
//}
//static void DisplayList(List<User> L)
//{
//    foreach (User u in L)
//    {
//        u.jo();
//    }
//}
//List<User> users = new List<User>();
//private void Metod(List<User> users)
//{
//    this.users = users;
//}
//public void Schit()
//{
//    List<User> users = new List<User>();
//    using (StreamReader sr = new StreamReader("jo.csv"))
//    {
//        while (sr.EndOfStream != true)
//        {
//            string[] Arr = sr.ReadLine().Split(';');
//            users.Add(new User() { i = Convert.ToInt32(Arr[0]), a = Convert.ToInt32(Arr[1]), b = Convert.ToInt32(Arr[2]) });
//        }
//    }
//    DisplayList(users);
//    //Class4 c4 = new Class4();
//    //c4.DijkstraAlgo(graph, 0, 9);
//}
//Конец
//}

