using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FuzzyLogicLib
{
    public class FuzzyRule
    {
        internal PointF[] Points { get; private set; }

        public PointF CurrentValue { get; private set; }

        internal float MaxY { get; private set; }

        internal float MinX { get; private set; }

        internal float MaxX { get; private set; }

        public FuzzyRule(params PointF[] points)
        {
            this.Points = points;

            float minX = points[0].X;
            float maxX = points[0].X;
            float maxY = points[0].Y;

            foreach (var item in points)
            {
                if (item.X < minX) minX = item.X;
                if (item.X > maxX) maxX = item.X;
                if (item.Y > maxY) maxY = item.Y;
            }

            this.MaxY = maxY;
            this.MaxX = maxX;
            this.MinX = minX;
        }

        internal PointF[] Result { get; set; }
    }
}