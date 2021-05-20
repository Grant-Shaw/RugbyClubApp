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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    //Grant Shaw Graded Unit
    //Updated 24/4/2019
    //Login page

    public partial class MainWindow : Window
    {

        //Constructor for Login PAge
        public MainWindow()
        {
            InitializeComponent();
            LoginBtn.Focus();
            this.Title = "Simply Rugby";
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        /// <summary>
        /// 
        /// Login button which checks if username and password is entered correctly before opening the start page and closing current page.
        /// </summary>
        /// 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if the user has entered "Admin"
            if (usernameTxt.Text == "Admin" || usernameTxt.Text == "admin")
            {
                // If the user has entered correct password
                if (password.Password == "123")
                {
                    //close current page and open the admin view for the Start Page
                    StartPage userPage = new StartPage("Admin", "Welcome Admin");
                    userPage.Show();
                    this.Close();
                }
                else
                {
                    //display error message if password wrong
                    MessageBox.Show("Password is incorrect");
                }


            }
            //if the user has entered Coach
            else if (usernameTxt.Text == "Coach" || usernameTxt.Text == "coach")
            {
                //If the password is correct
                if (password.Password == "567")
                {
                    //load Start Page with Coach View.
                    StartPage userPage = new StartPage("Coach", "Welcome Coach");
                    userPage.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Password is incorrect");
                }

            }

            else
            {
                MessageBox.Show("Please ensure username is correct");
            }


        }
    }
}
