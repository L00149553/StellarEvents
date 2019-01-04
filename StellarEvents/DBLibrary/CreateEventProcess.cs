using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class CreateEventProcess
    {
        public bool ValidateUserInput(string genre, string venue, string description)
        {
            bool validated = true;

            //Verify that the Genre field isn't empty
            if (genre.Length == 0)
                validated = false;

            //Verify that the Venue field isn't empty
            if (venue.Length == 0)
                validated = false;

            //Verify that the description field is between 10 and 30 characters.
            if (description.Length < 10 || description.Length > 30)
                validated = false;

            return validated;
        }

    }
}
