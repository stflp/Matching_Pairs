using System;
using System.Collections.Generic;
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
using System.IO;
using System.Text.RegularExpressions;

namespace TEMA_1_PAIRS
{
    /// <summary>
    /// Interaction logic for registerUser.xaml
    /// </summary>
    public partial class registerUser : Window
    {
        private static string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pfp");
        private string[] images = Directory.GetFiles(path, "*.jpg");
        private int pfpIndex = 0;
        private string pfp;

        public registerUser()
        {
            InitializeComponent();
            string[] images = Directory.GetFiles(path, "*.jpg");
            pfp = images[0];
            myImage.Source = new BitmapImage(new Uri(pfp));
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void regUser_Click(object sender, RoutedEventArgs e)
        {
            string username = regUsrBox.Text;
            string path = "database.txt";
            string regex = @"^[^\s]+$";

            string[] allUsers = File.ReadAllLines("database.txt");

            bool exists = false;

            if (Regex.IsMatch(username, regex))
            {
                foreach (string line in allUsers)
                {
                    string[] parts = line.Split(' ');
                    if (username == parts[0])
                    {
                        MessageBox.Show("Username already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        exists = true;
                    }

                }
                /*if (File.ReadLines(path).Any(line => line.Trim() == username))
                {
                    MessageBox.Show("Username already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }*/
                if (!exists)
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(username + " " + myImage.Source + " Games:0 Wins:0");
                    }
                    MessageBox.Show("Username added successfully", "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
            else
            {
                MessageBox.Show("Enter a valid name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void regUsrBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                regUser_Click(sender, e);
            }
        }

        private void nextImage(object sender, RoutedEventArgs e)
        {

            if (pfpIndex == 5) pfpIndex = 0;
            else pfpIndex++;

            pfp = images[pfpIndex];
            myImage.Source = new BitmapImage(new Uri(pfp));
        }
    }
}
