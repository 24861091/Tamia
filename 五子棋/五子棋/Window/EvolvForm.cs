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

        private MainFrame _main = new MainFrame();

        public void OnMessage(MessageKey name, object param)
        {
            switch (name)
            {
                case MessageKey.Restart:
                    break;
                case MessageKey.FinishTurn:
                    break;
                case MessageKey.Finish:
                    break;
            }
        }


        public EvolvForm()
        {
            Messager.Instance.Register(MessageKey.Restart, this);
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {

        }
    }
}
