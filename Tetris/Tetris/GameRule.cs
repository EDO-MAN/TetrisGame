using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    static class GameRule
    {
        internal const int Board_X = 10;//게임판 가로길이
        internal const int Board_Y = 20;//게임판 세로길이

        internal const int Start_X = 5;//블럭 시작 좌표X
        internal const int Start_Y = 0;//블럭 시작 좌표Y

        internal const int Pixel_X = 20;//게임 1칸당 X값
        internal const int Pixel_Y = 20;//게임 1칸당 치Y
    }
}
