using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace GiantGraph
{
    public partial class Graph : Form
    {
        public Graph()
        {
            var chart = new Chart()
            {
                ChartAreas = { new ChartArea() },
            };
            chart.Dock = DockStyle.Fill;
            Controls.Add(chart);

            var graph = new ListBasedRandomGraph();
            for (int i = 0; i < 10000; i++)
            {
                graph.AddVertice(1);
            }

            var graphToDraw = graph.graph.OrderByDescending(x => x.Count).ToList();

            var series = new Series();
            for (int i = 0; i < graphToDraw.Count;i++)
                series.Points.Add(new DataPoint(i, Math.Log(graphToDraw[i].Count)));
            series.ChartType = SeriesChartType.FastLine;

            chart.Series.Add(series);
            graph.GraphToFile();
        }
    }

    class RandomGraph
    {
        public List<List<int>> graph;

        int degreesSum;

        Random rand = new Random();

        public RandomGraph()
        {
            graph = new List<List<int>>();
            graph.Add(new List<int>() { 1 });
            graph.Add(new List<int>() { 0 });
            degreesSum = 2;
        }

        public void AddVertice(int M)
        {
            graph.Add(new List<int>());
            for (int k = 0; k < M; k++)
            {
                var a = rand.Next(0, degreesSum);
                int i = 0;
                while (a > 0)
                {
                    a -= graph[i].Count;
                    i++;
                }
                graph[i].Add(graph.Count - 1);
                graph.Last().Add(i);
                degreesSum += 2;
            }
        }

    }

    class ListBasedRandomGraph
    {
        public List<List<int>> graph;

        List<int> used;

        Random rand = new Random();

        public ListBasedRandomGraph()
        {
            graph = new List<List<int>>();
            graph.Add(new List<int>() { 1 });
            graph.Add(new List<int>() { 0 });
            used = new List<int>() { 0, 1 };
        }

        public void AddVertice(int M)
        {
            graph.Add(new List<int>());
            var last=graph.Last();
            for (int k = 0; k < M; k++)
            {
                var a = rand.Next(0, used.Count);
                if (!last.Contains(used[a]))
                {
                    last.Add(used[a]);
                    used.Add(used[a]);
                    graph[used[a]].Add(graph.Count - 1);
                    used.Add(graph.Count - 1);
                }
                else
                    k--;
            }
        }

        public void GraphToFile()
        {
            var sw = new StreamWriter("Graph.dot");
            sw.WriteLine("graph \"\"{");
            for (int i = 0; i < graph.Count; i++)
            {
                for (int j = 0; j < graph[i].Count; j++)
                {
                    if (graph[i][j] > i)
                        sw.WriteLine("\t{0}--{1}", i, graph[i][j]);
                }
            }
            sw.WriteLine("}");
            sw.Close();
        }
    }
}
