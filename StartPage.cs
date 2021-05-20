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

namespace Graded_Unit_research_4
{
    class StartPage : Window
    {
        //Grant Shaw Graded Unit
        // 24/4/2019
        //Start page where users are able to select from options


       //Declaring variables and objects

        public static string User { get; set; }

        public Canvas WindowCanvas { get; set; }
        private Button MembersBtn {get; set; }
        private Button Playersbtn { get; set; }

        private Button Logout { get; set; }

        public ViewMembersCoach CoachMember { get; set; }

        public ViewMembersAdmin AdminMember { get; set; }
        private Label ClubLbl { get; set; }

        //constructor
        public StartPage(string user, string title)
        {

            //sets the title
            this.Title = title;     
            //user variable keeps track of which user is logged in
            User = user;
            InitializeWindow();
            

        }

        //overloaded Constructor (as a backup)
        public StartPage(string user)
        {
            User = user;           
            InitializeWindow();
        }

        //A method which sets the window properties and calls another method which places objects on the canvas.
        protected virtual void InitializeWindow()
        {
            this.MaxHeight = 450;
            this.MaxWidth = 1200;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;           
            WindowCanvas = new Canvas();
            
            this.Content = WindowCanvas;
            DrawWindow();

            CoachMember = new ViewMembersCoach();
            SetUpPageEvents();
            
        }

        //places objects on the canvas, sets their properties
        protected virtual void DrawWindow()
        {
            //draws the window

            MembersBtn = new Button();
            MembersBtn.Content = "View Members";            
            MembersBtn.Height = 53;
            MembersBtn.Width = 187;

            Logout = new Button();
            Logout.Content = "Logout";
            Logout.Height = 50;
            Logout.Width = 100;


            ClubLbl = new Label();
            ClubLbl.Content = "Simply Rugby";
            ClubLbl.FontSize = 24;
            ClubLbl.FontWeight = FontWeights.Bold;

            //if user is logged in as coach , make this button visible
            if (User == "Coach" || User == "coach")
            {
                Playersbtn = new Button();
                Playersbtn.Content = "View Player Profiles";
                Playersbtn.Height = 53;
                Playersbtn.Width = 187;
                WindowCanvas.Children.Add(Playersbtn);
                Canvas.SetLeft(Playersbtn, 450);
                Canvas.SetTop(Playersbtn, 255);
            }

            WindowCanvas.Children.Add(MembersBtn);
            WindowCanvas.Children.Add(Logout);
            
            WindowCanvas.Children.Add(ClubLbl);

            Canvas.SetLeft(Logout, 30);
            Canvas.SetTop(Logout, 350);

            Canvas.SetLeft(ClubLbl, 316);
            Canvas.SetTop(ClubLbl, 56);
            
            Canvas.SetLeft(MembersBtn, 256);
            Canvas.SetTop(MembersBtn, 255);
        
        }

        //method for logout button
        protected void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newMain = new MainWindow();
            newMain.Show();

            this.Close();


        }

        //method for view members button
        protected void MembersBtn_Click(object sender, RoutedEventArgs e)
        {
            //what happens when members button is clicked
            //if user is logged in as admin then close page and open view members admin page
            if (User == "Admin")
            {
               AdminMember = new ViewMembersAdmin(CoachMember);
                AdminMember.Show();
                this.Close();

            }
            else
            {
                
                CoachMember.Show();
                this.Close();

            }
            

        }

        protected void PlayersBtn_Click(object sender, RoutedEventArgs e)
        {
            // what happens when view players button is clicked

            PlayerProfileSheet playerprof = new PlayerProfileSheet();
            playerprof.Show();
            this.Close();


        }

        //click events
        protected virtual void SetUpPageEvents()
        {
            MembersBtn.Click += MembersBtn_Click; //event for The View members button
            if(User == "Coach" || User == "coach")

            Playersbtn.Click += PlayersBtn_Click;
            Logout.Click += Logout_Click;
            
        }








    }
}
