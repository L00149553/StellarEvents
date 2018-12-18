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
    /// Interaction logic for Events.xaml
    /// </summary>
    public partial class Events : Page
    {
        stellarEntities db = new stellarEntities("metadata=res://*/StellarEventsModel.csdl|res://*/StellarEventsModel.ssdl|res://*/StellarEventsModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.15;initial catalog=stellar;user id=alex;password=Ernunk.26;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");

        public User user = new User();

        //Create a list to contain all events stored in the Events table of the database.
        List<Event> allEvents = new List<Event>();

        //Create variable to hold information about the selected event on ListView
        Event selectedEvent = new Event();

        public Events()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Verify the type of user to determine their options.
            CheckUserType(user);

            //Populate the ListView with events from the database.
            RefreshEventsList();

        }

        private void CheckUserType(User user)
        {
            //If user is of type Booker, allow him to access the Create Event option.
            if (user.TypeId == 1)
                btnCreate.Visibility = Visibility.Visible;
            else
                btnCreate.Visibility = Visibility.Hidden;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            //Create a Create Event Screen and display it to the user.
            CreateEvent createEvent = new CreateEvent();
            this.NavigationService.Navigate(createEvent);
            createEvent.user = user;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteEvent(selectedEvent);
        }

        public void DeleteEvent(Event deletedEvent)
        {
            //Verify whether event selected to delete was created by the user attempting to delete it.
            if (deletedEvent.UserId == user.UserId)
            {
                //Loop through all registration records in the Registrations table of the database.
                foreach (var reg in db.Registrations)
                {
                    //Check if Registration event is the same as the event that is being deleted.
                    if(reg.EventId == deletedEvent.EventId)
                    {
                        //Delete the registration record.
                        db.Registrations.RemoveRange(db.Registrations.Where(t => t.EventId == deletedEvent.EventId));
                        db.SaveChanges();
                    }
                }

                //Delete the event
                db.Events.RemoveRange(db.Events.Where(t => t.EventId == deletedEvent.EventId));
                db.SaveChanges();

                MessageBox.Show("Event deleted");

                //Refresh the ListView
                RefreshEventsList();

            }
            else
            {
                //Inform user they cannot delete event they did not create.
                MessageBox.Show("You cannot delete this event as you did not create it");
            }

        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            //Verify whether user attempting to modify event created the event.
            if (selectedEvent.UserId == user.UserId)
            {
                //Create a Modify Event screen to display details of selected event.
                ModifyEvent modifyEvent = new ModifyEvent();
                this.NavigationService.Navigate(modifyEvent);
                modifyEvent.user = user;
                modifyEvent.modifiedEvent = selectedEvent;

            }
            else
            {
                //Inform user they cannot modify event they did not create.
                MessageBox.Show("You cannot modify an event that you did not create");
            }

        }

        private void RefreshEventsList()
        {

            //Set source of list view items to events.
            lstEventsList.ItemsSource = allEvents;

            //Clear contents currently in list view.
            allEvents.Clear();

            //Populate the list view.
            foreach (var ev in db.Events)
            {
                allEvents.Add(ev);
            }

            //Refresh list view.
            lstEventsList.Items.Refresh();
        }

        private void lstEventsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstEventsList.SelectedIndex >= 0)
            {
                selectedEvent = allEvents.ElementAt(lstEventsList.SelectedIndex);
            }
         
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            ViewEvent viewEvent = new ViewEvent();
            this.NavigationService.Navigate(viewEvent);
            viewEvent.user = user;
            viewEvent.viewedEvent = selectedEvent;
        }
    }
}
