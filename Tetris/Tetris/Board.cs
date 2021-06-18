using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Board
    {
        #region 단일체
        public static Board GameBoard
        {
            get;
            private set;
        }
        static Board()
        {
            GameBoard = new Board();
        }
        Board()
        {

        }
        #endregion
        int[,] board = new int[GameRule.Board_X, GameRule.Board_Y];//벽돌 쌓기 위한 배열 생성
        
        public int this[int x, int y]
        {
            get
            {
                return board[x, y];
            }
        }
        public bool MoveEnable(int bn, int tn, int x, int y)//벽돌 이동 가능 판별
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BrickValue.bvals[bn, tn, xx, yy] != 0)
                    {
                        if (board[x + xx, y + yy] != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public void Store(int bn, int turn, int x, int y)//벽돌 쌓는 함수
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (((x + xx) >= 0) && (x + xx < GameRule.Board_X) &&
                        (y + yy >= 0) && (y + yy < GameRule.Board_Y))
                    {
                        board[x + xx, y + yy] += BrickValue.bvals[bn, turn, xx, yy];
                    }
                }
            }
            CheckLines(y + 3);
        }
        private void CheckLines(int y)
        {            
            for (int yy = 0; yy < 4; yy++)
            {
                if (y - yy < GameRule.Board_Y)
                {
                    
                    if (CheckLine(y-yy))
                    {
                        ClearLine(y - yy);
                        y++;

                    }
                }
            }
        }
        private bool CheckLine(int y)
        {
            for(int xx = 0; xx < GameRule.Board_X; xx++)
            {
                if (board[xx, y] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void ClearLine(int y)//라인의 모든 x좌표 블럭 확인 후 지우기
        {
            for(;y>0;y--)
            {
                for(int xx = 0; xx < GameRule.Board_X; xx++)
                {
                    board[xx, y] = board[xx, y - 1];
                   //scorenum += 10;
                }
            }
        }
        public void ClearBoard()//보드 초기화
        {
            for(int xx = 0; xx < GameRule.Board_X; xx++)
            {
                for(int yy=0;yy<GameRule.Board_Y; yy++)
                {
                    board[xx, yy] = 0;
                }
            }
        }
    }
}
