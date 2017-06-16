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
            SqlConnection Connectie = new SqlConnection(ConfigurationManager.ConnectionStrings["Top2000ConnectionString"].ConnectionString);
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Wilt u deze artiest toevoegen?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);

            if(TBArtiestNaam.Text.Length == 0 || TBArtiestUrl.Text.Length == 0 || TBArtiestBiografie.Text.Length == 0)
            {
                MessageBox.Show("Vul eerst alle velden in");
            }
            else
            {
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Connectie.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "AddArtiest";
                    cmd.Parameters.AddWithValue("@naam", TBArtiestNaam.Text);
                    cmd.Parameters.AddWithValue("@url", TBArtiestUrl.Text);
                    cmd.Parameters.AddWithValue("@biografie", TBArtiestBiografie.Text);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Connection = Connectie;
                    cmd.ExecuteNonQuery();
                    Connectie.Close();
                    MessageBox.Show("Artiest Toegevoegd!");
                    TBArtiestNaam.Text = "";
                    TBArtiestUrl.Text = "";
                    TBArtiestBiografie.Text = "";
                }
                if (messageBoxResult == MessageBoxResult.No)
                {
                    MessageBox.Show("Artiest niet Toegevoegd!");

                }
            }

        }
    }
}
