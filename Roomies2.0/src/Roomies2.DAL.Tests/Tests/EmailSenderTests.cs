using NUnit.Framework;
using Roomies2.WebApp.Services;

namespace Roomies2.DAL.Tests.Tests
{
    [TestFixture]
    public class EmailSenderTests
    {
        [Test]
        public void can_send_email()
        {
            EmailSender sut = new EmailSender();


            sut.SendEmail("saxelsyeguillaume@gmail.com");
        }
    }
}
