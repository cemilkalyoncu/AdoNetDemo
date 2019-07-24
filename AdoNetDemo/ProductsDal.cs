using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AdoNetDemo
{
    public class ProductsDal
    {
        SqlConnection _connection = new SqlConnection("Data Source=303--10;Initial Catalog=AdoDemo;Integrated Security=False;User ID=sa;Password=1234;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void connectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        public List<Products> GetAll()
        {
            connectionControl();
            SqlCommand command = new SqlCommand("Select * from Products order by Id desc", _connection);
            SqlDataReader reader = command.ExecuteReader();
            //DataTable dataTable = new DataTable();
            //dataTable.Load(reader);
            List<Products> products = new List<Products>();
            while (reader.Read())
            {
                Products product = new Products()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    UnitPrice = Convert.ToInt32(reader["UnitPrice"]),
                    StockAmount = Convert.ToInt32(reader["StockAmount"]),
                };
                products.Add(product);
            }

            reader.Close();
            _connection.Close();
            return products;
        }

        //public DataTable GetAll2()
        //{
        //    connectionControl();
        //    SqlCommand command = new SqlCommand("Select * from Products order by Id desc", _connection);
        //    SqlDataReader reader = command.ExecuteReader();
        //    DataTable dataTable = new DataTable();
        //    dataTable.Load(reader);
        //    reader.Close();
        //    _connection.Close();
        //    return dataTable;
        //}

        public void Add(Products product)
        {
            connectionControl();

            SqlCommand command = new SqlCommand("insert into Products Values(@name,@unitPrice,@stockAmount) ",_connection);
            command.Parameters.AddWithValue("@name",product.Name);
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.ExecuteNonQuery();
            _connection.Close();

        }
        public void Update(Products product)
        {
            connectionControl();

            SqlCommand command = new SqlCommand("update Products set Name=@name, unitPrice=@unitPrice, stockAmount=@stockAmount where Id=@id", _connection);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.Parameters.AddWithValue("@id", product.Id);
            command.ExecuteNonQuery();
            _connection.Close();

        }
        public void Delete(int id)
        {
            connectionControl();

            SqlCommand command = new SqlCommand("delete from Products where Id=@id", _connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            _connection.Close();

        }

    }
}
