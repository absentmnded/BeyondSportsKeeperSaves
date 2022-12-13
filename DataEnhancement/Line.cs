using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEnhancement
{ 
    public static class AdditionalFunctions
    {
        public static bool BallApproachingDangerZone(Line TL, Line BL, Point ballPoint)
        {
            // check if the point is under the top line and bottom the lower line
            var checkTL = TL.PlacePoint(ballPoint);
            var checkBL = BL.PlacePoint(ballPoint);
            if ((checkBL > 0) && (checkTL < 0))
                return true;
            else return false;
        }
    }
    public class Line
    {
        public float A { get; set; }
        public float B { get; set; }
        public float C { get; set; }

        public Line() { }
        public Line(Point p1, Point p2)
        {
            A = p2.Y - p1.Y;
            B = p1.X - p2.X;
            C = p2.X * p1.Y - p1.X * p2.Y;
        }

        public float PlacePoint(Point p)
        {
            return A * p.X + B * p.Y + C;
        }

    }

    public class Point
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Point(float x, float y) 
        {
            X = x;
            Y = y;
            Z = 0;
        }

        public Point(float x, float y, float z) : this(x, y)
        {
            Z = z;
        }

        public float Distance(Point p)
        {
            return (float)Math.Round(Math.Sqrt(Math.Pow(this.X - p.X, 2) + Math.Pow(this.Y - p.Y, 2)));
        }

        public float Distance(Line l)
        {
            return (float)(Math.Abs(l.A * X + l.B * Y + l.C)/Math.Sqrt(l.A*l.A + l.B*l.B));
        }
    }
}
