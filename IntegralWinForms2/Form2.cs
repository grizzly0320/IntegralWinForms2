using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegralWinForms2
{
    public partial class Form2 : Form
    {
        Painter painter;
        Graphics graphics;
        public Form2()
        {
            InitializeComponent();
            graphics = pictureBox1.CreateGraphics();
            painter = new Painter(pictureBox1.Width, pictureBox1.Height);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            painter.FillArea(e.Graphics);
            painter.DrawGrid(e.Graphics);
            painter.ToDrawAbscissaAxis(e.Graphics);
            painter.ToDrawOrdinateAxis(e.Graphics);
            painter.ToDrawFunction(e.Graphics);
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            Graphics graphics = pictureBox1.CreateGraphics();
            painter = new Painter(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Refresh();
            painter.FillArea(graphics);
            painter.DrawGrid(graphics);
            painter.ToDrawAbscissaAxis(graphics);
            painter.ToDrawOrdinateAxis(graphics);
            painter.ToDrawFunction(graphics);
        }

    }
}
