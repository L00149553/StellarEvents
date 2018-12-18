using DBLibrary;
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

namespace StellarEventsGUI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        stellarEntities db = new stellarEntities("metadata=res://*/StellarEventsModel.csdl|res://*/StellarEventsModel.ssdl|res://*/StellarEventsModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.15;initial catalog=stellar;user id=alex;password=Ernunk.26;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Set variables to hold name and password entered into the textfields.
            string currentUser = tbxName.Text;
            string currentPassword = tbxPassword.Password;

            //Loop through all users in the users table of the database.
            foreach (var user in db.Users)
            {
                //Check if the user's name and password matches those entered into the textfields.
                if(user.Name == currentUser && user.Password == currentPassword)
                {
                    //Create the dashboard screen upon successful Login
                    Dashboard dashboard = new Dashboard();
                    dashboard.user = user;
                    dashboard.ShowDialog();
                    dashboard.Owner = this;
                }
            }

            //Display error message for unsucessful login.
            MessageBox.Show("Please try again");
                
        }
    }
}
