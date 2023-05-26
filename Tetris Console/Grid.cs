namespace Tetris
{
    [Serializable]
    public class Grid
    {
        private byte[,] grid;

        public int rows { get { return grid.GetLength(0); } }
        public int columns { get { return grid.GetLength(1); } }
        public byte this[int i, int j] { get { return grid[i, j]; } }

        public Grid(int x, int y)
        {
            grid = new byte[x, y];
        }

        public int CheckTetrisRows()
        {
            int points = 0;
            int checkingRows = rows - 1;
            bool wasFull = false;
            while (checkingRows >= 0)
            {
                bool isFullRow = true;
                for (int i = 0; i < columns; i++)
                {
                    if (grid[checkingRows, i] == 0)
                    {
                        isFullRow = false;
                        break;
                    }
                }
                if (isFullRow)
                {
                    points++;
                    for (int j = checkingRows; j > 0; j--)
                    {
                        for (int k = 0; k < columns; k++)
                            grid[j, k] = grid[j - 1, k];
                    }
                    if (!wasFull)
                    {
                        for (int k = 0; k < columns; k++)
                            grid[0, k] = 0;
                        wasFull = true;
                    }
                }
                else
                    checkingRows--;
            }
            return points;
        }

        public void SetBlockOnGrid(Block x)
        {
            for (int i = 0; i < x.rows; i++)
                for (int j = 0; j < x.columns; j++)
                    if (x.blockShape[i, j] != 0)
                        if (x.positionRow + i >= 0)
                            grid[x.positionRow + i, x.positionColumn + j] = x.blockShape[i, j];
        }

        public void DeleteBlockFromGrid(Block x)
        {
            for (int i = 0; i < x.rows; i++)
                for (int j = 0; j < x.columns; j++)
                    if (x.blockShape[i, j] != 0)
                        if (x.positionRow + i >= 0)
                            grid[x.positionRow + i, x.positionColumn + j] = 0;
        }
    }
}