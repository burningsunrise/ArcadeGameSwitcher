using System;
using System.Drawing;
using System.Windows.Forms;

namespace Vector
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private CycleThroughGames gameCycle;

        public Form1()
        {
            InitializeComponent();
            // Make transparent
            BackColor = Color.Lime;
            TransparencyKey = Color.Lime;
            FormBorderStyle = FormBorderStyle.None;
            this.notifyIcon1.MouseDoubleClick += this.notifyIcon1_MouseDoubleClick;
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.contextMenu1.MenuItems.AddRange(
                new System.Windows.Forms.MenuItem[] { this.menuItem1 });

            this.menuItem1.Index = 0;
            this.menuItem1.Text = "E&xit";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);

            notifyIcon1.ContextMenu = this.contextMenu1;
        }

        public int TimeLeft { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.gameCycle = new CycleThroughGames(this);
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object Sender, EventArgs e)
        {
            // Set the WindowState to normal if the form is minimized.
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
            }
            else
            {
                this.Hide();
                this.WindowState = FormWindowState.Minimized;
                // Maybe don't need? Try on a different computer before removing..
                //this.ShowInTaskbar = false;
            }
            // Activate the form.
            this.Activate();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timeLabel.ForeColor = Color.White;
            timeLabel.Font = new Font("Calibri", 25, FontStyle.Bold);
            timeLabel.BackColor = Color.Black;
            if (TimeLeft > 0)
            {
                if (!timeLabel.Visible)
                {
                    timeLabel.Visible = true;
                }
                TimeLeft -= 1;
                timeLabel.Text = TimeLeft.ToString();
            }
            else
            {
                timer1.Stop();
                timeLabel.Text = "0";
                timeLabel.Visible = false;
                gameCycle.ChangeGame();
            }
        }
    }
}
