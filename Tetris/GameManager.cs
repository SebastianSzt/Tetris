namespace Tetris
{
    internal class GameManager
    {
        private Grid tetrisGrid;
        private BlockQueue tetrisBlockQueue;
        private Block tetrisTmpBlock;
        private bool gameOver = false;

        public int rows { get { return tetrisGrid.rows; } }
        public int columns { get { return tetrisGrid.columns; } }
        public byte this[int i, int j] { get { return tetrisGrid[i, j]; } }
        public bool gameOverStatus { get { return gameOver; } }
        public int nextBlockRows { get { return tetrisBlockQueue.nextBlock.rows; } }
        public int nextBlockColumns { get { return tetrisBlockQueue.nextBlock.columns; } }

        public GameManager(int rows, int columns)
        {
            tetrisGrid = new Grid(rows, columns);
            tetrisBlockQueue = new BlockQueue(tetrisGrid.columns);
            tetrisTmpBlock = tetrisBlockQueue.GetRandom();
        }

        public byte GetNextBlockShape(int i, int j)
        {
            return tetrisBlockQueue.nextBlock.blockShape[i, j];
        }

        public void CreateEnvironment()
        {
            if (!CheckGameOver())
            {
                tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
            }
        }

        public void MakeTick()
        {
            tetrisGrid.DeleteBlockFromGrid(tetrisTmpBlock);
            if (CanMoveBlock(1, 0))
            {
                tetrisTmpBlock.MoveBlock(1, 0);
                tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
            }
            else
            {
                tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                tetrisGrid.CheckTetrisRows();
                tetrisTmpBlock = tetrisBlockQueue.GetRandom();
                if (!CheckGameOver())
                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
            }
        }

        public void CheckKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                tetrisGrid.DeleteBlockFromGrid(tetrisTmpBlock);
                if (CanRotateBlock('q'))
                {
                    tetrisTmpBlock.RotateBlock('q');
                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                }
                else
                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
            }
            else if (e.KeyCode == Keys.E)
            {
                tetrisGrid.DeleteBlockFromGrid(tetrisTmpBlock);
                if (CanRotateBlock('e'))
                {
                    tetrisTmpBlock.RotateBlock('e');
                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                }
                else
                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
            }
            else if (e.KeyCode == Keys.A)
            {
                tetrisGrid.DeleteBlockFromGrid(tetrisTmpBlock);
                if (CanMoveBlock(0, -1))
                {
                    tetrisTmpBlock.MoveBlock(0, -1);
                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                }
                else
                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
            }
            else if (e.KeyCode == Keys.S)
            {
                tetrisGrid.DeleteBlockFromGrid(tetrisTmpBlock);
                if (CanMoveBlock(1, 0))
                {
                    tetrisTmpBlock.MoveBlock(1, 0);
                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                }
                else
                {
                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                    tetrisGrid.CheckTetrisRows();
                    tetrisTmpBlock = tetrisBlockQueue.GetRandom();
                    if (!CheckGameOver())
                        tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                }
            }
            else if (e.KeyCode == Keys.D)
            {
                tetrisGrid.DeleteBlockFromGrid(tetrisTmpBlock);
                if (CanMoveBlock(0, 1))
                {
                    tetrisTmpBlock.MoveBlock(0, 1);
                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                }
                else
                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
            }
        }

        private bool CanMoveBlock(int row, int column)
        {
            for (int i = 0; i < tetrisTmpBlock.rows; i++)
                for (int j = 0; j < tetrisTmpBlock.columns; j++)
                    if (tetrisTmpBlock.blockShape[i, j] != 0)
                    {
                        if (tetrisTmpBlock.positionRow + i + row < -1 || tetrisTmpBlock.positionColumn + j + column < 0 || tetrisTmpBlock.positionRow + i + row >= tetrisGrid.rows || tetrisTmpBlock.positionColumn + j + column >= tetrisGrid.columns)
                            return false;
                        if (tetrisGrid[tetrisTmpBlock.positionRow + i + row, tetrisTmpBlock.positionColumn + j + column] != 0)
                            return false;
                    }
            return true;
        }

        private bool CanRotateBlock(char x)
        {
            Block copiedBlock = (Block)tetrisTmpBlock.Clone();

            copiedBlock.RotateBlock(x);

            for (int i = 0; i < copiedBlock.rows; i++)
                for (int j = 0; j < copiedBlock.columns; j++)
                    if (copiedBlock.blockShape[i, j] != 0)
                    {
                        if (copiedBlock.positionRow + i < 0 || copiedBlock.positionRow + i >= tetrisGrid.rows || copiedBlock.positionColumn + j < 0 || copiedBlock.positionColumn + j >= tetrisGrid.columns)
                            return false;
                        if (tetrisGrid[copiedBlock.positionRow + i, copiedBlock.positionColumn + j] != 0)
                            return false;
                    }
            return true;
        }

        private bool CheckGameOver()
        {
            for (int i = 0; i < tetrisTmpBlock.rows; i++)
                for (int j = 0; j < tetrisTmpBlock.columns; j++)
                    if (tetrisTmpBlock.positionRow + i >= 0)
                        if (tetrisGrid[tetrisTmpBlock.positionRow + i, tetrisTmpBlock.positionColumn + j] != 0 && tetrisTmpBlock.blockShape[i, j] != 0)
                        {
                            gameOver = true;
                            return true;
                        }
            return false;
        }
    }
}