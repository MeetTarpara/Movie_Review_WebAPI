using Microsoft.Data.SqlClient;
using Movie_Review_WebAPI.Models;
using System.Data;

namespace Movie_Review_WebAPI.Data
{
    public class UserRepository
    {

        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        #region Select All User
        public IEnumerable<UserModel> SelectAllUser()
        {
            var users = new List<UserModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_User_SelectAll]", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    users.Add(new UserModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                        Role = reader["Role"].ToString()

                    }
                    );
                }
            } ;
            return users;
        }
        #endregion


        #region Select User By ID

        public UserModel SelectByPK(int UserID)
        {
            UserModel user = null;

            using (SqlConnection conn = new SqlConnection(_connectionString)) {
                SqlCommand cmd = new SqlCommand("PR_User_Select_ByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure

                };

                cmd.Parameters.AddWithValue("UserId", UserID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new UserModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                        Role = reader["Role"].ToString()
                    };
                }
            }

            return user;
        }

        #endregion

        #region Insert User

        public bool Insert(UserModel user) {

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_User_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Adding parameters for the stored procedure
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Role", user.Role);


                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        #endregion


        #region Update User
        public bool UpdateAll(UserModel user) {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_User_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Adding parameters for the stored procedure
                cmd.Parameters.AddWithValue("@UserID", user.UserID);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Role", user.Role);


                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion
    }
}
