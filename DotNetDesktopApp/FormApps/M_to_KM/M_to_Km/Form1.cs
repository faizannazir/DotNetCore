namespace M_to_Km
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        float km = 0;
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                    {
                     km = float.Parse(textBox1.Text);
                     }
                catch
                {
                    textBox1.Text = "0";
                }
                


            }
            textBox2.Text = (km * 1000).ToString();
        }
    }
}