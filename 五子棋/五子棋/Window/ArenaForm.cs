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
            FromText.Text = 1.ToString();
            ToText.Text = 2.ToString();
            TopNumText.Text = 3.ToString();
            ChildrenNumText.Text = 10.ToString();
            MutationRateText.Text = 50.ToString();
            MutationMinText.Text = 0.05f.ToString();
            MutationMaxText.Text = 10f.ToString();
            GenerationFactorText.Text = 100000.ToString();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {


            _mainForm.Visible = false;
            StartButton.Enabled = false;
            int from = int.Parse(FromText.Text);
            int to = int.Parse(ToText.Text);
            int topNum = int.Parse(TopNumText.Text);
            int childrenNum = int.Parse(ChildrenNumText.Text);
            int mutationRate = int.Parse(MutationRateText.Text);
            float mutationMin = float.Parse(MutationMinText.Text);
            float mutationMax = float.Parse(MutationMaxText.Text);
            int generationFactor = int.Parse(GenerationFactorText.Text);

            Generator.Instance.Initialize(childrenNum, mutationRate, mutationMin, mutationMax, generationFactor);
            DateTime now = DateTime.Now;
            //Logger.Begin();
            for (int j = from; j < to; j++)
            {
                //Logger.Log("Generate 1");
                Generator.Instance.Generate(j);
                //Logger.Log("Generate 2");
                DNA[] dnas = _main.StartLeague(j, 1, topNum);
                //Logger.Log("Generate 3");
                if (dnas != null)
                {
                    string parent = Utility.CreateSourcePath(j + 1);
                    Utility.MakeSure(parent);
                    Utility.ClearDirectory(parent);

                    for (int i = 0; i < dnas.Length; i++)
                    {
                        DNA dna = dnas[i];
                        string source = dna.GetPath();
                        string target = Path.Combine(parent, Path.GetFileName(dna.GetPath()));
                        File.Copy(source, target);
                    }
                }
                else
                {
                    MessageBox.Show("Error!没找到最优DNA");
                }
                Debug.WriteLine("4 Time:" + new TimeSpan(DateTime.Now.Ticks - now.Ticks).TotalSeconds.ToString());
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
