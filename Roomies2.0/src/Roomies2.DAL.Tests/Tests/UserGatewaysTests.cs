using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;

namespace Roomies2.DAL.Tests.Tests
{
    [TestFixture]
    internal class UserGatewaysTests
    {
        [Test]
        public async Task can_create_find_update_and_delete_user()
        {
            UserGateway sut = new UserGateway(TestHelpers.ConnectionString);

            string email = $"user{Guid.NewGuid()}@test.com";
            var password = Guid.NewGuid().ToByteArray();

            //Create
            var result = await sut.CreatePasswordUser(email, password);
            UserData user = await sut.FindById(result.Content);

            {
                Assert.That(user.Email, Is.EqualTo(email));
                Assert.That(user.HashedPassword, Is.EqualTo(password));
            }

            {
                UserData u = await sut.FindById(user.UserId);
                Assert.That(u.Email, Is.EqualTo(email));
                Assert.That(u.HashedPassword, Is.EqualTo(password));
            }

            {
                email = string.Format("user{0}@test.com", Guid.NewGuid());
                await sut.UpdateEmail(user.UserId, email);
            }

            {
                await sut.Delete(user.UserId);
                Assert.That(await sut.FindById(user.UserId), Is.Null);
            }
        }

        [Test]
        public async Task can_create_facebook_user()
        {
            UserGateway sut = new UserGateway(TestHelpers.ConnectionString);

            string email = string.Format("user{0}@test.com", Guid.NewGuid());
            string facebookId = Guid.NewGuid().ToString();
            string refreshToken = Guid.NewGuid().ToString().Replace("-", string.Empty);

            await sut.CreateOrUpdateFacebookUser(email, facebookId, refreshToken);
            UserData user = await sut.FindByEmail(email);

            Assert.That(user.FacebookRefreshToken, Is.EqualTo(refreshToken));
            Assert.That(user.FacebookId, Is.EqualTo(facebookId));

            refreshToken = Guid.NewGuid().ToString().Replace("-", string.Empty);
            await sut.CreateOrUpdateFacebookUser(user.Email, user.FacebookId, refreshToken);

            user = await sut.FindById(user.UserId);
            Assert.That(user.FacebookRefreshToken, Is.EqualTo(refreshToken));

            await sut.Delete(user.UserId);

        }

        [Test]
        public async Task can_create_google_user()
        {
            UserGateway sut = new UserGateway(TestHelpers.ConnectionString);
            string userName = TestHelpers.RandomTestName();
            string email = string.Format("user{0}@test.com", Guid.NewGuid());
            string googleId = Guid.NewGuid().ToString();
            string refreshToken = Guid.NewGuid().ToString().Replace("-", string.Empty);

            await sut.CreateOrUpdateGoogleUser( email, googleId, refreshToken);
            UserData user = await sut.FindByEmail(email);

            {
                Assert.That(user.Email, Is.EqualTo(user.Email));
                Assert.That(user.UserId, Is.EqualTo(user.UserId));
                Assert.That(user.GoogleId, Is.EqualTo(user.GoogleId));
            }

            Assert.That(user.GoogleRefreshToken, Is.EqualTo(refreshToken));

            refreshToken = Guid.NewGuid().ToString().Replace("-", string.Empty);
            await sut.CreateOrUpdateGoogleUser(user.Email, user.GoogleId, refreshToken);

            user = await sut.FindById(user.UserId);
            Assert.That(user.GoogleRefreshToken, Is.EqualTo(refreshToken));

            await sut.Delete(user.UserId);
        }
    }
}
