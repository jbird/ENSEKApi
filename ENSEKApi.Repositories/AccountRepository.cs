using ENSEKApi.Domain.Interfaces;
using ENSEKApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ENSEKApi.Repositories;

public class AccountRepository(IConfiguration configuration) : IAccountRepository
{
    private readonly string _connectionString = configuration.GetConnectionString("SqlConnectionString") ?? throw new InvalidOperationException("SqlConnectionString is null");

    public async Task<List<Account>> GetAllAccountsAsync()
    {
        var accounts = new List<Account>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using var command = new SqlCommand("dbo.GetAccounts", connection);
            command.CommandType = CommandType.StoredProcedure;

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var account = new Account
                {
                    AccountId = reader.GetInt32(0),
                    AccountName = reader.GetString(1),
                    AccountType = reader.GetString(2)
                };
                accounts.Add(account);
            }
        }

        return accounts;
    }
}
