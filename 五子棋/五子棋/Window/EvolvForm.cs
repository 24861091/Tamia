using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 五子棋
{
    public partial class EvolvForm : Form, IListener
    {
        private int _startGeneration = 0;
        private int _lastGeneration = 1;
        private float _evolvFactor = 1.0f;
        private int _amount = 100;

        private MainFrame _main = MainFrame.Instance;

        public void OnMessage(MessageKey name, object param)
        {
            switch (name)
            {
                case MessageKey.Restart:
                    break;
                case MessageKey.FinishTurn:
                    break;
                case MessageKey.Finish:
                    棋子 side = (棋子)(param);
                    MessageBox.Show(side.ToString() + " Win!");
                    StartButton.Enabled = true;
                    break;
                case MessageKey.Equal:
                    MessageBox.Show(" Equal!");
                    StartButton.Enabled = true;
                    break;
            }
        }

        private void StartChess(ChessPlayer black, ChessPlayer white)
        {
            
        }

        public EvolvForm()
        {
            Messager.Instance.Register(MessageKey.Restart, this);
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            CreatePlayer();
            StartButton.Enabled = false;
        }

        private void CreatePlayer()
        {
            _main.Restart(Utility.CreateDNAPlayer("1"), Utility.CreateDNAPlayer("2"));

        }

    }
}
