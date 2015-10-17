using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twozerofoureight
{
    class TwoZeroFourEightModel : Model
    {
        protected int boardSize; // default is 4
        protected int[,] board;
        protected Random rand;
        protected int score;
        protected bool isEnd = false;           // Endgame indicator
        

        public TwoZeroFourEightModel() : this(4)
        {
            // default board size is 4 
        }

        public int[,] GetBoard()
        {
            return board;
        }

        public bool IsEnd
        {
            get { return isEnd; }
        }                   // Endgame property

        public int Score
        {
            get { return score; }
        }


        public TwoZeroFourEightModel(int size)
        {
            boardSize = size;
            board = new int[boardSize, boardSize];
            var range = Enumerable.Range(0, boardSize);
            foreach(int i in range) {
                foreach(int j in range) {
                    board[i,j] = 0;
                }
            }
            rand = new Random();
            board = Random(board);
            NotifyAll();
        }

        private int[,] Random(int[,] input)
        {
            while (true)
            {
                int x = rand.Next(boardSize);
                int y = rand.Next(boardSize);
                if (board[x, y] == 0)
                {
                    board[x, y] = 2;
                    break;
                }
            }
            score += 2;
            return input;
        }

        public void PerformDown()
        {
            bool moved = false;                                                 // Movement check-------------------------
            int[] buffer;
            int pos;
            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeY);
            foreach (int i in rangeX)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in rangeX)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in rangeY)
                {
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in rangeX)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        moved = true;                                           // Move occurred--------------------------
                    }
                }
                // shift left again
                pos = 3;
                foreach (int j in rangeX)
                {
                    if (buffer[j] != 0)
                    {
                        if (board[pos, i] != buffer[j]) moved=true;             // Move occurred--------------------------
                        board[pos, i] = buffer[j];
                        pos--;   
                    }
                }
                // copy back
                for (int k = pos; k != -1; k--)
                {
                    board[k, i] = 0;
                }
            }
            if (moved) board = Random(board);                                   // no move no random----------------------
            NotifyAll();
        }

        public void PerformUp()
        {
            bool moved = false;
            int[] buffer;
            int pos;

            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in range)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in range)
                {
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in range)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        moved = true;
                    }
                }
                // shift left again
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] != 0)
                    {
                        if (board[pos, i] != buffer[j]) moved = true;
                        board[pos, i] = buffer[j];
                        pos++;
                    }
                }
                // copy back
                for (int k = pos; k != boardSize; k++)
                {
                    board[k, i] = 0;
                }
            }
            if (moved) board = Random(board);
            NotifyAll();
        }

        public void PerformRight()
        {
            bool moved = false;
            int[] buffer;
            int pos;

            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeX);
            foreach (int i in rangeY)
            {
                pos = 0;
                buffer = new int[4];
                foreach (int k in rangeY)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in rangeX)
                {
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in rangeY)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        moved = true;
                    }
                }
                // shift left again
                pos = 3;
                foreach (int j in rangeY)
                {
                    if (buffer[j] != 0)
                    {
                        if (board[i, pos] != buffer[j]) moved = true;
                        board[i, pos] = buffer[j];
                        pos--;
                    }
                }
                // copy back
                for (int k = pos; k != -1; k--)
                {
                    board[i, k] = 0;
                }
            }
            if (moved) board = Random(board);
            NotifyAll();
        }

        public void PerformLeft()
        {
            bool moved = false;
            int[] buffer;
            int pos;
            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                pos = 0;
                buffer = new int[boardSize];
                foreach (int k in range)
                {
                    buffer[k] = 0;
                }
                //shift left
                foreach (int j in range)
                {
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                    }
                }
                // check duplicate
                foreach (int j in range)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        moved = true;
                    }
                }
                // shift left again
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] != 0)
                    {
                        if (board[i, pos] != buffer[j]) moved = true;
                        board[i, pos] = buffer[j];
                        pos++;
                    }
                }
                for (int k = pos; k != boardSize; k++)
                {
                    board[i, k] = 0;
                }
            }
            if(moved) board = Random(board);
            NotifyAll();
        }

        public void EndingScan()
        {
            if (!isEnd)
            {
                for (int i = 0; i != boardSize; i++)
                    if (board[i, 0] != board[i, 1] && board[i, 1] != board[i, 2] && board[i, 2] != board[i, 3])   
                    {//^ All tiles in i row are different
                        if (board[0, i] != board[1, i] && board[1, i] != board[2, i] && board[2, i] != board[3, i])
                        {//^ All tiles in i column are different
                            if (i == boardSize - 1 && board[i, 0] != board[i, 1] && board[i, 1] != board[i, 2] && board[i, 2] != board[i, 3] && board[0, i] != board[1, i] && board[1, i] != board[2, i] && board[2, i] != board[3, i])
                            {//^ i is equal to 'boardSize - 1' and all conditions above are true
                                isEnd = true;               // All tiles are different! Game over!
                                break;
                            }
                            else continue;
                        }
                        else break;
                    }
                    else break;
            }
        }
        
    }
}
