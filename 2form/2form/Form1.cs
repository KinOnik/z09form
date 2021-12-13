using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace _2form
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
				result.Text = "";
				string curPatch = Environment.CurrentDirectory;
				int k1 = int.Parse(textBox1.Text);
				int k2 = int.Parse(textBox2.Text);
				if (k1 > k2)
				{
					MessageBox.Show(
							 "нарушены границы индексов элементов(k2>k1), меняю местами",
							 "Предупреждение",
							 MessageBoxButtons.OK,
							 MessageBoxIcon.Warning,
							 MessageBoxDefaultButton.Button3);
					int buffer = k1;
					k1 = k2;
					k2 = buffer;
				}
				string[] buf = System.IO.File.ReadAllLines($"{curPatch}/text.txt");
				for (int i = 0; i < buf.Length; i++)
				{
					if (buf[i].Length >= k2)
					{
						result.Text += ($"Символы строки №{i + 1}: ");
						for (int j = k1 - 1; j < k2; j++)
						{
							result.Text += ($"{buf[i][j]}");
						}
						result.Text += ($"\n");
					}
					else if (buf[i].Length >= k1 && buf[i].Length < k2)
					{
						result.Text += ($"В строке №{i + 1} меньше символов, чем вы ввели: ");
						for (int j = k1 - 1; j < buf[i].Length; j++)
						{
							result.Text += ($"{buf[i][j]}");
						}
						result.Text += ($"\n");
					}
					else
					{
						result.Text += ($"В строке №{ i + 1} нет символов с такими индексом в строке\n");
					}
				}
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