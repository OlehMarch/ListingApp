using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListingApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var res = folderBrowserDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                var path = folderBrowserDialog1.SelectedPath;
                DirSearch(path);
                var index = 1;
                using (var sw = new StreamWriter("listing.txt"))
                    foreach (var item in files)
                    {
                        if (item.ToLower().Contains("designer"))
                        {
                            continue;
                        }
                        using (var sr = new StreamReader(item))
                        {
                            var fileStr = sr.ReadToEnd();
                            string[] items = item.Split('\\');
                            sw.WriteLine(index++ + ". Клас " + items[items.Length-1]);
                            sw.Write(fileStr);
                            sw.WriteLine();
                            sw.WriteLine();
                            sw.WriteLine();
                        }
                    }

                MessageBox.Show("Finished");
            }
        }

        List<string> files = new List<string>();

        private void DirSearch(string sDir)
        {
            try
            {
                foreach (string f in Directory.GetFiles(sDir, "*.cs"))
                {
                    files.Add(f);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    DirSearch(d);
                }
            }
            catch (Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }
    }
}
