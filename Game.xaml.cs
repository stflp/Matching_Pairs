using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace TEMA_1_PAIRS
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        private string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

        private List<Button> buttons = new List<Button>();
        private Button firstButton = null;
        private Button secondButton = null;
        private Image firstImage = null;
        private Image secondImage = null;


        public Game()
        {
            InitializeComponent();
            assignImagesToButtons();
        }

        private void Game_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to quit game?", "Warning", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
            else
            {
                ConfirmationBox cb = new ConfirmationBox();
                cb.Show();
            }
        }

        private void assignImagesToButtons()
        {
            List<string> buttonNames = new List<string> {
                "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "B10", "B11", "B12", "B13", "B14", "B15", "B16", "B17", "B18", "B19", "B20", "B21",
                "B22", "B23","B24"
            };

            string[] images = Directory.GetFiles(path, "*.jpg");
            string[] allImages = images.SelectMany(path => Enumerable.Repeat(path, 2)).ToArray();

            List<string> availableImageFilePaths = new List<string>(allImages);
            Random random = new Random();
            foreach (string buttonName in buttonNames)
            {
                string imagePath = availableImageFilePaths[random.Next(availableImageFilePaths.Count)];
                availableImageFilePaths.Remove(imagePath);

                Image image = new Image();
                image.Stretch = Stretch.Fill;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
                image.Source = bitmap;
                image.Visibility = Visibility.Collapsed;

                Button button = (Button)FindName(buttonName);
                if (button != null)
                {
                    button.Content = image;
                    buttons.Add(button);
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

                if (secondImage != null && firstImage != null)
            {
                firstImage.Visibility = Visibility.Collapsed;
                secondImage.Visibility = Visibility.Collapsed;
                secondImage = null;
                firstImage = null;
            }

            if (button != null)
            {
                if (firstButton == null)
                {
                    firstButton = button;
                    firstImage = (Image)button.Content;
                    firstImage.Visibility = Visibility.Visible;
                }
                else if (secondButton == null && button != firstButton)
                {
                    secondButton = button;
                    secondImage = (Image)secondButton.Content;
                    secondImage.Visibility = Visibility.Visible;
                    CheckPair(firstButton, secondButton);
                }
            }
        }

        private void CheckPair(Button fB, Button sB)
        {
            firstButton = fB;
            secondButton = sB;

            firstImage = (Image)firstButton.Content;
            secondImage = (Image)secondButton.Content;


            if (firstImage.Source.ToString() == secondImage.Source.ToString())
            {
                foreach (Button button in buttons)
                {
                    if (button == fB)
                    {

                        button.Content = "";
                        button.IsEnabled = false;
                    }
                    if (button == sB)
                    {
                        button.Content = "";
                        button.IsEnabled = false;
                    }
                }
                firstButton = null;
                secondButton = null;
            }
            else
            {
                firstButton = null;
                secondButton = null;
            }

            if (CheckIfWon())
            {
                MessageBox.Show("Congratulations, you won!");

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
                            string[] wins = parts[3].Split(':');
                            int win = int.Parse(wins[1]);
                            win += 1;

                            string newWins = "Wins:" + win.ToString();

                            parts[3] = newWins;

                            writer.Write(parts[0] + " " + parts[1] + " " + parts[2] + " " + parts[3] + "\n");
                        }
                        else
                            writer.Write(parts[0] + " " + parts[1] + " " + parts[2] + " " + parts[3] + "\n");
                    }
                }

                File.Delete(usersPath);
                File.Move(temp, usersPath);
                File.Delete(temp);

                Close();
            }
        }

        private bool CheckIfWon()
        {
            foreach (Button button in buttons)
            {
                if (button.Content != "")
                {
                    return false;
                }
            }
            return true;
        }


    }
}