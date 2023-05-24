namespace Tetris
{
    [Serializable]
    public class Block_L : Block
    {
        public Block_L() : base() { }

        public Block_L(int x) : base(3, 3, x)
        {
            blockShape[0, 2] = 3;
            blockShape[1, 0] = 3;
            blockShape[1, 1] = 3;
            blockShape[1, 2] = 3;
        }
    }
}