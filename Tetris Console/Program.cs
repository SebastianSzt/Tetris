using System.Runtime.Serialization.Formatters.Binary;

namespace Tetris
{
    class Program
    {
        static void Main()
        {
            Console.SetWindowSize(Console.WindowWidth, 40);

            GameManager tetrisGameManager = new GameManager(20, 10);
            tetrisGameManager.StartGame();

            bool exit = false;
            while (!exit)
            {
                if (tetrisGameManager.saveGame)
                {
                    tetrisGameManager.saveGame = false;
                    Stream stream = new FileStream("save.bin", FileMode.Create);
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, tetrisGameManager);
                    formatter.Serialize(stream, tetrisGameManager.nextBlock);
                    stream.Close();

                    tetrisGameManager.saveGame = true;
                    tetrisGameManager.StartGame();
                }
                else if (tetrisGameManager.loadGame)
                {
                    if (File.Exists("save.bin"))
                    {
                        Stream stream = new FileStream("save.bin", FileMode.Open);
                        BinaryFormatter formatter = new BinaryFormatter();
                        tetrisGameManager = (GameManager)formatter.Deserialize(stream);
                        tetrisGameManager.nextBlock = (Block)formatter.Deserialize(stream);
                        stream.Close();

                        tetrisGameManager.loadGame = true;
                        tetrisGameManager.StartGame();
                    }
                }
                else if (tetrisGameManager.settings)
                {
                    Console.WriteLine();
                    Console.WriteLine("Ustawienia:");
                    Console.WriteLine("UWAGA! Zmiana ustawień spowoduje stworzenie nowej gry!");
                    Console.WriteLine("Niepoprawne dane spowodują wczytanie domyślnych ustawień.");
                    Console.WriteLine();
                    Console.WriteLine("Czy chcesz kontynuować: (TAK/NIE)\t");
                    string continueSettings = Console.ReadLine();

                    if (continueSettings == "TAK")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Szerokość planszy: (10-30)\t");
                        string width = Console.ReadLine();
                        Console.WriteLine("Wysokość planszy: (20-40)\t");
                        string height = Console.ReadLine();
                        Console.WriteLine("Interwał: (300ms-2000ms)\t");
                        string interval = Console.ReadLine();

                        int intWidth;
                        if (int.TryParse(width, out int parsedWidth))
                        {
                            if (parsedWidth < 10)
                            {
                                intWidth = 10;
                            }
                            else if (parsedWidth > 30)
                            {
                                intWidth = 30;
                            }
                            else
                            {
                                intWidth = parsedWidth;
                            }
                        }
                        else
                        {
                            intWidth = tetrisGameManager.columns;
                        }

                        int intHeight;
                        if (int.TryParse(height, out int parsedHeight))
                        {
                            if (parsedHeight < 20)
                            {
                                intHeight = 20;
                            }
                            else if (parsedHeight > 40)
                            {
                                intHeight = 40;
                            }
                            else
                            {
                                intHeight = parsedHeight;
                            }
                        }
                        else
                        {
                            intHeight = tetrisGameManager.rows;
                        }

                        double doubleInterval;
                        if (double.TryParse(interval, out double parsedInterval))
                        {
                            if (parsedInterval < 300)
                            {
                                doubleInterval = 300;
                            }
                            else if (parsedInterval > 2000)
                            {
                                doubleInterval = 2000;
                            }
                            else
                            {
                                doubleInterval = parsedInterval;
                            }
                        }
                        else
                        {
                            doubleInterval = tetrisGameManager.interval;
                        }

                        tetrisGameManager = new GameManager(intHeight, intWidth);
                        tetrisGameManager.interval = doubleInterval;
                        tetrisGameManager.settings = true;
                        tetrisGameManager.StartGame();

                    }
                    else if (continueSettings == "NIE")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Powrót do gry.");
                        Thread.Sleep(2000);
                        tetrisGameManager.settings = false;
                        tetrisGameManager.loadGame = true;
                        tetrisGameManager.StartGame();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Niepoprawna odpowiedź. Powrót do gry.");
                        Thread.Sleep(2000);
                        tetrisGameManager.settings = false;
                        tetrisGameManager.loadGame = true;
                        tetrisGameManager.StartGame();
                    }
                }
                else if (tetrisGameManager.gameOver)
                {
                    Console.WriteLine();
                    Console.WriteLine("Nowy blok wylosował się w zajętym miejscu, koniec gry!");
                    Console.WriteLine();
                    Console.WriteLine("Co chcesz zrobić: ");
                    Console.WriteLine("0. Wyjdź");
                    Console.WriteLine("1. Nowa gra stare ustawienia");
                    Console.WriteLine("2. Nowa gra nowe ustawienia");
                    Console.WriteLine("3. Wczytaj zapisaną gre");
                    Console.WriteLine();
                    string chooseGameOver = Console.ReadLine();

                    if (chooseGameOver == "0")
                        exit = true;
                    else if (chooseGameOver == "1")
                    {
                        double interval = tetrisGameManager.interval;
                        tetrisGameManager = new GameManager(tetrisGameManager.rows, tetrisGameManager.columns);
                        tetrisGameManager.interval = interval;
                        tetrisGameManager.StartGame();
                    }
                    else if (chooseGameOver == "2")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Szerokość planszy: (10-30)\t");
                        string width = Console.ReadLine();
                        Console.WriteLine("Wysokość planszy: (20-40)\t");
                        string height = Console.ReadLine();
                        Console.WriteLine("Interwał: (300ms-2000ms)\t");
                        string interval = Console.ReadLine();

                        int intWidth;
                        if (int.TryParse(width, out int parsedWidth))
                        {
                            if (parsedWidth < 10)
                            {
                                intWidth = 10;
                            }
                            else if (parsedWidth > 30)
                            {
                                intWidth = 30;
                            }
                            else
                            {
                                intWidth = parsedWidth;
                            }
                        }
                        else
                        {
                            intWidth = tetrisGameManager.columns;
                        }

                        int intHeight;
                        if (int.TryParse(height, out int parsedHeight))
                        {
                            if (parsedHeight < 20)
                            {
                                intHeight = 20;
                            }
                            else if (parsedHeight > 40)
                            {
                                intHeight = 40;
                            }
                            else
                            {
                                intHeight = parsedHeight;
                            }
                        }
                        else
                        {
                            intHeight = tetrisGameManager.rows;
                        }

                        double doubleInterval;
                        if (double.TryParse(interval, out double parsedInterval))
                        {
                            if (parsedInterval < 300)
                            {
                                doubleInterval = 300;
                            }
                            else if (parsedInterval > 2000)
                            {
                                doubleInterval = 2000;
                            }
                            else
                            {
                                doubleInterval = parsedInterval;
                            }
                        }
                        else
                        {
                            doubleInterval = tetrisGameManager.interval;
                        }

                        tetrisGameManager = new GameManager(intHeight, intWidth);
                        tetrisGameManager.interval = doubleInterval;
                        tetrisGameManager.settings = true;
                        tetrisGameManager.StartGame();
                    }
                    else if (chooseGameOver == "3")
                    {
                        if (File.Exists("save.bin"))
                        {
                            Stream stream = new FileStream("save.bin", FileMode.Open);
                            BinaryFormatter formatter = new BinaryFormatter();
                            tetrisGameManager = (GameManager)formatter.Deserialize(stream);
                            tetrisGameManager.nextBlock = (Block)formatter.Deserialize(stream);
                            stream.Close();

                            tetrisGameManager.loadGame = true;
                            tetrisGameManager.StartGame();
                        }
                        else
                        {
                            Console.WriteLine("Brak zapisanej gry. Następuje wyjście z gry.");
                            exit = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Niepoprawna odpowiedź. Następuje wyjście z gry.");
                        exit = true;
                    }
                }
            }
        }
    }
}