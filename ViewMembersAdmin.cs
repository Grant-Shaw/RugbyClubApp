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
using System.Text.RegularExpressions;

namespace Graded_Unit_research_4
{
    /// <summary>
    /// ViewMembersAdmin class which allows the admin to edit , add and delete member records.
    /// Grant Shaw
    /// Graded Unit
    /// 01/05/2019
    /// </summary>
    //INHERITS from ViewMembersCoach
    class ViewMembersAdmin : ViewMembersCoach
    {
        //A viewMembersCoach object which will hold the object to be passed in
        ViewMembersCoach Window { get; set; }

        //buttons ,label, variable declaration etc.

        private Label IDlbl { get; set; }
        private Label    NameLbl { get; set; }
         private Label EmailLbl { get; set; }
        private Label SRULbl { get; set; }
        private Label DOBLbl { get; set; }
        private Label PhoneLBl { get; set; }
         private Label TeamLbl { get; set; }
         private Label PermisionLbl { get; set; }
         private TextBlock IdTxt { get; set; }
         private TextBox NameTxt { get; set; }
         private TextBox EmailTxt { get; set; }
         private TextBox SruTxt { get; set; }

        private DatePicker DobPick { get; set; }
        private TextBox Phonetxt { get; set; }
     
        private TextBox PermTxt { get; set;}

        private Button AddBtn { get; set; }
        private Button UpdateBtn { get; set; }
        private Button DelBtn { get; set; }

        private string SquadChoice { get; set; }

        private string PermChoice { get; set; }

        private ComboBox SquadBox { get; set; }
        private ComboBox PermBox { get; set; }

       private string[] Teams = new string[6];
        private string[] Perms = new string[3];

        //contructor receives a ViewMemberCoach and initialises the Window object above.
        //uses the base initializewindow to set connection, set Window properties , create canvas, drawWindow() , update the Grid (select & statement) and setupPageEvents(Button clicks).

        public ViewMembersAdmin (ViewMembersCoach user)
        {

            this.Window = user;


            InitializeWindow();

            //Arrays which hold combo box options

            Perms[0] = "";
            Perms[1] = "Yes";
            Perms[2] = "No";
            PermBox.ItemsSource = Perms;

            Teams[0] = "";
            Teams[1] = "U16";
            Teams[2] = "U18";
            Teams[3] = "U21";
            Teams[4] = "Senior";
            Teams[5] = "Junior";
            SquadBox.ItemsSource = Teams;
            

        }

