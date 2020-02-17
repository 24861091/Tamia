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

        private Rule rule = null;
        private DebugForm debugForm = null;

        private Label[] leftLabels = new Label[Utility.sizeY];
        private Label[] topLabels = new Label[Utility.sizeX];
        private Label[] rightLabels = new Label[Utility.sizeY];
        private Label[] bottomLabels = new Label[Utility.sizeX];

        private 棋子[][] Chess
        {
            get
            {
                return rule.GetPanel().positions;
            }
        }
        
        private List<Position> Mark
        {
            get
            {
                return rule.GetPanel().Mark;
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

            Messager.Instance.Register(MessageKey.ChangeTurn, this);
            Messager.Instance.Register(MessageKey.Finish, this);
            Messager.Instance.Register(MessageKey.MakeStep, this);
            Messager.Instance.Register(MessageKey.Restart, this);

            rule = new Rule();
            rule.Initialize(sizeX, sizeY);
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
        private ChessPlayer CreatePlayer(string typeName)
        {
            string[] s = typeName.Split('_');
            if (s != null)
            {
                Type type = Type.GetType("五子棋.AI." + s[0]);
                if (type != null)
                {
                    ChessPlayer player = Activator.CreateInstance(type) as ChessPlayer;
                    if (s.Length == 2)
                    {
                        player.Name = s[1];
                    }
                    else
                    {
                        player.Name = s[0];
                    }

                    return player;
                }

            }
            return null;

        }
        private ChessPlayer CreateBlackPlayer()
        {
            string typeName = BlackPlayersBox.Text;
            return CreatePlayer(typeName);
        }
        private ChessPlayer CreateWhitePlayer()
        {
            string typeName = WhitePlayersBox.Text;
            return CreatePlayer(typeName);
        }

        public void TakeTurn(棋子 side)
        {

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
            if (rule == null) return;
            棋子[][] chess = Chess;
            for(int i = 0; i < chess.Length; i ++)
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
            //DrawMark(x, y, graphics);
            //DrawBlank(x, y, graphics);
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
            switch(name)
            {
                case MessageKey.ChangeTurn:
                    棋子 turn = (棋子)(param);
                    TakeTurn(turn);
                    break;
                case MessageKey.Finish:
                    棋子 side = (棋子)(param);
                    Finish(side);
                    
                    break;
                case MessageKey.MakeStep:
                    this.Invalidate();
                    break;
                case MessageKey.Restart:
                    this.Restart();
                    break;
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

            Messager.Instance.SendMessage(MessageKey.MouseDown, new int[] { indexX, indexY });
        }

        private void OnClickStartButton(object sender, EventArgs e)
        {
            BlackPlayersBox.Enabled = false;
            WhitePlayersBox.Enabled = false;

            Restart();

            
            if(Utility.IsDebugOpen)
            {
                debugForm.Show();
            }
        }
        public void Restart()
        {
            this.DisplayLabel.Text = "开始！";
            rule.Clear();
            rule.SetChessPlayers(CreateBlackPlayer(), CreateWhitePlayer());
            rule.ChangeTurn();
            rule.OnYourTurn();
            
            this.Invalidate();

        }
        private void SetPlayersBox()
        {
            //Register();

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

            //Dictionary<string, ChessPlayer> dics = AI.Creater.Dics;
            //foreach(KeyValuePair<string, ChessPlayer> pair in dics)
            //{
            //    BlackPlayersBox.Items.Add(pair.Key);
            //    WhitePlayersBox.Items.Add(pair.Key);
            //}

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
            //if(this.debugForm.IsDisposed)
            //{
                
            //} 
            //else
            //{
            //    this.debugForm.Show();
            //}
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Utility.IsDebugOpen = checkBox1.Checked;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Utility.IsDebugOpen = checkBox1.Checked;
        }
    }
}
