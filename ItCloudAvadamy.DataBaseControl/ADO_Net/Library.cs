using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Net
{
    public class Library
    {
        string connectionString;
        List<Book> books = new List<Book>();
        public Library(string connection)
        {
            this.connectionString = connection;
        }

        public void InsertBook(string name, string author, string publisher, int year)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                string sqlExpression = $"INSERT INTO Books (Name,Author,Publisher,Year) " +
                    $"VALUES ('{name}', '{author}', '{publisher}', {year})";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine($"Added objects: {number}");
            }
        }

        public void DeleteBook(string name)
        {
            string sqlExpression = $"DELETE  FROM Books WHERE Name='{name}'";

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine($"Deleted objects: {number}");
            }
        }

        public void UpdateBook(string oldname, string newname, string author, string publisher, int year)
        {
            string sqlExpression = $"UPDATE Books " +
                $"SET Name = {newname}, Author = {author}, Publisher = {publisher}, Year = {year} " +
                $"WHERE Name={oldname}";
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine($"Items were updated: {number}");
            }
        }

        public void GetAllBooksOfUser(string name)
        {
            string sqlExpression = $"SELECT * FROM Books b " +
                 $"JOIN Users u ON b.UserID = u.Id" +
                 $"WHERE u.Name = {name}";
            books.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var book = new Book();
                    book.Name = reader.GetString(1) != DBNull.Value.ToString() ? reader.GetString(1) : string.Empty;
                    book.Author = reader.GetString(2) != DBNull.Value.ToString() ? reader.GetString(2) : string.Empty;
                    book.Publisher = reader.GetString(3) != DBNull.Value.ToString() ? reader.GetString(3) : string.Empty;
                    book.Year = reader.GetInt32(4);
                }
            }
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Name} - {book.Author} - {book.Publisher} - {book.Year}");
            }
        }

        public void GetBookInfo(int id)
        {
            string sqlExpression = $"SELECT Name, Author, Publisher, Year FROM Books WHERE Id = {id}";
            var book = new Book();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    book.Name = reader.GetString(1);
                    book.Author = reader.GetString(2);
                    book.Publisher = reader.GetString(3);
                    book.Year = reader.GetInt32(4);
                }
            }

            Console.WriteLine($"{book.Name} - {book.Author} - {book.Publisher} - {book.Year}");
        }

        public int GetCountOfBook(string author)
        {
            string sqlExpression = $"SELECT COUNT(*) AS CountOfBooks FROM BOOKS WHERE Author = {author}";
            int countOfBook;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                countOfBook = (int)command.ExecuteScalar();
            }
            return countOfBook;
        }

        public void TakeBook(string name, string author, int IdofUser)
        {
            string sqlExpression = $"UPDATE Books " +
                $"SET UserId = {IdofUser} " +
                $"WHERE Name = '{name}' AND Author = '{author}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine($"Books were taken: {number}, Name = {name}");
            }
        }

        public void ReturnBook(string name, string author)
        {
            string sqlExpression = $"UPDATE Books" +
                $"SET UserId = NULL " +
                $"WHERE Name = '{name}' AND Author = '{author}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine($"Books were taken: {number} , Name = {name}");
            }
        }
    }
}
