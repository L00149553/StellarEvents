using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary;
using Xunit;

namespace DBLibrary.Tests
{
    public class CreateEventProcessTest
    {
        [Theory]
        [InlineData ("", "venue", "Description of event", false)]
        [InlineData("genre", "", "Description of event", false)]
        [InlineData("genre", "venue", "Too Small", false)]
        [InlineData("genre", "venue", "This description contains over thirty characters and is therefore too long", false)]
        [InlineData("genre", "venue", "The perfect input", true)]
        public void ValidateUserInput_StringsShouldVerify(string genre, string venue, string description, bool expected)
        {
            //Arrange
            CreateEventProcess createEventProcess = new CreateEventProcess();

            //Act
            bool actual = createEventProcess.ValidateUserInput(genre, venue, description);

            //Assert
            Assert.Equal(expected, actual);
        }


    }


}
