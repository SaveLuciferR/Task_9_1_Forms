using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_9_1_Forms
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			textBox1.Text = "";
			textBox2.Text = "";
			textBox3.Text = "";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			{
				try
				{
					string num = textBox1.Text;
					Regex regex = new Regex(@"(-|)(\d+)");

					for (int i = 0; i < num.Length; i++)
					{
						if (!(char.IsDigit(num[i]) || num[i] == ' ' || num[i] == '-'))
						{
							throw new Exception();
						}
					}

					FileStream f = new FileStream("t.dat", FileMode.OpenOrCreate);
					BinaryWriter fOut = new BinaryWriter(f);

					int n = Convert.ToInt32(textBox2.Text);
					foreach (Match it in regex.Matches(num))
					{
						if (it.Success && (Convert.ToInt32(it.Value) % n != 0))
						{
							fOut.Write(Convert.ToInt32(it.Value));
						}
					}
					fOut.Close();

					f = new FileStream("t.dat", FileMode.Open);
					BinaryReader fIn = new BinaryReader(f);
					long m = f.Length;

					textBox3.Text = "Числа, которые не кратны делителю\r\n";

					using (StreamWriter sw = new StreamWriter(File.Open("convert.txt", FileMode.Create), Encoding.UTF8))
					{
						for (long i = 0; i < m; i += 4)
						{
							f.Seek(i, SeekOrigin.Begin);
							int a = fIn.ReadInt32();
							textBox3.Text += a + " ";
							sw.Write(a + " ");
						}
					}
					f.Close();
					fIn.Close();
				}
				catch (FormatException)
				{
					textBox3.Text = ("Введите корректные числа");
				}
				catch
				{
					textBox3.Text = "В строке должны быть только целые числа и пробелы";
				}
			}
		}
	}
}
