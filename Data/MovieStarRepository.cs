using Microsoft.Data.SqlClient;
using Movie_Review_WebAPI.Models;
using System.Data;

namespace Movie_Review_WebAPI.Data
{
    public class MovieStarRepository
    {


        private readonly string _connectionString;

        public MovieStarRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region Select All Star By MovieID

        public IEnumerable<MovieStarModel> GetAllStarByMovieID(int MovieID)
        {
            var stars = new List<MovieStarModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_MovieStar_StarByMovieID]", conn)
                {
                    CommandType = CommandType.StoredProcedure

                };

                cmd.Parameters.AddWithValue("MovieID", MovieID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    stars.Add(new MovieStarModel
                    {
                        StarID = Convert.ToInt32(reader["StarID"]),
                        StarName = reader["StarName"].ToString(),
                        MovieID = Convert.ToInt32(reader["MovieID"]),
                        MovieName = reader["MovieName"].ToString(),
                        Rating = Convert.ToInt32(reader["Rating"]),
                        Poster = reader["Poster"].ToString(),
                        Description = reader["Description"].ToString(),
                        ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]),
                        DirectorID = Convert.ToInt32(reader["DirectorID"]),
                        Writer = reader["Writer"].ToString(),
                        Duration = reader["Duration"].ToString()
                    });
                }
            }

            return stars;
        }

        #endregion



        #region Select All Movie By StarID

        public IEnumerable<MovieStarModel> GetAllMovieByStarId(int StarID)
        {
            var movies = new List<MovieStarModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_MovieStar_MovieByStarID]", conn)
                {
                    CommandType = CommandType.StoredProcedure

                };

                cmd.Parameters.AddWithValue("StarID", StarID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    movies.Add(new MovieStarModel
                    {
                        StarID = Convert.ToInt32(reader["StarID"]),
                        StarName = reader["StarName"].ToString(),
                        MovieID = Convert.ToInt32(reader["MovieID"]),
                        MovieName = reader["MovieName"].ToString(),
                        Rating = Convert.ToInt32(reader["Rating"]),
                        Poster = reader["Poster"].ToString(),
                        Description = reader["Description"].ToString(),
                        ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]),
                        DirectorID = Convert.ToInt32(reader["DirectorID"]),
                        Writer = reader["Writer"].ToString(),
                        Duration = reader["Duration"].ToString()
                    });
                }
            }

            return movies;
        }
        #endregion


        #region Insert Star of Movie
        public bool Insert(int movieID, int starID)
        {

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_MovieStar_Insert]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Adding parameters for the stored procedure
                cmd.Parameters.AddWithValue("@MovieID", movieID);
                cmd.Parameters.AddWithValue("@StarID", starID);


                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion



        #region Delete MovieCategory
        public bool Delete(int starID, int movieID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_MovieStar_Delete]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StarID", starID);
                cmd.Parameters.AddWithValue("@MovieID", movieID);


                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion
    }
}
