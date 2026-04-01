namespace тема_8
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            
            this.gamePanel = new System.Windows.Forms.Panel();
            this.newGameButton = new System.Windows.Forms.Button();
            this.statsPanel = new System.Windows.Forms.Panel();
            this.shotsLabel = new System.Windows.Forms.Label();
            this.hitsLabel = new System.Windows.Forms.Label();
            this.missesLabel = new System.Windows.Forms.Label();
            this.shipsLabel = new System.Windows.Forms.Label();
            this.helpLabel = new System.Windows.Forms.Label();
            this.showButton = new System.Windows.Forms.Button();
            
            this.menuStrip.SuspendLayout();
            this.statsPanel.SuspendLayout();
            this.SuspendLayout();
            
            // menuStrip
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.fileMenuItem, this.settingsMenuItem, this.helpMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            
            // File menu
            this.fileMenuItem.Text = "Файл";
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.newGameMenuItem, this.exitMenuItem});
            
            this.newGameMenuItem.Text = "Новая игра";
            this.newGameMenuItem.Click += new System.EventHandler(this.BtnNewGame_Click);
            
            this.exitMenuItem.Text = "Выход";
            this.exitMenuItem.Click += (s, e) => this.Close();
            
            // Settings menu
            this.settingsMenuItem.Text = "Настройки";
            this.settingsMenuItem.Click += (s, e) => this.ShowSettings();
            
            // Help menu
            this.helpMenuItem.Text = "Справка";
            this.helpMenuItem.Click += (s, e) => this.ShowHelp();
            
            // statusStrip
            this.statusStrip.Items.Add(this.statusLabel);
            this.statusStrip.Location = new System.Drawing.Point(0, 528);
            this.statusStrip.Size = new System.Drawing.Size(800, 22);
            this.statusLabel.Text = "Готов к игре";
            
            // gamePanel
            this.gamePanel.Location = new System.Drawing.Point(20, 60);
            this.gamePanel.Size = new System.Drawing.Size(500, 500);
            this.gamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gamePanel.BackColor = System.Drawing.Color.LightBlue;
            this.gamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelGame_Paint);
            this.gamePanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PanelGame_MouseClick);
            
            // newGameButton
            this.newGameButton.Location = new System.Drawing.Point(20, 570);
            this.newGameButton.Size = new System.Drawing.Size(200, 40);
            this.newGameButton.Text = "НОВАЯ ИГРА";
            this.newGameButton.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            this.newGameButton.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.newGameButton.ForeColor = System.Drawing.Color.White;
            this.newGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newGameButton.Click += new System.EventHandler(this.BtnNewGame_Click);
            
            // statsPanel
            this.statsPanel.Location = new System.Drawing.Point(540, 60);
            this.statsPanel.Size = new System.Drawing.Size(240, 300);
            this.statsPanel.BackColor = System.Drawing.Color.White;
            this.statsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statsPanel.Controls.Add(this.shotsLabel);
            this.statsPanel.Controls.Add(this.hitsLabel);
            this.statsPanel.Controls.Add(this.missesLabel);
            this.statsPanel.Controls.Add(this.shipsLabel);
            this.statsPanel.Controls.Add(this.helpLabel);
            
            // Labels
            this.shotsLabel.Location = new System.Drawing.Point(15, 20);
            this.shotsLabel.Size = new System.Drawing.Size(210, 25);
            this.shotsLabel.Font = new System.Drawing.Font("Arial", 14);
            this.shotsLabel.Text = "Выстрелов: 0";
            
            this.hitsLabel.Location = new System.Drawing.Point(15, 55);
            this.hitsLabel.Size = new System.Drawing.Size(210, 25);
            this.hitsLabel.Font = new System.Drawing.Font("Arial", 14);
            this.hitsLabel.ForeColor = System.Drawing.Color.Green;
            this.hitsLabel.Text = "Попаданий: 0";
            
            this.missesLabel.Location = new System.Drawing.Point(15, 90);
            this.missesLabel.Size = new System.Drawing.Size(210, 25);
            this.missesLabel.Font = new System.Drawing.Font("Arial", 14);
            this.missesLabel.ForeColor = System.Drawing.Color.Red;
            this.missesLabel.Text = "Промахов: 0 / 30";
            
            this.shipsLabel.Location = new System.Drawing.Point(15, 130);
            this.shipsLabel.Size = new System.Drawing.Size(210, 25);
            this.shipsLabel.Font = new System.Drawing.Font("Arial", 14);
            this.shipsLabel.ForeColor = System.Drawing.Color.Blue;
            this.shipsLabel.Text = "Кораблей: 0 / 10";
            
            this.helpLabel.Location = new System.Drawing.Point(15, 170);
            this.helpLabel.Size = new System.Drawing.Size(210, 100);
            this.helpLabel.Font = new System.Drawing.Font("Arial", 10);
            this.helpLabel.Text = "Управление:\n\nЛКМ - выстрел\nПКМ - пометить\n\nЧисло = корабли рядом";
            
            // showButton
            this.showButton.Location = new System.Drawing.Point(540, 370);
            this.showButton.Size = new System.Drawing.Size(240, 35);
            this.showButton.Text = "Показать корабли";
            this.showButton.Font = new System.Drawing.Font("Arial", 10);
            this.showButton.Click += new System.EventHandler(this.BtnShowAll_Click);
            
            // Form1
            this.ClientSize = new System.Drawing.Size(800, 620);
            this.Controls.Add(this.showButton);
            this.Controls.Add(this.statsPanel);
            this.Controls.Add(this.newGameButton);
            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Text = "Морской бой";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Panel gamePanel;
        private System.Windows.Forms.Button newGameButton;
        private System.Windows.Forms.Panel statsPanel;
        private System.Windows.Forms.Label shotsLabel;
        private System.Windows.Forms.Label hitsLabel;
        private System.Windows.Forms.Label missesLabel;
        private System.Windows.Forms.Label shipsLabel;
        private System.Windows.Forms.Label helpLabel;
        private System.Windows.Forms.Button showButton;
    }
}
