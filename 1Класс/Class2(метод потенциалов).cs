using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1Класс
{
    class Class2
    {
        public static int[,] ReadMassive(string path, out int n, out int m)
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

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < Convert.ToInt32(temp_list_massive[0].Length); j++)
                {
                    massive[i, j] = Convert.ToInt32(temp_list_massive[i][j]);
                }
            }
            return massive;
        }
        public static void WriteMassive(string path, string line)
        {
            StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);
            sw.Write(line);
            sw.Close();
        }
        public void Poten(string in_path, string out_path)
        {
            List<List<int[]>> data = new List<List<int[]>>();
            data.Add(new List<int[]>());
            data.Add(new List<int[]>());
            data.Add(new List<int[]>());

            data[0].Add(new int[] { 6, 0 });
            data[0].Add(new int[] { 8, 0 });
            data[0].Add(new int[] { 1000, 0 });

            data[1].Add(new int[] { 3, 0 });
            data[1].Add(new int[] { 4, 0 });
            data[1].Add(new int[] { 3, 0 });

            data[2].Add(new int[] { 1000, 0 });
            data[2].Add(new int[] { 1, 0 });
            data[2].Add(new int[] { 6, 0 });

            List<int> M = new List<int>() { 30, 30, 20 };//M предложение
            List<int> N = new List<int>() { 40, 30, 10 };//N спрос

            //List<List<int>> _1st_list = new List<List<int>>();
            //List<List<int>> _2st_list = new List<List<int>>();
            //List<List<int>> _3st_list = new List<List<int>>();

            //_1st_list.Add(new List<int>());
            //_1st_list.Add(new List<int>());
            //_2st_list.Add(new List<int>());
            //_2st_list.Add(new List<int>());
            //_3st_list.Add(new List<int>());
            //_3st_list.Add(new List<int>());

            int iN = 0, iM = 0, i = 0, j = 0;
            while (iN != 3 && iM != 3)
            {
                try
                {
                    if (N[iN] > M[iM])
                    {
                        data[i][j][1] = M[iM];
                        N[iN] = N[iN] - M[iM];
                        iM++;
                        i++;
                    }

                    if (N[iN] < M[iM])
                    {
                        data[i][j][1] = N[iN];
                        M[iM] = M[iM] - N[iN];
                        iN++;
                        j++;
                    }
                    if (N[iN] == M[iM])
                    {
                        data[i][j][1] = N[iN];
                        M[iM] = 0;
                        N[iN] = 0;
                        i++;
                        j++;
                    }
                }
                catch
                {
                    break;
                }
            }

            while (true)
            {
                int v = 0; //Количество заполенных клеток
                int _vdata = data.Count + data[0].Count - 1;
                for (int q = 0; q < data.Count; q++)
                {
                    foreach (int[] a in data[q])
                    {
                        Console.Write("{0}[{1}]; ", a[0], a[1]);
                        if (a[1] != 0)
                        {
                            v++;
                        }
                    }
                    Console.WriteLine();
                }
                if (v == _vdata)
                {
                    Console.WriteLine("Таблица невырожденная!");
                }

                List<int> U = new List<int>() { 0, 0, 0 };
                List<int> V = new List<int>() { 0, 0, 0 };

                //Расставление потенциалов
                for (int q = 0; q < data.Count; q++)
                {
                    for (int k = 0; k < data[q].Count; k++)
                    {
                        if (data[q][k][1] != 0)
                        {
                            if (U[k] == 0 && q != 0 && k != 0)
                            {
                                U[k] = data[q][k][0] - V[q];
                            }
                            if (V[q] == 0)
                            {
                                V[q] = data[q][k][0] - U[k];
                            }
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Потенциалы:");
                foreach (int a in U)
                {
                    Console.Write("{0}; ", a);
                }
                Console.WriteLine();
                foreach (int a in V)
                {
                    Console.Write("{0}; ", a);
                }

                //Проверка оптимальности распределения
                List<int[]> dnk = new List<int[]>();
                for (int q = 0; q < data.Count; q++)
                {
                    for (int k = 0; k < data[q].Count; k++)
                    {
                        if (data[q][k][1] == 0 && data[q][k][0] != 0)
                        {
                            int[] temp = new int[3];
                            temp[0] = V[q] + U[k] - data[q][k][0];
                            temp[1] = q;
                            temp[2] = k;
                            dnk.Add(temp);
                        }
                    }
                    Console.WriteLine();
                }

                foreach (int[] a in dnk)
                {
                    Console.Write("{0}[{1},{2}]; ", a[0], a[1], a[2]);
                }
                Console.WriteLine();

                //Поиск максимального элемента ДНК
                int max = dnk[0][0];
                int _imax = dnk[0][1];
                int _jmax = dnk[0][2];
                for (int q = 0; q < dnk.Count; q++)
                {
                    if (max < dnk[q][0])
                    {
                        max = dnk[q][0];
                        _imax = dnk[q][1];
                        _jmax = dnk[q][2];
                    }
                }
                Console.WriteLine("Максимальный элемент: {0}[{1},{2}]", max, _imax, _jmax);

                //Цикл перераспределения
                if (max > 0)
                {
                    Console.WriteLine("Распределение не оптимально");
                    int min = 10000;
                    int selecti = _imax;
                    int selectj = _jmax;
                    bool move = true;
                    while (true)
                    {
                        for (int l = 1; l < data.Count; l++)
                        {
                            if (selecti + l < data.Count && data[selecti + l][selectj][1] != 0 && move == true)
                            {
                                selecti = selecti + l;
                                if (min > data[selecti][_jmax][1])
                                {
                                    min = data[selecti][_jmax][1];
                                }
                                move = false;
                                break;
                            }

                            if (selecti - l < data.Count && selecti - l >= 0 && data[selecti - l][selectj][1] != 0 && move == true)
                            {
                                selecti = selecti - l;
                                if (min > data[selecti][selectj][1])
                                {
                                    min = data[selecti][selectj][1];
                                }
                                move = false;
                                break;
                            }
                        }

                        if (selecti == _imax)
                        {
                            Console.WriteLine(min);
                            break;
                        }


                        for (int l = 1; l < data.Count; l++)
                        {
                            if (selectj + l < data.Count && data[selecti][selectj + l][1] != 0 && move == false)
                            {
                                selectj = selectj + l;
                                move = true;
                                break;
                            }

                            if (selectj - l < data.Count && selecti - l >= 0 && data[selecti][selectj - l][1] != 0 && move == false)
                            {
                                selectj = selectj - l;
                                move = true;
                                break;
                            }
                        }
                        if (selectj == _jmax)
                        {
                            Console.WriteLine(min);
                            break;
                        }
                    }
                    //Повторный проход по циклу распределения с распределением элементов

                    data[_imax][_jmax][1] = min;
                    selecti = _imax;
                    selectj = _jmax;
                    move = true;

                    while (true)
                    {
                        for (int l = 1; l < data.Count; l++)
                        {
                            if (selecti + l < data.Count && data[selecti + l][selectj][1] != 0 && move == true)
                            {
                                selecti = selecti + l;
                                data[selecti][selectj][1] = data[selecti][selectj][1] - min;
                                move = false;
                                break;
                            }

                            if (selecti - l < data.Count && selecti - l >= 0 && data[selecti - l][selectj][1] != 0 && move == true)
                            {
                                selecti = selecti - l;
                                data[selecti][selectj][1] = data[selecti][selectj][1] - min;
                                move = false;
                                break;
                            }
                        }

                        if (selecti == _imax)
                        {
                            break;
                        }

                        for (int l = 1; l < data.Count; l++)
                        {
                            if (selectj + l < data.Count && data[selecti][selectj + l][1] != 0 && move == false)
                            {
                                selectj = selectj + l;
                                data[selecti][selectj][1] = data[selecti][selectj][1] + min;
                                move = true;
                                break;
                            }

                            if (selectj - l < data.Count && selecti - l >= 0 && data[selecti][selectj - l][1] != 0 && move == false)
                            {
                                selectj = selectj - l;
                                data[selecti][selectj][1] = data[selecti][selectj][1] + min;
                                move = true;
                                break;
                            }
                        }
                        if (selectj == _jmax)
                        {
                            break;
                        }
                    }

                    //Вывод таблицы
                    for (int q = 0; q < data.Count; q++)
                    {
                        foreach (int[] a in data[q])
                        {
                            Console.Write("{0}[{1}]; ", a[0], a[1]);
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("-------------------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine("Распределение оптимально");
                    for (int q = 0; q < data.Count; q++)
                    {
                        for (int k = 0; k < data[0].Count; k++)
                        {
                            if (data[q][k][0] == 1000)
                            {
                                data[q][k][0] = 0;
                            }
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine("Итоговое перераспределение:");
                    for (int q = 0; q < data.Count; q++)
                    {
                        foreach (int[] a in data[q])
                        {
                            Console.Write("{0}[{1}]; ", a[0], a[1]);
                        }
                        Console.WriteLine();
                    }
                    break;
                }
            }
            Console.Read();
        }
    }
}
