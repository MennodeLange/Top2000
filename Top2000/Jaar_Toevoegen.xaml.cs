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
using BusinessLayer;
using DataLayer;
using System.Timers;
namespace Top2000
{
    /// <summary>
    /// Interaction logic for Jaar_Toevoegen.xaml
    /// </summary>
    public partial class Jaar_Toevoegen : Window
    {
        public Jaar_Toevoegen()
        {
            InitializeComponent();
        }

        private void BTNUploaden_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Wilt u dit jaar toevoegen?", "Zeker weten?", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {

                try
                {

                }
                catch
                {
                    MessageBox.Show("het gokezen bestan is niet geldig (kijk help)");
                }

                MessageBox.Show("Jaar Toegevoegd!!");
            }
            if (messageBoxResult == MessageBoxResult.No)
            {
                MessageBox.Show("Jaar niet Toegevoegd!!");
            }
        }

        private void BTNTerug_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void BTNHelp_Click(object sender, RoutedEventArgs e)
        {
            Help newhelp = new Help();
            newhelp.Show();       
        }

        private void BtnFileUploaden_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
