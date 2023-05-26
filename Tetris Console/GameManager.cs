using System.Runtime.Serialization;
using Timer = System.Timers.Timer;

namespace Tetris
{
    [Serializable]
    internal class GameManager
    {
        private Grid tetrisGrid;
        private Visualizer tetrisVisualizer;
        private Block tetrisTmpBlock;
        private int score = 0;
        public bool gameOver = false;
        public bool saveGame = false;
        public bool loadGame = false;
        public bool settings = false;
        private static Timer timer1 = new Timer(750);
        private static Timer timer2 = new Timer(75);
        private object lockObject = new object();

        [NonSerialized]
        private BlockQueue tetrisBlockQueue;

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            tetrisBlockQueue = new BlockQueue(tetrisGrid.columns);
        }

        public Block nextBlock { get { return tetrisBlockQueue.nextBlock; } set { tetrisBlockQueue.nextBlock = value; } }
        public double interval { get { return timer1.Interval; } set { timer1.Interval = value; } }
        public int rows { get { return tetrisGrid.rows; } }
        public int columns { get { return tetrisGrid.columns; } }

        public GameManager(int rows, int columns)
        {
            tetrisGrid = new Grid(rows, columns);
            tetrisVisualizer = new Visualizer(tetrisGrid);
            tetrisBlockQueue = new BlockQueue(tetrisGrid.columns);
            tetrisTmpBlock = tetrisBlockQueue.GetRandom();

            timer1.Enabled = false;
            timer1.Elapsed += (sender, e) => MakeTick();
            timer2.Enabled = false;
            timer2.Elapsed += (sender, e) => CheckKeyboard();
        }

        public void StartGame()
        {
            timer1 = new Timer(interval);
            timer2 = new Timer(75);

            timer1.Enabled = false;
            timer1.Elapsed += (sender, e) => MakeTick();
            timer2.Enabled = false;
            timer2.Elapsed += (sender, e) => CheckKeyboard();
            if (saveGame)
            {
                saveGame = false;

                lock (lockObject)
                {
                    Console.Clear();
                    tetrisVisualizer.PrintTetrisGrid();
                    Console.WriteLine("Wynik: " + score);
                    Console.WriteLine();
                    Console.WriteLine("Zapisano gre!");
                    Console.WriteLine();
                    Console.WriteLine("PAUZA!");
                }

                timer2.Start();
                while (!gameOver && !saveGame && !loadGame && !settings) { }
                timer1.Enabled = false;
                timer2.Enabled = false;
                Thread.Sleep(100);
            }
            else if (loadGame)
            {
                loadGame = false;

                lock (lockObject)
                {
                    Console.Clear();
                    tetrisVisualizer.PrintTetrisGrid();
                    Console.WriteLine("Wynik: " + score);
                    Console.WriteLine();
                    Console.WriteLine("Wczytano gre!");
                    Console.WriteLine();
                    Console.WriteLine("PAUZA!");
                }
                
                timer2.Start();
                while (!gameOver && !saveGame && !loadGame && !settings) { }
                timer1.Enabled = false;
                timer2.Enabled = false;
                Thread.Sleep(100);
            }
            else if (settings)
            {
                settings = false;

                if (!CheckGameOver())
                {
                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                }

                lock (lockObject)
                {
                    Console.Clear();
                    tetrisVisualizer.PrintTetrisGrid();
                    Console.WriteLine("Wynik: " + score);
                    Console.WriteLine();
                    Console.WriteLine("Stworzono gre z nowymi ustawieniami!");
                    Console.WriteLine();
                    Console.WriteLine("PAUZA!");
                }

                timer2.Start();
                while (!gameOver && !saveGame && !loadGame && !settings) { }
                timer1.Enabled = false;
                timer2.Enabled = false;
                Thread.Sleep(100);
            }
            else
            {
                if (!CheckGameOver())
                {
                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                }

                lock (lockObject)
                {
                    Console.Clear();
                    tetrisVisualizer.PrintTetrisGrid();
                    Console.WriteLine("Wynik: " + score);
                    Console.WriteLine();
                    Console.WriteLine("PAUZA!");
                }

                timer2.Start();
                while (!gameOver && !saveGame && !loadGame && !settings) { }
                timer1.Enabled = false;
                timer2.Enabled = false;
                Thread.Sleep(100);
            }
        }

