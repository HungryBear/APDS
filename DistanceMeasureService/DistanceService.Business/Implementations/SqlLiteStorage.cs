using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using DistanceService.Domain;

namespace DistanceService.Business.Implementations
{
    // ADD Logging EVERYWHERE

    public class SqlLiteStorage : IAirportDataStorage, IDisposable
    {
        private const string IataCodeParam = "@iataCode";
        private static readonly string SelectCoordsQuery = $"SELECT LAT, LON FROM airports WHERE IATA_CODE = {IataCodeParam}";

        public const string ConnectionStringKey = "ConnectionStrings:SqlLite";

        private readonly SQLiteConnection _connection;
        private readonly SQLiteCommand _coordsCmd;
        private readonly SQLiteCommand _codesCmd;

        public SqlLiteStorage(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            _connection = new SQLiteConnection(connectionString);
            _connection.Open();
            _coordsCmd = new SQLiteCommand(SelectCoordsQuery, _connection);
            _coordsCmd.Prepare();

            _codesCmd = new SQLiteCommand("SELECT IATA_CODE FROM airports;", _connection);
            _codesCmd.Prepare();
        }

        public bool Query(string iataCode, out LatLongCoordinates coords)
        {
            coords = new LatLongCoordinates();

            if (string.IsNullOrWhiteSpace(iataCode))
            {
                return false;
            }

            if (_coordsCmd.Parameters.Contains(IataCodeParam))
            {
                _coordsCmd.Parameters[IataCodeParam].Value = iataCode;
                _coordsCmd.Prepare();
            }
            else
            {
                _coordsCmd.Parameters.AddWithValue(IataCodeParam, iataCode);
            }

            using var reader = _coordsCmd.ExecuteReader();
            while (reader.Read())
            {
                var lat = reader.GetDouble(0);
                var lon = reader.GetDouble(1);
                coords = LatLongCoordinates.New(lat, lon);
                return true;
            }

            return false;
        }

        public async IAsyncEnumerable<string> GetAllCodes()
        {
            await using var reader = await _codesCmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                yield return reader.GetString(0);
            }
        }

        public async ValueTask<(bool, LatLongCoordinates)> QueryAsync(string iataCode)
        {
            var coords = new LatLongCoordinates();

            if (string.IsNullOrWhiteSpace(iataCode))
            {
                return (false, coords);
            }

            if (_coordsCmd.Parameters.Contains(IataCodeParam))
            {
                _coordsCmd.Parameters[IataCodeParam].Value = iataCode;
            }
            else
            {
                _coordsCmd.Parameters.AddWithValue(IataCodeParam, iataCode);
            }

            await using var reader = await _coordsCmd.ExecuteReaderAsync();
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
            _coordsCmd.Dispose();
            _connection.Close();
            _connection?.Dispose();
        }
    }
}