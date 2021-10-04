using System;
using System.Media;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIngPong
{
    public partial class Form1 : Form
    {

        private int speed_vertical = 2;
        private int speed_hor = 2;
        private int speed_vertical1 = 2;
        private int speed_hor1 = 2;
        private int score = 0;
        private int melody = 0;
        private SoundPlayer player = new SoundPlayer();
        private string path = AppDomain.CurrentDomain.BaseDirectory;
        private Random randColor = new Random();
        private Random randX = new Random();
        public Form1()
        {
            InitializeComponent();

            timer.Enabled = true;

            Cursor.Hide();
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;

            this.Bounds = Screen.PrimaryScreen.Bounds;

            gamePanel.Top = background.Bottom - (background.Bottom / 10);
            result.Left  = (background.Width / 2) - (result.Width / 2);
            loseLabel.Visible = false;
            
            loseLabel.Top = (background.Height / 2) - (loseLabel.Height / 2);
            loseLabel.Text = "Ви програли! \nДля наступної спроби натисніть \"R\" \nДля виходу - \"Esc\"";
            loseLabel.Left = (background.Width / 2) - (loseLabel.Width / 2);
            player.SoundLocation = path + "\\music\\1.wav";
            player.Play();

            System.Drawing.Drawing2D.GraphicsPath tmp = new System.Drawing.Drawing2D.GraphicsPath();
            tmp.AddEllipse(0, 0, gameBall.Width, gameBall.Height);
            Region rgn = new Region(tmp);
            gameBall.Region = rgn;
            gameBall1.Region = rgn;
            do
            {
                
                gameBall.BackColor = Color.FromArgb(randColor.Next(150, 255), randColor.Next(150, 255), randColor.Next(150, 255));
                gameBall1.BackColor = Color.FromArgb(randColor.Next(150, 255), randColor.Next(150, 255), randColor.Next(150, 255));
            } while (background.BackColor == gameBall1.BackColor || background.BackColor == gameBall.BackColor);

            do
            {
                gameBall.Top = randX.Next(0, this.Height / 3);
                gameBall.Left = randX.Next(0, this.Width - gameBall.Width);
                speed_hor = 2;
                speed_vertical = 2;
                score = 0;
                gameBall1.Top = randX.Next(1, this.Height / 3);
                gameBall1.Left = randX.Next(1, this.Width - gameBall1.Width);
            } while (gameBall.Bounds.IntersectsWith(gameBall1.Bounds));

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();

            if(e.KeyCode == Keys.R)
            {
                do
                {
                    gameBall.Top = randX.Next(0, this.Height / 3);
                    gameBall.Left = randX.Next(0, this.Width - gameBall.Width);
                    speed_hor = 2;
                    speed_vertical = 2;
                    score = 0;
                    gameBall1.Top = randX.Next(1, this.Height / 3);
                    gameBall1.Left = randX.Next(1, this.Width - gameBall1.Width);
                } while (gameBall.Bounds.IntersectsWith(gameBall1.Bounds));
                


                speed_hor1 = 2;
                speed_vertical1 = 2;
                
                loseLabel.Visible = false;
                timer.Enabled = true;
                result.Text = "Score: 0";
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            gamePanel.Left = Cursor.Position.X - (gamePanel.Width / 2);

            gameBall.Left += speed_hor;
            gameBall.Top += speed_vertical;
            gameBall1.Left += speed_hor1;
            gameBall1.Top += speed_vertical1;


            if (gameBall.Left <= background.Left)
                speed_hor *= -1;

            if (gameBall.Right >= background.Right)
                speed_hor *= -1;

            if (gameBall.Top <= background.Top)
                speed_vertical *= -1;

            if (gameBall.Bottom >= background.Bottom || gameBall1.Bottom >= background.Bottom)
            {
                loseLabel.Visible = true;
                timer.Enabled = false;
            }
            if (gameBall.Bounds.IntersectsWith(gameBall1.Bounds))
            {
                speed_hor *= -1;
                speed_hor1 *= -1;
            }
            if(gameBall.Bounds.IntersectsWith(gamePanel.Bounds))
            {
                speed_hor += 2;
                speed_vertical += 2;
                speed_vertical *= -1;
                score += 1;
                result.Text = "Score: " + score.ToString();

                
                do
                {

                    gameBall.BackColor = Color.FromArgb(randColor.Next(150, 255), randColor.Next(150, 255), randColor.Next(150, 255));
                } while (background.BackColor == gameBall.BackColor);

                if (score % 10 == 0)
                {
                    do
                    {
                        background.BackColor = Color.FromArgb(randColor.Next(150, 255), randColor.Next(150, 255), randColor.Next(150, 255));
                    } while (background.BackColor == gameBall1.BackColor || background.BackColor == gameBall.BackColor);

                    player.Stop();
                    melody++;

                    switch (melody % 3)
                    {
                        case 1:
                            player.SoundLocation = path + "\\music\\2.wav";
                            break;
                        case 2:
                            player.SoundLocation = path + "\\music\\3.wav";
                            break;
                        default:
                            melody = 0;
                            player.SoundLocation = path + "\\music\\1.wav";
                            break;
                    }
                    player.Play();
                }

            }

            if (gameBall1.Left <= background.Left)
                speed_hor1 *= -1;

            if (gameBall1.Right >= background.Right)
                speed_hor1 *= -1;

            if (gameBall1.Top <= background.Top)
                speed_vertical1 *= -1;

            if (gameBall1.Bounds.IntersectsWith(gamePanel.Bounds))
            {
                speed_hor1 += 2;
                speed_vertical1 += 2;
                speed_vertical1 *= -1;
                score += 1;
                result.Text = "Score: " + score.ToString();


                do
                {

                    gameBall1.BackColor = Color.FromArgb(randColor.Next(150, 255), randColor.Next(150, 255), randColor.Next(150, 255));
                } while (background.BackColor == gameBall1.BackColor);

                if (score % 10 == 0)
                {
                    do
                    {
                        background.BackColor = Color.FromArgb(randColor.Next(150, 255), randColor.Next(150, 255), randColor.Next(150, 255));
                    } while (background.BackColor == gameBall1.BackColor || background.BackColor == gameBall.BackColor);

                    player.Stop();
                    melody++;

                    switch (melody % 3)
                    {
                        case 1:
                            player.SoundLocation = path + "\\music\\2.wav";
                            break;
                        case 2:
                            player.SoundLocation = path + "\\music\\3.wav";
                            break;
                        default:
                            melody = 0;
                            player.SoundLocation = path + "\\music\\1.wav";
                            break;
                    }
                    player.Play();
                }

            }


        }
  
    }
}