        //uses the base SetupPageEvents from View MembersCoach and adds more button click events.
        protected override void SetUpPageEvents()
        {
            
            base.SetUpPageEvents();
            
            AddBtn.Click += AddBtn_Click;
            UpdateBtn.Click += UpdateBtn_Click;
            DelBtn.Click += DelBtn_Click;

            //creates a click event for when the user makes a selection on the dataGrid.
            myDataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.MyDataGrid_SelectionChanged);
            SquadBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SquadBox_SelectionChanged);
            PermBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.PermBox_SelectionChanged);

        }

        //sets all text boxes to "" , hides some buttons and updates the data in the Grid.
        protected override void reset()
        {
            NameTxt.Text = "";
            SruTxt.Text = "";
            Phonetxt.Text = "";
            DobPick.SelectedDate = null;
            EmailTxt.Text = "";
            PermBox.SelectedItem = Perms[0];
            SquadBox.SelectedItem = Teams[0];
            IdTxt.Text = "";

            AddBtn.IsEnabled = true;
            UpdateBtn.IsEnabled = false;
            DelBtn.IsEnabled = false;
            UpdateDataGrid();


            base.reset();
        }

        //positions new buttons and labels on the canvas that are only available to admin.
        protected override void DrawWindow()
        {
            base.DrawWindow();
            AddBtn = new Button();
            AddBtn.Content = "Add";
            AddBtn.FontWeight = FontWeights.Bold;

            UpdateBtn = new Button();
            UpdateBtn.Content = "Update";       
            UpdateBtn.FontWeight = FontWeights.Bold;

            SquadBox = new ComboBox();
            SquadBox.Width = 100;

            PermBox = new ComboBox();
            PermBox.Width = 100;

            DelBtn = new Button();
            DelBtn.Content = "Delete";           
            DelBtn.FontWeight = FontWeights.Bold;

            IDlbl = new Label();
            IDlbl.Content = "ID: ";
            IDlbl.FontWeight = FontWeights.Bold;

            IdTxt = new TextBlock();        
            IdTxt.Text = "";
            IdTxt.Width = 100;
            
            NameLbl = new Label();
            NameLbl.Content = "Name: ";
            NameLbl.FontWeight = FontWeights.Bold;

            NameTxt = new TextBox();
            NameTxt.Text = "";
            NameTxt.Width = 100;

            EmailLbl = new Label();
            EmailLbl.Content = "Email: ";
            EmailLbl.FontWeight = FontWeights.Bold;

            EmailTxt = new TextBox();
            EmailTxt.Text = "";
            EmailTxt.Width = 100;

            SRULbl = new Label();
            SRULbl.Content = "SRU-Number: ";
            SRULbl.FontWeight = FontWeights.Bold;

            SruTxt = new TextBox();
            SruTxt.MaxLength = 5;
            SruTxt.Text = "";
            SruTxt.Width = 100;

            DOBLbl = new Label();
            DOBLbl.Content = "DOB: ";
            DOBLbl.FontWeight = FontWeights.Bold;

            DobPick = new DatePicker();
            DobPick.Width = 100;
            

            PhoneLBl = new Label();
            PhoneLBl.Content = "Phone Number: ";
            PhoneLBl.FontWeight = FontWeights.Bold;

            Phonetxt = new TextBox();
            Phonetxt.MaxLength = 11;
            Phonetxt.Text = "";
            Phonetxt.Width = 100;

            TeamLbl = new Label();
            TeamLbl.Content = "Team: ";
            TeamLbl.FontWeight = FontWeights.Bold;

           

            PermisionLbl = new Label();
            PermisionLbl.Content = "Permission: ";
            PermisionLbl.FontWeight = FontWeights.Bold;


            WindowCanvas.Children.Add(PermBox);
            WindowCanvas.Children.Add(SquadBox);
            WindowCanvas.Children.Add(AddBtn);
            WindowCanvas.Children.Add(UpdateBtn);
            WindowCanvas.Children.Add(DelBtn);
            WindowCanvas.Children.Add(IDlbl);
            WindowCanvas.Children.Add(IdTxt);
            WindowCanvas.Children.Add(NameLbl);
            WindowCanvas.Children.Add(NameTxt);
            WindowCanvas.Children.Add(EmailLbl);
            WindowCanvas.Children.Add(EmailTxt);
            WindowCanvas.Children.Add(SRULbl);
            WindowCanvas.Children.Add(SruTxt);
            WindowCanvas.Children.Add(DOBLbl);
            WindowCanvas.Children.Add(DobPick);
            WindowCanvas.Children.Add(PhoneLBl);
            WindowCanvas.Children.Add(Phonetxt);
            WindowCanvas.Children.Add(TeamLbl);
            
            WindowCanvas.Children.Add(PermisionLbl);
           

            Canvas.SetLeft(AddBtn, 25);
            Canvas.SetTop(AddBtn, 350);

            Canvas.SetLeft(SquadBox, 192);
            Canvas.SetTop(SquadBox, 224);

            Canvas.SetLeft(PermBox, 192);
            Canvas.SetTop(PermBox, 254);

            Canvas.SetLeft(UpdateBtn, 60);
            Canvas.SetTop(UpdateBtn, 350);

            Canvas.SetLeft(DelBtn, 110);
            Canvas.SetTop(DelBtn, 350);

            Canvas.SetLeft(IDlbl, 72);
            Canvas.SetTop(IDlbl, 43);
            Canvas.SetLeft(IdTxt, 192);
            Canvas.SetLeft(IdTxt, 50);

            Canvas.SetLeft(NameLbl, 72);
            Canvas.SetTop(NameLbl, 67);
            Canvas.SetLeft(NameTxt, 192);
            Canvas.SetTop(NameTxt, 71);

            Canvas.SetLeft(EmailLbl, 72);
            Canvas.SetTop(EmailLbl, 100);
            Canvas.SetLeft(EmailTxt, 192);
            Canvas.SetTop(EmailTxt, 104);

            Canvas.SetLeft(SRULbl, 72);
            Canvas.SetTop(SRULbl, 131);
            Canvas.SetLeft(SruTxt, 192);
            Canvas.SetTop(SruTxt, 134);

            Canvas.SetLeft(DOBLbl, 72);
            Canvas.SetTop(DOBLbl, 162);
            Canvas.SetLeft(DobPick, 192);
            Canvas.SetTop(DobPick, 164);

            Canvas.SetLeft(PhoneLBl, 72);
            Canvas.SetTop(PhoneLBl, 193);
            Canvas.SetLeft(Phonetxt, 192);
            Canvas.SetTop(Phonetxt, 194);

            Canvas.SetLeft(TeamLbl, 72);
            Canvas.SetTop(TeamLbl, 224);
           
            Canvas.SetLeft(PermisionLbl, 72);
            Canvas.SetTop(PermisionLbl, 255);
            

        }

        //click event for the Add Button.

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string sql = "INSERT INTO PLAYERS(Name, SRUnum, DOB, PhoneNumber,Email,Squad,PERMISSION) VALUES (@Name, @SRUnum, @DOB, @PhoneNumber,@Email, @Squad, @PERMISSION);";
            this.IUD(sql, 0);
            AddBtn.IsEnabled = false;
            UpdateBtn.IsEnabled = true;
            DelBtn.IsEnabled = true;
            reset();
        }

        //click event for the update button 
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string sql = "UPDATE Players SET Name = @Name, SRUnum = @SRUnum, DOB = @DOB, PhoneNumber = @PhoneNumber, Email = @Email, Squad = @Squad, PERMISSION = @PERMISSION WHERE ID_no = @ID_no;";

            this.IUD(sql, 1);

        }

        //Click event for the Delete button
        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            string sql = "DELETE FROM PLAYERS WHERE ID_No = @ID_No;";

            string msg = "";


            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;

            msg = "Player Removed Successfully";
            cmd.Parameters.Add("@ID_no", SqlDbType.VarChar).Value = Convert.ToInt16(IdTxt.Text);


            try
            {
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show(msg);
                    UpdateDataGrid();
                    this.reset();
                }

            }
            catch (Exception f)
            {
                MessageBox.Show("Please enter information correctly");
            }

     

        }

        //insert , Update and Delete method. Receives a SQL Statement and number for the switch case it relates to.
        private void IUD(string sql, int state)
        {
            string msg = "";


            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            try
            {
                //member object created and passed the data from the text boxes for validation.
                Member member1 = new Member(NameTxt.Text, Convert.ToDouble(SruTxt.Text), Convert.ToDateTime(DobPick.SelectedDate), Convert.ToInt64(Phonetxt.Text), EmailTxt.Text, SquadChoice, PermChoice);

                switch (state)
                {
                    case 0:
                        
                        msg = "Player Inserted Succesfully!";
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Convert.ToString(member1.Name);
                        cmd.Parameters.Add("@SRUnum", SqlDbType.VarChar).Value = Convert.ToString(member1.SruNum);
                        cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = member1.Dob;
                        cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = "0" + member1.Phonenum.ToString();
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = Convert.ToString(member1.Email);
                        cmd.Parameters.Add("@Squad", SqlDbType.VarChar).Value = Convert.ToString(member1.Squad);
                        cmd.Parameters.Add("@Permission", SqlDbType.VarChar).Value = Convert.ToString(member1.Permission);
                        break;


                    case 1:
                        
                        msg = "Player updated Successfully!";
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Convert.ToString(member1.Name);
                        cmd.Parameters.Add("@SRUnum", SqlDbType.VarChar).Value = Convert.ToString(member1.SruNum);
                        cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = member1.Dob;
                        cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = "0" + member1.Phonenum.ToString();
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = Convert.ToString(member1.Email);
                        cmd.Parameters.Add("@Squad", SqlDbType.VarChar).Value = member1.Squad;
                        cmd.Parameters.Add("@Permission", SqlDbType.VarChar).Value = Convert.ToString(member1.Permission);
                        cmd.Parameters.Add("@ID_no", SqlDbType.VarChar).Value = IdTxt.Text;
                        break;
                    case 2:
                        msg = "Player Removed Successfully";
                        cmd.Parameters.Add("@ID_no", SqlDbType.VarChar).Value = Convert.ToInt16(IdTxt.Text);
                        break;


                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

            //If the Grid updates successfully , display the appropriate message and update the Grid.
            try
            {
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show(msg);
                    UpdateDataGrid();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Edit Failed - Please ensure information entered is correct and the SRU is unique.");
            }



        }

        //A method which automatically fills in the text boxes with data depending on which Row is clicked on the Table.
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
                    DobPick.SelectedDate = Convert.ToDateTime(dr["DOB"].ToString());
                    Phonetxt.Text = dr["PhoneNumber"].ToString();
                    EmailTxt.Text = dr["EMAIL"].ToString();
                    SquadChoice = Convert.ToString(dr["Squad"]);
                    SquadChoice = RemoveWhiteSpace(SquadChoice);
                    switch (SquadChoice)
                    {

                        case "U16":
                            SquadBox.SelectedItem = Teams[1];
                            break;

                        case "U18":
                            SquadBox.SelectedItem = Teams[2];
                            break;
                        case "U21":
                            SquadBox.SelectedItem = Teams[3];
                            break;
                        case "Senior":
                            SquadBox.SelectedItem = Teams[4];
                            break;
                        case "Junior":
                            SquadBox.SelectedItem = Teams[5];
                            break;
 
                   }
                    PermChoice = Convert.ToString(dr["PERMISSION"]);
                    PermChoice = RemoveWhiteSpace(PermChoice);

                    switch (PermChoice)
                    {
                        case "Yes":
                            PermBox.SelectedItem = Perms[1];
                            break;
                        case "No":
                            PermBox.SelectedItem = Perms[2];
                            break;

                    }
                    IdTxt.Text = Convert.ToString(dr["ID_no"]);

                    AddBtn.IsEnabled = false;
                    UpdateBtn.IsEnabled = true;
                    DelBtn.IsEnabled = true;

                }
            }
            catch (Exception f)
            {
                MessageBox.Show("Action not possible, please use the interface provided");
            }

        }

        //Event handler for SquadBox
        private void SquadBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SquadChoice = SquadBox.SelectedItem.ToString();
            
           
        }

        //event handler for PermBox

        private void PermBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PermChoice = PermBox.SelectedItem.ToString();
        }


        //a method which removes white space from strings.
        private string RemoveWhiteSpace (string stuff)
        {
            for (int i = 0; i <= stuff.Length; i++)
            {
                stuff = Regex.Replace(stuff, @"\s+", "");

            }

            return stuff;

        }

    }
}
