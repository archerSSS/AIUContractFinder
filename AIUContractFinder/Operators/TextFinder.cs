using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AIUContractFinder.Operators
{
    class TextFinder
    {
        private string text;


        public TextFinder()
        {
        }

        public TextFinder(string t)
        {
            text = t;
        }

        public void SetText(string t)
        {
            text = t;
        }

        private void Find()
        {
            Thread.Sleep(2000);
            
        }


        public string FindText(string p)
        {
            StreamReader SR = new StreamReader(p);
            string str = SR.ReadLine();
            while (!string.IsNullOrEmpty(str))
            {
                if (str.Contains(text)) return p;
                str = SR.ReadLine();
            }
            SR.Close();

            return "";
        }
    }
}
