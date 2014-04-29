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

            var graph = new RandomGraph();
            for (int i = 0; i < 10000; i++)
            {
                graph.AddVertice(2);
            }
            

            var series = new Series();
            for (int i = 0; i < graph.graph.Count;i++)
                series.Points.Add(new DataPoint(i, graph.graph[i].Count));
            series.ChartType = SeriesChartType.FastLine;

            chart.Series.Add(series);
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
}
