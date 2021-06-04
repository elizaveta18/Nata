using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1Класс
{
    public class CrtPth
    {
        string s = "";
        public CrtPth(string path)
        {
            List<Rbt> ret;
            List<Rbt> ls = Flrd(path);
            ret = ls.FindAll(x => x.point1 == ls[Minel(ls)].point1);
            List<List<Rbt>> fnlcn = new List<List<Rbt>>();
            foreach (Rbt rb in ret)
            {
                Mv(ls, rb);
                fnlcn.Add(RtPrs(ls, s));
                s = "";
            }
            int max = fnlcn[0][0].length, maxind = 0;
            for (int i = 0; i < ret.Count; i++)
            {
                if (FnlMv(fnlcn[i]) >= max)
                {
                    max = FnlMv(fnlcn[i]);
                    maxind = i;
                }
            }
            using (StreamWriter sr = new StreamWriter("Итог.txt"))
            {
                foreach (Rbt rb in fnlcn[maxind])
                {
                    sr.WriteLine(rb.point1 + " - " + rb.point2);
                }
                sr.WriteLine(max);
            }
        }
        struct Rbt
        {
            public int point1;
            public int point2;
            public int length;
            public bool Equals(Rbt obj)
            {
                if (this.point1 == obj.point1 && this.point2 == obj.point2 && this.length == obj.length) return true;
                else return false;
            }
            public override string ToString()
            {
                return point1.ToString() + " - " + point2.ToString() + " " + length.ToString();
            }
        }
        int Minel(List<Rbt> ls)
        {
            int min = ls[0].point1, minind = 0;
            foreach (Rbt rb in ls)
            {
                if (rb.point1 <= min)
                {
                    min = rb.point1;
                    minind = ls.IndexOf(rb);
                }
            }
            return minind;
        }
        int Maxel(List<Rbt> ls)
        {
            int min = ls[0].point2, maxind = 0;
            foreach (Rbt rb in ls)
            {
                if (rb.point2 >= min)
                {
                    min = rb.point1;
                    maxind = ls.IndexOf(rb);
                }
            }
            return maxind;
        }
        int Mv(List<Rbt> ls, Rbt minel)
        {
            int ret = 0;
            Rbt rb = ls.Find(x => x.point1 == minel.point1 && x.point2 == minel.point2);
            s += rb.point1.ToString() + "-" + rb.point2.ToString();
            if (rb.point2 == ls[Maxel(ls)].point2)
            {
                s += ";";
                return rb.length;
            }
            else
            {
                for (int i = 0; i < ls.Count; i++)
                {
                    if (ls[i].point1 == rb.point2)
                    {
                        s += ",";
                        ret = Mv(ls, ls[i]) + rb.length;
                    }
                }
            }
            return ret;
        }
        int Mv(List<Rbt> ls, Rbt minel, StreamWriter sr)
        {
            int ret = 0;
            Rbt rb = ls.Find(x => x.point1 == minel.point1 && x.point2 == minel.point2);
            sr.WriteLine(rb.point1 + " - " + rb.point2);
            if (rb.point2 == ls[Maxel(ls)].point2)
            {
                return rb.length;
            }
            else
            {
                for (int i = 0; i < ls.Count; i++)
                {
                    if (ls[i].point1 == rb.point2)
                    {
                        ret = Mv(ls, ls[i], sr) + rb.length;
                    }
                }
            }
            return ret;
        }
        List<Rbt> Flrd(string path)
        {
            List<Rbt> ret = new List<Rbt>();
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.EndOfStream != true)
                {
                    string[] str1 = sr.ReadLine().Split(';');
                    string[] str2 = str1[0].Split('-');
                    ret.Add(new Rbt { point1 = Convert.ToInt32(str2[0]), point2 = Convert.ToInt32(str2[1]), length = Convert.ToInt32(str1[1]) });
                }
            }
            return ret;
        }
        List<Rbt> RtPrs(List<Rbt> ls, string s)
        {
            List<List<Rbt>> ret = new List<List<Rbt>>();
            string[] str1 = s.Split(';');
            foreach (string st1 in str1)
            {
                if (st1 != "")
                {
                    ret.Add(new List<Rbt>());
                    string[] str2 = st1.Split(',');
                    foreach (string st2 in str2)
                    {
                        if (st2 != "")
                        {
                            string[] str3 = st2.Split('-');
                            ret[ret.Count - 1].Add(ls.Find(x => x.point1 == Convert.ToInt32(str3[0]) && x.point2 == Convert.ToInt32(str3[1])));
                        }
                    }
                }
            }
            foreach (List<Rbt> l in ret)
            {
                if (l[0].point1 != ls[Minel(ls)].point1)
                {
                    foreach (List<Rbt> l1 in ret)
                    {
                        if (l1[0].point1 == ls[Minel(ls)].point1)
                        {
                            l.InsertRange(0, l1.FindAll(x => l1.IndexOf(x) <= l1.FindIndex(y => y.point2 == l[0].point1)));
                        }
                    }
                }
            }
            int max = ret[0][0].length, maxind = 0;
            for (int i = 0; i < ret.Count; i++)
            {
                if (FnlMv(ret[i]) >= max)
                {
                    max = FnlMv(ret[i]);
                    maxind = i;
                }
            }
            return ret[maxind];
        }
        int FnlMv(List<Rbt> ls)
        {
            int ret = 0;
            foreach (Rbt rb in ls)
            {
                ret += rb.length;
            }
            return ret;
        }
    }
    //public class GraphVertexInfo
    //{
    //    public int[,] ReadMassive(string path, out int n, out int m)
    //    {
    //        List<string[]> temp_list_massive = new List<string[]>();
    //        try
    //        {
    //            StreamReader sr = new StreamReader(path);
    //            while (true)
    //            {
    //                string line = sr.ReadLine();
    //                if (line == null)
    //                {
    //                    break;
    //                }
    //                temp_list_massive.Add(line.Split(';'));
    //            }
    //        }
    //        catch
    //        { }
    //        n = Convert.ToInt32(temp_list_massive.Count);
    //        m = Convert.ToInt32(temp_list_massive[0].Length);
    //        int[,] massive = new int[Convert.ToInt32(temp_list_massive.Count), Convert.ToInt32(temp_list_massive[0].Length)];

    //        for (int i = 0; i < 2; i++)
    //        {
    //            for (int j = 0; j < Convert.ToInt32(temp_list_massive[0].Length); j++)
    //            {
    //                massive[i, j] = Convert.ToInt32(temp_list_massive[i][j]);
    //            }
    //        }
    //        return massive;
    //    }
    //    public void WriteMassive(string path, string line)
    //    {
    //        StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);
    //        sw.Write(line);
    //        sw.Close();
    //    }
    //    /// <summary>
    //    /// Вершина
    //    /// </summary>
    //    public GraphVertex Vertex { get; set; }

    //    /// <summary>
    //    /// Не посещенная вершина
    //    /// </summary>
    //    public bool IsUnvisited { get; set; }

    //    /// <summary>
    //    /// Сумма весов ребер
    //    /// </summary>
    //    public int EdgesWeightSum { get; set; }

    //    /// <summary>
    //    /// Предыдущая вершина
    //    /// </summary>
    //    public GraphVertex PreviousVertex { get; set; }

    //    /// <summary>
    //    /// Конструктор
    //    /// </summary>
    //    /// <param name="vertex">Вершина</param>
    //    public GraphVertexInfo(GraphVertex vertex)
    //    {
    //        Vertex = vertex;
    //        IsUnvisited = true;
    //        EdgesWeightSum = int.MaxValue;
    //        PreviousVertex = null;
    //    }
    //}
    //public class GraphEdge
    //{
    //    /// <summary>
    //    /// Связанная вершина
    //    /// </summary>
    //    public GraphVertex ConnectedVertex { get; }

    //    /// <summary>
    //    /// Вес ребра
    //    /// </summary>
    //    public int EdgeWeight { get; }

    //    /// <summary>
    //    /// Конструктор
    //    /// </summary>
    //    /// <param name="connectedVertex">Связанная вершина</param>
    //    /// <param name="weight">Вес ребра</param>
    //    public GraphEdge(GraphVertex connectedVertex, int weight)
    //    {
    //        ConnectedVertex = connectedVertex;
    //        EdgeWeight = weight;
    //    }
    //}
    //public class GraphVertex
    //{
    //    /// <summary>
    //    /// Название вершины
    //    /// </summary>
    //    public string Name { get; }

    //    /// <summary>
    //    /// Список ребер
    //    /// </summary>
    //    public List<GraphEdge> Edges { get; }

    //    /// <summary>
    //    /// Конструктор
    //    /// </summary>
    //    /// <param name="vertexName">Название вершины</param>
    //    public GraphVertex(string vertexName)
    //    {
    //        Name = vertexName;
    //        Edges = new List<GraphEdge>();
    //    }

    //    /// <summary>
    //    /// Добавить ребро
    //    /// </summary>
    //    /// <param name="newEdge">Ребро</param>
    //    public void AddEdge(GraphEdge newEdge)
    //    {
    //        Edges.Add(newEdge);
    //    }

    //    /// <summary>
    //    /// Добавить ребро
    //    /// </summary>
    //    /// <param name="vertex">Вершина</param>
    //    /// <param name="edgeWeight">Вес</param>
    //    public void AddEdge(GraphVertex vertex, int edgeWeight)
    //    {
    //        AddEdge(new GraphEdge(vertex, edgeWeight));
    //    }

    //    /// <summary>
    //    /// Преобразование в строку
    //    /// </summary>
    //    /// <returns>Имя вершины</returns>
    //    public override string ToString() => Name;
    //}
    //public class Graph
    //{
    //    /// <summary>
    //    /// Список вершин графа
    //    /// </summary>
    //    public List<GraphVertex> Vertices { get; }

    //    /// <summary>
    //    /// Конструктор
    //    /// </summary>
    //    public Graph()
    //    {
    //        Vertices = new List<GraphVertex>();
    //    }

    //    /// <summary>
    //    /// Добавление вершины
    //    /// </summary>
    //    /// <param name="vertexName">Имя вершины</param>
    //    public void AddVertex(string vertexName)
    //    {
    //        Vertices.Add(new GraphVertex(vertexName));
    //    }

    //    /// <summary>
    //    /// Поиск вершины
    //    /// </summary>
    //    /// <param name="vertexName">Название вершины</param>
    //    /// <returns>Найденная вершина</returns>
    //    public GraphVertex FindVertex(string vertexName)
    //    {
    //        foreach (var v in Vertices)
    //        {
    //            if (v.Name.Equals(vertexName))
    //            {
    //                return v;
    //            }
    //        }

    //        return null;
    //    }

    //    /// <summary>
    //    /// Добавление ребра
    //    /// </summary>
    //    /// <param name="firstName">Имя первой вершины</param>
    //    /// <param name="secondName">Имя второй вершины</param>
    //    /// <param name="weight">Вес ребра соединяющего вершины</param>
    //    public void AddEdge(string firstName, string secondName, int weight)
    //    {
    //        var v1 = FindVertex(firstName);
    //        var v2 = FindVertex(secondName);
    //        if (v2 != null && v1 != null)
    //        {
    //            v1.AddEdge(v2, weight);
    //            v2.AddEdge(v1, weight);
    //        }
    //    }
    //}
    //public class Dijkstra
    //{
    //    Graph graph;
    //    List<List<int>> _1st_list = new List<List<int>>();
    //    List<List<int>> _2st_list = new List<List<int>>();

    //    List<GraphVertexInfo> infos;

    //    /// <summary>
    //    /// Конструктор
    //    /// </summary>
    //    /// <param name="graph">Граф</param>
    //    public Dijkstra(Graph graph)
    //    {
    //        List<List<int>> _1st_list = new List<List<int>>();
    //        List<List<int>> _2st_list = new List<List<int>>();
    //        _1st_list.Add(new List<int>());
    //        _1st_list.Add(new List<int>());

    //        this.graph = graph;
    //    }

    //    /// <summary>
    //    /// Инициализация информации
    //    /// </summary>
    //    void InitInfo()
    //    {
    //        infos = new List<GraphVertexInfo>();
    //        foreach (var v in graph.Vertices)
    //        {
    //            infos.Add(new GraphVertexInfo(v));
    //        }
    //    }

    //    /// <summary>
    //    /// Получение информации о вершине графа
    //    /// </summary>
    //    /// <param name="v">Вершина</param>
    //    /// <returns>Информация о вершине</returns>
    //    GraphVertexInfo GetVertexInfo(GraphVertex v)
    //    {
    //        foreach (var i in infos)
    //        {
    //            if (i.Vertex.Equals(v))
    //            {
    //                return i;
    //            }
    //        }

    //        return null;
    //    }

    //    /// <summary>
    //    /// Поиск непосещенной вершины с минимальным значением суммы
    //    /// </summary>
    //    /// <returns>Информация о вершине</returns>
    //    public GraphVertexInfo FindUnvisitedVertexWithMinSum()
    //    {
    //        var minValue = int.MaxValue;
    //        GraphVertexInfo minVertexInfo = null;
    //        foreach (var i in infos)
    //        {
    //            if (i.IsUnvisited && i.EdgesWeightSum < minValue)
    //            {
    //                minVertexInfo = i;
    //                minValue = i.EdgesWeightSum;
    //            }
    //        }
    //        return minVertexInfo;
    //    }

    //    /// <summary>
    //    /// Поиск кратчайшего пути по названиям вершин
    //    /// </summary>
    //    /// <param name="startName">Название стартовой вершины</param>
    //    /// <param name="finishName">Название финишной вершины</param>
    //    /// <returns>Кратчайший путь</returns>
    //    public string FindShortestPath(string startName, string finishName)
    //    {
    //        return FindShortestPath(graph.FindVertex(startName), graph.FindVertex(finishName));
    //    }

    //    /// <summary>
    //    /// Поиск кратчайшего пути по вершинам
    //    /// </summary>
    //    /// <param name="startVertex">Стартовая вершина</param>
    //    /// <param name="finishVertex">Финишная вершина</param>
    //    /// <returns>Кратчайший путь</returns>
    //    public string FindShortestPath(GraphVertex startVertex, GraphVertex finishVertex)
    //    {
    //        InitInfo();
    //        var first = GetVertexInfo(startVertex);
    //        first.EdgesWeightSum = 0;
    //        while (true)
    //        {
    //            var current = FindUnvisitedVertexWithMinSum();
    //            if (current == null)
    //            {
    //                break;
    //            }

    //            SetSumToNextVertex(current);
    //        }
    //        return GetPath(startVertex, finishVertex);
    //    }

    //    /// <summary>
    //    /// Вычисление суммы весов ребер для следующей вершины
    //    /// </summary>
    //    /// <param name="info">Информация о текущей вершине</param>
    //    void SetSumToNextVertex(GraphVertexInfo info)
    //    {
    //        info.IsUnvisited = false;
    //        foreach (var e in info.Vertex.Edges)
    //        {
    //            var nextInfo = GetVertexInfo(e.ConnectedVertex);
    //            var sum = info.EdgesWeightSum + e.EdgeWeight;
    //            if (sum < nextInfo.EdgesWeightSum)
    //            {
    //                nextInfo.EdgesWeightSum = sum;
    //                nextInfo.PreviousVertex = info.Vertex;
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Формирование пути
    //    /// </summary>
    //    /// <param name="startVertex">Начальная вершина</param>
    //    /// <param name="endVertex">Конечная вершина</param>
    //    /// <returns>Путь</returns>
    //    string GetPath(GraphVertex startVertex, GraphVertex endVertex)
    //    {
    //        var path = endVertex.ToString();
    //        while (startVertex != endVertex)
    //        {
    //            endVertex = GetVertexInfo(endVertex).PreviousVertex;
    //            path = endVertex.ToString() + path;
    //        }

    //        return path;
    //    }
    //}
}
