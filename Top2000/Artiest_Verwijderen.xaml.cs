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

using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using BusinessLayer;
using DataLayer;

namespace Top2000
{

    /// <summary>
    /// Interaction logic for Artiest_Verwijderen.xaml
    /// </summary>
    public partial class Artiest_Verwijderen : Window
    {
        public SqlConnection Connectie = new SqlConnection(ConfigurationManager.ConnectionStrings["Top2000ConnectionString"].ConnectionString);

        public Artiest_Verwijderen()
        {
            InitializeComponent();
            Loaded();
        }

        private void BTNVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection Connectie = new SqlConnection(ConfigurationManager.ConnectionStrings["Top2000ConnectionString"].ConnectionString);
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Wilt u deze artiest echt verwijderen?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Connectie.Open();
                string artist = CBVerwijderArtiest.SelectedItem.ToString();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "RemoveArtiestZonderLied";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = Connectie;
                cmd.ExecuteNonQuery();
                Connectie.Close();
                MessageBox.Show("Artiest verwijderd!");
                CBVerwijderArtiest.Items.RemoveAt
                    (CBVerwijderArtiest.Items.IndexOf(artist));
                if (CBVerwijderArtiest.Items.Count == 0)
                {
                    MessageBox.Show("Geen Artiesten om te verwijderen. \r\n U kunt alleen artiesten verijwderen wanneer ze geen liedjes hebben");
                }
            }
            if (messageBoxResult == MessageBoxResult.No)
            {
                MessageBox.Show("Artiest niet verwijderd!");

            }
        }

        private void BTNTerug_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        public void Loaded()
        {
            try
            {
                using (Connectie)
                {

                    Connectie.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "GetAllArtiesten";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = Connectie;
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            string x = cmd.ExecuteReader().ToString();

                        
                                for (int i =0; i < dt.Rows.Count; i++ )
                                {
                                    CBVerwijderArtiest.Items.Add(dt.Rows[i][0].ToString());
                                    i++;
                                }
                            
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("kan de gegevens niet ophalen");
            }
        }
    }
}
