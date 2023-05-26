namespace Tetris
{
    [Serializable]
    public class Block
    {
        public int positionRow;
        public int positionColumn;
        public byte[,] blockShape;

        public int rows { get { return blockShape.GetLength(0); } }
        public int columns { get { return blockShape.GetLength(1); } }

        public Block()
        {
            positionRow = 0;
            positionColumn = 0;
            blockShape = new byte[0, 0];
        }

        public Block(int x, int y, int z)
        {
            blockShape = new byte[x, y];
            positionRow = 0;
            Random random = new Random();
            positionColumn = random.Next(0, z - columns + 1);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void RotateBlock(char x)
        {
            byte[,] transposedBlock = new byte[columns, rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    transposedBlock[j, i] = blockShape[i, j];
                }
            }

            byte[,] rotatedBlock = new byte[columns, rows];

            if (x == 'q' || x == 'Q')
            {
                for (int i = 0; i < columns; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        rotatedBlock[i, j] = transposedBlock[columns - 1 - i, j];
                    }
                }
            }
            else if (x == 'e' || x == 'E')
            {
                for (int i = 0; i < columns; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        rotatedBlock[i, j] = transposedBlock[i, rows - 1 - j];
                    }
                }
            }

            blockShape = rotatedBlock;
        }

        public void MoveBlock(int x, int y)
        {
            positionRow += x;
            positionColumn += y;
        }
    }
}