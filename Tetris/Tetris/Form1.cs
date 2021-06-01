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
        }
    }
}
