using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Brick
    {
        internal int X
        {
            get;
            private set;
        }
        internal int Y
        {
            get;
            private set;
        }
        internal Brick()
        {
            Reset();
        }
        internal void Reset()
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
