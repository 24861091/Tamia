using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using 五子棋.AI;

namespace 五子棋
{
    public partial class ArenaForm : Form, IListener
    {
        private MainFrame _main = MainFrame.Instance;
        private Form _mainForm = null;


        public void OnMessage(MessageKey name, object param)
        {
            if(!this.Visible || !this.Enabled)
            {
                return;
            }
            switch (name)
            {
                case MessageKey.FinishArena:
                    Finish();
                    break;
            }
        }
        private void Finish()
        {
            StartButton.Enabled = true;
        }
        private void StartChess(ChessPlayer black, ChessPlayer white)
        {
            
        }

        public ArenaForm(Form mainForm)
        {
            _mainForm = mainForm;

            Messager.Instance.Register(MessageKey.FinishArena, this);

            InitializeComponent();
        }
        private void ArenaForm_Load(object sender, EventArgs e)
        {
            TimesText.Text = 1000.ToString();
            //ToText.Text = 2.ToString();
            //TopNumText.Text = 3.ToString();
            ChildrenNumText.Text = 1000.ToString();
            MutationRateText.Text = 50.ToString();
            MutationMinText.Text = 0.05f.ToString();
            MutationMaxText.Text = 10f.ToString();
            GenerationFactorText.Text = 100000.ToString();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            _mainForm.Visible = false;
            StartButton.Enabled = false;
            int times = int.Parse(TimesText.Text);
            //int to = int.Parse(ToText.Text);
            //int topNum = int.Parse(TopNumText.Text);
            //int childrenNum = int.Parse(ChildrenNumText.Text);
            //int mutationRate = int.Parse(MutationRateText.Text);
            //float mutationMin = float.Parse(MutationMinText.Text);
            //float mutationMax = float.Parse(MutationMaxText.Text);
            //int generationFactor = int.Parse(GenerationFactorText.Text);

            
            int black = 0;
            int white = 0;

            string path = Utility.CreateArenaPath();
            string[] files = Directory.GetFiles(path);
            if(files != null)
            {
                int min = 0;
                int max = files.Length;
                for (int i = 0; i < times; i++)
                {
                    black = 0;
                    white = 0;

                    while (white == black)
                    {
                        black = Utility.RandomInt(min, max);
                        white = Utility.RandomInt(min, max);
                    }
                    black = 2;
                    white = 4;
                    _main.StartArena(files[black], files[white]);
                }
            }
            else
            {
                MessageBox.Show("Shit! No File!");
            }

            MessageBox.Show("Done!");
        }

        private void ArenaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Messager.Instance.UnRegister(MessageKey.Restart, this);
            Messager.Instance.UnRegister(MessageKey.FinishTurn, this);
            Messager.Instance.UnRegister(MessageKey.Finish, this);
            Messager.Instance.UnRegister(MessageKey.Equal, this);

            e.Cancel = true;
            this.Visible = false;

        }
    }
}
