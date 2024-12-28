using Microsoft.Data.SqlClient;
using Movie_Review_WebAPI.Models;
using System.Data;

namespace Movie_Review_WebAPI.Data
{
    public class CategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region Select All Categories
        public IEnumerable<CategoryModel> SelectAllCategories()
        {
            var categories = new List<CategoryModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_Category_SelectAll]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    categories.Add(new CategoryModel
                    {
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        CategoryName = reader["CategoryName"].ToString()
                    });
                }
            }

            return categories;
        }
        #endregion

        #region Select Category By ID
        public CategoryModel SelectByPK(int categoryID)
        {
            CategoryModel category = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_Category_SelectByPK]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    category = new CategoryModel
                    {
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        CategoryName = reader["CategoryName"].ToString()
                    };
                }
            }

            return category;
        }
        #endregion

        #region Insert Category
        public bool Insert(CategoryModel category)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_Category_Insert]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Adding parameters for the stored procedure
                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update Category
        public bool Update(CategoryModel category)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_Category_Update]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Adding parameters for the stored procedure
                cmd.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region Delete Category
        public bool Delete(int categoryID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[PR_Category_Delete]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CategoryID", categoryID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion
    }
}
