using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Roomies2.DAL.Gateways
{
    public class PictureGateway
    {
        readonly string _connectionString;
        readonly string _path;

        public PictureGateway( string connectionString)
        {
            _connectionString = connectionString;
            _path = @"../Roomies2.WebApp/wwwroot/pictures";
        }

        public async Task<List<string>> UploadPicture(IFormFile image, int id, bool isRoomie)
        {
            string path = _path + "/RoomiesPics/" + id;

            if (!isRoomie)
            {
                path = _path + "/ColocPics/" + id;
            }

            this.ExistDirectory(path);
            string fileName = image.FileName;

            string ext = image.FileName.Substring(fileName.LastIndexOf("."));

            string name = id.ToString() + ext;
            string filePath = Path.Combine(path, name);

            using ( var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return new List<string>();
        }
        internal void ExistDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                var test = Directory.GetCurrentDirectory();
                Directory.CreateDirectory(path);
            }
        }
    }
}
