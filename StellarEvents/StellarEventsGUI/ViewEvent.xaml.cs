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
    /// Interaction logic for ViewEvent.xaml
    /// </summary>
    public partial class ViewEvent : Page
    {
        stellarEntities db = new stellarEntities("metadata=res://*/StellarEventsModel.csdl|res://*/StellarEventsModel.ssdl|res://*/StellarEventsModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.15;initial catalog=stellar;user id=alex;password=Ernunk.26;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");

        public User user = new User();

        public Event viewedEvent = new Event();

        public ViewEvent()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CheckRegistered();

            CheckUserType();

            lblDescription.Content = viewedEvent.Description.Trim();
            lblDate.Content = viewedEvent.Date.ToString();
            lblVenue.Content = viewedEvent.Venue.Trim();
            lblGenre.Content = viewedEvent.Genre.Trim();
            lblCreator.Content = viewedEvent.User.Name.Trim();
            lblAttendees.Content = AttendeeCount().ToString();

        }

        private int AttendeeCount()
        {
            int count = 0;

            //Loop through records in Registrations table and increase the count by 1 for every record to determine no. of attendees to event.
            foreach (var reg in db.Registrations)
            {
                if (reg.EventId == viewedEvent.EventId)
                    count++;
            }

            return count;
        }

        private void CheckUserType()
        {
            //If user is of type Booker, allow him to access the Create Event option.
            if (user.TypeId == 1 || user.TypeId == 3)
            {
                btnRegister.Visibility = Visibility.Hidden;
                btnUnregister.Visibility = Visibility.Hidden;
            }
        }

        private void CheckRegistered()
        {
            int registered = 0;

            //Loop through each record in the Registrations table
            foreach (var reg in db.Registrations)
            {
                //Check to see if any records have Event ID and User ID of current event and logged in user to verify if they've registered for event.
                if (reg.EventId == viewedEvent.EventId && reg.UserId == user.UserId)
                    registered = 1;
            }

            if (registered == 0)
            {
                btnRegister.Visibility = Visibility.Visible;
                btnUnregister.Visibility = Visibility.Hidden;
            }
            else
            {
                btnRegister.Visibility = Visibility.Hidden;
                btnUnregister.Visibility = Visibility.Visible;
            }

        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            //Create a new record in the Registration table
            Registration registration = new Registration();

            registration.EventId = viewedEvent.EventId;
            registration.UserId = user.UserId;

            db.Entry(registration).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();

            MessageBox.Show("You have successfully registered for this event");

            btnRegister.Visibility = Visibility.Hidden;
            btnUnregister.Visibility = Visibility.Visible;

            //Update attendees count
            lblAttendees.Content = AttendeeCount().ToString();
        }

        private void btnUnregister_Click(object sender, RoutedEventArgs e)
        {
            //Remove registration record from database table
            db.Registrations.RemoveRange(db.Registrations.Where(t => t.EventId == viewedEvent.EventId && t.UserId == user.UserId));
            db.SaveChanges();

            MessageBox.Show("Unregistered from event");

            btnRegister.Visibility = Visibility.Visible;
            btnUnregister.Visibility = Visibility.Hidden;

            //Update attendees count
            lblAttendees.Content = AttendeeCount().ToString();
        }
    }
}
