using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace TB_Save_Editor
{
    public partial class RouteGraph : Form
    {
        List<float> x;
        List<float> y;

        float mapRatio;
        //Thread thread;
        Graphics g;
        Pen p;
        SolidBrush sb;

        public RouteGraph()
        {
            InitializeComponent();
            x = new List<float>();
            y = new List<float>();
            //mapRatio = 274.7928571f;
            mapRatio = 261.4554167f;

            //mapRatio = (274.7928571f+ 261.4554167f)/2f;

            //thread = new Thread(new ThreadStart(drawRoute));
            //thread.Start();

            g = pictureBox1.CreateGraphics();
            p = new Pen(Color.White);
            sb = new SolidBrush(Color.Red);
        }

        private void RouteGraph_Load(object sender, EventArgs e)
        {
            zoomIN.Hide();
            zoomOUT.Hide();


            panel1.SetBounds(5,5,this.Width-30,this.Height-120);
            zoomIN.SetBounds(((this.Width / 2) - 60), this.Height - 110, 60, 60);
            zoomOUT.SetBounds(zoomIN.Right + 10, this.Height - 110, 60, 60);

            label1.Top = this.Height - 100;
            label2.Top = this.Height - 100;
            textBox1.Top = this.Height - 100;
            button1.Top = this.Height - 100;



        }
        private void RouteGraph_SizeChange(object sender, EventArgs e)
        {
            zoomIN.SetBounds(((this.Width / 2) - 60), this.Height- 110, 60, 60);
            zoomOUT.SetBounds(zoomIN.Right + 10, this.Height - 110, 60, 60);
            panel1.SetBounds(5, 5, this.Width - 30, this.Height - 120);

            label1.Top = this.Height - 100;
            label2.Top = this.Height - 100;
            textBox1.Top = this.Height - 100;
            button1.Top = this.Height - 100;

            /*for (int i = 1; i < x.Count; i++)
            {
                pictureBox1.CreateGraphics().DrawLine(new Pen(Brushes.White, 3), x[i - 1], y[i - 1], x[i], y[i]);
            }*/

            //System.Console.WriteLine(panel1.Height + " " + panel1.Width);

        }

        private void RouteGraph_Click(object sender, EventArgs e)
        {
            Point point = pictureBox1.PointToClient(Cursor.Position);
            label2.Text = point.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //pictureBox1.CreateGraphics().DrawLine(new Pen(Brushes.White,3),1334,341,1395,2335);
            //pictureBox1.CreateGraphics().
            x.Clear();
            y.Clear();

            setCordinats();

            //g.Clear();



            //ThreadExceptionDialog 
            for (int i = 1; i < x.Count; i +=3)
            {
                //button2.SetBounds((int)x[i], (int)y[i], 7, 7);
                pictureBox1.CreateGraphics().DrawLine(new Pen(Brushes.White, 3), x[i - 1], y[i - 1], x[i], y[i]);
                g.DrawEllipse(p, x[i], y[i], 3, 3);
                g.FillEllipse(sb, x[i], y[i], 3, 3);
                

            }

        }

        public void drawRoute()
        {

            
           
        }

        public void setCordinats()
        {
            //int pointCount = 0;

            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("invalid job id", "Error");
                MessageBox.Show("tracking.bin file can't find", "Error");
            }
            else if (!File.Exists(@"output\upl_" + textBox1.Text + ".bin"))
            {
                MessageBox.Show("upl file can't find", "Error");
            }

            else
            {
                List<float> num1 = new List<float>();
                List<float> num2 = new List<float>();
                List<short> num3 = new List<short>();
                List<short> num4 = new List<short>();
                List<short> num5 = new List<short>();
                List<int> date = new List<int>();

                BinaryReader br = new BinaryReader(File.Open(@"output\upl_" + textBox1.Text + ".bin", FileMode.Open));

                while (true)
                {
                    try
                    {
                        num1.Add(br.ReadSingle());
                        num2.Add(br.ReadSingle());
                        num3.Add(br.ReadInt16());
                        num4.Add(br.ReadInt16());
                        num5.Add(br.ReadInt16());
                        date.Add(br.ReadInt32());

                    }
                    catch (Exception exception)
                    {
                        //Console.WriteLine(exception);
                        break;
                    }

                }

                br.Close();

                for (int i=0;i<num1.Count;i++)
                {
                    this.x.Add(((4889.7f - num1[i])/(mapRatio*-1)) + 321);
                    this.y.Add(((-51894.2f - num2[i]) / (mapRatio*-1)) + 82);
                }
            }
        }

        private void RouteGraph_Scroll(object sender, ScrollEventArgs e)
        {
            /*for (int i = 1; i < x.Count; i ++)
            {
                pictureBox1.CreateGraphics().DrawLine(new Pen(Brushes.White, 3), x[i - 1], y[i - 1], x[i], y[i]);
            }*/
        }

        private void RouteGraph_FormClosing(object sender, FormClosingEventArgs e)
        {
            //thread.Abort();
        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
           /* for (int i = 1; i < x.Count; i++)
            {
                
                pictureBox1.CreateGraphics().DrawLine(new Pen(Brushes.White, 3), x[i - 1], y[i - 1], x[i], y[i]);
            }*/
        }
    }
}
