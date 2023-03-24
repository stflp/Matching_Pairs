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
using System.Windows.Shapes;

namespace TEMA_1_PAIRS
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        public Statistics()
        {

            InitializeComponent();
            string[] users = File.ReadAllLines("database.txt");

            foreach(string line in users)
            {
                string[] parts = line.Split(' ');
                if(MainWindow.playingUser==parts[0])
                {
                    string image = parts[1];
                    usrImage.Source = new BitmapImage(new Uri(image));
                    myGames.Text = parts[2];
                    myWins.Text = parts[3];
                }
            }

        }
    }
}
