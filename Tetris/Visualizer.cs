using System.Runtime.Serialization.Formatters.Binary;

namespace Tetris
{
    public partial class Visualizer : Form
    {
        private GameManager tetrisGameManager = new GameManager(20, 10);
        private List<PictureBox> pictureBoxList = new List<PictureBox>();
        private List<PictureBox> nextBlockList = new List<PictureBox>();

        public Visualizer()
        {
            InitializeComponent();
            CreateEnvironment();
        }

        private void CreateEnvironment()
        {
            pictureBoxList = new List<PictureBox>();
            nextBlockList = new List<PictureBox>();
            tetrisGamePanel.Controls.Clear();
            nextBlockPanel.Controls.Clear();
            scoreValueLabel.Text = tetrisGameManager.scoreValue.ToString();

            tetrisGamePanel.Width = tetrisGameManager.columns * 25 - 5;
            tetrisGamePanel.Height = tetrisGameManager.rows * 25 - 5;

            for (int row = 0; row < tetrisGameManager.rows; row++)
            {
                for (int col = 0; col < tetrisGameManager.columns; col++)
                {
                    PictureBox pictureBox = new PictureBox();

                    pictureBox.Width = 20;
                    pictureBox.Height = 20;
                    pictureBox.Left = col * 25;
                    pictureBox.Top = row * 25;
                    pictureBox.BackColor = Color.White;

                    pictureBoxList.Add(pictureBox);
                    tetrisGamePanel.Controls.Add(pictureBox);
                }
            }

            nextBlockPanel.Width = nextBlockLabel.Width;
            nextBlockPanel.Height = nextBlockLabel.Width;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PictureBox pictureBox = new PictureBox();

                    pictureBox.Width = (int)(nextBlockLabel.Width / 4 * 0.8);
                    pictureBox.Height = (int)(nextBlockLabel.Width / 4 * 0.8);
                    pictureBox.Left = j * (int)(nextBlockLabel.Width / 4);
                    pictureBox.Top = i * (int)(nextBlockLabel.Width / 4);
                    pictureBox.BackColor = Color.White;

                    nextBlockList.Add(pictureBox);
                    nextBlockPanel.Controls.Add(pictureBox);
                }
            }

