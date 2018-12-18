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
    /// Interaction logic for ModifyEvent.xaml
    /// </summary>
    public partial class ModifyEvent : Page
    {

        stellarEntities db = new stellarEntities("metadata=res://*/StellarEventsModel.csdl|res://*/StellarEventsModel.ssdl|res://*/StellarEventsModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.15;initial catalog=stellar;user id=alex;password=Ernunk.26;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");

        public User user = new User();

        public Event modifiedEvent = new Event();

        public ModifyEvent()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tbxDescription.Text = modifiedEvent.Description;
            tbxDate.Text = modifiedEvent.Date.ToString();
            tbxGenre.Text = modifiedEvent.Genre;
            tbxVenue.Text = modifiedEvent.Venue;

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            foreach (var ev in db.Events.Where(t => t.EventId == modifiedEvent.EventId))
            {
                ev.Description = tbxDescription.Text.Trim();
                ev.Date = DateTime.Parse(tbxDate.Text);
                ev.Genre = tbxGenre.Text.Trim();
                ev.Venue = tbxVenue.Text.Trim();

            }

            db.SaveChanges();

            MessageBox.Show("Event Updated");

            Events events = new Events();
            this.NavigationService.Navigate(events);
            events.user = user;

        }

    }
}
