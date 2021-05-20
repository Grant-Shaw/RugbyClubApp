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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Data;


namespace Graded_Unit_research_4
{
    /// <summary>
    /// Player Profile sheet , where coach will enter skills for players.
    /// Grant Shaw
    /// Graded Unit
    /// 01/05/2019
    /// </summary>
    public partial class PlayerProfileSheet : Window
    {


        /// <summary>
        /// Declares an SQL connection object which will be used to open a connection to database
        /// </summary>
        private SqlConnection con { get; set; }

        public PlayerProfileSheet()
        {

            //uses base Initializecomponent
            //Sets connection (opens the connection to Database)
            
            InitializeComponent();
            setConnection();
            UpdateDataGrid();

            //Sets the parameters for the window
            this.MaxHeight = 800;
            this.MaxWidth = 1400;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            CommentTxt.MaxLength = 249;



        }

        //A method which displays the content of the Database in a data grid.
        private void UpdateDataGrid()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT  SRUnum, Name, Squad, LastUpdate, Standard, Spin , Pop , Front, Rear, Side, Scrabble, Dr0p ,Punt, Grubber, Goal, Comments FROM PLAYERS;";
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            myDataGrid.ItemsSource = dt.DefaultView;
            dr.Close();

        }

        //A button which uses an SQL update statement to update entries in the database with the data entered into the textboxes where the ID numbers match.
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
           
            string sql = "UPDATE players SET LastUpdate = @LastUpdate, Standard = @Standard, Spin = @Spin, Pop = @Pop, Front = @Front, Rear = @Rear, Side = @Side, Scrabble = @Scrabble, Dr0p = @Dr0p, Punt = @Punt, Grubber = @Grubber, Goal = @Goal, Comments = @Comments WHERE SRUnum = @SRUnum;";
            string msg = "";
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;

            try
            {

                //Creates a player object which is used to validate the data that has been entered
               
                    Player player1 = new Player(DateTime.Now, Convert.ToInt32(StdTxt.Text), Convert.ToInt32(SpinTxt.Text), Convert.ToInt32(PopTxt.Text), Convert.ToInt32(FrontTxt.Text), Convert.ToInt32(RearTxt.Text),
                                               Convert.ToInt32(SideTxt.Text), Convert.ToInt32(ScrabbleTxt.Text), Convert.ToInt32(DropTxt.Text), Convert.ToInt32(PuntTxt.Text), Convert.ToInt32(GrubberTxt.Text), Convert.ToInt32(GoalTxt.Text));

               
                

                msg = "Player updated Successfully!";
                cmd.Parameters.Add("@LastUpdate", SqlDbType.Date).Value = player1.Update;
                cmd.Parameters.Add("@Standard", SqlDbType.VarChar).Value = Convert.ToString(player1.Std);
                cmd.Parameters.Add("@Spin", SqlDbType.VarChar).Value = player1.Spin.ToString();
                cmd.Parameters.Add("@Pop", SqlDbType.VarChar).Value = player1.Pop.ToString();
                cmd.Parameters.Add("@Front", SqlDbType.VarChar).Value = player1.Front.ToString();
                cmd.Parameters.Add("@Rear", SqlDbType.VarChar).Value = player1.Rear.ToString();
                cmd.Parameters.Add("@Side", SqlDbType.VarChar).Value = player1.Side.ToString();
                cmd.Parameters.Add("@Scrabble", SqlDbType.VarChar).Value = player1.Scrabble.ToString();
                cmd.Parameters.Add("@Dr0p", SqlDbType.VarChar).Value = player1.Drop.ToString();
                cmd.Parameters.Add("@Punt", SqlDbType.VarChar).Value = player1.Punt.ToString();
                cmd.Parameters.Add("@Grubber", SqlDbType.VarChar).Value = player1.Grubber.ToString();
                cmd.Parameters.Add("@Goal", SqlDbType.VarChar).Value = player1.Goal.ToString();
                cmd.Parameters.Add("@Comments", SqlDbType.VarChar).Value = CommentTxt.Text;



                cmd.Parameters.Add("@SRUnum", SqlDbType.VarChar).Value = SruTxt.Text;
            }
            
