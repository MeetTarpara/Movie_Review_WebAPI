using Microsoft.Data.SqlClient;
using Movie_Review_WebAPI.Models;
using System.Data;

namespace Movie_Review_WebAPI.Data
{
    public class MovieCategoryRepository
    {
        private readonly string _connectionString;

        public MovieCategoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        
        #region Select All Category by MovieID
        public IEnumerable<MovieCategoryModel> GetAllCategoryByMovieID(int MovieID)
        {
            var categories = new List<MovieCategoryModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_MovieCategory_CategorySelectByMovieID]", conn)
                {
                    CommandType = CommandType.StoredProcedure

                };

                cmd.Parameters.AddWithValue("MovieID", MovieID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
        
                while (reader.Read())
                {
                    
                    categories.Add(new MovieCategoryModel
                    {
                        CategoryID= Convert.ToInt32(reader["CategoryID"]),
                        CategoryName = reader["CategoryName"].ToString(),
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

            return categories;
        }

        #endregion


        #region Select All Movie by the CategoryID
        public IEnumerable<MovieCategoryModel> GetAllMovieByCategoryId(int CategoryID)
        {
            var movies = new List<MovieCategoryModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_MovieCategory_MovieSelectByCategoryID]", conn)
                {
                    CommandType = CommandType.StoredProcedure

                };

                cmd.Parameters.AddWithValue("CategoryID", CategoryID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    movies.Add(new MovieCategoryModel
                    {
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        CategoryName = reader["CategoryName"].ToString(),
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


        #region Insert Category of Movie

        public bool Insert(int movieID , int categoryID)
        {

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_MovieCategory_Insert]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Adding parameters for the stored procedure
                cmd.Parameters.AddWithValue("@MovieID",movieID);
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);


                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        #endregion


        #region Delete MovieCategory
        public bool Delete(int categoryID,int movieID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_MovieCategory_Delete]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                cmd.Parameters.AddWithValue("@MovieID", movieID);


                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion


 


    }
}