        private void MakeTick()
        {
            tetrisGrid.DeleteBlockFromGrid(tetrisTmpBlock);
            if (CanMoveBlock(1, 0))
            {
                tetrisTmpBlock.MoveBlock(1, 0);
                tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
            }
            else
            {
                tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                score += tetrisGrid.CheckTetrisRows() * tetrisGrid.columns;
                tetrisTmpBlock = tetrisBlockQueue.GetRandom();
                if (!CheckGameOver())
                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
            }
            if (!gameOver)
                lock (lockObject)
                {
                    Console.Clear();
                    tetrisVisualizer.PrintTetrisGrid();
                    Console.WriteLine("Wynik: " + score);
                }
        }

        private void CheckKeyboard()
        {
            if (!gameOver)
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true);

                    switch (key.KeyChar)
                    {
                        case 'q':
                        case 'e':
                        case 'Q':
                        case 'E':
                            if (timer1.Enabled)
                            {
                                tetrisGrid.DeleteBlockFromGrid(tetrisTmpBlock);
                                if (CanRotateBlock(key.KeyChar))
                                {
                                    tetrisTmpBlock.RotateBlock(key.KeyChar);
                                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                                    lock (lockObject)
                                    {
                                        Console.Clear();
                                        tetrisVisualizer.PrintTetrisGrid();
                                        Console.WriteLine("Wynik: " + score);
                                    }
                                }
                                else
                                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                            }
                            break;

                        case 'a':
                        case 'A':
                            if (timer1.Enabled)
                            {
                                tetrisGrid.DeleteBlockFromGrid(tetrisTmpBlock);
                                if (CanMoveBlock(0, -1))
                                {
                                    tetrisTmpBlock.MoveBlock(0, -1);
                                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                                    lock (lockObject)
                                    {
                                        Console.Clear();
                                        tetrisVisualizer.PrintTetrisGrid();
                                        Console.WriteLine("Wynik: " + score);
                                    }
                                }
                                else
                                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                            }
                            break;

                        case 's':
                        case 'S':
                            if (timer1.Enabled)
                            {
                                timer1.Stop();
                                tetrisGrid.DeleteBlockFromGrid(tetrisTmpBlock);
                                if (CanMoveBlock(1, 0))
                                {
                                    tetrisTmpBlock.MoveBlock(1, 0);
                                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                                }
                                else
                                {
                                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                                    score += tetrisGrid.CheckTetrisRows() * tetrisGrid.columns;
                                    tetrisTmpBlock = tetrisBlockQueue.GetRandom();
                                    if (!CheckGameOver())
                                        tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                                }
                                lock (lockObject)
                                {
                                    Console.Clear();
                                    tetrisVisualizer.PrintTetrisGrid();
                                    Console.WriteLine("Wynik: " + score);
                                }
                                timer1.Start();
                            }
                            break;

                        case 'd':
                        case 'D':
                            if (timer1.Enabled)
                            {
                                tetrisGrid.DeleteBlockFromGrid(tetrisTmpBlock);
                                if (CanMoveBlock(0, 1))
                                {
                                    tetrisTmpBlock.MoveBlock(0, 1);
                                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                                    lock (lockObject)
                                    {
                                        Console.Clear();
                                        tetrisVisualizer.PrintTetrisGrid();
                                        Console.WriteLine("Wynik: " + score);
                                    }
                                }
                                else
                                    tetrisGrid.SetBlockOnGrid(tetrisTmpBlock);
                            }
                            break;
                        case 'p':
                        case 'P':
                            if (timer1.Enabled == true)
                            {
                                timer1.Stop();
                                Console.WriteLine();
                                Console.WriteLine("PAUZA!");
                            }
                            else
                            {
                                Console.Clear();
                                tetrisVisualizer.PrintTetrisGrid();
                                Console.WriteLine("Wynik: " + score);
                                timer1.Start();
                            }
                            
                            break;
                        case 'u':
                        case 'U':
                            timer1.Stop();
                            timer2.Stop();

                            string option;

                            lock (lockObject)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Opcje:");
                                Console.WriteLine("0 - Anuluj");
                                Console.WriteLine("1 - Zmień ustawienia");
                                Console.WriteLine("2 - Zapisz gre");
                                if (File.Exists("save.bin"))
                                    Console.WriteLine("3 - Wczytaj gre");
                                Console.WriteLine();
                                option = Console.ReadLine();
                            }

