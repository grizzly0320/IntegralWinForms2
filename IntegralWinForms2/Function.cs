using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegralWinForms2
{
    internal class Function
    {
        // Левая и правая граница
        private double a, b;
        private int width, height;
        private double max, min;
        public delegate double FunctionDelegate(double x);
        private double distanceX;
        private double distanceY;
        public double A => a;
        public double B => b;
        public double Min => min;
        public double Max => max;
        public double DistanceX => distanceX;
        public double DistanceY => distanceY;

        public Function(double a, double b, FunctionDelegate func)
        {
            this.a = a;
            this.b = b;
            this.max = ToFindMax(func);
            this.min = ToFindMin(func);
            this.distanceX = Math.Abs(b - a);
            this.distanceY = Math.Abs(this.max - this.min);
        }
        public double ToFindMax(FunctionDelegate func)
        {
            double maxValue = Math.Min(func(this.A), func(this.B));
            double maxValueOx = 0;
            double epsilon = 1.0;
            for (double i = this.A; i <= this.B; i++)
            {
                if (func(i) >= maxValue)
                {
                    maxValueOx = i;
                    maxValue = func(i);
                }
            }
            while (epsilon != 0.001)
            {
                for (double i = maxValueOx - epsilon; i <= maxValueOx + epsilon;)
                {
                    if (func(i) > maxValue && func(i) <= func(Math.Max(this.A, this.B)))
                        maxValue = func(i);
                    i += epsilon / 10.0;
                }
                epsilon /= 10.0;
            }
            return maxValue;
        }

        public double ToFindMin(FunctionDelegate func)
        {
            double minValue = Math.Max(func(this.A), func(this.B));
            double minValueOx = 0;
            double epsilon = 1.0;
            for (double i = this.A; i <= this.B; i++)
            {
                if (func(i) < minValue)
                    minValueOx = i;
                minValue = func(i);
            }
            while (epsilon != 0.001)
            {
                for (double i = minValueOx - epsilon; i <= minValueOx + epsilon;)
                {
                    if (func(i) < minValue && func(i) <= func(Math.Min(this.A, this.B)))
                        minValue = func(i);
                    i += epsilon / 10;
                }
                epsilon /= 10.0;
            }
            return minValue;
        }
    }
}
