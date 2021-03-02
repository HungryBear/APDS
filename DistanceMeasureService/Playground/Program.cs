using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            // Csv data taken from https://pkgstore.datahub.io/core/airport-codes/airport-codes_csv/
            ConvertCsvToSqlLiteDb(@"F:\Downloads\airport-codes_csv.csv", @"E:\Depot\Source\AirportService\lDistanceService\DistanceService.sqlite");

        }

        static void ConvertCsvToSqlLiteDb(string csvFileName, string dbFileName)
        {
            using var reader = new StreamReader(csvFileName);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<dynamic>().ToArray();
            if (records.Length > 0)
            {
                CreateDbAndSchema(dbFileName);
            }
            else
            {
                return;
            }

            var data = new List<(string, double, double)>();
            foreach (var @record in records)
            {
                if (@record.iata_code != null && record.coordinates != null)
                {
                    var cv = (string)record.coordinates.ToString();
                    var coord = cv.Split(',');
                    var iataCode = (string)@record.iata_code.ToString();
                    if (double.TryParse(coord[0], out var lon) && double.TryParse(coord[1], out var lat) && !string.IsNullOrWhiteSpace(iataCode))
                        data.Add(new(iataCode, lat, lon));
                }
            }
            InsertRecords(dbFileName, data.ToArray());
        }

        static void CreateDbAndSchema(string dbFile)
        {
            if (File.Exists(dbFile))
            {
                File.Delete(dbFile);
            };
            SQLiteConnection.CreateFile(dbFile);

            using var connection = new SQLiteConnection($"Data Source={dbFile}");
            connection.Open();
            var sql =
                @"CREATE TABLE airports (
	                IATA_CODE TEXT PRIMARY KEY,
   	                LAT REAL NOT NULL,
	                LON REAL NOT NULL
                );";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        static void InsertRecords(string dbFileName, (string, double, double)[] data)
        {
            using var dbConnection = new SQLiteConnection($"Data Source={dbFileName}");
            dbConnection.Open();
            foreach (var (iata, lat, lon) in data)
            {
                try
                {
                    var sql =
                        $"insert into airports (IATA_CODE,LAT, LON) VALUES ('{iata}', {lat}, {lon})";
                    var command = new SQLiteCommand(sql, dbConnection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

    }
}
