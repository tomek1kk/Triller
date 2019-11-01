using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triller
{
    public static class HelperFunctions
    {
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

        public static void FillPolygon(List<Point> points, Graphics g, Triller triller, Triangle t = null)
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
                            AET.Remove(AET.Find(e => e.A == points[ind[k] - 1 >= 0 ? ind[k] - 1 : points.Count - 1] && e.B == points[ind[k]]));

                        if (points[(ind[k] + 1) % points.Count].Y > points[ind[k]].Y)
                        {
                            AET.Add(new Edge
                            {
                                A = points[(ind[k] + 1) % points.Count],
                                B = points[ind[k]]
                            });
                        }
                        else
                            AET.Remove(AET.Find(e => e.A == points[(ind[k] + 1) % points.Count] && e.B == points[ind[k]]));
                    }
                }
                AET.Sort((e1, e2) => e1.GetX(y) > e2.GetX(y) ? 1 : -1);
                for (int j = 0; j < AET.Count / 2; j++)
                {
                    for (int p = AET[2 * j + 1].GetX(y); p >= AET[2 * j].GetX(y); p--)
                        g.FillRectangle(new SolidBrush(GetColor(p, y, t, triller)), p, y, 1, 1);

                }
            }
        }

        public static Color GetColor(int x, int y, Triangle t, Triller triller)
        {
            double kd, ks;
            int m;
            Color objectColor;
            Color lightColor = triller.pictureBox3.BackColor;
            MyVector L;
            MyVector N = new MyVector(0, 0, 1);
            MyVector V = new MyVector(0, 0, 1);

            if (triller.settings.coefficients == Coefficients.Constant)
            {
                kd = (double)triller.trackBar1.Value / 100;
                ks = (double)triller.trackBar2.Value / 100;
                m = triller.trackBar3.Value;
            }
            else
            {
                t.SetCoefficients();
                kd = t.kd;
                ks = t.ks;
                m = t.m;
            }
            if (triller.settings.objectColor == ObjectColor.Constant)
                objectColor = triller.panel6.BackColor;
            else if (triller.settings.objectColor == ObjectColor.FromTexture && triller.settings.fillColor == FillColor.Exact)
                objectColor = triller.bitmap.GetPixel(x, y);
            else if (triller.settings.objectColor == ObjectColor.FromTexture && triller.settings.fillColor == FillColor.Interpolated)
                objectColor = t.GetInterpolationPixel(x, y, triller.bitmap.GetPixel(t.A.X, t.A.Y), triller.bitmap.GetPixel(t.B.X, t.B.Y), triller.bitmap.GetPixel(t.C.X, t.C.Y));
            else if (triller.settings.objectColor == ObjectColor.FromTexture && triller.settings.fillColor == FillColor.Hybrid && triller.settings.vectorN == VectorN.FromTexture)
            {
                objectColor = t.GetInterpolationPixel(x, y, triller.bitmap.GetPixel(t.A.X, t.A.Y), triller.bitmap.GetPixel(t.B.X, t.B.Y), triller.bitmap.GetPixel(t.C.X, t.C.Y));
                var pixel = t.GetInterpolationPixel(x, y, triller.normalBitmap.GetPixel(t.A.X, t.A.Y), triller.normalBitmap.GetPixel(t.B.X, t.B.Y), triller.normalBitmap.GetPixel(t.C.X, t.C.Y));
                N = new MyVector((double)(pixel.R - 127) / 128, (double)(pixel.G - 127) / 128, (double)(pixel.B) / 255);
            }
            else
            {
                objectColor = t.GetInterpolationPixel(x, y, triller.bitmap.GetPixel(t.A.X, t.A.Y), triller.bitmap.GetPixel(t.B.X, t.B.Y), triller.bitmap.GetPixel(t.C.X, t.C.Y));
                N = new MyVector(0, 0, 1);
            }

            if (triller.settings.light == Light.Constant)
                L = new MyVector(0, 0, 1);
            else
            { 
                // TODO
                L = new MyVector(0, 0, 1);
            }
            if (triller.settings.vectorN == VectorN.Constant)
                N = new MyVector(0, 0, 1);
            else if (triller.settings.vectorN == VectorN.FromTexture && triller.settings.fillColor != FillColor.Hybrid)
            {
                var pixel = triller.normalBitmap.GetPixel(x, y);
                N = new MyVector((double)(pixel.R - 127) / 128, (double)(pixel.G - 127) / 128, (double)(pixel.B) / 255);
            }

            MyVector R = new MyVector(2 * N.X - L.X, 2 * N.Y - L.Y, 2 * N.Z - L.Z);

            var IR = kd * ((double)lightColor.R / 255) * ((double)objectColor.R / 255) * MyVector.MyCos(N, L) +
                     ks * ((double)lightColor.R / 255) * ((double)objectColor.R / 255) * Math.Pow(MyVector.MyCos(V, R), m);
            var IG = kd * ((double)lightColor.G / 255) * ((double)objectColor.G / 255) * MyVector.MyCos(N, L) +
                     ks * ((double)lightColor.G / 255) * ((double)objectColor.G / 255) * Math.Pow(MyVector.MyCos(V, R), m);
            var IB = kd * ((double)lightColor.B / 255) * ((double)objectColor.B / 255) * MyVector.MyCos(N, L) +
                     ks * ((double)lightColor.B / 255) * ((double)objectColor.B / 255) * Math.Pow(MyVector.MyCos(V, R), m);

            return Color.FromArgb(255, (int)(IR * 255) > 255 ? 255 : (int)(IR * 255), (int)(IG * 255) > 255 ? 255 : (int)(IG * 255),
                (int)(IB * 255) > 255 ? 255 : (int)(IB * 255));
        }
    }
}
