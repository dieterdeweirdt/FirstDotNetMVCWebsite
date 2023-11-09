using System.Data;
using MySql.Data.MySqlClient;

namespace DotNet.Models;

public class Article : BaseModel
{

        private static readonly string connectionString = "Server=localhost;Database=3306;Uid=root;Pwd=secret;Database=gardenblog;";

    public int Id { get; set; }
    public string Slug { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public static IEnumerable<Article> GetAll()
    {
        string query = "SELECT * FROM Articles";
        return GetAll(query, (IDataRecord record) =>
        {
            return new Article
            {
                Id = Convert.ToInt32(record["id"]),
                Slug = record["slug"].ToString(),
                Title = record["title"].ToString(),
                Content = record["content"].ToString()
            };
        });
    }


    public static Article GetById(int id)
    {
        string query = "SELECT * FROM articles WHERE id = @id";
        return GetById(query, id, (IDataRecord record) =>
        {
            return new Article
            {
                Id = Convert.ToInt32(record["id"]),
                Slug = record["slug"].ToString(),
                Title = record["title"].ToString(),
                Content = record["content"].ToString()
            };
        });
    }
    /*public static Article getArticleById(int id) {

        Article article = new Article();

    using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT * FROM articles WHERE id = @id";

            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        article.Id = reader.GetInt32("id");
                        article.Slug = reader.GetString("slug");
                        article.Title = reader.GetString("title");
                        article.Content = reader.GetString("content");
                    }
                }
            }

            connection.Close();
        }

        return article;
    }*/
}