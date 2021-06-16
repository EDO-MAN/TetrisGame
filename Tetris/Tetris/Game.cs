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
        Brick brick;//벽돌 개체 참조 선언
        Board gboard = Board.GameBoard;
        public Point NowPosition//블럭 현재 좌표
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
        #region 단일체
        public static Game Singleton
        {
            get;
            private set;
        }
        public int this[int x, int y]//보드 영역 값 속성 정의
        {
            get
            {
                return gboard[x, y];
            }
        }

        static Game()
        {
            Singleton = new Game();
        }
        Game()
        {
            brick = new Brick();//블럭 생성
        }
        #endregion
        public int BrickNum//현재 벽돌 종류 확인하는 속성
        {
            get
            {
                return brick.BrickNum;
            }
        }
        public int Turn//현재 벽돌 회전 정도 확인하는 속성
        {
            get
            {
                return brick.Turn;
            }
        }
        public bool MoveLeft()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BrickValue.bvals[brick.BrickNum, Turn, xx, yy] != 0)
                    {
                        if (brick.X + xx <= 0)
                        {
                            return false;
                        }
                    }
                }
            }
            if (gboard.MoveEnable(brick.BrickNum, Turn, brick.X - 1, brick.Y))
            {
                brick.MoveLeft();
                return true;
            }
            return false;
        }
        public bool MoveRight()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BrickValue.bvals[brick.BrickNum,Turn, xx, yy] != 0)
                    {
                        if (brick.X + xx + 1 >= GameRule.Board_X)
                        { 
                            return false;
                        }
                    }
                }
            }
            if (gboard.MoveEnable(brick.BrickNum, Turn, brick.X + 1, brick.Y))
            {
                brick.MoveRight();
                return true;
            }
            return false;
        }
        public bool MoveDown()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BrickValue.bvals[brick.BrickNum,Turn, xx, yy] != 0)
                    {
                        if (brick.Y + yy + 1 >= GameRule.Board_Y)
                        {
                            gboard.Store(brick.BrickNum, Turn, brick.X, brick.Y);
                            return false;
                        }
                    }
                }
            }
            if (gboard.MoveEnable(brick.BrickNum, Turn, brick.X, brick.Y + 1))
            {
                brick.MoveDown();
                return true;
            }
            gboard.Store(brick.BrickNum, Turn, brick.X, brick.Y);
            return false;
        }
        public bool MoveTurn()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BrickValue.bvals[brick.BrickNum, (Turn + 1) % 4, xx, yy] != 0)
                    {
                        if (((brick.X+xx)<0)||(brick.X+xx)>=GameRule.Board_X||((brick.Y + yy)  >= GameRule.Board_Y))
                        {
                            return false;
                        }
                    }
                }
            }
            if (gboard.MoveEnable(brick.BrickNum, (Turn + 1) % 4, brick.X, brick.Y))
            {
                brick.MoveTurn();
                return true;
            }
            return false;
        }

        public bool Next()
        {
            brick.Reset();
            return gboard.MoveEnable(brick.BrickNum, Turn, brick.X, brick.Y);
        }
        public void Restart()//다시 시작
        {
            gboard.ClearBoard();
        }
    }
}
