using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                result.Text = ($"Реузльтат:\n");
                string curPatch = Environment.CurrentDirectory;
                double a = double.Parse(textBox1.Text);
                double b = double.Parse(textBox2.Text);
                if (a > b)
                {
                    MessageBox.Show(
                        "Нарушен порядок ввода диапазона записываемых чисел, меняю местами",
                        "Предупреждение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button3);
                    double buf = a;
                    textBox1.Text = b.ToString();
                    a = b;
                    textBox2.Text = buf.ToString();
                    b = buf;
                }
                double h = double.Parse(textBox3.Text);

                FileStream f = new FileStream($"{curPatch}/tmp.dat", FileMode.Create);
                BinaryWriter fOut = new BinaryWriter(f);
                for (double i = a; i <= b; i += h)
                {
                    fOut.Write(i);
                }
                fOut.Close();

                f = new FileStream($"{curPatch}/tmp.dat", FileMode.Open);
                BinaryReader fIn = new BinaryReader(f);
                long m = f.Length;

                double startIgnor = double.Parse(textBox4.Text);
                double stopIgnor = double.Parse(textBox5.Text);

                if (startIgnor > stopIgnor)
                {
                    MessageBox.Show(
                        "Нарушен порядок ввода диапазона игнорируемых чисел, меняю местамии",
                        "Предупреждение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button3);
                    double buf = startIgnor;
                    textBox4.Text = stopIgnor.ToString();
                    startIgnor = stopIgnor;
                    textBox5.Text = buf.ToString();
                    stopIgnor = buf;
                }
                double tmpDigit = 0;


                for (long i = 0; i < m; i += 8)
                {
                    f.Seek(i, SeekOrigin.Begin);
                    tmpDigit = Math.Round(fIn.ReadDouble(), 2);
                    if (tmpDigit >= startIgnor && tmpDigit <= stopIgnor)
                    {
                        result.Text += (".");
                    }
                    else
                    {
                        result.Text += ($"{tmpDigit:f2} ");
                    }
                }

                Console.Write("\n\n");
                fIn.Close();
                f.Close();
            }
            catch (Exception)
            {
                MessageBox.Show(
                         "Вы ввели не число",
                         "Предупреждение",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Warning,
                         MessageBoxDefaultButton.Button3);
            }
        }
    }
}