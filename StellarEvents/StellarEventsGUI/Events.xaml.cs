﻿using DBLibrary;
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

        List<Event> events = new List<Event>();

        public User user = new User();

        public Events()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CheckUserType(user);

            lstEventsList.ItemsSource = events;
            foreach (var ev in db.Events)
            {
                events.Add(ev);
            }
        }

        private void CheckUserType(User user)
        {
            if (user.TypeId == 1)
                btnCreate.Visibility = Visibility.Visible;
            else
                btnCreate.Visibility = Visibility.Hidden;
        }
    }
}
