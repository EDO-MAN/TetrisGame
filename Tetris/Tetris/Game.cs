using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Game
    {
        Brick brick;
        internal Point NowPosition//현재 블럭 위치
        {
            get
            {
                if (brick == null)
                {
                    return new Point(0, 0);//블럭 없다면 0으로 초기화
                }
                return new Point(brick.X, brick.Y);//블럭 위치 반환
            }
        }
    }
}
