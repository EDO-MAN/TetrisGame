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
        int board_X;
        int board_Y;
        int board_W;
        int board_H;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = Game.Singleton;
            board_X = GameRule.Board_X;
            board_Y = GameRule.Board_Y;
            board_W = GameRule.Pixel_X;
            board_H = GameRule.Pixel_Y;


            this.SetClientSizeCore(board_X * board_W, board_Y * board_H);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black);
            Graphics g = e.Graphics;

            for (int x = 0; x < board_X; x++)
            {
                for (int y = 0; y < board_Y; y++)
                {
                    Rectangle rec = new Rectangle(x * board_W, y * board_H, board_W, board_H);
                    g.DrawRectangle(pen, rec);
                }
            }
            //DrawGraduation(e.Graphics);//선그리는 함수
            DrawBrick(e.Graphics);//블럭 만드는 함수
            DoubleBuffered = true;
        }
        private void DrawGraduation(Graphics graphics)
        {
            DrawHorizons(graphics);
            DrawVerticals(graphics);
        }

        private void DrawVerticals(Graphics graphics)//수직선 그리기
        {
            Point st = new Point();//선 시작점
            Point et = new Point();//선 끝점

            for(int cx = 0; cx < board_X; cx++)
            {
                st.X = cx * board_W;
                st.Y = 0;
                et.X = st.X;
                et.Y = board_Y * board_H;
                graphics.DrawLine(Pens.DarkBlue, st, et);
            }
        }

        private void DrawHorizons(Graphics graphics)//수평선 그리기
        {
            Point st = new Point();//선 시작점
            Point et = new Point();//선 끝점

            for (int cy = 0; cy < board_Y; cy++)
            {
                st.X = 0;
                st.Y = cy * board_H;
                et.X = board_X * board_H;
                et.Y = st.Y;
                graphics.DrawLine(Pens.DarkBlue, st, et);
            }
        }

        private void DrawBrick(Graphics graphics)//블럭 그리기
        {
            Pen pen = new Pen(Color.Brown, 4);//그릴 펜 설정
            Point now = game.NowPosition;//블럭 위치 설정
            int bn = game.BrickNum;//몇번째 블럭인지 체크
            int tn = game.Turn;//턴을 몇번 했는지 체크
            for(int xx=0; xx< 4; xx++)
            {
                for(int yy= 0; yy<4; yy++)
                {
                    if (BrickValue.bvals[bn, tn, xx, yy] != 0)
                    {
                        Rectangle now_rt = new Rectangle((now.X + xx) * board_W + 2, (now.Y + yy) * board_H + 2, board_W - 4, board_H - 4);
                        graphics.DrawRectangle(pen, now_rt);
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
                    MoveDown();
                    return;
                case Keys.Up:
                    MoveTurn();
                    return;
            }
        }

        private void MoveDown()
        {
            if(game.MoveDown())
            {
                Region rg = MakeRegion(0, -1);
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
            /*else//도형 바뀌는지 체크하는 방법
            {
                game.Next();
                Invalidate();
            }*/
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
            for(int xx=0; xx< 4; xx++)
            {
                for(int yy=0; yy < 4; yy++)
                {
                    if(BrickValue.bvals[bn,tn,xx,yy] != 0)
                    {
                        Rectangle rect1 = new Rectangle((now.X + xx) * board_W, (now.Y + yy) * board_H, board_W, board_H);
                        Rectangle rect2 = new Rectangle((now.X + cx + xx) * board_W, (now.Y + cy + yy) * board_H, board_W, board_H);
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

        private void MoveTurn()
        {
            //도형 회전 함수
            if (game.MoveTurn())
            {
                Region rg = MakeRegion();
                Invalidate(rg);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveDown();
        }
    }
}
