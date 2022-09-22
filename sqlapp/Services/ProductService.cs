using MySql.Data.MySqlClient;
using sqlapp.Models;
using System.Text.Json;

namespace sqlapp.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private MySqlConnection GetConnection()
        {
            string connectionString = "Server=mysql; Port=3306; Database=appdb; Uid=root; Pwd=Password01#; SslMode=Preferred;";
            return new MySqlConnection(connectionString);
        }

        public async Task<List<Product>> GetProducts()
        {
            List<Product> _product_lst = new List<Product>();
            string _statement = "SELECT ProductID,ProductName,Quantity from Products";
            MySqlConnection _connection = GetConnection();

            _connection.Open();

            MySqlCommand _sqlcommand = new MySqlCommand(_statement, _connection);

            using (MySqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    Product _product = new Product()
                    {
                        ProductID = _reader.GetInt32(0),
                        ProductName = _reader.GetString(1),
                        Quantity = _reader.GetInt32(2)
                    };

                    _product_lst.Add(_product);
                }
            }
            _connection.Close();
            return _product_lst;

            //string functionUrl = "https://appfunction1258.azurewebsites.net/api/GetProducts?code=zX1OwH6QdIu-KD9hMyk4aaSXYIRmo3ADhylteK91YLYQAzFuK27UBw==";

            //using (HttpClient client = new HttpClient())
            //{
            //    HttpResponseMessage response = await client.GetAsync(functionUrl);
            //    string content = await response.Content.ReadAsStringAsync();
            //    return JsonSerializer.Deserialize<List<Product>>(content);
            //}
        }
    }
}
