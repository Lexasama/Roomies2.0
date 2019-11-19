using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Roomies2.DAL.Gateways
{
    public class PictureGateway
    {
        readonly string _connectionString;
        public PictureGateway( string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<string>> UploadPicture(IFormFile image, int id, bool isRoomie)
        {
            using( var fileStream = new FileStream(@"D:\Users\saxel\ITI\ITI-S5\PI\Roomies2.0\Roomies2.0\src\Roomies2.WebApp\wwwroot\pictures", FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return new List<string>();
        }
    }
}
