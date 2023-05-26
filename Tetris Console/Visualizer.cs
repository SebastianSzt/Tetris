namespace Tetris
{
    [Serializable]
    internal class Visualizer
    {
        private Grid tetrisGrid;

        public Visualizer(Grid x)
        {
            tetrisGrid = x;
        }

        public void PrintTetrisGrid()
        {
            Console.WriteLine("Instrukcja:");
            Console.WriteLine("A - ruch w lewo, D - ruch w prawo, S - ruch w dół");
            Console.WriteLine("Q - obrót w lewo, E - obrót w prawo");
            Console.WriteLine("P - Pauza/Wznowienie, U - Opcje");
            Console.WriteLine();
            for (int i = 0; i < tetrisGrid.rows; i++)
            {
                for (int j = 0; j < tetrisGrid.columns; j++)
                {
                    if (tetrisGrid[i, j] == 0)
                        Console.Write("- ");
                    else
                        Console.Write("X ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}