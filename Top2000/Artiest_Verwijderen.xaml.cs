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

using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;

namespace Top2000
{

    /// <summary>
    /// Interaction logic for Artiest_Verwijderen.xaml
    /// </summary>
    public partial class Artiest_Verwijderen : Window
    {
        public SqlConnection Connectie = new SqlConnection(@"Data Source=(localdb)\MSSQLLocaldb;Initial Catalog=TOP2000;Integrated Security=True");

        public Artiest_Verwijderen()
        {
            InitializeComponent();
            Loaded();
        }

        private void BTNVerwijderen_Click(object sender, RoutedEventArgs e)
        {

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
