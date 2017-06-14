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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;


namespace Top2000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {

        /// <summary>
        /// string die word gebruikt voor het menu
        /// </summary>
        public string openclosed = "Closed";
        /// <summary>
        /// de database connectie string
        /// </summary>
        public SqlConnection Connectie = new SqlConnection(@"Data Source=(localdb)\MSSQLLocaldb;Initial Catalog=TOP2000;Integrated Security=True");


        public MainWindow()
        {
            InitializeComponent();
            Loaded();
        }

        /// <summary>
        /// functie die de combobox vult met alle jaren die bestaan in de database
        /// </summary>
        public void MyFunction()
        {
            try
            {
                using (Connectie)
                {
                    Connectie.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "GetJaren";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = Connectie;

                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            ///string x is het eerste jaar van de top2000
                            string x = cmd.ExecuteScalar().ToString();
                            ///voor elk jaar dat de top2000 bestaat word ii met 1 verhoogd
                         
                            ///voor elk jaar word er een item toegevoegd aan de combobox die per jaar met 1 word verhoogd
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                CBJaar.Items.Add(dt.Rows[i][0].ToString());
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
        /// <summary>
        /// Functie die word aangeroepen wanneer het programma opstart
        /// </summary>
        public void Loaded()
        {
            MyFunction();
            CBJaar.SelectedIndex = CBJaar.Items.Count - 1;
        }
        /// <summary>
        /// Functie die word aangeroepen waneer de search textbox gefocussed is
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GotFocusE(object sender, RoutedEventArgs e)
        {
            TBSearch.Text = "";
        }
        /// <summary>
        /// Functie die word aangeroepen waneer de search textbox niet langer gefocussed is
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LostFocusE(object sender, RoutedEventArgs e)
        {
            TBSearch.Text = "search";
        }

     /// <summary>
     /// Functie die de groote van de menu container aanpast op click
     /// </summary>
     /// <param name="sender"></param>
     /// <param name="e"></param>
        public void MenuH_Click(object sender, RoutedEventArgs e)
        {
            if (openclosed == "Closed")
            {
                MenuCon.Height = 190;
                openclosed = "Open";
            }
            else
            {
                MenuCon.Height = 30;
                openclosed = "Closed";
            }
        }

        private void CBJaar_changed(object sender, System.EventArgs e)
        {
            ///haal de data op van het jaar dat gelijk is aan CBJaar.Selectedvalue uit de database
            ///nieuwe stored procedure met als paramater @jaartal waar  CBJaar.Selectedvalue aan word gegeven

            using (Connectie)
            {
              ///  SqlCommand cmd = new SqlCommand();
              ///  cmd.CommandText = "procedure die de goede data ophaalt";
              ///  cmd.CommandType = CommandType.StoredProcedure;
              ///  cmd.Connection = Connectie;

                {
                    ///geef het jaartal mee aan de parameter
                ///    cmd.Parameters.Add("@jaartal", SqlDbType.VarChar).Value = CBJaar.SelectedValue;

                ////    Connectie.Open();
                ///    cmd.ExecuteNonQuery();
                }
            } }

        private void Artiest_Toevoegen_Click(object sender, RoutedEventArgs e)
        {
            Artiest_Toevoegen Toevoegen = new Artiest_Toevoegen();
            Toevoegen.Show();
            this.Close();
        }

        private void Artiest_Verwijderen_Click(object sender, RoutedEventArgs e)
        {
            Artiest_Verwijderen Verwijderen = new Artiest_Verwijderen();
            Verwijderen.Show();
            this.Close();
        }

        private void Artiest_Bewerken_Click(object sender, RoutedEventArgs e)
        {
            Artiest_Bewerken Bewerken = new Artiest_Bewerken();
            Bewerken.Show();
            this.Close();
        }
        protected void TBSearch_TextChanged(object sender, EventArgs e)
        {
            MessageBox.Show("changed");
        }

        private void Jaar_Toevoegen_Click(object sender, RoutedEventArgs e)
        {
            Jaar_Toevoegen toevoegen = new Jaar_Toevoegen();
            toevoegen.Show();
            this.Close();
        }
    }
}
