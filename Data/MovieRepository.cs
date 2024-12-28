using Microsoft.Data.SqlClient;
using Movie_Review_WebAPI.Models;
using System.Data;

namespace Movie_Review_WebAPI.Data
{
    public class MovieRepository
    {
        private readonly string _connectionString;

        public MovieRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region Select All Movies
        public IEnumerable<MovieModel> SelectAllMovies()
        {
            var movies = new List<MovieModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_Movie_SelectAll]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    movies.Add(new MovieModel
                    {
                        MovieID = Convert.ToInt32(reader["MovieID"]),
                        Poster = reader["Poster"].ToString(),
                        MovieName = reader["MovieName"].ToString(),
                        Rating = Convert.ToDecimal(reader["Rating"]),
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

        #region Select Movie By ID
        public MovieModel SelectByPK(int movieID)
        {
            MovieModel movie = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_Movie_SelectByPK]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@MovieID", movieID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    movie = new MovieModel
                    {
                        MovieID = Convert.ToInt32(reader["MovieID"]),
                        MovieName = reader["MovieName"].ToString(),
                        Rating = Convert.ToDecimal(reader["Rating"]),
                        Poster = reader["Poster"].ToString(),
                        Description = reader["Description"].ToString(),
                        ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]),
                        DirectorID = Convert.ToInt32(reader["DirectorID"]),
                        Writer = reader["Writer"].ToString(),
                        Duration = reader["Duration"].ToString()
                    };
                }
            }

            return movie;
        }
        #endregion

        #region Insert Movie
        public bool Insert(MovieModel movie)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_Movie_Insert]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Adding parameters for the stored procedure
                cmd.Parameters.AddWithValue("@MovieName", movie.MovieName);
                cmd.Parameters.AddWithValue("@Poster", movie.Poster);
                cmd.Parameters.AddWithValue("@Rating", movie.Rating);

                cmd.Parameters.AddWithValue("@Description", movie.Description);
                cmd.Parameters.AddWithValue("@ReleaseDate", movie.ReleaseDate);
                cmd.Parameters.AddWithValue("@DirectorID", movie.DirectorID);
                cmd.Parameters.AddWithValue("@Writer", movie.Writer);
                cmd.Parameters.AddWithValue("@Duration", movie.Duration);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update Movie
        public bool Update(MovieModel movie)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_Movie_Update]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Adding parameters for the stored procedure
                cmd.Parameters.AddWithValue("@MovieID", movie.MovieID);
                cmd.Parameters.AddWithValue("@MovieName", movie.MovieName);
                cmd.Parameters.AddWithValue("@Rating", movie.Rating);

                cmd.Parameters.AddWithValue("@Poster", movie.Poster);
                cmd.Parameters.AddWithValue("@Description", movie.Description);
                cmd.Parameters.AddWithValue("@ReleaseDate", movie.ReleaseDate);
                cmd.Parameters.AddWithValue("@DirectorID", movie.DirectorID);
                cmd.Parameters.AddWithValue("@Writer", movie.Writer);
                cmd.Parameters.AddWithValue("@Duration", movie.Duration);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region Delete Movie
        public bool Delete(int movieID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_Movie_Delete]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@MovieID", movieID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion
    }
}
