namespace Tetris
{
    public class Block_Z : Block
    {
        public Block_Z() : base() { }

        public Block_Z(int x) : base(3, 3, x)
        {
            blockShape[0, 0] = 7;
            blockShape[0, 1] = 7;
            blockShape[1, 1] = 7;
            blockShape[1, 2] = 7;
        }
    }
}