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
            X = GameRule.Start_X;
            Y = GameRule.Start_Y;
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
    }
}
