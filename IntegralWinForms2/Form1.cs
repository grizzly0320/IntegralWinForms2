namespace IntegralWinForms2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Show_Click(object sender, EventArgs e)
        {
            var myForm = new Form2();
            myForm.Show();
            Hide();
        }
    }
}