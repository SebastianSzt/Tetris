namespace Tetris
{
    public class Block_T : Block
    {
        public Block_T() : base() { }

        public Block_T(int x) : base(3, 3, x)
        {
            blockShape[0, 1] = 6;
            blockShape[1, 0] = 6;
            blockShape[1, 1] = 6;
            blockShape[1, 2] = 6;
        }
    }
}