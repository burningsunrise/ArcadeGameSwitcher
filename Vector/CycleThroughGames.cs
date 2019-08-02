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
        bool isDown;
        private int gameNumber = 0;
        private Form1 theForm;
        private List<Process[]> listOfProcesses = new List<Process[]>();
        private PictureBox currentPicture = new PictureBox();
        private List<Image> gamePictures = new List<Image> { Properties.Resources.SDVX_VIVID_WAVE, Properties.Resources.POPN_MUSIC };
        private Dictionary<string, string> games = new Dictionary<string, string> {
            { "usc-game", @"C:\Users\burningsunrise\Desktop\USDVXC\usc-game.exe" },
            { "notepad", @"C:\Windows\System32\Notepad.exe" }
        };
        
        public CycleThroughGames(Form1 theForm)
        {

            this.theForm = theForm;
            Program.GlobalKeyboardHook.KeyDownOrUp += GlobalKeyboardHook_KeyDownOrUp;
            Disposed += MainView_Disposed;
            AddPicture();
            // Time to register that we're loading a game
            StartGame();
        }

        private void MainView_Disposed(object sender, EventArgs e)
        {
            Program.GlobalKeyboardHook.KeyDownOrUp -= GlobalKeyboardHook_KeyDownOrUp;
        }

        private void GlobalKeyboardHook_KeyDownOrUp(object sender, GlobalKeyboardHookEventArgs e)
        {
            // If we need to find key
            //Console.WriteLine($"{e.KeyCode} {(e.IsUp ? "up" : "down")}");
            if (e.KeyCode.ToString() == "Pause" && !e.IsUp && !isDown)
            {
                isDown = true;
                RemovePicture();
            }
            else if (e.KeyCode.ToString() == "Pause" && e.IsUp)
            {
                isDown = false;
            }
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

        private void MinimizeForm()
        {
            if (theForm.WindowState == FormWindowState.Normal)
            {
                theForm.Hide();
                theForm.WindowState = FormWindowState.Minimized;
            }
        }

        private void MaximizeForm()
        {
            if (theForm.WindowState == FormWindowState.Minimized)
            {
                theForm.Show();
                theForm.Activate();
                theForm.WindowState = FormWindowState.Normal;
                theForm.ShowInTaskbar = true;
            }
        }

        public void StartGame()
        {
            // Starts game at element 0 (startup)
            Process.Start(games.Values.ElementAt(GameNumber));
        }

        private void AddPicture()
        {
            currentPicture.Image = gamePictures[GameNumber];
            currentPicture.SizeMode = PictureBoxSizeMode.Zoom;
            currentPicture.Dock = DockStyle.Fill;
            theForm.Controls.Add(currentPicture);
            // Always bring to the foreground on open and on key activation
            theForm.WindowState = FormWindowState.Minimized;
            MaximizeForm();
            theForm.ShowInTaskbar = true;
        }

        private void RemovePicture()
        {
            MaximizeForm();
            currentPicture.Refresh();
            if (theForm.timer1.Enabled)
            {
                theForm.timer1.Stop();
            }
            ++GameNumber;
            AddPicture();
            theForm.TimeLeft = 5;
            theForm.timer1.Start();
        }

        public void ChangeGame()
        {
            try
            {
                foreach (var name in games.Keys)
                {
                    listOfProcesses.Add(Process.GetProcessesByName(name));
                }
                foreach (var processes in listOfProcesses)
                {
                    foreach (var proc in processes)
                    {
                        proc.CloseMainWindow();
                        proc.WaitForExit();
                    }
                }
                Process.Start(games.Values.ElementAt(GameNumber));
                MinimizeForm();
                listOfProcesses.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong with changing the game: {ex}");
            }
        }
    }
}
