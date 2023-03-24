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
    /// Interaction logic for PlayMenu.xaml
    /// </summary>
    public partial class PlayMenu : Window
    {
        public PlayMenu()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            Statistics statistics = new Statistics();
            statistics.Show();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            string usersPath = "database.txt";
            string temp = "temp.txt";

            string[] users = File.ReadAllLines(usersPath);


            using (var writer = File.CreateText(temp))
            {
                foreach (string line in users)
                {
                    string[] parts = line.Split(' ');
                    if (MainWindow.playingUser == parts[0])
                    {
                        string[] games = parts[2].Split(':');
                        int match = int.Parse(games[1]);
                        match += 1;

                        string newGames = "Games:" + match.ToString();

                        parts[2] = newGames;

                        writer.Write(parts[0] + " " + parts[1] + " " + parts[2] + " " + parts[3] + "\n");
                    }
                    else
                        writer.Write(parts[0] + " " + parts[1] + " " + parts[2] + " " + parts[3] + "\n");
                }
            }

            File.Delete(usersPath);
            File.Move(temp, usersPath);
            File.Delete(temp);


            Game game = new Game();
            game.Show();
            Close();
        }
    }
}
