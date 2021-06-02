using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Brick
    {
        internal int X//도형 x좌표
        {
            get;
            private set;
        }
        internal int Y//도형 y좌표
        {
            get;
            private set;
        }
        internal Brick()//블럭 생성
        {
            Reset();
        }
        public void Reset()
        {
            Random random = new Random();
            X = GameRule.Start_X;
            Y = GameRule.Start_Y;
            Turn = random.Next() % 4;
            BrickNum = random.Next() % 7;//7개 도형 만들때 random.Next() % 7;
        }
        internal void MoveLeft()
        {
            X--;
        }
        internal void MoveRight()
        {
            X++;
        }
        internal void MoveDown()
        {
            Y++;
        }
        internal void MoveTurn()
        {
            Turn = (Turn + 1) % 4;
        }
        internal int Turn
        {
            get;
            private set;
        }
        internal int BrickNum
        {
            get;
            private set;
        }
    }
}
