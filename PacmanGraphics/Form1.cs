using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacmanGraphics
{
    public partial class Form1 : Form
    {
        bool goUp, goDown, goLeft, goRight, isGameOver;
        int score, playerSpeed, redGhostSpeed, yellowGhostSpeed,
            pinkGhostX, pinkGhostY;
        
        public Form1()
        {
            InitializeComponent();
            resetGame(); //при загрузке формы игра в первоначальном состоянии
        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
        }

        private void mainGameTimer(object sender, EventArgs e)
        {

        }

        private void resetGame()
        {
            txtScore.Text = "Score: 0";
            score = 0;
            redGhostSpeed = 5;
            yellowGhostSpeed = 5;
            pinkGhostX = 5;
            pinkGhostY = 5;
            isGameOver = false;

            pacman.Left = 31; //координаты героя в начале игры
            pacman.Top = 46;
            redGhost.Left = 189; redGhost.Top = 63;
            pinkGhost.Left = 582; pinkGhost.Top = 248;
            yellowGhost.Left = 444; yellowGhost.Top = 448;

            foreach (Control x in this.Controls) //сделать все элементы видимыми
                if (x is PictureBox)
                {
                    x.Visible = true;
                }

            gameTimer.Start();
        }

        //accept message either 'win' or 'lost'
        private void gameOver(string message)
        { 
        
        }
    }
}
