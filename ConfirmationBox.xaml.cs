﻿using System;
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

namespace TEMA_1_PAIRS
{
    /// <summary>
    /// Interaction logic for ConfirmationBox.xaml
    /// </summary>
    public partial class ConfirmationBox : Window
    {
        public ConfirmationBox()
        {
            InitializeComponent();
        }

        private void Q2MM_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Q2D_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
