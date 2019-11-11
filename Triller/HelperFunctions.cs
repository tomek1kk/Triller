using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triller.Settings;

namespace Triller
{
    public static class HelperFunctions
    {
        public static int iteration;

        public static bool InArea(Point p1, Point p2, int dist)
        {
            return ((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y)) < dist * dist;
        }

        public static void GenerateTriangles(List<Triangle> triangles, int width, int height, int M, int N)
        {
            var triangleA = width / M;
            var triangleB = height / N;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    triangles.Add(new Triangle
                    {
                        A = new Point(j * triangleA, i * triangleB),
                        B = new Point((j + 1) * triangleA, i * triangleB),
                        C = new Point(j * triangleA, (i + 1) * triangleB)
                    });
                    triangles.Add(new Triangle
                    {
                        A = new Point((j + 1) * triangleA, i * triangleB),
                        B = new Point(j * triangleA, (i + 1) * triangleB),
                        C = new Point((j + 1) * triangleA, (i + 1) * triangleB)
                    });
                }
            }
        }

        public static void FillPolygon(List<Point> points, Triller triller, Triangle t = null, DirectBitmap bitmap = null)
        {
            List<Point> pointsSorted = new List<Point>(points);
            pointsSorted.Sort((p1, p2) => p1.Y > p2.Y ? 1 : -1);
            List<int> ind = new List<int>();
            for (int i = 0; i < pointsSorted.Count; i++)
                ind.Add(points.FindIndex(p => p == pointsSorted[i]));

            int ymin = pointsSorted[0].Y;
            int ymax = pointsSorted[pointsSorted.Count - 1].Y;

            List<Edge> AET = new List<Edge>();

            for (int y = ymin; y <= ymax; y++)
            {
                for (int k = 0; k < points.Count; k++)
                {
                    if (points[ind[k]].Y == y - 1)
                    {
                        if (points[ind[k] - 1 >= 0 ? ind[k] - 1 : points.Count - 1].Y > points[ind[k]].Y)
                        {
                            AET.Add(new Edge
                            {
                                A = points[ind[k] - 1 >= 0 ? ind[k] - 1 : points.Count - 1],
                                B = points[ind[k]]
                            });
                        }
                        else
                        {
                            AET.Remove(AET.Find(e => e.A == points[ind[k]]));
                        }

                        if (points[(ind[k] + 1) % points.Count].Y > points[ind[k]].Y)
                        {
                            AET.Add(new Edge
                            {
                                A = points[(ind[k] + 1) % points.Count],
                                B = points[ind[k]]
                            });
                        }
                        else
                        {
                            AET.Remove(AET.Find(e => e.A == points[ind[k]]));
                        }
                    }
                }
                AET.Sort((e1, e2) => e1.GetX(y) > e2.GetX(y) ? 1 : -1);
                for (int j = 0; j < AET.Count / 2; j++)
                {
                    for (int p = AET[2 * j + 1].GetX(y); p >= AET[2 * j].GetX(y); p--)
                    {
                        if (p >= 0)
                            bitmap.SetPixel(p, y, GetColor(p, y, t));
                    }

                }
            }
        }

        public static Color GetColor(int x, int y, Triangle t)
        {
            SettingsFactory settingsFactory = new SettingsFactory();
            double kd = settingsFactory.GetCoefficients().Kd;
            double ks = settingsFactory.GetCoefficients().Ks;
            int m = settingsFactory.GetCoefficients().M;
            MyVector L = settingsFactory.GetLight(x, y, GetAnimationPoint(Triller.iteration, 20, 1000, new Point(400,250))).L;
            Color objectColor = settingsFactory.GetObjectColor(x, y, t).ObjectColor;
            Color lightColor = settingsFactory.GetLightColor();
            MyVector N = settingsFactory.GetVectorN(x, y, t).N;
            MyVector V = new MyVector(0, 0, 1);
            double NL = N.X * L.X + N.Y * L.Y + N.Z * L.Z;
            MyVector R = new MyVector(2 * NL * N.X - L.X, 2 * NL * N.Y - L.Y, 2 * NL * N.Z - L.Z);

            var IR = kd * ((double)lightColor.R / 255) * ((double)objectColor.R / 255) * MyVector.MyCos(N, L) +
                     ks * ((double)lightColor.R / 255) * ((double)objectColor.R / 255) * Math.Abs(Math.Pow(MyVector.MyCos(V, R), m));
            var IG = kd * ((double)lightColor.G / 255) * ((double)objectColor.G / 255) * MyVector.MyCos(N, L) +
                     ks * ((double)lightColor.G / 255) * ((double)objectColor.G / 255) * Math.Abs(Math.Pow(MyVector.MyCos(V, R), m));
            var IB = kd * ((double)lightColor.B / 255) * ((double)objectColor.B / 255) * MyVector.MyCos(N, L) +
                     ks * ((double)lightColor.B / 255) * ((double)objectColor.B / 255) * Math.Abs(Math.Pow(MyVector.MyCos(V, R), m));

            return Color.FromArgb(255, (int)(IR * 255) > 255 ? 255 : (int)(IR * 255), (int)(IG * 255) > 255 ? 255 : (int)(IG * 255),
                (int)(IB * 255) > 255 ? 255 : (int)(IB * 255));
        }

        private static Point GetAnimationPoint(int iteration, int step, int r, Point middle)
        {
            double angle = (iteration * step) % 360;

            int animationX = middle.X + (int)(r * Math.Cos(angle * Math.PI / 180));
            int animationY = middle.Y + (int)(r * Math.Sin(angle * Math.PI / 180));

            return new Point(animationX, animationY);
        }
    }
}
