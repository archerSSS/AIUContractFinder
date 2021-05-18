using AIUContractFinder.Operators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        private string baseFilesDirection;

        public MainWindow()
        {
            InitializeComponent();

            baseFilesDirection = "Contracts direction";
            PC = new PathCollector();
            string dir = Directory.GetCurrentDirectory() + "/ContractsDirection.txt";

            StreamReader SR = new StreamReader(dir);
            string str = SR.ReadLine();
            if (str.Contains(baseFilesDirection))
            {
                string s = str.Substring(21);
            }

            PC.AddPath("C:/SRPDContracts");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string p = PC.GetPath(0);
            List<string> l = PC.CollectChildPaths(p);
            PC.SetPaths(l);
            PC.CollectFilesPaths();
        }
    }
}