                            switch (option)
                            {
                                case "0":
                                    lock (lockObject)
                                    {
                                        Console.Clear();
                                        tetrisVisualizer.PrintTetrisGrid();
                                        Console.WriteLine("Wynik: " + score);
                                        Console.WriteLine();
                                        Console.WriteLine("PAUZA!");
                                        timer2.Start();
                                    }
                                    break;
                                case "1":
                                    settings = true;
                                    break;
                                case "2":
                                    saveGame = true;
                                    break;
                                case "3":
                                    if (File.Exists("save.bin"))
                                        loadGame = true;
                                    else
                                        goto default;
                                    break;
                                default:
                                    lock (lockObject)
                                    {
                                        Console.Clear();
                                        tetrisVisualizer.PrintTetrisGrid();
                                        Console.WriteLine("Wynik: " + score);
                                        Console.WriteLine();
                                        Console.WriteLine("Niepoprawna opcja.");
                                        Console.WriteLine();
                                        Console.WriteLine("PAUZA!");
                                        timer2.Start();
                                    }
                                    break;
                            }
                            break;
                    }

                    while (Console.KeyAvailable)
                        Console.ReadKey(intercept: true);
                }
        }

        private bool CanMoveBlock(int row, int column)
        {
            for (int i = 0; i < tetrisTmpBlock.rows; i++)
                for (int j = 0; j < tetrisTmpBlock.columns; j++)
                    if (tetrisTmpBlock.blockShape[i, j] != 0)
                    {
                        if (tetrisTmpBlock.positionRow + i + row < -1 || tetrisTmpBlock.positionColumn + j + column < 0 || tetrisTmpBlock.positionRow + i + row >= tetrisGrid.rows || tetrisTmpBlock.positionColumn + j + column >= tetrisGrid.columns)
                            return false;
                        if (tetrisGrid[tetrisTmpBlock.positionRow + i + row, tetrisTmpBlock.positionColumn + j + column] != 0)
                            return false;
                    }
            return true;
        }

        private bool CanRotateBlock(char x)
        {
            Block copiedBlock = (Block)tetrisTmpBlock.Clone();

            copiedBlock.RotateBlock(x);

            for (int i = 0; i < copiedBlock.rows; i++)
                for (int j = 0; j < copiedBlock.columns; j++)
                    if (copiedBlock.blockShape[i, j] != 0)
                    {
                        if (copiedBlock.positionRow + i < 0 || copiedBlock.positionRow + i >= tetrisGrid.rows || copiedBlock.positionColumn + j < 0 || copiedBlock.positionColumn + j >= tetrisGrid.columns)
                            return false;
                        if (tetrisGrid[copiedBlock.positionRow + i, copiedBlock.positionColumn + j] != 0)
                            return false;
                    }
            return true;
        }

        private bool CheckGameOver()
        {
            for (int i = 0; i < tetrisTmpBlock.rows; i++)
                for (int j = 0; j < tetrisTmpBlock.columns; j++)
                    if (tetrisTmpBlock.positionRow + i >= 0)
                        if (tetrisGrid[tetrisTmpBlock.positionRow + i, tetrisTmpBlock.positionColumn + j] != 0 && tetrisTmpBlock.blockShape[i, j] != 0)
                        {
                            timer1.Stop();
                            timer2.Stop();
                            gameOver = true;
                            return true;
                        }
            return false;
        }
    }
}