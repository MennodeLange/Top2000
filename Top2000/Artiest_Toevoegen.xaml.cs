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

        private void BTNToevoegen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTNTerug_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
