using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Dapper;
using System;

namespace Roomies2.DAL.Gateways
{
    public class RoomieGateway
    {
        readonly string _connectionString;

        public RoomieGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Result<RoomieData>> FindById( int roomieId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                RoomieData r = await con.QueryFirstOrDefaultAsync<RoomieData>(
                    @"SELECT * FROM rm2.vRoomie r WHERE c.RoomieId = @RoomieId;",
                    new {RoomieId = roomieId}
                    );

                if (r == null) return Result.Failure<RoomieData>(Status.NotFound, "Roomie was not found.");
                return Result.Success(Status.Ok, r);
        
            }

        }

        public async Task<Result<int>> Create( string lastName, string firstName, string phone, int sex, DateTime  birthDate, string desc, string pic  )
        {
            if (!IsNameValid(lastName)) return Result.Failure<int>(Status.BadRequest, "The firstname is not valid");
            if(!IsNameValid(firstName)) return Result.Failure<int>(Status.BadRequest, "The lastname is not valid");

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", firstName );
                p.Add("@LastName", lastName);
                p.Add("@Phone", phone);
                p.Add("@Sex", sex);
                p.Add("@BirthDate", birthDate);
                p.Add("@PicPath", pic);
                p.Add("@RoomieId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sRoomieCreate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "A Roomie with this userBame already exists");

                Debug.Assert(status == 0);
                return Result.Success(Status.Created, p.Get<int>("@RoomieId"));
            }
        }

        public  async Task<Result> Update(string lastName, string firstName, string phone, int sex, DateTime birthDate, string desc, string pic)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Delete(int roomieId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@RoomieId", roomieId);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sRoomieDelete", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure(Status.NotFound, "Roomie not found");

                Debug.Assert(status == 0);
                return Result.Success();
            }
        }

        bool IsNameValid(string name) => !string.IsNullOrWhiteSpace(name);
    }
}
