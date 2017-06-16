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

        public SqlConnection Connectie = new SqlConnection(ConfigurationManager.ConnectionStrings["TOP2000ConnectionString"].ConnectionString);


        public Artiest_Bewerken()
        {
            InitializeComponent();
            Loaded();
        }

        private void BTNAanpassen_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Wilt u de gegevens echt aanpassen?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                SqlConnection Connectie = new SqlConnection(ConfigurationManager.ConnectionStrings["TOP2000ConnectionString"].ConnectionString);
                try
                {
                    using (Connectie)
                    {

                        Connectie.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "UpdateArtiest";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = Connectie;
                        cmd.Parameters.AddWithValue("@artiest", CBArtiestNaam.SelectedValue);

                        if (TBArtiestNaam.Text.Length == 0)
                        {
                            cmd.Parameters.AddWithValue("@naam", CBArtiestNaam.SelectedValue);
                        }
                        if (TBArtiestNaam.Text.Length != 0)
                        {
                            cmd.Parameters.AddWithValue("@naam", TBArtiestNaam.Text);
                        }
                        if (TBArtiestUrl.Text.Length == 0)
                        {
                            cmd.Parameters.AddWithValue("@url", DBNull.Value);
                        }
                        if (TBArtiestUrl.Text.Length != 0)
                        {
                            cmd.Parameters.AddWithValue("@url", TBArtiestUrl.Text);
                        }
                        if (TBArtiestBiografie.Text.Length == 0)
                        {
                            cmd.Parameters.AddWithValue("@biografie", DBNull.Value);
                        }
                        if (TBArtiestBiografie.Text.Length != 0)
                        {
                            cmd.Parameters.AddWithValue("@biografie", TBArtiestBiografie.Text);
                        }

                        {


                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Artiest is succesvol geupdate!");
                        }

                    }
                }
                catch
                {
                    MessageBox.Show("Artiest kon niet worden bijgewerkt");
                }
                CBArtiestNaam.SelectedItem = 0;
                TBArtiestNaam.Text = null;
                TBArtiestUrl.Text = null;
                TBArtiestBiografie.Text = null;
            }
            if (messageBoxResult == MessageBoxResult.No)
            {
                MessageBox.Show("Niet bijgewerkt!");
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