            tetrisGameManager.CreateEnvironment();
            CheckColor();
        }

        private void ChangeSize(object sender, EventArgs e)
        {
            bool timerStatus = timer.Enabled;

            if (timerStatus)
                timer.Stop();

            int blockHeight = (int)((this.Height - 200) / tetrisGameManager.rows * 0.8);
            int blockWidth = (int)((this.Width - 420) / tetrisGameManager.columns * 0.8);
            int space;

            if (blockHeight <= blockWidth)
            {
                blockWidth = blockHeight;
                space = (int)((this.Height - 200) / tetrisGameManager.rows * 0.2);
            }
            else
            {
                blockHeight = blockWidth;
                space = (int)((this.Width - 420) / tetrisGameManager.columns * 0.2);
            }

            tetrisGamePanel.Width = tetrisGameManager.columns * (blockWidth + space) - space;
            tetrisGamePanel.Height = tetrisGameManager.rows * (blockHeight + space) - space;

            for (int row = 0; row < tetrisGameManager.rows; row++)
            {
                for (int col = 0; col < tetrisGameManager.columns; col++)
                {
                    pictureBoxList[row * tetrisGameManager.columns + col].Width = blockWidth;
                    pictureBoxList[row * tetrisGameManager.columns + col].Height = blockHeight;
                    pictureBoxList[row * tetrisGameManager.columns + col].Left = col * (blockWidth + space);
                    pictureBoxList[row * tetrisGameManager.columns + col].Top = row * (blockHeight + space);
                }
            }

            if (timerStatus)
                timer.Start();
        }

        private void CheckColor()
        {
            for (int row = 0; row < tetrisGameManager.rows; row++)
            {
                for (int col = 0; col < tetrisGameManager.columns; col++)
                {
                    switch (tetrisGameManager[row, col])
                    {
                        case 1:
                            pictureBoxList[row * tetrisGameManager.columns + col].BackColor = Color.Cyan;
                            break;
                        case 2:
                            pictureBoxList[row * tetrisGameManager.columns + col].BackColor = Color.Blue;
                            break;
                        case 3:
                            pictureBoxList[row * tetrisGameManager.columns + col].BackColor = Color.Orange;
                            break;
                        case 4:
                            pictureBoxList[row * tetrisGameManager.columns + col].BackColor = Color.Yellow;
                            break;
                        case 5:
                            pictureBoxList[row * tetrisGameManager.columns + col].BackColor = Color.Green;
                            break;
                        case 6:
                            pictureBoxList[row * tetrisGameManager.columns + col].BackColor = Color.Purple;
                            break;
                        case 7:
                            pictureBoxList[row * tetrisGameManager.columns + col].BackColor = Color.Red;
                            break;
                        default:
                            pictureBoxList[row * tetrisGameManager.columns + col].BackColor = Color.White;
                            break;
                    }
                }
            }

            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                    nextBlockList[row * 4 + col].BackColor = Color.White;

            for (int row = 0; row < tetrisGameManager.nextBlockRows; row++)
            {
                for (int col = 0; col < tetrisGameManager.nextBlockColumns; col++)
                {
                    switch (tetrisGameManager.GetNextBlockShape(row, col))
                    {
                        case 1:
                            nextBlockList[row * 4 + col].BackColor = Color.Cyan;
                            break;
                        case 2:
                            nextBlockList[row * 4 + col].BackColor = Color.Blue;
                            break;
                        case 3:
                            nextBlockList[row * 4 + col].BackColor = Color.Orange;
                            break;
                        case 4:
                            nextBlockList[row * 4 + col].BackColor = Color.Yellow;
                            break;
                        case 5:
                            nextBlockList[row * 4 + col].BackColor = Color.Green;
                            break;
                        case 6:
                            nextBlockList[row * 4 + col].BackColor = Color.Purple;
                            break;
                        case 7:
                            nextBlockList[row * 4 + col].BackColor = Color.Red;
                            break;
                        default:
                            nextBlockList[row * 4 + col].BackColor = Color.White;
                            break;
                    }
                }
            }

            tetrisGamePanel.Refresh();
            nextBlockPanel.Refresh();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (timer.Enabled == false)
            {
                timer.Start();
                pause_play.BackgroundImage = Properties.Resources.pause;
            }
            else
            {
                timer.Stop();
                pause_play.BackgroundImage = Properties.Resources.play;
            }
        }

        private void timer_tick(object sender, EventArgs e)
        {
            tetrisGameManager.MakeTick();
            CheckColor();
            scoreValueLabel.Text = tetrisGameManager.scoreValue.ToString();
            CheckGameOver();
        }

        private void Visualizer_KeyDown(object sender, KeyEventArgs e)
        {
            if (timer.Enabled && (e.KeyCode == Keys.Q || e.KeyCode == Keys.E || e.KeyCode == Keys.A || e.KeyCode == Keys.S || e.KeyCode == Keys.D))
            {
                if (e.KeyCode == Keys.S)
                {
                    timer.Stop();
                    tetrisGameManager.CheckKeyDown(e);
                    timer.Start();
                }
                else
                {
                    tetrisGameManager.CheckKeyDown(e);
                }
                CheckColor();
                scoreValueLabel.Text = tetrisGameManager.scoreValue.ToString();
                CheckGameOver();
            }
        }

        private void CheckGameOver()
        {
            if (tetrisGameManager.gameOverStatus)
            {
                timer.Stop();

                pause_play.Visible = false;
                InstructionsLabel.Visible = false;
                nextBlockLabel.Visible = false;
                nextBlockPanel.Visible = false;
                gameOverLabel.Visible = true;
                newGameButton.Visible = true;
            }
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            gameOverLabel.Visible = false;
            newGameButton.Visible = false;
            pause_play.BackgroundImage = Properties.Resources.play;
            pause_play.Visible = true;
            InstructionsLabel.Visible = true;
            nextBlockLabel.Visible = true;
            nextBlockPanel.Visible = true;
            this.Focus();

            tetrisGameManager = new GameManager(tetrisGameManager.rows, tetrisGameManager.columns);

            CreateEnvironment();
        }

        private void ChangeSettings(object sender, EventArgs e)
        {
            timer.Stop();
            pause_play.BackgroundImage = Properties.Resources.play;

            Form settingsForm = new Form();
            settingsForm.Text = "Ustawienia";
            settingsForm.Size = new Size(350, 220);
            settingsForm.StartPosition = FormStartPosition.CenterScreen;
            settingsForm.FormBorderStyle = FormBorderStyle.FixedSingle; // Blokowanie zmiany rozmiaru okna

            Label widthLabel = new Label();
            widthLabel.Text = "Szerokoœæ planszy: (10-30)";
            widthLabel.AutoSize = true;
            widthLabel.Location = new Point(20, 20);
            settingsForm.Controls.Add(widthLabel);

            Label heightLabel = new Label();
            heightLabel.Text = "Wysokoœæ planszy: (20-40)";
            heightLabel.AutoSize = true;
            heightLabel.Location = new Point(20, 60);
            settingsForm.Controls.Add(heightLabel);

            Label intervalLabel = new Label();
            intervalLabel.Text = "Interwa³: (200ms-2000ms)";
            intervalLabel.AutoSize = true;
            intervalLabel.Location = new Point(20, 100);
            settingsForm.Controls.Add(intervalLabel);

            NumericUpDown widthNumericUpDown = new NumericUpDown();
            widthNumericUpDown.Minimum = 10;
            widthNumericUpDown.Maximum = 30;
            widthNumericUpDown.Value = tetrisGameManager.columns;
            widthNumericUpDown.Location = new Point(190, 15);
            settingsForm.Controls.Add(widthNumericUpDown);

            NumericUpDown heightNumericUpDown = new NumericUpDown();
            heightNumericUpDown.Minimum = 20;
            heightNumericUpDown.Maximum = 40;
            heightNumericUpDown.Value = tetrisGameManager.rows;
            heightNumericUpDown.Location = new Point(190, 55);
            settingsForm.Controls.Add(heightNumericUpDown);

            NumericUpDown intervalNumericUpDown = new NumericUpDown();
            intervalNumericUpDown.Minimum = 200;
            intervalNumericUpDown.Maximum = 2000;
            intervalNumericUpDown.Value = timer.Interval;
            intervalNumericUpDown.Location = new Point(190, 95);
            settingsForm.Controls.Add(intervalNumericUpDown);

            Button cancelButton = new Button();
            cancelButton.Text = "Anuluj";
            cancelButton.AutoSize = true;
            cancelButton.Location = new Point(60, 140);
            cancelButton.Click += (s, ev) =>
            {
                settingsForm.Close();
            };
            settingsForm.Controls.Add(cancelButton);

            Button submitButton = new Button();
            submitButton.Text = "WprowadŸ zmiany";
            submitButton.AutoSize = true;
            submitButton.Location = new Point(160, 140);
            submitButton.Click += (s, ev) =>
            {
                settingsForm.Close();

                tetrisGameManager = new GameManager(((int)heightNumericUpDown.Value), ((int)widthNumericUpDown.Value));
                timer.Interval = ((int)intervalNumericUpDown.Value);

                CreateEnvironment();

                ChangeSize(sender, EventArgs.Empty);

                gameOverLabel.Visible = false;
                newGameButton.Visible = false;
                pause_play.BackgroundImage = Properties.Resources.play;
                pause_play.Visible = true;
                InstructionsLabel.Visible = true;
                nextBlockLabel.Visible = true;
                nextBlockPanel.Visible = true;
                this.Focus();
            };
            settingsForm.Controls.Add(submitButton);

            settingsForm.ShowDialog();
        }

        private void SaveGame(object sender, EventArgs e)
        {
            Stream stream = new FileStream("save.bin", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, tetrisGameManager);
            formatter.Serialize(stream, tetrisGameManager.nextBlock);
            stream.Close();

            StreamWriter writer = new StreamWriter("save.txt");
            writer.WriteLine(this.Width);
            writer.WriteLine(this.Height);
            writer.WriteLine(timer.Interval);
            writer.WriteLine(timer.Enabled);
            writer.Close();
        }

        private void LoadGame(object sender, EventArgs e)
        {
            Stream stream = new FileStream("save.bin", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            tetrisGameManager = (GameManager)formatter.Deserialize(stream);
            tetrisGameManager.nextBlock = (Block)formatter.Deserialize(stream);
            stream.Close();

            StreamReader reader = new StreamReader("save.txt");
            this.Width = int.Parse(reader.ReadLine());
            this.Height = int.Parse(reader.ReadLine());
            timer.Interval = int.Parse(reader.ReadLine());
            bool timerStatus = bool.Parse(reader.ReadLine());
            reader.Close();

            if (timerStatus)
            {
                timer.Start();
                pause_play.BackgroundImage = Properties.Resources.pause;
            }
            else
            {
                timer.Stop();
                pause_play.BackgroundImage = Properties.Resources.play;
            }

            CreateEnvironment();
            ChangeSize(sender, e);
            CheckGameOver();
        }
    }
}