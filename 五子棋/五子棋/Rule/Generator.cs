using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 五子棋.AI;

namespace 五子棋
{
    public class Generator
    {
        private static Generator sInstance = new Generator();
        public static Generator Instance
        {
            get
            {
                return sInstance;
            }
        }
        private Generator()
        {

        }
        private int childrenNum = 20;
        private int mutationRate = 50;
        private float mutationMin = 0.05f;
        private float mutationMax = 10f;
        private int generationFactor = 100000;
        
        public void Initialize(int childrenNum, int mutationRate, float mutationMin, float mutationMax, int generationFactor)
        {
            this.childrenNum = childrenNum;
            this.mutationRate = mutationRate;
            this.mutationMin = mutationMin;
            this.mutationMax = mutationMax;
            this.generationFactor = generationFactor;
        }


        private DNA[] GetParents(int generation)
        {
            string source = Utility.CreateSourcePath(generation);
            string[] files = Directory.GetFiles(source);
            if(files != null)
            {
                DNA[] fs = new DNA[files.Length];
                for(int i = 0; i < files.Length; i ++)
                {
                    string file = files[i];
                    fs[i] = new DNA(file, Path.GetFileName(file));
                    fs[i].Load();
                }
                return fs;
            }
            return null;
        }
        public void Generate(int generation)
        {
            DNA[] parents = GetParents(generation);
            int[] rates = new int[parents.Length];
            for(int i = 0; i < rates.Length; i ++)
            {
                rates[i] = 1;
            }
            if(parents != null && parents.Length > 0)
            {
                Utility.ClearDirectory(Utility.CreateTargetPath(generation));

                int num = parents.Length;
                for(int m = 0; m < num; m ++)
                {
                    DNA dna = new DNA(Path.Combine(Utility.CreateTargetPath(generation), parents[m].Name), parents[m].Name);
                    Dictionary<string, float> all = parents[m].GetAll();
                    foreach (KeyValuePair<string, float> pair in all)
                    {
                        dna.SetValue(pair.Key, pair.Value);
                    }
                    dna.Factor = parents[m].Factor;
                    dna.Generation = parents[m].Generation;
                    dna.Save();
                }
                for (int i = 0; i < childrenNum; i ++)
                {
                    string name = (generation * generationFactor + i).ToString();
                    DNA dna = new DNA(Path.Combine(Utility.CreateTargetPath(generation), name), name);
                    dna.InitDefaultValues();
                    Dictionary<string, float> all = parents[0].GetAll();
                    all.Remove("name");
                    all.Remove("generation");
                    all.Remove("f5");
                    //all.Remove("e4e");

                    all["selfFactor"] = 1f;

                    foreach (KeyValuePair<string, float> pair in all)
                    {
                        if (pair.Key == "rotcaFfles")
                        {

                        }
                        DNA chosen = Utility.RandomInt<DNA>(parents, rates);
                        float val = chosen.GetValue(pair.Key);
                        if (pair.Key == "selfFactor")
                        {
                            dna.Factor = (float)Utility.RandomValue(chosen.Factor, mutationRate, mutationMin, mutationMax);
                        }
                        else
                        {
                            dna.SetValue(pair.Key, (float)Utility.RandomValue(val, mutationRate, mutationMin, mutationMax));
                        }
                    }
                    
                    dna.Generation = generation;
                    dna.Save();
                }
            }
        }

    }
}
