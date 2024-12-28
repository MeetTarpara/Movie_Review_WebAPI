using Microsoft.Data.SqlClient;
using Movie_Review_WebAPI.Models;
using System.Data;

namespace Movie_Review_WebAPI.Data
{
    public class StarRepository
    {
        private readonly string _connectionString;

        public StarRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region Select All Stars
        public IEnumerable<StarModel> SelectAllStars()
        {
            var stars = new List<StarModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_Star_SelectAll]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    stars.Add(new StarModel
                    {
                        StarId = Convert.ToInt32(reader["StarId"]),
                        StarName = reader["StarName"].ToString(),
                        DOB = reader["DOB"] != DBNull.Value ? Convert.ToDateTime(reader["DOB"]) : null
                    });
                }
            }

            return stars;
        }
        #endregion

        #region Select Star By ID
        public StarModel SelectByPK(int starId)
        {
            StarModel star = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_Star_SelectByPK]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StarId", starId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    star = new StarModel
                    {
                        StarId = Convert.ToInt32(reader["StarId"]),
                        StarName = reader["StarName"].ToString(),
                        DOB = reader["DOB"] != DBNull.Value ? Convert.ToDateTime(reader["DOB"]) : null
                    };
                }
            }

            return star;
        }
        #endregion

        #region Insert Star
        public bool Insert(StarModel star)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_Star_Insert]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Adding parameters for the stored procedure
                cmd.Parameters.AddWithValue("@StarName", star.StarName);
                cmd.Parameters.AddWithValue("@DOB", star.DOB ?? (object)DBNull.Value);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update Star
        public bool Update(StarModel star)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_Star_Update]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Adding parameters for the stored procedure
                cmd.Parameters.AddWithValue("@StarId", star.StarId);
                cmd.Parameters.AddWithValue("@StarName", star.StarName);
                cmd.Parameters.AddWithValue("@DOB", star.DOB ?? (object)DBNull.Value);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region Delete Star
        public bool Delete(int starId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_Star_Delete]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StarId", starId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion
    }
}