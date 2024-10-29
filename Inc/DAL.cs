using LogInOut.Models.Tables;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LogInOut.Inc
{
    public class DAL
    {
        private readonly DBConnContext _connContext;
        public DAL(DBConnContext connContext)
        { 
            _connContext = connContext;
        }

        #region 로그인
        ///<summary>
        ///아이디/비번 입력 후 사용자 유무 확인 후 로그인
        ///</summary>
        public MemberInfo LoginCheck(string userId, string userPwd)
        {
            SqlConnection db = _connContext.Database.GetDbConnection() as SqlConnection;
            db.Open();

            MemberInfo userInfo = new MemberInfo();
            using (SqlCommand cmd = db.CreateCommand())
            {
                string SQL = @"
                    SELECT
	                    UserId,
	                    UserName
                    FROM
	                    MemberInfo
                    WHERE
	                    UserId = @UserId
	                    AND UserPwd = @UserPwd
                ";
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@UserId", userId.ToString());
                cmd.Parameters.AddWithValue("@UserPwd", userPwd.ToString());
                cmd.CommandType = System.Data.CommandType.Text;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        userInfo.UserId = dr["UserId"].ToString();
                        userInfo.UserName = dr["UserName"].ToString();
                    }
                }
                else
                {
                    userInfo = null;
                }

                dr.Close();
            }

            db.Close();

            return userInfo;
        }
        #endregion
    }
}
