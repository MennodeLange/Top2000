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
using System.Collections.ObjectModel;

using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;

using BusinessLayer;
using DataLayer;
namespace Top2000
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        public SqlConnection Connectie = new SqlConnection(ConfigurationManager.ConnectionStrings["Top2000ConnectionString"].ConnectionString);


        public int val = 0;
        /// <summary>
        /// string die word gebruikt voor het menu
        /// </summary>
        public string openclosed = "Closed";
        /// <summary>
        /// de database connectie string
        /// </summary>
        public int above = 0;
        public MainWindow()
        {
            InitializeComponent();
            Loaded();
            TBSearch.TextChanged += new TextChangedEventHandler(TextChanged);
        }

        /// <summary>
        /// functie die de combobox vult met alle jaren die bestaan in de database
        /// </summary>
        public void MyFunction()
        {
        SqlConnection Connectie = new SqlConnection(ConfigurationManager.ConnectionStrings["Top2000ConnectionString"].ConnectionString);

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
            CBJaar.SelectedIndex = 0;
            Top10.Background = Brushes.Black;

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
            above = 0;
            GetTop10();

        }

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

        private void Jaar_Toevoegen_Click(object sender, RoutedEventArgs e)
        {
            Jaar_Toevoegen toevoegen = new Jaar_Toevoegen();
            toevoegen.Show();
            this.Close();
        }

        public void BtnEerste_Click(object sender, RoutedEventArgs e)
        {
            above = 0;
            GetTop10();
        }

        public void BtnVorige_Click(object sender, RoutedEventArgs e)
        {
            above = above - 10;
            GetTop10();
        }

        public void BtnVolgende_Click(object sender, RoutedEventArgs e)
        {
            above = above + 10;
            GetTop10();
        }

        public void BtnLaatste_Click(object sender, RoutedEventArgs e)
        {
            above = 1990;
            GetTop10();
        }


        private void TextChanged(object Sender, TextChangedEventArgs e)
        {
            above = 0;
            if (TBSearch.Text.Length >= 3 && !(TBSearch.Text == "Search"))
            {
                GetTop10Search();

            }
            if (val != 0 && TBSearch.Text.Length < val)
            {
                GetTop10Search();
            }
            if (TBSearch.Text.Length == 0 && !(TBSearch.Text == "Search") || TBSearch.Text == "Search...")
            {
                GetTop10();
            }
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
            TBSearch.Text = "Search...";
        }



        public void GetTop10Search()
        {
            val = TBSearch.Text.Length;
            SqlConnection Connectie = new SqlConnection(ConfigurationManager.ConnectionStrings["Top2000ConnectionString"].ConnectionString);
            SqlDataAdapter CONad = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connectie;
            cmd.CommandText = "GetTop10Search";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@jaartal", SqlDbType.Int, 50).Value = CBJaar.SelectedValue;
            cmd.Parameters.Add("@above", SqlDbType.Int, 50).Value = above;
            cmd.Parameters.Add("@input", SqlDbType.VarChar, 100).Value = TBSearch.Text;
            CONad.SelectCommand = cmd;
            DataSet ds = new DataSet();
            CONad.Fill(ds);
            Top10.DataContext = ds.Tables[0].DefaultView;
            Connectie.Close();
        }

        public void GetTop10()
        {
            val = TBSearch.Text.Length;
            SqlConnection Connectie = new SqlConnection(ConfigurationManager.ConnectionStrings["Top2000ConnectionString"].ConnectionString);
            SqlDataAdapter CONad = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connectie;
            cmd.CommandText = "GetTop10";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@jaartal", SqlDbType.Int, 50).Value = CBJaar.SelectedValue;
            cmd.Parameters.Add("@above", SqlDbType.Int, 50).Value = above;
            CONad.SelectCommand = cmd;
            DataSet ds = new DataSet();
            CONad.Fill(ds);
            Top10.DataContext = ds.Tables[0].DefaultView;
            Connectie.Close();
        }

      
    }  
}