            catch(Exception f)
            {

                MessageBox.Show(f.Message);
            }

            try
            {
                //a check which checks to see if the Query has executed correctly
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show(msg);
                    UpdateDataGrid();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Please ensure you don't leave any blanks & your comment is less than 250 characters");
            }

            



        }
        
        //A button which closes the current page and opens up the StartPage again (as the correct user).
        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            StartPage userpage = new StartPage(StartPage.User);
            userpage.Show();

            this.Close();

        }

        //opens the connection to the database
        private void setConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            con = new SqlConnection(connectionString);

            try
            {
                con.Open();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
        }

        //a method which takes an SQL statement as input and then uses it to display the correct results in the Datagrid with an appropriate message.
        private void DisplaySwitch(string sql, int num)
        {
            string msg = "";
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            myDataGrid.ItemsSource = dt.DefaultView;
            dr.Close();

            switch (num)
            {
               
                case 0:
                    msg = "Showing All Junior Players";
                    break;
                case 1:
                    msg = "Showing all Adult Players";
                    break;            
            }

            try
            {
                MessageBox.Show(msg);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }



        }

        //a button which displays all Senior players in the datagrid.
        private void AdultBtn_Click(object sender, RoutedEventArgs e)
        {
                string sql = "SELECT SRUnum, Name, Squad, LastUpdate, Standard, Spin , Pop , Front, Rear, Side, Scrabble, Dr0p ,Punt, Grubber, Goal FROM PLAYERS WHERE Squad='U21' OR Squad ='Senior';";
                DisplaySwitch(sql, 1);

            
        }

        //A button which displays all junior players in the datagrid.
        private void JuniorBtn_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT SRUnum, Name, Squad, LastUpdate, Standard, Spin , Pop , Front, Rear, Side, Scrabble, Dr0p ,Punt, Grubber, Goal FROM PLAYERS WHERE Squad='U18' OR Squad = 'U16' OR Squad = 'Junior';";
            DisplaySwitch(sql, 0);

        }
        
        //A method which displays the detials of the player selected on the datagrid within the text boxes provided.
        private void MyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            try
            {
               
                DataGrid dg = sender as DataGrid;
                DataRowView dr = dg.SelectedItem as DataRowView;
                if (dr != null)
                {
                    NameTxt.Text = dr["Name"].ToString();
                    SruTxt.Text = dr["SRUnum"].ToString();
                    DateTxt.Text = dr["LastUpdate"].ToString();
                    SqdTxt.Text = dr["Squad"].ToString();
                    StdTxt.Text = dr["Standard"].ToString();
                    SpinTxt.Text = dr["Spin"].ToString();
                    PopTxt.Text = dr["Pop"].ToString();
                    FrontTxt.Text = dr["Front"].ToString();
                    RearTxt.Text = dr["Rear"].ToString();
                    SideTxt.Text = dr["Side"].ToString();
                    ScrabbleTxt.Text = dr["Scrabble"].ToString();
                    DropTxt.Text = dr["Dr0p"].ToString();
                    PuntTxt.Text = dr["Punt"].ToString();
                    GrubberTxt.Text = dr["Grubber"].ToString();
                    GoalTxt.Text = dr["Goal"].ToString();
                    CommentTxt.Text = dr["Comments"].ToString();
                    
                    UpdateBtn.IsEnabled = true;
                    

                }
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
            
        }

        //a method which clears all boxes.
        private void Clearbtn_Click(object sender, RoutedEventArgs e)
        {


            NameTxt.Text = "";
            SruTxt.Text = "";
            DateTxt.Text = "";
            SqdTxt.Text = "";
            StdTxt.Text = "";
            SpinTxt.Text = "";
            PopTxt.Text = "";
            FrontTxt.Text = "";
            RearTxt.Text = "";
            SideTxt.Text = "";
            ScrabbleTxt.Text = "";
            DropTxt.Text = "";
            PuntTxt.Text = "";
            GrubberTxt.Text = "";
            GoalTxt.Text = "";
            CommentTxt.Text = "";



            UpdateDataGrid();
        }


            public void TextBox_TextChanged(object sender, RoutedEventArgs e)
            {



            }
        
    

       
    }
}
