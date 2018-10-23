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
        public int score = 0;
        public bool isFull = false;
        protected int[,] board;
        protected Random rand;
        

        public TwoZeroFourEightModel() : this(4)
        {
            // default board size is 4 
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

        public int[,] GetBoard()
        {
            return board;
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
            return input;
        }

        private void Checkfull(int[,] board)
        {
            int count = 0;
            for(int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] != board[i, j + 1] && board[i, j] != 0 && board[i, j + 1] != 0)
                    {
                        count++;
                    }
                }
            }
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (board[i, j] != board[i + 1, j] && board[i, j] != 0 && board[i + 1, j] != 0)
                    {
                        count++;
                    }
                }
            }
            if (count == 24)
            {
                isFull = true;
            }
        }

        public void ResetAll()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    board[i, j] = 0;
                }
            }
            score = 0;
            board = Random(board);
            board = Random(board);
            NotifyAll();

        }

        public void PerformDown()
        {
            System.Console.WriteLine("Down");
            int count = 0;
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
                foreach (int j in rangeY)
                {
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;
                    }
                }
                foreach (int j in rangeX)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        score += buffer[j - 1];
                    }
                }
                pos = 3;
                foreach (int j in rangeX)
                {
                    if(buffer[j] == board[pos, i])
                    {
                        count++;
                    }
                    if (buffer[j] != 0)
                    {
                        board[pos, i] = buffer[j];
                        pos--;
                    }
                }
                for (int k = pos; k != -1; k--)
                {
                    board[k, i] = 0;
                }
                if (i == 3 && count !=16)
                {
                    board = Random(board);
                    System.Console.WriteLine("Rand");
                }
            }
            Checkfull(board);
            NotifyAll();
        }

        public void PerformUp()
        {
            System.Console.WriteLine("Up");
            int count = 0;
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
                foreach (int j in range)
                {
                    if (board[j, i] != 0)
                    {
                        buffer[pos] = board[j, i];
                        pos++;
                    }
                }
                foreach (int j in range)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        score += buffer[j - 1];
                    }
                }
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] == board[pos, i])
                    {
                        count++;
                    }
                    if (buffer[j] != 0)
                    {
                        board[pos, i] = buffer[j];
                        pos++;
                    }
                }
                for (int k = pos; k != boardSize; k++)
                {
                    board[k, i] = 0;
                }
                if (i == 3 && count != 16)
                {
                    board = Random(board);
                    System.Console.WriteLine("Rand");
                }
            }
            Checkfull(board);
            NotifyAll();
        }

        public void PerformRight()
        {
            System.Console.WriteLine("Right");
            int count = 0;
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
                foreach (int j in rangeX)
                {
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                    }
                }
                foreach (int j in rangeY)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        score += buffer[j - 1];
                    }
                }
                pos = 3;
                foreach (int j in rangeY)
                {
                    if (buffer[j] == board[i, pos])
                    {
                        count++;
                    }
                    if (buffer[j] != 0)
                    {
                        board[i, pos] = buffer[j];
                        pos--;
                    }
                }
                for (int k = pos; k != -1; k--)
                {
                    board[i, k] = 0;
                }
                if (i == 3 && count != 16)
                {
                    board = Random(board);
                    System.Console.WriteLine("Rand");
                }
            }
            Checkfull(board);
            NotifyAll();
        }

        public void PerformLeft()
        {
            System.Console.WriteLine("Left");
            int count = 0;
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
                foreach (int j in range)
                {
                    if (board[i, j] != 0)
                    {
                        buffer[pos] = board[i, j];
                        pos++;
                    }
                }
                foreach (int j in range)
                {
                    if (j > 0 && buffer[j] != 0 && buffer[j] == buffer[j - 1])
                    {
                        buffer[j - 1] *= 2;
                        buffer[j] = 0;
                        score += buffer[j - 1];
                    }
                }
                pos = 0;
                foreach (int j in range)
                {
                    if (buffer[j] == board[i, pos])
                    {
                        count++;
                    }
                    if (buffer[j] != 0)
                    {
                        board[i, pos] = buffer[j];
                        pos++;
                    }
                }
                for (int k = pos; k != boardSize; k++)
                {
                    board[i, k] = 0;
                }
                if (i == 3 && count != 16)
                {
                    board = Random(board);
                    System.Console.WriteLine("Rand");
                }
            }
            Checkfull(board);
            NotifyAll();
        }
    }
}
