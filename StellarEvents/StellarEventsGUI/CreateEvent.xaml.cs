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
    /// Interaction logic for CreateEvent.xaml
    /// </summary>
    public partial class CreateEvent : Page
    {

        stellarEntities db = new stellarEntities("metadata=res://*/StellarEventsModel.csdl|res://*/StellarEventsModel.ssdl|res://*/StellarEventsModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.15;initial catalog=stellar;user id=alex;password=Ernunk.26;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");

        public CreateEvent()
        {
            InitializeComponent();
        }

        public User user = new User();

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            Event newEvent = new Event();
            newEvent.Date = DateTime.Parse(tbxDate.Text);
            newEvent.Genre = tbxGenre.Text;
            newEvent.Description = tbxDescription.Text;
            newEvent.Venue = tbxVenue.Text;
            newEvent.UserId = user.UserId;
            SaveEvent(newEvent);

            MessageBox.Show("Event has been added");

            tbxDate.Text = "";
            tbxGenre.Text = "";
            tbxDescription.Text = "";
            tbxVenue.Text = "";

        }

        public void SaveEvent(Event newEvent)
        {
            db.Entry(newEvent).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }

    }
}
