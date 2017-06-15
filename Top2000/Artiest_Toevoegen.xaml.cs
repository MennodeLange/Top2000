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
using BusinessLayer;
using DataLayer;

using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace Top2000
{
    /// <summary>
    /// Interaction logic for Artiest_Toevoegen.xaml
    /// </summary>
    public partial class Artiest_Toevoegen : Window
    {
        public Artiest_Toevoegen()
        {
            InitializeComponent();
        }

        private void BTNTerug_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void BTNAanpassen_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {







                MessageBox.Show("Artiest Toegevoegd!!");
            }

            if (messageBoxResult == MessageBoxResult.No)
            {
                MessageBox.Show("Artiest niet Toegevoegd!!");
            }
        }
    }
}
