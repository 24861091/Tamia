using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 五子棋
{
    public partial class Main : Form , IListener
    {
        public int sizeX = 15;
        public int sizeY = 15;
        public int left = 550;
        public int top = 50;

        public int interval = 50;

        private DebugForm debugForm = null;

        private Label[] leftLabels = new Label[Utility.sizeY];
        private Label[] topLabels = new Label[Utility.sizeX];
        private Label[] rightLabels = new Label[Utility.sizeY];
        private Label[] bottomLabels = new Label[Utility.sizeX];

        private MainFrame _main = MainFrame.Instance;
        private 棋子[][] Chess
        {
            get
            {
                return _main.Chess;
            }
        }
        
        private List<Position> Mark
        {
            get
            {
                return _main.Mark;
            }
        }
        public bool CanShowTestButton = true;
        public Main()
        {
            
            InitializeComponent();
            InitLabels();

            SetPlayersBox();

            this.Width = left + sizeX * interval + interval * 2;
            this.Height = top + sizeY * interval + interval * 2;

            Messager.Instance.Register(MessageKey.FinishTurn, this);
            Messager.Instance.Register(MessageKey.Finish, this);
            Messager.Instance.Register(MessageKey.Restart, this);
            Messager.Instance.Register(MessageKey.Equal, this);

            debugForm = new DebugForm();
        }

        private void InitLabels()
        {
            for(int i = 0; i < leftLabels.Length; i ++)
            {
                leftLabels[i] = new Label();
                rightLabels[i] = new Label();
                topLabels[i] = new Label();
                bottomLabels[i] = new Label();
            }
        }
        private void Register()
        {
            AI.Creater.Register("HumanChessPlayer", new AI.HumanChessPlayer());
            AI.Creater.Register("程序测试用AI", new AI.程序测试用AI());
            AI.Creater.Register("10岁AI", new AI.DNAPlayer());
        }

        public void Finish(棋子 side)
        {
            DisplayLabel.Text = side.ToString() + " Win!";
            BlackPlayersBox.Enabled = true;
            WhitePlayersBox.Enabled = true;
        }

        private void DrawPanel(Graphics graphics)
        {
            if(graphics == null)
                return;
            Point start;
            Point end;

            for (int i = 0; i < sizeX; i++)
            {
                topLabels[i].Text = i.ToString();
                topLabels[i].Top = top - interval/2;
                topLabels[i].Left = left + i * interval- 8;
                topLabels[i].Width = interval;
                topLabels[i].Height = interval/3;
                topLabels[i].Font = new Font(topLabels[i].Font.FontFamily, interval / 4);
                this.Controls.Add(topLabels[i]);

                leftLabels[i].Text = i.ToString();
                leftLabels[i].Top = top + i * interval - 8;
                leftLabels[i].Left = left - interval / 2;
                leftLabels[i].Width = interval / 2;
                leftLabels[i].Height = interval / 3;
                leftLabels[i].Font = new Font(leftLabels[i].Font.FontFamily, interval / 4);
                this.Controls.Add(leftLabels[i]);

                start = new Point(left, top + i * interval);
                end = new Point(left + sizeX * interval - interval, top + i * interval);
                graphics.DrawLine(Pens.Black, start, end);

                start = new Point(left + i * interval , top);
                end = new Point(left + i * interval, top + sizeY * interval - interval);
                graphics.DrawLine(Pens.Black, start, end);
            }
        }

        private void DrawChess(Graphics graphics)
        {
            棋子[][] chess = Chess;
            if (chess == null)
            {
                return;
            }
            for (int i = 0; i < chess.Length; i ++)
            {
                for(int j = 0; j < chess[i].Length; j ++)
                {
                    DrawPosition(i, j, chess[i][j], graphics);
                    //DrawBlank(i, j , graphics);
                }
            }
            
        }
        private void DrawLast(Graphics graphics)
        {
            棋子[][] chess = Chess;
            if(chess == null)
            {
                return;
            }
            for (int i = 0; i < Mark.Count; i ++)
            {
                Position p = Mark[i];
                if(chess[p.X][p.Y] != 棋子.无)
                {
                    DrawMark(p.X, p.Y, graphics);
                }
            }
        }
        private void DrawPosition(int x, int y, 棋子 side, Graphics graphics)
        {
            Brush brush = null;
            if(side == 棋子.白子)
            {
                brush = Brushes.White;
            }
            else if(side == 棋子.黑子)
            {
                brush = Brushes.Black;
            }
            else
            {
                return;
            }

            graphics.FillEllipse(brush, left + x * interval - interval * 0.4f, top + y * interval - interval * 0.4f, interval * 0.8f, interval * 0.8f);
        }
        private void DrawMark(int x, int y, Graphics graphics)
        {
            Pen pen = Pens.Purple;
            graphics.DrawRectangle(pen, left + x * interval - interval * 0.4f, top + y * interval - interval * 0.4f, interval * 0.8f, interval * 0.8f);
        }
        private void DrawBlank(int x, int y, Graphics graphics)
        {
            Pen pen = Pens.White;
            graphics.DrawRectangle(pen, left + x * interval - interval * 0.4f, top + y * interval - interval * 0.4f, interval * 0.8f, interval * 0.8f);
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            DrawPanel(e.Graphics);
            DrawChess(e.Graphics);
            DrawLast(e.Graphics);
        }

        public void OnMessage(MessageKey name, object param)
        {
            if(this.Enabled && this.Visible)
            {
                switch (name)
                {
                    case MessageKey.Finish:
                        棋子 side = (棋子)(param);
                        Finish(side);
                        break;
                    case MessageKey.FinishTurn:
                        this.Invalidate();
                        break;
                    case MessageKey.Equal:
                        Finish(棋子.无);
                        break;
                    case MessageKey.Restart:
                        ChessPlayer[] players = param as ChessPlayer[];
                        this.Restart(players[0], players[1]);
                        break;
                }
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            int indexX = -1;
            int indexY = -1;

            int deltaX = e.X - left + interval / 2;
            if (deltaX < 0) return;
            indexX = deltaX / interval;
            
            int deltaY = e.Y - top + interval / 2;
            if (deltaY < 0) return;
            indexY = deltaY / interval;

            Messager.Instance.SendMessageLater(MessageKey.MouseDown, new int[] { indexX, indexY });
        }

        private void OnClickStartButton(object sender, EventArgs e)
        {
            _main.Restart(Utility.CreatePlayer(BlackPlayersBox.Text), Utility.CreatePlayer(WhitePlayersBox.Text));
        }
        public void Restart(ChessPlayer black, ChessPlayer white)
        {
            BlackPlayersBox.Enabled = false;
            WhitePlayersBox.Enabled = false;
            this.DisplayLabel.Text = "开始！";
            this.Invalidate();
            if (Utility.IsDebugOpen && !debugForm.Visible)
            {
                debugForm.Show();
            }
        }
        private void SetPlayersBox()
        {
            BlackPlayersBox.Items.Clear();
            WhitePlayersBox.Items.Clear();
            Assembly assembly = Assembly.GetAssembly(typeof(ChessPlayer));
            Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                Type type = types[i];
                if (type.IsSubclassOf(typeof(ChessPlayer)) && !type.IsAbstract)
                {
                    if(Directory.Exists(type.Name))
                    {
                        string[] files = Directory.GetFiles(type.Name);
                        for(int j = 0; j < files.Length; j ++)
                        {
                            string file = files[j];
                            file = Path.GetFileName(file);
                            BlackPlayersBox.Items.Add(type.Name + "_" + file);
                            WhitePlayersBox.Items.Add(type.Name + "_" + file);
                        }
                    }
                    else
                    {
                        BlackPlayersBox.Items.Add(type.Name);
                        WhitePlayersBox.Items.Add(type.Name);
                    }
                }
            }

            if (BlackPlayersBox.Items.Count > 0)
            {
                BlackPlayersBox.Text = typeof(AI.HumanChessPlayer).Name;/*BlackPlayersBox.Items[0].ToString();*/
            }
            if (WhitePlayersBox.Items.Count > 0)
            {
                WhitePlayersBox.Text = typeof(AI.程序测试用AI).Name; /*WhitePlayersBox.Items[0].ToString();*/
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Utility.IsDebugOpen = checkBox1.Checked;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Utility.IsDebugOpen = checkBox1.Checked;
        }

        private void EvolvButton_Click(object sender, EventArgs e)
        {
            EvolvForm form = new EvolvForm(this);
            form.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ArenaForm form = new ArenaForm(this);
            form.Show();
        }
    }
}
