using Roomies2.DAL.Services;
using System;
using System.Threading.Tasks;

namespace Roomies2.DAL.Gateways
{
    public class InviteGateway
    {
        public string _connectionString;
        

        public InviteGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Result> Invite(object roomieId, int colocId, string email)
        {
            var code = Guid.NewGuid();

            //using (SqlConnection con = new SqlConnection(_connectionString))
            //{
            //    var p = new DynamicParameters();
            //    p.Add("@RoomieId", roomieId);
            //    p.Add("@ColocId", colocId);
            //    p.Add("@Email", email);
            //    p.Add("@Code", code);
            //    p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            //    await con.ExecuteAsync("rm2.sRoomieInvite", p, commandType: CommandType.StoredProcedure);

            //    int status = p.Get<int>("@Status");

            //    Debug.Assert(status == 0);
            //    return Result.Success();
            //}
            return Result.Success();
            
            
        }
    }
}
