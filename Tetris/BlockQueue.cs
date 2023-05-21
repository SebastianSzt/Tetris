using System.Reflection;

namespace Tetris
{
    internal class BlockQueue
    {
        public Block nextBlock;
        private int GridColumns;

        Type[] types;
        IEnumerable<Type> subclasses;
        Dictionary<string, Type> blockTypes;

        private readonly Random random = new Random();

        public BlockQueue(int x)
        {
            this.GridColumns = x;
            types = Assembly.GetAssembly(typeof(Block)).GetTypes();
            subclasses = types.Where(t => t.IsSubclassOf(typeof(Block)));
            blockTypes = new Dictionary<string, Type>();

            foreach (Type type in subclasses)
            {
                blockTypes.Add(type.Name, type);
            }

            nextBlock = NewRandom();
        }

        private Block NewRandom()
        {
            int index = random.Next(blockTypes.Count);
            Type randomType = blockTypes.ElementAt(index).Value;

            Block block = (Block)Activator.CreateInstance(randomType, GridColumns);

            return block;
        }

        public Block GetRandom()
        {
            Block block = nextBlock;

            nextBlock = NewRandom();

            return block;
        }
    }
}