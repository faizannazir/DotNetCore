namespace Calculator
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
        }
        float num1,ans;
        int op;
        private void Calculator_Load(object sender, EventArgs e)
        {

        }

        private void zero_Click(object sender, EventArgs e)
        {
            if (result.Text == "∞" || result.Text=="0")
            {
              result.Text = "0";
            }
            else
            {
                result.Text =result.Text + 0;
            }
        }

        private void one_Click(object sender, EventArgs e)
        {
            if (result.Text != "0" && result.Text != "∞")
            { result.Text = result.Text + 1;}
            else
            {
               result.Text = "1";
            }
                
        }
        private void two_Click(object sender, EventArgs e)
        {
            if (result.Text != "0" && result.Text != "∞")
            { result.Text = result.Text + 2; }
            else
            {
                result.Text = "2";
            }
        }

        private void three_Click(object sender, EventArgs e)
        {
            if (result.Text != "0" && result.Text != "∞" )
            { result.Text = result.Text + 3; }
            else
            {
                result.Text = "3";
            }
        }

        private void four_Click(object sender, EventArgs e)
        {
            if (result.Text != "0" && result.Text != "∞")
            { result.Text = result.Text + 4; }
            else
            {
                result.Text = "4";
            }
        }
        private void five_Click(object sender, EventArgs e)
        {
            if (result.Text != "0" && result.Text != "∞")
            { result.Text = result.Text + 5; }
            else
            {
                result.Text = "5";
            }
        }


        private void six_Click(object sender, EventArgs e)
        {
            if (result.Text != "0" && result.Text != "∞")
            { result.Text = result.Text + 6; }
            else
            {
                result.Text = "6";
            }
        }

        private void seven_Click(object sender, EventArgs e)
        {
            if (result.Text != "0" && result.Text != "∞")
            { result.Text = result.Text + 7; }
            else
            {
                result.Text = "7";
            }
        }

        private void eight_Click(object sender, EventArgs e)
        {
            if (result.Text != "0" && result.Text != "∞")
            { result.Text = result.Text + 8; }
            else
            {
                result.Text = "8";
            }
        }

        private void nine_Click(object sender, EventArgs e)
        {
            if (result.Text != "0" && result.Text != "∞")
            { result.Text = result.Text + 9; }
            else
            {
                result.Text = "9";
            }
        }

        private void plus_Click(object sender, EventArgs e)
        {
            if (result.Text != "∞" )
            {
                op = 1;
                num1 = float.Parse(result.Text);
                result.Clear();
                result.Focus();
            }
        }

        private void minus_Click(object sender, EventArgs e)
        {
            if (result.Text != "∞")
            {
                op = 2;
                num1 = float.Parse(result.Text);
                result.Clear();
                result.Focus();
            }
        }

        private void multiply_Click(object sender, EventArgs e)
        {
            if (result.Text != "∞")
            {
                op = 3;
                num1 = float.Parse(result.Text);
                result.Clear();
                result.Focus();
            }
        }

        private void divide_Click(object sender, EventArgs e)
        {
            if (result.Text != "∞")
            {
            op = 4;
            num1 = float.Parse(result.Text);
            result.Clear();
            result.Focus();
            }

        }

        private void result_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            result.Text = "0";
            num1 = 0;
        }

        private void equal_Click(object sender, EventArgs e)
        {
            switch (op)
            {
                case 1:
                    //Add
                    ans = num1 + float.Parse(result.Text);
                    num1 = ans;
                    
                    break;
                case 2:
                    //Sub
                    ans = num1 - float.Parse(result.Text);
                    num1 = ans;
                   
                    break;
                case 3:
                    //Mul
                    ans = num1 * float.Parse(result.Text);
                    num1 = ans;
                   
                    break;
                case 4:
                    //Div
                    ans = num1 / float.Parse(result.Text);
                    num1 = ans;
                    break;
                default:
                    result.Text = "0";
                    break;
            }
            result.Text = ans.ToString();
        }
    }
}