using Dapper;
using DapperLab.Domain;
using System.Data.Common;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace DapperLab;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            Dapper.SqlMapper.AddTypeHandler(new StringUlidHandler());
            DbProviderFactories.RegisterFactory("System.Data.SQLite", SQLiteFactory.Instance);
            var factory = DbProviderFactories.GetFactory("System.Data.SQLite");
            var connection = factory.CreateConnection();
            string connectionString = @"Data Source=C:\Temp\test.db";
            connection.ConnectionString = connectionString;
            await connection.OpenAsync();
            var users = (await connection.QueryAsync<User>("select * from Users")).ToList();
            

            var newUser = new User
            {
                Id = Ulid.NewUlid(),
                Name = "Kirk Hammett",
                Created = DateTime.Now
            };
            
            var res = await connection.ExecuteAsync("insert into Users (Id, Name, Created) values (@Id, @Name, @Created)", newUser);
            
            var users2 = await connection.QueryAsync<User>("select * from Users");

            await connection.CloseAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        
        // Console.WriteLine("Press any key to exit...");
        // Console.Read();
    }
}



public class StringUlidHandler : SqlMapper.TypeHandler<Ulid>
{
    public override Ulid Parse(object value)
    {
        return Ulid.Parse((string)value);
    }

    public override void SetValue(IDbDataParameter parameter, Ulid value)
    {
        parameter.DbType = DbType.StringFixedLength;
        parameter.Size = 26;
        parameter.Value = value.ToString();
    }
}