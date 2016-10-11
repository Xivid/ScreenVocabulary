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

namespace ScreenVocabulary
{
    public partial class ScreenVocabulary : Form
    {
        private bool isCounting = false;
        private int timeLeft;
        StreamReader reader;
        List<string> words;
        List<string> meanings;
        private bool random = true;
        private Random rnd;
        int curIndex, totNum;

        public ScreenVocabulary()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.Show();
            reader = new StreamReader(File.OpenRead(@"3000.txt"));
            words = new List<string>();
            meanings = new List<string>();
            totNum = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split('\t');
                
                words.Add(values[0]);
                meanings.Add(values[1]);

                ++totNum;
            }
            rnd = new Random();
            if(random)
            {
                curIndex = rnd.Next(0, totNum);
            }
            else
            {
                curIndex = 0;
            }
            lblWord.Text = words[curIndex];
            lblMeaning.Text = meanings[curIndex];
            isCounting = true;
            timeLeft = 60;
            timer1.Start();
        }

        private void btnCtrl_Click(object sender, EventArgs e)
        {
            isCounting = !isCounting;
            btnCtrl.Text = isCounting ? "Pause" : "Resume";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isCounting == true)
            {
                if (timeLeft > 0)
                {
                    --timeLeft;
                    lblCountdown.Text = "0:" + timeLeft.ToString("D2");
                }
                else
                {
                    timeLeft = 60;
                    lblCountdown.Text = "1:00";
                    if (random)
                    {
                        curIndex = rnd.Next(0, totNum);
                    }
                    else
                    {
                        if (++curIndex == totNum)
                        {
                            curIndex = 0;
                        }
                    }
                    lblWord.Text = words[curIndex];
                    lblMeaning.Text = meanings[curIndex];
                }
            }
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = trackBar1.Value;
        }
    }
}
