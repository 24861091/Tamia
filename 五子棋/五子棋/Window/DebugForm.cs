using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 五子棋
{
    public partial class DebugForm : Form , IListener
    {
        private int interval = 50;
        private int left = 100;
        private int top = 100;
        private Label[][] labels = null;
        private 棋子[][] chess;
        public DebugForm()
        {
            InitializeComponent();
            Messager.Instance.Register(MessageKey.FinishTurn, this);
        }

        public void DrawPanel<T>(T[][] positions)
        {
            labels = new Label[positions.Length][];
            for (int i = 0; i < positions.Length; i ++)
            {
                labels[i] = new Label[positions[i].Length];
                for (int j  = 0; j < positions[i].Length; j++)
                {
                    Label l = new Label();
                    labels[i][j] = l;
                    l.Left = left + i * interval;
                    l.Top = top + j * interval;
                    l.Width = interval / 2;
                    l.Height = interval / 2;
                    l.Text = positions[i][j].ToString();
                    this.Controls.Add(l);
                    l.Show();
                }
            }
            this.Width = left + positions.Length * interval;
            this.Height = top + positions[0].Length * interval;
        }

        public void OnMessage(MessageKey name, object param)
        {
            switch(name)
            {
                case MessageKey.FinishTurn:
                    Refresh(程序测试用AI.sTest);
                    break;
                default:
                    break;
            }

        }

        public void Refresh<T>(T[][] positions)
        {
            if(labels == null)
            {
                DrawPanel(positions);
            }
            for (int i = 0; i < positions.Length; i++)
            {
                for (int j = 0; j < positions[i].Length; j++)
                {
                    Label l = labels[i][j];
                    l.Left = left + i * interval;
                    l.Top = top + j * interval;
                    l.Width = interval / 2;
                    l.Height = interval / 2;
                    l.Text = positions[i][j].ToString();
                    this.Controls.Add(l);
                    l.Show();
                }
            }

        }
    }
}
