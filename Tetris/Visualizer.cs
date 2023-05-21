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

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PictureBox pictureBox = new PictureBox();

                    pictureBox.Width = 20;
                    pictureBox.Height = 20;
                    pictureBox.Left = j * 25;
                    pictureBox.Top = i * 25;
                    pictureBox.BackColor = Color.White;

                    nextBlockList.Add(pictureBox);
                    nextBlockPanel.Controls.Add(pictureBox);
                }
            }

            tetrisGameManager.CreateEnvironment();
            CheckColor();
            tetrisGamePanel.Refresh();
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
            {
                for (int col = 0; col < 4; col++)
                {
                    nextBlockList[row * 4 + col].BackColor = Color.White;
                }
            }

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
            tetrisGamePanel.Refresh();
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
                tetrisGamePanel.Refresh();
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
    }
}