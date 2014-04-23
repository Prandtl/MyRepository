using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzlingPuzzle
{
    public class A_Star
    {
        public class PathNode
        {
            public int[][] Position { get; set; }
            public int PathLengthFromStart { get; set; }
            public PathNode CameFrom { get; set; }
            public int HeuristicEstimatePathLength { get; set; }
            public int EstimateFullPathLength
            {
                get
                {
                    return this.PathLengthFromStart + this.HeuristicEstimatePathLength;
                }
            }
            public PathNode(int[][] pos, int pathLength, PathNode camefrom, int heuristcLength)
            {
                Position = pos;
                PathLengthFromStart = pathLength;
                CameFrom = camefrom;
                HeuristicEstimatePathLength = heuristcLength;
            }
        }

        public static List<int[][]> FindPath(int[][] start, int[][] goal)
        {
            var closedSet = new HashSet<PathNode>();
            var openSet = new HashSet<PathNode>();
            PathNode startNode = new PathNode(start, 0,null, GetEuristic(new GameField(start)));
            openSet.Add(startNode);
            while (openSet.Count > 0)
            {
                var currentNode = openSet.OrderBy(node => node.EstimateFullPathLength)
                                         .First();
                if (currentNode.Position.CheckEquality(goal))
                    return GetPathForNode(currentNode);
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
                foreach (var neighbourNode in GetNeighbours(currentNode, goal))
                {
                    if (closedSet.Count(node => node.Position.CheckEquality(neighbourNode.Position)) > 0)
                        continue;
                    var openNode = openSet.FirstOrDefault(node => node.Position.CheckEquality(neighbourNode.Position));
                    if (openNode == null)
                        openSet.Add(neighbourNode);
                    else
                        if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                        {
                            openNode.CameFrom = currentNode;
                            openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                        }
                }
            }
            return null;
        }

        public static int GetEuristic(GameField field)
        {
            int euristic = 0;
            for (int i = 0; i < field.Field.Length; i++)
            {
                for (int j = 0; j < field.Field[i].Length; j++)
                {
                    euristic += GetEuristicForTile(field, new GameField.Point(i, j));
                }
            }
            return euristic;
        }

        public static IEnumerable<int[][]> GetPossibleFields(GameField field)
        {
            var a = field.GetNeighbours().Select(x => x.Add(field.zeroTile))
                                         .Where(point => (point.X >= 0 && point.X < 3) && (point.Y >= 0 && point.Y < 3));
            return a.Select(point => field.Swap(point, field.zeroTile));
        }

        public static IEnumerable<PathNode> GetNeighbours(PathNode node,int[][] goal)
        {
            var possibleFields = GetPossibleFields(new GameField(node.Position));
            return possibleFields.Select(x => new PathNode(x, node.PathLengthFromStart + 1, node, GetEuristic(new GameField(x))));
        }

        public static int GetEuristicForTile(GameField field, GameField.Point point)
        {
            var needToBe = GetPosition(field, field.Field[point.X][point.Y]);
            var sub = point.Sub(needToBe);
            return Math.Abs(sub.X) + Math.Abs(sub.Y);
        }

        public static GameField.Point GetPosition(GameField field, int val)
        {
            for (int i = 0; i < field.Field.Length; i++)
            {
                for (int j = 0; j < field.Field[i].Length; j++)
                {
                    if (field.Field[i][j] == val)
                        return new GameField.Point(i, j);
                }
            }
            throw new System.Exception("no value like this in the field!");
        }

        public static List<int[][]> GetPathForNode(PathNode node)
        {
            List<PathNode> path=new List<PathNode>();
            var a = node;
            path.Add(a);
            while((a=a.CameFrom)!=null)
            {
                path.Add(a);
            }
            return path.Select(x => x.Position).Reverse().ToList();
        }
    }
}
