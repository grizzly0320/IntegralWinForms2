using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static IntegralWinForms2.Function;

namespace IntegralWinForms2
{
    internal class Painter
    {
        Graphics graphics;
        private int width;
        private int height;
        Function function;

        public int Width => width;
        public int Height => height;
        public Graphics Graphics => graphics;
        public Painter(int width, int hieght) 
        {
            this.width = width;
            this.height = hieght;
            this.function = new Function(-3.0, 3.0, (x) => x * x);
        }
        public void FillArea(Graphics graphics)
        {
            Point[] pointsFunction = new Point[this.Width];
            pointsFunction = GetFunctionPoints();
            Point[] pointstAbscissaAxis = new Point[this.Width];
            pointstAbscissaAxis = GetAbscissaAxisPoints();
            for (int i = 0; i < this.Width; i++)
            {
                graphics.DrawLine(new Pen(Color.FromArgb(228, 181, 175)), pointsFunction[i], pointstAbscissaAxis[i]);
            }
        }
        public void ToDrawFunction(Graphics graphics)
        {
            Point[] points = GetFunctionPoints();
            graphics.DrawLines(new Pen(Color.FromArgb(193, 124, 121)), points);
        }
        // Перенести в класс Function
        public Point[] GetFunctionPoints()
        {
            List<double> list = ToFill((x) => x * x);
            Point[] points = new Point[this.Width];
            for (int i = 0; i < this.Width; i++)
            {
                points[i].X = i;
                points[i].Y = this.Height - (int)list[i];
            }
            return points;
        }
        // Перенести в класс Function
        public Point[] GetAbscissaAxisPoints()
        {
            Point[] points = new Point[this.Width];
            int intervalY = (Math.Abs((int)Math.Truncate(this.function.Max) - (int)Math.Truncate(this.function.Min)));
            if (this.function.Min >= 0)
                for (int i = 0; i < this.Width; i++)
                {
                    points[i].X = i;
                    points[i].Y = this.Height - 2;
                }
            else if (this.function.Max <= 0)
                for (int i = 0; i < this.Width; i++)
                {
                    points[i].X = i;
                    points[i].Y = 2;
                }
            else
                for (int i = 0; i < this.Width; i++)
                {
                    points[i].X = i;
                    points[i].Y = (this.Width / intervalY) * (intervalY / 2) + 2;
                }
            return points;
        }
        public List<double> ToFill(FunctionDelegate func)
        {
            //Сколько пикселей в сантиметре
            double px_cmX = this.width / this.function.DistanceX;
            double px_cmY = this.width / this.function.DistanceY;
            //Сколько сантиметров в пикселе
            double cm_pxX = this.function.DistanceX / this.width;
            double cm_pxY = this.function.DistanceY / this.width;

            List<double> list = new List<double>();
            for (double i = this.function.A; i <= this.function.B;)
            {
                list.Add(Translation(func, i));
                i += cm_pxX;
            }
            return list;
        }
        public double Translation(FunctionDelegate func, double n)
        {
            //Сколько пикселей в сантиметре
            double px_cmX = this.width / this.function.DistanceX;
            double px_cmY = this.width / this.function.DistanceY;
            //Сколько сантиметров в пикселе
            double cm_pxX = this.function.DistanceX / this.width;
            double cm_pxY = this.function.DistanceY / this.width;
            //return (int)(func(n) * px_cmY);
            return (func(n) * px_cmY);
        }
        public void DrawGrid(Graphics graphics) 
        {
            double distanceX = Math.Abs(this.function.B - this.function.A);
            double distanceY = Math.Abs(this.function.Max - this.function.Min);
            int intervalX = (Math.Abs((int)Math.Truncate(this.function.B) - (int)Math.Truncate(this.function.A)));
            int intervalY = (Math.Abs((int)Math.Truncate(this.function.Max) - (int)Math.Truncate(this.function.Min)));
            for (int i = 0; i <= this.Width; i++)
            {
                graphics.DrawLine(new Pen(Color.FromArgb(196, 207, 201)), i, 0, i, this.Height);
                i += this.Width / intervalX;
            }
            for (int i = 0; i < this.Height;)
            {
                graphics.DrawLine(new Pen(Color.FromArgb(196, 207, 201)), 0, i, this.Width, i);
                i += this.Height / intervalY;
            }
        }
        public void ToDrawAbscissaAxis(Graphics graphics)
        {
            int intervalY = (Math.Abs((int)Math.Truncate(this.function.Max) - (int)Math.Truncate(this.function.Min)));
            if (this.function.Min >= 0)
                graphics.DrawLine(new Pen(Color.Black, 4f), 0, this.Height-2, this.Width, this.Height-2);
            else if (this.function.Max <= 0)
                graphics.DrawLine(new Pen(Color.Black, 4f), 0, 2, this.Width, 2);
            else
                graphics.DrawLine(new Pen(Color.Black, 4f), 0, (this.Width / intervalY) * (intervalY / 2) + 2,
                    this.Width, (this.Width / intervalY) * (intervalY / 2) + 2);
        }
        public void ToDrawOrdinateAxis(Graphics graphics)
        {
            int intervalX = (Math.Abs((int)Math.Truncate(this.function.B) - (int)Math.Truncate(this.function.A)));
            if (this.function.A >= 0)
                graphics.DrawLine(new Pen(Color.Black, 4f), 2, 0, 2, this.Height);
            else if (this.function.B <= 0)
                graphics.DrawLine(new Pen(Color.Black, 4f), 2, 0, 2, this.Height);
            else
                graphics.DrawLine(new Pen(Color.Black, 4f), (this.Width / intervalX) * (intervalX / 2) + 2, 0,
                    (this.Width / intervalX) * (intervalX / 2) + 2, this.Height);
        }
    }
}
