namespace Tetris
{
    partial class Visualizer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TitleLabel = new Label();
            InstructionsLabel = new Label();
            tetrisGamePanel = new Panel();
            pause_play = new PictureBox();
            timer = new System.Windows.Forms.Timer(components);
            gameOverLabel = new Label();
            newGameButton = new Button();
            nextBlockLabel = new Label();
            nextBlockPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)pause_play).BeginInit();
            SuspendLayout();
            // 
            // TitleLabel
            // 
            TitleLabel.AutoSize = true;
            TitleLabel.Font = new Font("Segoe UI", 48F, FontStyle.Bold, GraphicsUnit.Point);
            TitleLabel.ForeColor = SystemColors.ControlText;
            TitleLabel.Location = new Point(50, 50);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(200, 86);
            TitleLabel.TabIndex = 0;
            TitleLabel.Text = "Tetris";
            // 
            // InstructionsLabel
            // 
            InstructionsLabel.AutoSize = true;
            InstructionsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            InstructionsLabel.Location = new Point(75, 200);
            InstructionsLabel.Name = "InstructionsLabel";
            InstructionsLabel.Size = new Size(137, 126);
            InstructionsLabel.TabIndex = 1;
            InstructionsLabel.Text = "Sterowanie:\r\nA - Ruch w lewo\r\nS - Ruch w dół\r\nD - Ruch w prawo\r\nQ - Obrót w lewo\r\nE - Obrót w prawo\r\n";
            // 
            // tetrisGamePanel
            // 
            tetrisGamePanel.BackColor = SystemColors.ScrollBar;
            tetrisGamePanel.Location = new Point(350, 100);
            tetrisGamePanel.Name = "tetrisGamePanel";
            tetrisGamePanel.Size = new Size(250, 400);
            tetrisGamePanel.TabIndex = 2;
            // 
            // pause_play
            // 
            pause_play.BackgroundImage = Properties.Resources.play;
            pause_play.BackgroundImageLayout = ImageLayout.Stretch;
            pause_play.Location = new Point(608, 12);
            pause_play.Name = "pause_play";
            pause_play.Size = new Size(64, 64);
            pause_play.TabIndex = 3;
            pause_play.TabStop = false;
            pause_play.Click += button_Click;
            // 
            // timer
            // 
            timer.Interval = 750;
            timer.Tick += timer_tick;
            // 
            // gameOverLabel
            // 
            gameOverLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            gameOverLabel.Location = new Point(75, 200);
            gameOverLabel.Name = "gameOverLabel";
            gameOverLabel.Size = new Size(200, 100);
            gameOverLabel.TabIndex = 4;
            gameOverLabel.Text = "Przegrałeś/aś!\r\n\r\nNowy blok wylosował się w zajętym miejscu.";
            gameOverLabel.Visible = false;
            // 
            // newGameButton
            // 
            newGameButton.BackColor = SystemColors.ButtonHighlight;
            newGameButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            newGameButton.Location = new Point(75, 300);
            newGameButton.Name = "newGameButton";
            newGameButton.Size = new Size(200, 50);
            newGameButton.TabIndex = 5;
            newGameButton.Text = "Nowa gra";
            newGameButton.UseVisualStyleBackColor = false;
            newGameButton.Visible = false;
            newGameButton.Click += newGameButton_Click;
            // 
            // nextBlockLabel
            // 
            nextBlockLabel.AutoSize = true;
            nextBlockLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            nextBlockLabel.Location = new Point(200, 375);
            nextBlockLabel.Name = "nextBlockLabel";
            nextBlockLabel.Size = new Size(126, 21);
            nextBlockLabel.TabIndex = 6;
            nextBlockLabel.Text = "Następny blok:";
            // 
            // nextBlockPanel
            // 
            nextBlockPanel.Location = new Point(225, 400);
            nextBlockPanel.Name = "nextBlockPanel";
            nextBlockPanel.Size = new Size(95, 95);
            nextBlockPanel.TabIndex = 7;
            // 
            // Visualizer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(684, 661);
            Controls.Add(nextBlockPanel);
            Controls.Add(nextBlockLabel);
            Controls.Add(newGameButton);
            Controls.Add(gameOverLabel);
            Controls.Add(pause_play);
            Controls.Add(tetrisGamePanel);
            Controls.Add(InstructionsLabel);
            Controls.Add(TitleLabel);
            MinimumSize = new Size(700, 700);
            Name = "Visualizer";
            Text = "Tetris";
            KeyDown += Visualizer_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pause_play).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label TitleLabel;
        private Label InstructionsLabel;
        private Panel tetrisGamePanel;
        private PictureBox pause_play;
        private System.Windows.Forms.Timer timer;
        private Label gameOverLabel;
        private Button newGameButton;
        private Label nextBlockLabel;
        private Panel nextBlockPanel;
    }
}