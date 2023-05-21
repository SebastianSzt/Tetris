namespace Tetris
{
    public class Block_I : Block
    {
        public Block_I() : base() { }

        public Block_I(int x) : base(4, 4, x)
        {
            positionRow = -1;
            blockShape[1, 0] = 1;
            blockShape[1, 1] = 1;
            blockShape[1, 2] = 1;
            blockShape[1, 3] = 1;
        }
    }
}