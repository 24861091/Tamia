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
        private int blackInterval = 30;
        private int blackLeft = 30;
        private int blackTop = 30;
        private Label[][] blackLabels = null;

        private int whiteInterval = 30;
        private int whiteLeft = 30;
        private int whiteTop = 30;
        private Label[][] whiteLabels = null;

        public DebugForm()
        {
            InitializeComponent();
            Messager.Instance.Register(MessageKey.FinishTurn, this);
            Messager.Instance.Register(MessageKey.RefreshDebug, this);
        }

        public void DrawPanel<T>(T[][] positions, int left, int top, int interval,ref Label[][] labels)
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
                    //l.Width = interval;
                    l.AutoSize = true;
                    l.Height = interval / 2;
                    l.Text = positions[i][j].ToString();
                    this.Controls.Add(l);
                    l.Show();
                }
            }
        }
        public void OnMessage(MessageKey name, object param)
        {
            switch(name)
            {
                case MessageKey.RefreshDebug:
                    object[] test = param as object[];
                    float[][] black = test[0] as float[][];
                    float[][] white = test[1] as float[][];
                    Refresh(black, white);
                    
                    break;
                default:
                    break;
            }

        }
        public void Refresh<T>(T[][] black, T[][] white)
        {
            int top = black.Length * blackInterval + blackTop + blackInterval * 1 + whiteTop;
            Refresh(black, blackLeft, blackTop, blackInterval,ref blackLabels);
            Refresh(white, whiteLeft, top, whiteInterval,ref whiteLabels);

            if(black !=null)
            {
                this.Width = blackLeft + black[0].Length * blackInterval + blackInterval;
            }
            if(white != null)
            {
                this.Height = top + white.Length * whiteInterval + whiteInterval;
            }
            else
            {
                this.Height = blackTop + black.Length * blackInterval + blackInterval;
            }
            
        }

        public void Refresh<T>(T[][] positions, int left, int top, int interval,ref Label[][] labels)
        {
            if(positions == null)
            {
                return;
            }
            if(labels == null)
            {
                DrawPanel(positions, left, top, interval,ref labels);
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

        private void DebugForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            MessageBox.Show(e.CloseReason.ToString());
        }

        private void DebugForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.MinimizeBox = true;
        }
    }
}
