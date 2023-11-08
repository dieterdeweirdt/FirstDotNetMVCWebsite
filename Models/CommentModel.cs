using MySql.Data.MySqlClient;

namespace DotNet.Models;

public class Comment
{
    public int Id { get; set; }
    public string User { get; set; }
    public string Text { get; set; }
    private static string connectionString = "Server=localhost;Database=3306;Uid=root;Pwd=secret;Database=gardenblog;";

    public static List<Comment> getComments(int articleId)
    {
        List<Comment> comments = new List<Comment>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT * FROM comments INNER JOIN users on users.id = comments.user_id WHERE article_id = @articleId";

            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@articleId", articleId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Comment comment = new Comment
                        {
                            Id = reader.GetInt32("id"),
                            User = reader.GetString("firstname") + " " + reader.GetString("lastname"),
                            Text = reader.GetString("text")
                        };

                        comments.Add(comment);
                    }
                }
            }

            connection.Close();
        }

        return comments;
    }

   
}