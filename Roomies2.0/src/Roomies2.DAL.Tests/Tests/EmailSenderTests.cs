using NUnit.Framework;
using Roomies2.WebApp.Services;
using System;

namespace Roomies2.DAL.Tests.Tests
{
    [TestFixture]
    public class EmailSenderTests
    {
        [Test]
        public void can_send_email()
        {
            EmailSender emailSender = new EmailSender();
            emailSender.SendEmail("saxelsyeguillaume@gmail.com", new Guid().ToString().Substring(0, 12)) ;
        }
    }
}
