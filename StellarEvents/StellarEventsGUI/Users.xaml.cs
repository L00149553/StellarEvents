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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StellarEventsGUI
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Page
    {

        stellarEntities db = new stellarEntities("metadata=res://*/StellarEventsModel.csdl|res://*/StellarEventsModel.ssdl|res://*/StellarEventsModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.15;initial catalog=stellar;user id=alex;password=Ernunk.26;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");

        User selectedUser = new User();

        List<User> users = new List<User>();

        public Users()
        {
            InitializeComponent();
        }

        private void lstUsersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUsersList.SelectedIndex >= 0)
            {
                selectedUser = users.ElementAt(lstUsersList.SelectedIndex);

                int numReg = db.Registrations.Where(t => t.UserId == selectedUser.UserId).Count();
                int numCreated = db.Events.Where(t => t.UserId == selectedUser.UserId).Count();

                if (selectedUser.TypeId == 1)
                    lblDetails.Content = "This booker has created " + numCreated.ToString() + " events";
                else if (selectedUser.TypeId == 2)
                    lblDetails.Content = "This eventgoer has attended " + numReg.ToString() + " events";
                else if (selectedUser.TypeId == 3)
                    lblDetails.Content = "Administrators cannot create or attend events";

            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Set source of list view items to users.
            lstUsersList.ItemsSource = users;

            //Clear contents currently in list view.
            users.Clear();

            //Populate the list view.
            foreach (var user in db.Users)
            {
                users.Add(user);
            }

            //Refresh list view.
            lstUsersList.Items.Refresh();
        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = ((ComboBoxItem)cmbType.SelectedItem).Content.ToString();

            //Set source of list view items to users.
            lstUsersList.ItemsSource = users;

            //Clear contents currently in list view.
            users.Clear();

            //Populate the list view.
            foreach (var user in db.Users)
            {
                if (user.UserType.UserRole == selected)
                    users.Add(user);
            }

            //Refresh list view.
            lstUsersList.Items.Refresh();
        }
    }
}
