using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Roomies2.DAL.Tests
{
    public static class TestHelpers
    {
        private static readonly Random Random = new Random();
        private static IConfiguration _configuration;

        public static string ConnectionString => Configuration["ConnectionStrings:PrimarySchoolDB"];

        private static IConfiguration Configuration =>
            _configuration ?? (_configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build());

        public static string RandomTestName() => $"Test-{Guid.NewGuid().ToString().Substring(24)}";
        public static DateTime RandomBirthDate(int age) => DateTime.UtcNow.AddYears(-age).AddMonths(Random.Next(-11, 0)).Date;
    }
}
