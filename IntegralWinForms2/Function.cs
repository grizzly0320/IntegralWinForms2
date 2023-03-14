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
        private double max, min;
        public delegate double FunctionDelegate(double x);

        public double A => a;
        public double B => b;
        public double Min => min;
        public double Max => max;

        public Function(double a, double b, FunctionDelegate func)
        {
            this.a = a;
            this.b = b;
            this.max = ToFindMax(func);
            this.min = ToFindMin(func);
        }
        //public List<int> ToFill(FunctionDelegate func)
        //{   
        //    List<int> list = new List<int>();
        //    int intervalX = (Math.Abs((int)Math.Truncate(this.B) - (int)Math.Truncate(this.A)));
        //    int intervalY = (Math.Abs((int)Math.Truncate(this.Max) - (int)Math.Truncate(this.Min)));
        //    for (double i = this.A; i <= this.B;)
        //    {
        //        list.Add(Translation(func, i));
        //        i+= 426 / intervalY;
        //    }
        //    return list;
        //}
        //public int Translation(FunctionDelegate func, double n)
        //{
        //    int intervalX = (Math.Abs((int)Math.Truncate(this.B) - (int)Math.Truncate(this.A)));
        //    int intervalY = (Math.Abs((int)Math.Truncate(this.Max) - (int)Math.Truncate(this.Min)));
        //    n = func(n);
        //    n *= intervalY / 426; // pictureBox1.width
        //    return (int)n;
        //}
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
