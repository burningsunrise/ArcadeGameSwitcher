using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LowLevelHooking;

namespace Vector
{
    class CycleThroughGames : Form
    {
        private int gameNumber = 0;
        private readonly List<Image> gamePictures = new List<Image> { Properties.Resources.SDVX_VIVID_WAVE, Properties.Resources.POPN_MUSIC };
        private readonly Dictionary<string, string> games = new Dictionary<string, string> {
            { "usc-game", @"C:\Users\burningsunrise\Desktop\USDVXC\usc-game.exe" },
            { "notepad", @"C:\Windows\System32\Notepad.exe" }
        };
        
        public CycleThroughGames(Form1 theForm)
        {
            this.TheForm = theForm;
            Program.GlobalKeyboardHook.KeyDownOrUp += GlobalKeyboardHook_KeyDownOrUp;
            Disposed += MainView_Disposed;
            AddPicture();
            StartGame();
            MinimizeForm();
        }

        public int GameNumber
        {
            get
            {
                return gameNumber;
            }
            set
            {
                if (value > gamePictures.Count - 1)
                {
                    gameNumber = 0;
                }
                else
                {
                    gameNumber = value;
                }
            }
        }
        public bool IsDown { get; set; }
        public Form1 TheForm { get; set; }
        public List<Process[]> ListOfProcesses { get; set; } = new List<Process[]>();
        public PictureBox CurrentPicture { get; set; } = new PictureBox();

        private void MainView_Disposed(object sender, EventArgs e)
        {
            Program.GlobalKeyboardHook.KeyDownOrUp -= GlobalKeyboardHook_KeyDownOrUp;
        }

        private void GlobalKeyboardHook_KeyDownOrUp(object sender, GlobalKeyboardHookEventArgs e)
        {
            // If we need to find key
            //Console.WriteLine($"{e.KeyCode} {(e.IsUp ? "up" : "down")}");
            if (e.KeyCode.ToString() == "Pause" && !e.IsUp && !IsDown)
            {
                IsDown = true;
                RemovePicture();
            }
            else if (e.KeyCode.ToString() == "Pause" && e.IsUp)
            {
                IsDown = false;
            }
        }

        private void MinimizeForm()
        {
            if (TheForm.WindowState == FormWindowState.Normal)
            {
                TheForm.Hide();
                TheForm.WindowState = FormWindowState.Minimized;
            }
        }

        private void MaximizeForm()
        {
            if (TheForm.WindowState == FormWindowState.Minimized)
            {
                TheForm.Show();
                TheForm.Activate();
                TheForm.WindowState = FormWindowState.Normal;
                TheForm.ShowInTaskbar = true;
            }
        }

        public void StartGame()
        {
            // Starts game at element 0 (startup)
            Process.Start(games.Values.ElementAt(GameNumber));
        }

        private void AddPicture()
        {
            CurrentPicture.Image = gamePictures[GameNumber];
            CurrentPicture.SizeMode = PictureBoxSizeMode.Zoom;
            CurrentPicture.Dock = DockStyle.Fill;
            TheForm.Controls.Add(CurrentPicture);
            // Always bring to the foreground on open and on key activation
            TheForm.WindowState = FormWindowState.Minimized;
            MaximizeForm();
            TheForm.ShowInTaskbar = true;
        }

        private void RemovePicture()
        {
            MaximizeForm();
            CurrentPicture.Refresh();
            if (TheForm.timer1.Enabled)
            {
                TheForm.timer1.Stop();
            }
            ++GameNumber;
            AddPicture();
            TheForm.TimeLeft = 5;
            TheForm.timer1.Start();
        }

        public void ChangeGame()
        {
            try
            {
                foreach (var name in games.Keys)
                {
                    ListOfProcesses.Add(Process.GetProcessesByName(name));
                }
                foreach (var processes in ListOfProcesses)
                {
                    foreach (var proc in processes)
                    {
                        proc.CloseMainWindow();
                        proc.WaitForExit();
                    }
                }
                StartGame();
                MinimizeForm();
                ListOfProcesses.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong with changing the game: {ex}");
            }
        }
    }
}
