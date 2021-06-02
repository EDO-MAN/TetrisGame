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
        #region 단일체
        internal static Game Singleton
        {
            get;
            private set;
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
        internal int BrickNum
        {
            get
            {
                return brick.BrickNum;
            }
        }
        internal int Turn
        {
            get
            {
                return brick.Turn;
            }
        }
        internal bool MoveLeft()
        {
            for(int xx = 0; xx < 4; xx++)
            {
                for(int yy = 0; yy < 4; yy++)
                {
                    if(BrickValue.bvals[brick.BrickNum, Turn, xx, yy] != 0)
                    {
                        if(brick.X + xx <= 0)
                        {
                            return false;
                        }
                    }
                }
            }
            brick.MoveLeft();
            return true;
           
       
        }
        internal bool MoveRight()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BrickValue.bvals[brick.BrickNum, Turn, xx, yy] != 0)
                    {
                        if (brick.X + xx + 1 >= GameRule.Board_X)
                        {
                            return false;
                        }
                    }
                }
            }
            brick.MoveRight();
            return true;
        }
        internal bool MoveDown()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BrickValue.bvals[brick.BrickNum, Turn, xx, yy] != 0)
                    {
                        if (brick.Y + yy + 1 >= GameRule.Board_Y)
                        {
                            return false;
                        }
                    }
                }
            }
            brick.MoveDown();
            return true;
        }
        internal bool MoveTurn()
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BrickValue.bvals[brick.BrickNum, (Turn+1) % 4, xx, yy] != 0)
                    {
                        if (((brick.X + xx) < 0) || ((brick.X + xx) >= GameRule.Board_X) || ((brick.Y + yy) >= GameRule.Board_X))
                        {
                            return false;
                        }
                    }
                }
            }
            brick.MoveTurn();
            return true;
        }

        internal void Next()
        {
            brick.Reset();
        }
    }
}
