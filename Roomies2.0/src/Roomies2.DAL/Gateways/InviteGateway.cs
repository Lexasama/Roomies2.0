using Roomies2.DAL.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Roomies2.DAL.Model.People;
using Dapper;

namespace Roomies2.DAL.Gateways
{
    public class InviteGateway
    {
        public string ConnectionString { get; }

        public InviteGateway(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task<Result<InviteData>> FindInvite(string code)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                InviteData i = await con.QueryFirstOrDefaultAsync<InviteData>(
                    @"SELECT * FROM rm2.tInvite i WHERE i.Code = @Code;",
                    new { Code = code });

                if (i == null) return Result.Failure<InviteData>(Status.NotFound, "Not found.");
                return Result.Success(Status.Ok, i);
            }
        }
        
        public async Task<Result<InviteInfo>> FindInviteInfo(string code)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                InviteInfo i = await con.QueryFirstOrDefaultAsync<InviteInfo>(
                    @"SELECT * FROM rm2.vInvite i WHERE i.Code = @Code;",
                    new { Code = code });

                if (i == null) return Result.Failure<InviteInfo>(Status.NotFound, "Not found.");
                return Result.Success(Status.Ok, i);
            }
        }

        public async Task<Result> DeleteInvite(string email, int colocId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("@ColocId", colocId);
                p.Add("@Email", email);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sInviteDelete", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure(Status.NotFound, "Invite not found");

                Debug.Assert(status == 0);
                return Result.Success();
            }
        }


        public async Task<Result> Invite(object roomieId, int colocId, string email, string code)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@RoomieId", roomieId);
                p.Add("@ColocId", colocId);
                p.Add("@Email", email);
                p.Add("@Code", code);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sInviteCreate", p, commandType: CommandType.StoredProcedure);
                
                int status = p.Get<int>("@Status");

                Debug.Assert(status == 0);
                return Result.Success();
            }
        }
    }
}
