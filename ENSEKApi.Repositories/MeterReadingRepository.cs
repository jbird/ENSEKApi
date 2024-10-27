using ENSEKApi.Domain.Interfaces;
using ENSEKApi.Models;
using ENSEKApi.Services.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ENSEKApi.Repositories;

public class MeterReadingRepository(IConfiguration configuration) : IMeterReadingRepository
{
    private readonly string _connectionString = configuration.GetConnectionString("SqlConnectionString") ?? throw new InvalidOperationException("SqlConnectionString is null");

    public async Task AddAsync(IEnumerable<MeterReading> meterReadings)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new SqlCommand("dbo.PopulateMeterReadings", connection);
        command.CommandType = CommandType.StoredProcedure;

        var parameter = new SqlParameter("@MeterReadings", SqlDbType.Structured)
        {
            TypeName = "dbo.MeterReadingTableType",
            Value = meterReadings.ToDataTable()
        };
        command.Parameters.Add(parameter);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<List<MeterReading>> GetAllAsync()
    {
        var meterReadings = new List<MeterReading>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using var command = new SqlCommand("dbo.GetMeterReadings", connection);
            command.CommandType = CommandType.StoredProcedure;

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var meterReading = new MeterReading
                {
                    AccountId = reader.GetInt32(0),
                    ReadingDateTime = reader.GetDateTime(1),
                    ReadValue = reader.GetInt32(2)
                };
                meterReadings.Add(meterReading);
            }
        }

        return meterReadings;
    }
}
