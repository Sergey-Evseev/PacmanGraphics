using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
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
        //МЕТОД-ОБРАБОТЧИК ТАЙМЕРА

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
            if (pacman.Top < -10) //при уходе вверх
            {
                pacman.Top = 550;
            }
            if (pacman.Top > 550) // при уходе вниз
            {
                pacman.Top = 0;
            }
            foreach (Control x in this.Controls) //перебирая все контролы формы
                //на каждой итерации таймера
            {
                if (x is PictureBox) //отбираем только пикчербоксы
                {
                    //ВЗАИМОДЕЙСТВИЕ С МОНЕТАМИ
                    //приводим к строке и сравниваем тег контрола
                    //счет увеличивается только когда объект видимый
                    //чтобы счет увеличивался только один раз
                    if ((string)x.Tag == "coin" && x.Visible == true) 
                    {
                        //если границы игрока пересекаются с монетой
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {   
                            score+=1; //счет увеличить на единицу
                            x.Visible = false; //монету сделать невидимой
                        }
                    }
                    //ВЗАИМОДЕЙСТВИЕ С ВНУТРЕННИМИ СТЕНАМИ
                    if ((string)x.Tag == "wall")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            //игра закончена
                            //this.Close();
                        }
                    }
                    //взаимодействие с привидениями
                    if ((string)x.Tag == "ghost")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        { //игра закончена
                        
                        }
                    }
                }    
            } // end of: foreach PictureBox

            //MOVING GHOSTS
            //поведение красного госта
            //по счету таймера менять положение госта на величину его скорости
            redGhost.Left += redGhostSpeed;
            //Bounds returns rectangle with location and sizes of object
            if (redGhost.Bounds.IntersectsWith(pictureBox1.Bounds) ||
                redGhost.Bounds.IntersectsWith(pictureBox2.Bounds))
                { //при соприкосновении со стенками менять направление скороси
                redGhostSpeed = -redGhostSpeed;
                }

            if (score == 46)
            { //игра закончена
            
            }

        } //end of: private void mainGameTimer ======================

        // обработчик таймера с ограничением движения игрока стенками 
        //private void mainGameTimer(object sender, EventArgs e)
        //{ 
        //    txtScore.Text = "Score: " + score;

        //    if (goLeft && pacman.Left > 0)
        //    {                   
        //        pacman.Image = Properties.Resources.left;                
        //        pacman.Left -= playerSpeed;
        //    }
        //    if (goRight && pacman.Left < 635)
        //    {
        //        pacman.Image = Properties.Resources.right;
        //        pacman.Left += playerSpeed;                
        //    }
        //    if (goUp && pacman.Top > 0)
        //    {
        //        pacman.Image = Properties.Resources.Up;
        //        pacman.Top -= playerSpeed;                
        //    }
        //    if (goDown && pacman.Top < 510)
        //    {
        //        pacman.Image = Properties.Resources.down;
        //        pacman.Top += playerSpeed;                
        //    }            
        //} //end of: private void mainGameTimer

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

            //сделать все элементы (coins) видимыми
            foreach (Control x in this.Controls) 
                if (x is PictureBox)
                {
                    x.Visible = true;
                }

            gameTimer.Start(); //таймер запускается
        }

        //accepts message either 'win' or 'lost'
        private void gameOver(string message)
        { 
        
        }
    }
}
