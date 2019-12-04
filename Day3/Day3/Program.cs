using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = "Hello Day3!!";
            Console.WriteLine(message);

            List<Vector> wire1 = new List<Vector>();
            Dictionary<Vector, int> wire1Dict = new Dictionary<Vector, int>();

            List<Vector> wire2 = new List<Vector>();
            Dictionary<Vector, int> wire2Dict = new Dictionary<Vector, int>();

            //string FileName = "E:\\Users\\Kevin\\AdventOfCode2019\\Day3\\Input.txt";
            string FileName = "E:\\Users\\Kevin\\AdventOfCode2019\\Day3\\TestInput.txt";

            using (StreamReader sr = new StreamReader(FileName, Encoding.Default))
            {
                string text1 = sr.ReadLine();
                string text2 = sr.ReadLine();
                string[] directions1 = text1.Split(',');
                string[] directions2 = text2.Split(',');

                Tuple<List<Vector>, Dictionary<Vector, int>> wire1Results = PopulateArray(directions1);
                wire1 = wire1Results.Item1;
                wire1Dict = wire1Results.Item2;
                List<Vector> wire1noDupes = wire1.Distinct().ToList();

                Tuple<List<Vector>, Dictionary<Vector, int>> wire2Results = PopulateArray(directions2);
                wire2 = wire2Results.Item1;
                wire2Dict = wire2Results.Item2;
                List<Vector> wire2noDupes = wire2.Distinct().ToList();

                List<Vector> intersects = wire1noDupes.Intersect(wire2noDupes).ToList();
                List<Vector> sortedIntersects = intersects.OrderBy(o => o.x).ThenBy(o=>o.y).ToList();

                int lowestDistance = 2147483647;
                Vector lowestVector = new Vector();

                foreach (Vector a in intersects)
                {
                    Console.WriteLine(a.x + ",", a.y);
                    int distance = a.getDistanceFromZero();
                    if (distance > 0 && distance < lowestDistance)
                    {
                        lowestDistance = distance;
                        lowestVector = a;
                        var keys = wire1Dict.Keys.Where((key => key.Equals(a)));
                        //int lowestCount = wire1Dict.MinBy(kvp => kvp.Value);
                    }            
                }
                Console.WriteLine("The lowest common distance is " + lowestVector.x + "," + lowestVector.y + " that is " + lowestDistance + " distance from 0,0.");
                Console.ReadLine();
            }
        }

        public static Tuple<List<Vector>, Dictionary<Vector, int>> PopulateArray(string[] directions)
        {
            List<Vector> wire = new List<Vector>();
            Dictionary<Vector, int> wireDict = new Dictionary<Vector, int>();

            int currentX = 0;
            int currentY = 0;
            int thisCounter = 0;

            foreach (string direction in directions)
            {
                string vector = direction.Substring(0, 1);
                int distance = Int32.Parse(direction.Substring(1, direction.Length - 1));

                if (vector == "R")
                {
                    int endValue = currentY + distance;
                    for (int i = currentY; i <= endValue; i++)
                    {
                        Vector thisVector = new Vector();
                        thisVector.x = currentX;
                        thisVector.y = i;
                        wire.Add(thisVector);
                        if (wireDict.ContainsKey(thisVector))
                        {
                            int dummy = 0;
                        }
                        else
                        { 
                            wireDict.Add(thisVector, thisCounter);
                        }
                        thisCounter++;
                    }
                    currentY = endValue;
                }
                else if (vector == "U")
                {
                    int endValue = currentX + distance;
                    for (int i = currentX; i <= endValue; i++)
                    {
                        Vector thisVector = new Vector();
                        thisVector.x = i;
                        thisVector.y = currentY;
                        wire.Add(thisVector);
                        if (wireDict.ContainsKey(thisVector))
                        {
                            int dummy = 0;
                        }
                        else
                        {
                            wireDict.Add(thisVector, thisCounter);
                        }
                        thisCounter++;
                    }
                    currentX = endValue;

                }
                else if (vector == "L")
                {
                    int endValue = currentY - distance;
                    for (int i = currentY; i >= endValue; i--)
                    {
                        Vector thisVector = new Vector();
                        thisVector.x = currentX;
                        thisVector.y = i;
                        wire.Add(thisVector);
                        if (wireDict.ContainsKey(thisVector))
                        {
                            int dummy = 0;
                        }
                        else
                        {
                            wireDict.Add(thisVector, thisCounter);
                        }
                        thisCounter++;
                    }
                    currentY = endValue;

                }
                else if (vector == "D")
                {
                    int endValue = currentX - distance;
                    for (int i = currentX; i >= endValue; i--)
                    {
                        Vector thisVector = new Vector();
                        thisVector.x = i;
                        thisVector.y = currentY;
                        wire.Add(thisVector);
                        if (wireDict.ContainsKey(thisVector))
                        {
                            int dummy = 0;
                        }
                        else
                        {
                            wireDict.Add(thisVector, thisCounter);
                        }
                        thisCounter++;
                    }
                    currentX = endValue;

                }
            }
            return Tuple.Create(wire, wireDict);
        }

        public class Vector : IEquatable<Vector>
        {
            public int x { get; set; }
            public int y { get; set; }

            public bool Equals(Vector other)
            {
                if (other == null) return false;
                return  x == other.x &&
                        y == other.y ;
            }

            public int getDistanceFromZero()
            {
                int distance = 0;
                distance = distance + Math.Abs(this.x);
                distance = distance + Math.Abs(this.y);
                return distance;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals(obj as Vector);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = 13;
                    hashCode = (hashCode * 397) ^ x;
                    hashCode = (hashCode * 397) ^ y;
                    return hashCode;
                }
            }
        }
    }
}
