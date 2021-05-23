using AIUContractFinder.Operators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIUContractFinder
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PathCollector PC;
        private string baseDirectionInfo;
        TextBox[] numberFields;
        TextBox[] contractFields;

        bool up;
        bool windowClosed;

        public MainWindow()
        {
            InitializeComponent();


            baseDirectionInfo = "Contracts direction";
            PC = new PathCollector();
            string dir = Directory.GetCurrentDirectory() + "/ContractsDirection.txt";

            StreamReader SR = new StreamReader(dir);
            string str = SR.ReadLine();
            SR.Close();
            if (str.Contains(baseDirectionInfo))
            {
                string s = str.Substring(21);
                PC.AddPath(s);
            }

            numberFields = new TextBox[10];
            numberFields[0] = TextNumber1;
            numberFields[1] = TextNumber2;
            numberFields[2] = TextNumber3;
            numberFields[3] = TextNumber4;
            numberFields[4] = TextNumber5;
            numberFields[5] = TextNumber6;
            numberFields[6] = TextNumber7;
            numberFields[7] = TextNumber8;
            numberFields[8] = TextNumber9;
            numberFields[9] = TextNumber10;

            contractFields = new TextBox[10];
            contractFields[0] = TextContract1;
            contractFields[1] = TextContract2;
            contractFields[2] = TextContract3;
            contractFields[3] = TextContract4;
            contractFields[4] = TextContract5;
            contractFields[5] = TextContract6;
            contractFields[6] = TextContract7;
            contractFields[7] = TextContract8;
            contractFields[8] = TextContract9;
            contractFields[9] = TextContract10;

            //CreateFiles("alp", 800);
            /*Thread t = new Thread(AnimateButton);
            t.Start();*/
        }


        // For Testers Only
        #region
        private void CreateFiles(string baseName, int nn)
        {
            for (int i = 0; i < nn; i++)
            {
                StreamWriter SW = new StreamWriter("E:/Univers/" + baseName + i + ".txt");
                Random r = new Random();

                for (int j = 0; j < 10000; j++)
                {
                    string n = r.Next(999999) + "";
                    n += r.Next(999999) + "";
                    n += r.Next(999999) + "";
                    SW.WriteLine(n);
                }
                SW.Close();
            }
        }
        #endregion


        private void AnimateButton()
        {
            if (windowClosed) return; 
            Dispatcher.Invoke(() =>
            {
                /*double d = ButtonAnimated.Margin.Bottom;
                if (up)
                {
                    d += 0.01;
                    if (d > 120) up = false;
                }
                else
                {
                    d -= 0.01;
                    if (d < 70) up = true;
                }

                Thickness tt = ButtonAnimated.Margin;
                tt.Bottom = d;
                ButtonAnimated.Margin = tt;*/
            });
            Thread t = new Thread(AnimateButton);
            t.Start();
        }

        private void CollectFiles_Click(object sender, RoutedEventArgs e)
        {
            if (!(PC.GetPaths().Count > 1))
            {
                string root = PC.GetPath(0);
                List<string> paths = PC.CollectChildPaths(root);
                PC.SetPaths(paths);
                PC.AddPath(root);
                PC.CollectFilesPaths();
            }
        }

        private void FindFile_Click(object sender, RoutedEventArgs e)
        {
            TextFinder TF = new TextFinder();
            for (int i = 0; i < numberFields.Length; i++)
            {
                if (!string.IsNullOrEmpty(numberFields[i].Text))
                    TF.SetText(numberFields[i].Text);
                else continue;

                string contract = "";
                foreach (string p in PC.GetFiles())
                {
                    contract = TF.FindText(p);
                    if (!string.IsNullOrEmpty(contract))
                    {
                        contractFields[i].Text = contract;
                        break;
                    }
                }
                if (string.IsNullOrEmpty(contract)) contractFields[i].Text = "NONE";
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < numberFields.Length; i++)
            {
                numberFields[i].Text = "";
                contractFields[i].Text = "";
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            windowClosed = true;
            base.OnClosing(e);
        }
    }
}
