using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        Game game;
        int brick_X;
        int bx;
        int by;
        int board_W;
        int board_H;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = Game.Singleton;
            bx = GameRule.Board_X;//보드 폭
            by = GameRule.Board_Y;//보드 넓이
            board_W = GameRule.Pixel_X;//X좌표의 1의 x Pixels
            board_H = GameRule.Pixel_Y;//y좌표의 1의 y Pixels
            //SetClientSizeCore(bx * board_W, by * board_H);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawGraduation(e.Graphics);//선그리는 함수
            DrawBrick(e.Graphics);//블럭 만드는 함수
            DrawBoard(e.Graphics);//보드 그리는 함수
            DoubleBuffered = true;
        }

        private void DrawBoard(Graphics graphics)
        {
            for (int xx = 0; xx < bx; xx++)
            {
                for (int yy = 0; yy < by; yy++)
                {
                    if (game[xx, yy] != 0)
                    {
                        Rectangle rec = new Rectangle(xx * board_W + 2, yy * board_H + 2, board_W - 4, board_H - 4);
                        //graphics.DrawRectangle(Pens.Green, rec);
                        graphics.FillRectangle(Brushes.Red, rec);//쌓인 블럭 표현
                    }
                }
            }
        }

        private void DrawGraduation(Graphics graphics)//선그리는 함수
        {
            Pen pen = new Pen(Color.Black);
           
            for (int x = 0; x < bx; x++)
            {
                for (int y = 0; y < by; y++)
                {
                    Rectangle rec = new Rectangle(x * board_W, y * board_H, board_W, board_H);
                    graphics.DrawRectangle(pen, rec);
                }
            }
        }

        private void DrawBrick(Graphics graphics)//사각형 그리기
        {
            Pen pen = new Pen(Color.Blue, 4);//그릴 펜 설정
            Point now = game.NowPosition;//블럭 위치 설정
            int bn = game.BrickNum;//블럭 모양
            int tn = game.Turn;//블럭 회전 값
            for(int xx=0;xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BrickValue.bvals[bn, tn, xx, yy] != 0)
                    {
                        Rectangle now_rt = new Rectangle((now.X+xx) * board_W + 2, (now.Y+yy) * board_H + 2, board_W - 4, board_H - 4);
                        graphics.DrawRectangle(pen, now_rt);
                        brick_X = now_rt.X;
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    MoveRight();
                    return;
                case Keys.Left:
                    MoveLeft();
                    return;
                case Keys.Space:
                    MoveSDown();
                    return;
                case Keys.Up:
                    if (brick_X < bx * GameRule.Pixel_X / 2)
                    {
                        MoveRight();
                        MoveRight();
                        MoveTurn();
                        MoveLeft();
                        MoveLeft();
                    }
                    else
                    {
                        MoveLeft();
                        MoveTurn();
                        MoveRight();
                    }
                    return;
                case Keys.Down:
                    MoveDown();
                    return;
                case Keys.Escape:
                    timer1_down.Enabled = false;
                    DialogResult re = MessageBox.Show("다시시작", "끝내기", MessageBoxButtons.YesNo);
                    if (re == DialogResult.Yes)
                    {
                        game.Restart();
                        timer1_down.Enabled = true;
                        Invalidate();
                    }
                    else
                    {
                        this.Close();
                    }
                    return;

            }
        }

        private void MoveSDown()//한번에 내려가는 함수
        {
            while(game.MoveDown())
            {
                Region rg = MakeRegion(0, -1);
                Invalidate(rg);
            }
            EndingCheck();
        }
        private void MoveDown()//천천히 내려가는 함수
        {
            if(game.MoveDown())
            {
                Region rg = MakeRegion(0, -1);
                Invalidate(rg);
            }
            else
            {
                EndingCheck();
            }
        }
        private void MoveTurn()//도형 회전 함수
        {
            if (game.MoveTurn())
            {
                Region rg = MakeRegion();
                Invalidate(rg);
            }
        }
        private void MoveLeft()
        {
            if (game.MoveLeft())
            {
                Region rg = MakeRegion(1, 0);
                Invalidate(rg);
            }
        }

        private void MoveRight()
        {
            if (game.MoveRight())
            {
                Region rg = MakeRegion(-1, 0);
                Invalidate(rg);
            }
        }
        
        private Region MakeRegion(int cx, int cy)
        {
            Point now = game.NowPosition;

            int bn = game.BrickNum;
            int tn = game.Turn;
            Region region = new Region();
            for(int xx=0; xx < 4; xx++)
            {
                for(int yy=0; yy < 4; yy++)
                {
                    if(BrickValue.bvals[bn,tn,xx,yy] != 0)
                    {
                        Rectangle rect1 = new Rectangle((now.X + xx) * board_W + 2, (now.Y + yy) * board_H + 2, board_W - 4, board_H - 4);
                        Rectangle rect2 = new Rectangle((now.X +cx+ xx) * board_W, (now.Y + cy + yy) * board_H, board_W, board_H);
                        Region rg1 = new Region(rect1);
                        Region rg2 = new Region(rect2);
                        region.Union(rg1);
                        region.Union(rg2);
                    }
                }
            }
            return region;
        }

        private Region MakeRegion()
        {
            Point now = game.NowPosition;
            int bn = game.BrickNum;
            int tn = game.Turn;
            int oldtn = (tn + 3) % 4;
            Region region = new Region();
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BrickValue.bvals[bn, tn, xx, yy] != 0)
                    {
                        Rectangle rect1 = new Rectangle((now.X + xx) * board_W, (now.Y + yy) * board_H, board_W, board_H);
                        Region rg1 = new Region(rect1);
                        region.Union(rg1);
                    }
                    if (BrickValue.bvals[bn, oldtn, xx, yy] != 0)
                    {
                        Rectangle rect1 = new Rectangle((now.X + xx) * board_W, (now.Y + yy) * board_H, board_W, board_H);
                        Region rg1 = new Region(rect1);
                        region.Union(rg1);
                    }
                }
            }
            return region;
        }
        private void EndingCheck()
        {
            if (game.Next())
            {
                Invalidate();
            }
            else
            {
                timer1_down.Enabled = false;
                DialogResult re = MessageBox.Show("다시시작","끝내기", MessageBoxButtons.YesNo);
                if(re == DialogResult.Yes)
                {
                    game.Restart();
                    timer1_down.Enabled = true;
                    Invalidate();
                }
                else
                {
                    this.Close();
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool ch = true;
            if (bx / 2 > brick_X)
                ch = true;
            else
                ch = false;
            Console.WriteLine(bx / 2 + " : " + brick_X);

            MoveDown();
        }
    }
}
