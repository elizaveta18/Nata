using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1Класс
{
    public class Class1
    {
        public int[,] ReadMassive(string path, out int n, out int m)
        {
            List<string[]> temp_list_massive = new List<string[]>();
            try
            {
                StreamReader sr = new StreamReader(path);
                while (true)
                {
                    string line = sr.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    temp_list_massive.Add(line.Split(';'));
                }
            }
            catch
            { }
            n = Convert.ToInt32(temp_list_massive.Count);
            m = Convert.ToInt32(temp_list_massive[0].Length);
            int[,] massive = new int[Convert.ToInt32(temp_list_massive.Count), Convert.ToInt32(temp_list_massive[0].Length)];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < Convert.ToInt32(temp_list_massive[0].Length); j++)
                {
                    massive[i, j] = Convert.ToInt32(temp_list_massive[i][j]);
                }
            }
            return massive;
        }
        public void WriteMassive(string path, string line)
        {
            StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);
            sw.Write(line);
            sw.Close();
        }
        public int[,] Jonson(string in_path, string out_path)
        {
            int n; //Количество станков
            int m; //Количество деталей

            int[,] massive = ReadMassive(in_path, out n, out m);
            int k = m; //Количество деталей которые нужно обработать

            List<List<int>> _1st_list = new List<List<int>>();
            List<List<int>> _2st_list = new List<List<int>>();

            _1st_list.Add(new List<int>());
            _1st_list.Add(new List<int>());
            _2st_list.Add(new List<int>());
            _2st_list.Add(new List<int>());

            for (int q = 0; q < k; q++)
            {
                int tmini = 0;
                int tminj = q;
                int min = massive[tmini, tminj];
                for (int i = 0; i < n; i++)
                {
                    for (int j = q; j < m; j++)
                    {
                        if (min > massive[i, j])
                        {
                            tmini = i;
                            tminj = j;
                            min = massive[i, j];
                        }
                    }
                }
                int _1st_temp = massive[0, q];
                int _2st_temp = massive[1, q];
                massive[0, q] = massive[0, tminj];
                massive[1, q] = massive[1, tminj];
                massive[0, tminj] = _1st_temp;
                massive[1, tminj] = _2st_temp;
                if (tmini == 0)
                {
                    _1st_list[0].Add(massive[0, q]);
                    _1st_list[1].Add(massive[1, q]);
                }
                else
                {
                    _2st_list[0].Add(massive[0, q]);
                    _2st_list[1].Add(massive[1, q]);
                }
            }
            _2st_list[0].Reverse();
            _2st_list[1].Reverse();
            string line = "";
            List<int> _1st_list_find = new List<int>();
            List<int> _2st_list_find = new List<int>();

            foreach (int a in _1st_list[0])
            {
                line = line + a + "; ";
                _1st_list_find.Add(a);
            }

            foreach (int a in _2st_list[0])
            {
                line = line + a + "; ";
                _1st_list_find.Add(a);
            }
            line = line + "\n";

            foreach (int a in _1st_list[1])
            {
                line = line + a + "; ";
                _2st_list_find.Add(a);
            }
            foreach (int a in _2st_list[1])
            {
                line = line + a + "; ";
                _2st_list_find.Add(a);
            }
            Console.WriteLine(line);
            WriteMassive(out_path, line);

            // Нахождение простоя
            int[] prost = new int[10];
            int _s1 = _1st_list_find[0];
            int _s2 = -_2st_list_find[0];
            for (int i = 0; i < m; i++)
            {
                if (i == 0)
                {
                    prost[i] = _s1;
                    continue;
                }
                _s1 = _s1 + _1st_list_find[i];
                if (i == 1)
                {
                    prost[i] = _s1 + _s2;
                    continue;
                }
                _s2 = _s2 - _2st_list_find[i - 1];
                prost[i] = _s1 + _s2;
            }
            int max = prost[0];
            for (int i = 0; i < prost.Length; i++)
            {
                if (max < prost[i])
                {
                    max = prost[i];
                }
            }            
            Console.WriteLine("Простой: " + max);
            // Конец нахождения простоя
            WriteMassive(out_path, "Простой: " + max);
            return new int[1, 1];
        }
    }
}

