using System.Data;

namespace Inventory.API.Helpers
{
    public static class CsvParser//Create s static class which contains a static method which is public to accessible for the other classes
    {
        public static DataTable ParseProductsCsv(Stream fileStream)
        {
            var table = new DataTable();
            table.Columns.Add("ProductCode", typeof(string));
            table.Columns.Add("ProductName", typeof(string));
            table.Columns.Add("Price", typeof(decimal));

            using var reader = new StreamReader(fileStream);
            string? line;
            bool isHeader = true;

            while ((line = reader.ReadLine()) != null)
            {
                if (isHeader)
                {
                    isHeader = false;
                    continue;
                }

                var values = line.Split(',');

                table.Rows.Add(
                    values[0],
                    values[1],
                    Convert.ToDecimal(values[2]));
            }

            return table;
        }
    }
}
