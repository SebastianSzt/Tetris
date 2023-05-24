namespace Tetris
{
    [Serializable]
    public class Block_J : Block
    {
        public Block_J() : base() { }

        public Block_J(int x) : base(3, 3, x)
        {
            blockShape[0, 0] = 2;
            blockShape[1, 0] = 2;
            blockShape[1, 1] = 2;
            blockShape[1, 2] = 2;
        }
    }
}