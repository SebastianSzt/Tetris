namespace Tetris
{
    [Serializable]
    public class Block_O : Block
    {
        public Block_O() : base() { }

        public Block_O(int x) : base(2, 2, x)
        {
            blockShape[0, 0] = 4;
            blockShape[0, 1] = 4;
            blockShape[1, 0] = 4;
            blockShape[1, 1] = 4;
        }
    }
}