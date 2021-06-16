using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Brick
    {
        public int X//도형 x좌표
        {
            get;
            private set;
        }
        public int Y//도형 y좌표
        {
            get;
            private set;
        }
        public int Turn//현재 회전 정도 구현
        {
            get;
            private set;
        }
        public int BrickNum//현재 벽돌 모양
        {
            get;
            private set;
        }
        public Brick()//블럭 생성
        {
            Reset();
        }
        public void Reset()
        {
            Random random = new Random();
            X = GameRule.Start_X;
            Y = GameRule.Start_Y;
            //Turn = random.Next() % 4;
            BrickNum = random.Next() % 7;//7개 도형 만들때 random.Next() % 7;
        }
        public void MoveLeft()
        {
            X--;
        }
        public void MoveRight()
        {
            X++;
        }
        public void MoveDown()
        {
            Y++;
        }
        public void MoveTurn()//회전 메서드
        {
            Turn = (Turn + 1) % 4;
        }
        
    }
}
