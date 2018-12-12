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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {

        stellarEntities db = new stellarEntities("metadata=res://*/StellarEventsModel.csdl|res://*/StellarEventsModel.ssdl|res://*/StellarEventsModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.15;initial catalog=stellar;user id=alex;password=Ernunk.26;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");

        public User user = new User();

        public Dashboard()
        {
            InitializeComponent();
        }

        private void CheckUserType (User user)
        {
            if (user.TypeId == 3)
                btnUsers.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckUserType(user);
        }

        private void btnEvents_Click(object sender, RoutedEventArgs e)
        {
            Events events = new Events();
            events.user = user;

            List<Event> allEvents = new List<Event>();

            events.lstEventsList.ItemsSource = allEvents;

            foreach (var ev in db.Events)
            {
                allEvents.Add(ev);
            }

            frmMain.Navigate(events);

        }
    }
}
