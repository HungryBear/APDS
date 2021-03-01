using System;
using System.Data.SQLite;
using System.Threading.Tasks;
using DistanceService.Domain;

namespace DistanceService.Business.Implementations
{
    public class SqlLiteStorage : IAirportDataStorage, IDisposable
    {
        private const string SelectQuery = "SELECT LAT, LON FROM airports WHERE IATA_CODE = @iataCode";

        public const string ConnectionStringKey = "ConnectionStrings:SqlLite";

        private readonly SQLiteConnection _connection;

        public SqlLiteStorage(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            _connection = new SQLiteConnection(connectionString);
            _connection.Open();

        }

        public bool Query(string iataCode, out LatLongCoordinates coords)
        {
            coords = new LatLongCoordinates();

            if (string.IsNullOrWhiteSpace(iataCode))
            {
                return false;
            }

            var sql = SelectQuery;
            SQLiteCommand cmd = new SQLiteCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@iataCode", iataCode);
            cmd.Prepare();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var lat = reader.GetDouble(0);
                var lon = reader.GetDouble(1);
                coords = LatLongCoordinates.New(lat, lon);
                return true;
            }

            return false;
        }

        public async ValueTask<(bool, LatLongCoordinates)> QueryAsync(string iataCode)
        {
            var coords = new LatLongCoordinates();

            if (string.IsNullOrWhiteSpace(iataCode))
            {
                return (false, coords);
            }

            var sql = SelectQuery;
            var cmd = new SQLiteCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@iataCode", iataCode);
            await cmd.PrepareAsync();
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var lat = reader.GetDouble(0);
                var lon = reader.GetDouble(1);
                coords = LatLongCoordinates.New(lat, lon);
                return (true, coords);
            }

            return (false, coords);
        }

        public void Dispose()
        {
            _connection.Close();
            _connection?.Dispose();
        }
    }
}