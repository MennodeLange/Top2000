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
    /// Interaction logic for Artiest_Bewerken.xaml
    /// </summary>
    public partial class Artiest_Bewerken : Window
    {

        Artiest artiest = new Artiest();

        public SqlConnection Connectie = new SqlConnection(ConfigurationManager.ConnectionStrings["TOP2000ConnectionString"].ConnectionString);


        public Artiest_Bewerken()
        {
            InitializeComponent();
            Loaded();
          
        }
        public void Aanpassen()
        {
            
            string ArtiestUrl = TBArtiestUrl.Text;
            int UrlLengte = TBArtiestUrl.Text.Length;
            int BioLengte = TBArtiestBiografie.Text.Length;
            string BioText = TBArtiestBiografie.Text;
            string ArtiestNaam = TBArtiestNaam.Text;

            Artiest artiest = new Artiest();
            artiest.Bio = BioText;
            artiest.Naam = ArtiestNaam;
            artiest.Url = ArtiestUrl;   
        }

        private void BTNAanpassen_Click(object sender, RoutedEventArgs e)
        {
            Aanpassen();
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


                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                CBArtiestNaam.Items.Add(dt.Rows[i][0].ToString());
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
            CBArtiestNaam.SelectedIndex = 0;
        }
    }
}
