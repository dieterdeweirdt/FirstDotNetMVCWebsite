using MySql.Data.MySqlClient;

namespace DotNet.Models;

public class Article
{
    public int Id { get; set; }
    public string Slug { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    private static string connectionString = "Server=localhost;Database=3306;Uid=root;Pwd=secret;Database=gardenblog;";

   /* private static List<Article> _articles = new List<Article>
    {
        new Article { Id = 1, Title = "Article 1", Content = "This is the content of article 1." },
        new Article { Id = 2, Title = "Article 2", Content = "This is the content of article 2." },
        new Article { Id = 3, Title = "Article 3", Content = "This is the content of article 3." }
    };*/

    public static List<Article> getArticles()
    {
        List<Article> articles = new List<Article>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT * FROM articles";

            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Article article = new Article
                        {
                            Id = reader.GetInt32("id"),
                            Slug = reader.GetString("slug"),
                            Title = reader.GetString("title"),
                            Content = reader.GetString("content")
                        };

                        articles.Add(article);
                    }
                }
            }

            connection.Close();
        }

        return articles;
    }

    public static Article getArticleById(int id) {

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
    }
}