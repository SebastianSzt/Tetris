using System.Runtime.Serialization;

namespace Tetris
{
    [Serializable]
    internal class GameManager
    {
        private Grid tetrisGrid;
        [NonSerialized]
        private BlockQueue tetrisBlockQueue;
        private Block tetrisTmpBlock;
        private bool gameOver = false;
        private int score = 0;

        public int rows { get { return tetrisGrid.rows; } }
        public int columns { get { return tetrisGrid.columns; } }
        public byte this[int i, int j] { get { return tetrisGrid[i, j]; } }
        public bool gameOverStatus { get { return gameOver; } }
        public int scoreValue { get { return score; } }
        public int nextBlockRows { get { return tetrisBlockQueue.nextBlock.rows; } }
        public int nextBlockColumns { get { return tetrisBlockQueue.nextBlock.columns; } }
        public Block nextBlock { get { return tetrisBlockQueue.nextBlock; } set { tetrisBlockQueue.nextBlock = value; } }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            tetrisBlockQueue = new BlockQueue(tetrisGrid.columns);
        }

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
            tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
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
                score += tetrisGrid.CheckTetrisRows() * columns;
                tetrisTmpBlock = tetrisBlockQueue.GetRandom();
                if (!CheckGameOver())
                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
            }
        }

        public void CheckKeyDown(char key)
        {
            if (key == 'Q' || key == 'q' || key == 'E' || key == 'e')
            {
                tetrisGrid.DeleteBlockFromGrid(tetrisTmpBlock);
                if (CanRotateBlock(key))
                {
                    tetrisTmpBlock.RotateBlock(key);
                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                }
                else
                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
            }
            else if (key == 'A' || key == 'a')
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
            else if (key == 'S' || key == 's')
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
                    score += tetrisGrid.CheckTetrisRows() * columns;
                    tetrisTmpBlock = tetrisBlockQueue.GetRandom();
                    if (!CheckGameOver())
                        tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                }
            }
            else if (key == 'D' || key == 'd')
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