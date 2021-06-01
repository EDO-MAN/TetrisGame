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
        internal bool MoveLeft()
        {
            if(brick.X > 0)
            {
                brick.MoveLeft();
                return true;
            }
            return false;
        }
        internal bool MoveRight()
        {
            if ((brick.X + 1) < GameRule.Board_X)
            {
                brick.MoveRight();
                return true;
            }
            return false;
        }
        internal bool MoveDown()
        {
            if ((brick.Y + 1) < GameRule.Board_Y)
            {
                brick.MoveDown();
                return true;
            }
            return false;
        }
    }
}
