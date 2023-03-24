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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace TEMA_1_PAIRS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static string playingUser = null;

        public MainWindow()
        {
            InitializeComponent();
            if (!File.Exists("database.txt"))
                File.Create("database.txt");
            showUsersList();
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            if (userList.SelectedItem != null)
            {
                playingUser = ((ListBoxItem)userList.SelectedItem).Content.ToString();
                PlayMenu pm = new PlayMenu();
                pm.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Please select a user", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void newUser_Click(object sender, RoutedEventArgs e)
        {
            registerUser regUsr = new registerUser();
            regUsr.Show();
            Close();

            userList.Items.Clear();
            showUsersList();
        }

        private void deleteUser_Click(object sender, RoutedEventArgs e)
        {
            string file = "database.txt";

            var result = MessageBox.Show("Are you sure you want to delete user?", "Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                string currentUser = "";
                if (userList.SelectedItem != null)
                {
                    currentUser = ((ListBoxItem)userList.SelectedItem).Content.ToString();
                    MessageBox.Show("Deleted user " + currentUser, "", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                    List<string> lines = new List<string>(File.ReadAllLines(file));
                    lines.RemoveAll(line => line.Contains(currentUser));
                    File.WriteAllLines(file, lines);
                }
                else
                {
                    MessageBox.Show("Select a user", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                userList.Items.Clear();
                showUsersList();
            }
        }

        public void showUsersList()
        {
            string[] users = File.ReadAllLines("database.txt");

            foreach (string line in users)
            {
                string[] parts = line.Split(' ');
                ListBoxItem user = new ListBoxItem();
                user.Content = parts[0];
                userList.Items.Add(user);
            }
        }

        private void user_select(object sender, SelectionChangedEventArgs e)
        {

            string[] users = File.ReadAllLines("database.txt");

            string username = "";
            if (userList.SelectedItem != null)
            {
                username = ((ListBoxItem)userList.SelectedItem).Content.ToString();
                foreach (string line in users)
                {
                    string[] parts = line.Split(' ');
                    if (username == parts[0])
                    {
                        string image = parts[1];
                        userImage.Source = new BitmapImage(new Uri(image));
                    }
                }

            }
        }
    }
}
