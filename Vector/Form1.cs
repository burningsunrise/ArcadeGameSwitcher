using System;
using System.Drawing;
using System.Windows.Forms;

namespace Vector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Make transparent
            BackColor = Color.Lime;
            TransparencyKey = Color.Lime;
            FormBorderStyle = FormBorderStyle.None;
            this.notifyIcon1.MouseDoubleClick += this.NotifyIcon1_MouseDoubleClick;
            this.ContextMenu1 = new ContextMenu();
            this.MenuItem1 = new MenuItem();
            this.ContextMenu1.MenuItems.AddRange(new MenuItem[] { this.MenuItem1 });

            this.MenuItem1.Index = 0;
            this.MenuItem1.Text = "E&xit";
            this.MenuItem1.Click += new EventHandler(this.MenuItem1_Click);

            notifyIcon1.ContextMenu = this.ContextMenu1;
        }

        public int TimeLeft { get; set; }
        public ContextMenu ContextMenu1 { get; }
        public MenuItem MenuItem1 { get; }
        internal CycleThroughGames GameCycle { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.GameCycle = new CycleThroughGames(this);
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

        private void NotifyIcon1_MouseDoubleClick(object Sender, EventArgs e)
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

        private void MenuItem1_Click(object sender, EventArgs e)
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
                GameCycle.ChangeGame();
            }
        }
    }
}
