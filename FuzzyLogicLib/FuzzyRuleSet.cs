using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FuzzyLogicLib
{
    public class FuzzyRuleSet
    {
        private List<FuzzyRule> _rules = new List<FuzzyRule>();

        public void AddRule(FuzzyRule low)
        {
            _rules.Add(low);
        }

        internal PointF Evaluate(float value)
        {
            foreach (FuzzyRule rule in _rules)
            {
                if (value < rule.MaxX && value > rule.MinX)
                {
                    PointF[] result = new PointF[rule.Points.Length];
                    PointF p1 = rule.Points.Where(x => x.X < value).Last();
                    PointF p2 = rule.Points.Where(x => x.X >= value).First();

                    float a = (p2.Y - p1.Y) / (p2.X - p1.X);
                    float b = p2.Y - (a * p2.X);

                    float multiplier = (a * value + b) / rule.MaxY;
                    for (int i = 0; i < result.Length; i++)
                    {
                        result[i] = new PointF(rule.Points[i].X, rule.Points[i].Y * multiplier);
                    }

                    rule.Result = result;
                }
                else
                {
                    rule.Result = new PointF[rule.Points.Length];
                }
            }

            return CalculateCentroid();
        }

        private PointF CalculateCentroid()
        {
            PointF centroid = new PointF(0, 0);
            float signedArea = 0.0f;
            float x0 = 0.0f; // Current vertex X
            float y0 = 0.0f; // Current vertex Y
            float x1 = 0.0f; // Next vertex X
            float y1 = 0.0f; // Next vertex Y
            float a = 0.0f;  // Partial signed area

            foreach (FuzzyRule rule in _rules)
            {
                int i = 0;
                for (; i < rule.Result.Length - 1; i++)
                {
                    x0 = rule.Result[i].X;
                    y0 = rule.Result[i].Y;
                    x1 = rule.Result[i + 1].X;
                    y1 = rule.Result[i + 1].Y;
                    a = x0 * y1 - x1 * y0;
                    signedArea += a;
                    centroid.X += (x0 + x1) * a;
                    centroid.Y += (y0 + y1) * a;
                }

                x0 = rule.Result[i].X;
                y0 = rule.Result[i].Y;
                x1 = rule.Result[0].X;
                y1 = rule.Result[0].Y;
                a = x0 * y1 - x1 * y0;
                signedArea += a;
                centroid.X += (x0 + x1) * a;
                centroid.Y += (y0 + y1) * a;
            }

            signedArea *= 0.5f;
            centroid.X /= (6.0f * signedArea);
            centroid.Y /= (6.0f * signedArea);

            return centroid;
        }

        //Point2D compute2DPolygonCentroid(const Point2D* vertices, int vertexCount)
        //{
        //    Point2D centroid = {0, 0};
        //    double signedArea = 0.0;
        //    double x0 = 0.0; // Current vertex X
        //    double y0 = 0.0; // Current vertex Y
        //    double x1 = 0.0; // Next vertex X
        //    double y1 = 0.0; // Next vertex Y
        //    double a = 0.0;  // Partial signed area

        //    // For all vertices except last
        //    int i=0;
        //    for (i=0; i<vertexCount-1; ++i)
        //    {
        //        x0 = vertices[i].x;
        //        y0 = vertices[i].y;
        //        x1 = vertices[i+1].x;
        //        y1 = vertices[i+1].y;
        //        a = x0*y1 - x1*y0;
        //        signedArea += a;
        //        centroid.x += (x0 + x1)*a;
        //        centroid.y += (y0 + y1)*a;
        //    }

        //    // Do last vertex
        //    x0 = vertices[i].x;
        //    y0 = vertices[i].y;
        //    x1 = vertices[0].x;
        //    y1 = vertices[0].y;
        //    a = x0*y1 - x1*y0;
        //    signedArea += a;
        //    centroid.x += (x0 + x1)*a;
        //    centroid.y += (y0 + y1)*a;

        //    signedArea *= 0.5;
        //    centroid.x /= (6.0*signedArea);
        //    centroid.y /= (6.0*signedArea);

        //    return centroid;
        //}
    }
}