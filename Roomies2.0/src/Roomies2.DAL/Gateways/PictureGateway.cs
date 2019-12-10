using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using Roomies2.DAL.Services;
using Dapper;

namespace Roomies2.DAL.Gateways
{
    public class PictureGateway
    {
        readonly string _connectionString;
        readonly string _path;
        readonly string _serverLink;
        readonly string _colocFolder;
        readonly string _roomieFolder;


        public PictureGateway( string connectionString)
        {
            _connectionString = connectionString;
            _path = @"../Roomies2.WebApp/wwwroot/pictures";
            _serverLink = "http://localhost:5000/Pictures";
            _colocFolder = "/ColocPics/";
            _roomieFolder = "/ RoomiesPics /";
        }

        public async Task<Result> UploadPicture(IFormFile image, int id, bool isRoomie)
        {
            
            string path = _path + _roomieFolder + id;
            string pictureLink = _roomieFolder + id;

            if (!isRoomie)
            {
                path = _path + _colocFolder + id;
                pictureLink = _colocFolder + id;
            }

            this.ExistDirectory(path);
            string fileName = image.FileName;

            string ext = image.FileName.Substring(fileName.LastIndexOf(".", StringComparison.Ordinal));

            string name = id.ToString() + ext;
            string filePath = Path.Combine(path, name);
            string savedPath = _serverLink + pictureLink+ "/"+ name ;

            DeletePicture(path);


            using ( var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            Result result = await UpdateRoomiePic(id, savedPath);

            if (!isRoomie)
            {
                result =  await UpdateColocPic(id, savedPath);
            }

            return result;
        }
        internal void ExistDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                string test = Directory.GetCurrentDirectory();
                Directory.CreateDirectory(path);
            }
        }


        /// <summary>
        /// Delete the previous picture
        /// </summary>
        /// <param name="path"></param>
        internal static void DeletePicture(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            foreach(FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
        }


        /// <summary>
        /// Update  Roomie Picture link in the Database
        /// </summary>
        /// <param name="roomieId"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<Result> UpdateRoomiePic(int roomieId, string path)
        {

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("RoomieId", roomieId);
                p.Add("PicturePath", path);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sRoomiePicUpdate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.NotFound, "Not Found");

                Debug.Assert(status == 0);
                return Result.Success();
            }
        }


        /// <summary>
        /// Update  Coloc Picture link in the Database
        /// </summary>
        /// <param name="colocId"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<Result> UpdateColocPic(int colocId, string path)
        {

            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("ColocId", colocId);
                p.Add("PicPath", path);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sColocPicUpdate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.NotFound, "Not Found");

                Debug.Assert(status == 0);
                return Result.Success();
            }
        }
    }
}
