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

        //private League league = League.Instance;
        private Form _mainForm = null;


        public void OnMessage(MessageKey name, object param)
        {
            if(!this.Visible || !this.Enabled)
            {
                return;
            }
            switch (name)
            {
                case MessageKey.FinishLeague:
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

        public EvolvForm(Form mainForm)
        {
            _mainForm = mainForm;

            Messager.Instance.Register(MessageKey.FinishLeague, this);

            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            _mainForm.Visible = false;
            StartButton.Enabled = false;
            _main.StartLeague("league/generation1/children", 1);
        }

        private void OnFinishOne(棋子 side, int times)
        {
            DisplayLabel.Text = side.ToString() + "  " + times;
        }

        private void OnFinish(LeagueResult[][] result)
        {
            DisplayLabel.Text = "All Finished\n";
            if(result != null)
            {
                for(int i = 0; i < result.Length; i ++)
                {
                    for(int j = 0; j < result[i].Length; j ++)
                    {
                        LeagueResult r = result[i][j];
                        DisplayLabel.Text += r.Win + " " + r.Lose + " " + r.Equal+ " || ";
                    }
                    DisplayLabel.Text += "\n";
                }
            }
            _mainForm.Visible = true;
        }

        private void EvolvForm_FormClosing(object sender, FormClosingEventArgs e)
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
