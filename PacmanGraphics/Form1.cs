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
        //когда клавиша направления отпущена переменные направления 
        //меняются на false 
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
        //метод-обработчик таймера
        private void mainGameTimer(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;

            if (goLeft) 
            {   //при нажатии влево уменьшать координату X на скорость
                pacman.Left -= playerSpeed;
                pacman.Image = Properties.Resources.left;
            }
            if (goRight) 
            {
                pacman.Left += playerSpeed;
                pacman.Image = Properties.Resources.right;
            }
            if (goUp) 
            { 
                pacman.Top -= playerSpeed;
                pacman.Image = Properties.Resources.Up;
            }
            if (goDown) 
            {
                pacman.Top += playerSpeed;
                pacman.Image = Properties.Resources.down;
            }
            //телепорт на правую сторону при уходе в левую стенку
            if (pacman.Left < -10)
            {
                pacman.Left = 680;
            }
            //телепорт налево при уходе в правую стенку
            if (pacman.Left > 680)
            {
                pacman.Left = -10;
            }
        }

        private void resetGame()
        {
            txtScore.Text = "Score: 0";
            score = 0;

            redGhostSpeed = 5;
            yellowGhostSpeed = 5;
            pinkGhostX = 5;
            pinkGhostY = 5;
            playerSpeed = 8;

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

            gameTimer.Start(); //таймер запускается
        }

        //accept message either 'win' or 'lost'
        private void gameOver(string message)
        { 
        
        }
    }
}
