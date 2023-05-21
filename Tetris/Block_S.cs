namespace Tetris
{
    public class Block_S : Block
    {
        public Block_S() : base() { }

        public Block_S(int x) : base(3, 3, x)
        {
            blockShape[0, 1] = 5;
            blockShape[0, 2] = 5;
            blockShape[1, 0] = 5;
            blockShape[1, 1] = 5;
        }
    }
}