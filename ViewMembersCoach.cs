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
    /// ViewMembersCoach class which allows the coach to view player records
    /// Grant Shaw
    /// Graded Unit
    /// 01/05/2019
    /// </summary>
    //Inherits from Window (for certain functionality)
    class ViewMembersCoach : Window
    {
        // declaring Buttons , variables and others objects
        public Button EmailBtn { get; set; }
        public Button JuniorBtn { get; set; }

        public Button AdultBtn { get; set; }

        public Button ResetBtn { get; set; }

        public Canvas WindowCanvas { get; set; }
      
        public string User { get; set; }

       public  DataGrid myDataGrid { get; set; }

        public SqlConnection con { get; set; }

        public Label SRLabel { get; set; }
        public Button ReturnBtn { get; set; }

       

        //constructor
        public ViewMembersCoach()
        {
                    
            InitializeWindow();

        }

       
        //sets the properties for the window
        //Creates a canvas 
        //Calls DrawWindow which Draws the objects
        //Set Connection which opens the SQL connection
        //Update Data grid which populates the DataGrid with a table
        //SetUpPageEvents which handles button clicks.

        protected void InitializeWindow()
        {
            this.MaxHeight = 450;
            this.MaxWidth = 1200;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowCanvas = new Canvas();
            this.Content = WindowCanvas;
            DrawWindow();
            this.setConnection();
            this.UpdateDataGrid();


            SetUpPageEvents();

        }

        //handles click events
        protected virtual void SetUpPageEvents()
        {
            EmailBtn.Click += EmailBtn_Click;
            ResetBtn.Click += ResetBtn_Click;
            JuniorBtn.Click += JuniorBtn_Click;
            AdultBtn.Click += AdultBtn_Click;
            ReturnBtn.Click += ReturnBtn_Click;
            
            

        }

        //Draws the Buttons and Labels , adds them as Children to Canvas and positions them.

        protected virtual void DrawWindow()
        {
            EmailBtn = new Button();
            EmailBtn.Content = "View Emails";
           
            EmailBtn.FontWeight = FontWeights.Bold;

            ResetBtn = new Button();
            ResetBtn.Content = "Reset";
            
            ResetBtn.FontWeight = FontWeights.Bold;

            myDataGrid = new DataGrid();
            myDataGrid.Height = 301;
            myDataGrid.Width = 820;

            SRLabel = new Label();
            SRLabel.Content = "Simply Rugby Member List";
            SRLabel.Width = 300;
            SRLabel.Height = 40;
            SRLabel.FontWeight = FontWeights.Bold;
            SRLabel.FontSize = 20;
            SRLabel.FontFamily = new FontFamily("Times New Roman");


            ReturnBtn = new Button();
            ReturnBtn.Content = "Return";
            ReturnBtn.FontWeight = FontWeights.Bold;


            JuniorBtn = new Button();
            JuniorBtn.Content = "Junior Players";
            
            JuniorBtn.FontWeight = FontWeights.Bold;

            AdultBtn = new Button();
            AdultBtn.Content = "Senior Players";
            
            AdultBtn.FontWeight = FontWeights.Bold;

            WindowCanvas.Children.Add(EmailBtn);
            WindowCanvas.Children.Add(JuniorBtn);
            WindowCanvas.Children.Add(AdultBtn);
            WindowCanvas.Children.Add(myDataGrid);
            WindowCanvas.Children.Add(ResetBtn);
            WindowCanvas.Children.Add(SRLabel);
            WindowCanvas.Children.Add(ReturnBtn);
            

            Canvas.SetLeft(EmailBtn, 25);
            Canvas.SetTop(EmailBtn, 380);

            Canvas.SetLeft(JuniorBtn, 100);
            Canvas.SetTop(JuniorBtn, 380);

            Canvas.SetLeft(AdultBtn, 187);
            Canvas.SetTop(AdultBtn, 380);

            Canvas.SetLeft(ResetBtn, 270);       
            Canvas.SetTop(ResetBtn, 380);

            Canvas.SetLeft(ReturnBtn, 170);
            Canvas.SetTop(ReturnBtn, 350);

            Canvas.SetLeft(myDataGrid, 340);
            Canvas.SetTop(myDataGrid, 100);

            Canvas.SetLeft(SRLabel, 440);
            Canvas.SetTop(SRLabel, 30);


        }

        //Fills the Datagrid with the table.
        protected void UpdateDataGrid()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT ID_No, Name, SRUnum, DOB, PhoneNumber, Email, Squad, PERMISSION FROM PLAYERS;";
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            myDataGrid.ItemsSource = dt.DefaultView;
            dr.Close();

        }

        //Opens SQL connection using connection string.
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
                MessageBox.Show("connection to database cannot be opened");

            }
        }

        //A switch which executes appropriate SQL Statement and displays a message when Complete.
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

                
                case 1:
                    msg = "Showing All Junior Players";
                    break;
                case 2:
                    msg = "Showing all Adult Players";
                    break;
                case 3:
                    msg = "Showing all Email addresses";
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

        //a method which updates the grid
        protected virtual void reset()
        {

            this.UpdateDataGrid();
        }

        //A button which displays all email addresses.
        private void EmailBtn_Click(object sender,RoutedEventArgs e)
        {
            string sql = "SELECT EMAIL FROM PLAYERS;";
            DisplaySwitch(sql, 3);

        }

        //a Button which displays the entire contents of table.
        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {

            reset();

        }

        //A Button which shows only adult members
        private void AdultBtn_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT ID_No, Name, SRUnum, DOB, PhoneNumber, Email, Squad, PERMISSION FROM PLAYERS WHERE Squad='U21' OR Squad ='Senior';";
            DisplaySwitch(sql, 2);

        }

        
        /// <summary>
        /// A button click event which filters  only Junior Players
        /// </summary>
        
        private void JuniorBtn_Click(object sender, RoutedEventArgs e)
        {

            string sql = "SELECT ID_No, Name, SRUnum, DOB, PhoneNumber, Email, Squad, PERMISSION FROM PLAYERS WHERE Squad='U18' OR Squad = 'U16' OR Squad = 'Junior' ;";
            DisplaySwitch(sql, 1);
        }

        //A button which returns to the StartPage, it passes the current User (a static variable) so that the user doesn't need to login again.
        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            StartPage userpage = new StartPage(StartPage.User);
            userpage.Show();
            
            this.Close();
        }




    }
}
