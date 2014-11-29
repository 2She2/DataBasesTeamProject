namespace VehicleVendorConsole.Client
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Data.OleDb;
    using System.Data;
    using System.Collections.Generic;
    using System.Linq;
    using VehicleVendor.Data.Repositories;
    using VehicleVendor.Models;

    /// <summary>
    /// Reads MS Excel (.xls) files in a Zip through the OLE DB data provider. 
    /// CAUTION: This method does not call SaveChanges() to the repo.
    /// </summary>
    public class ZipExcelLoaderNew : IRepositoryLoader
    {
        private IVehicleVendorRepository repo;
        public ZipExcelLoaderNew(IVehicleVendorRepository repo)
        {
            this.repo = repo;
        }

        public void LoadRepository()
        {
            //1. recursive delete \extracted [catching NonExisting exception]
            string extractPath = @"..\..\extracted";
            if (Directory.Exists(extractPath))
            {
                Directory.Delete(extractPath, true);
            }

            //2. create \extracted
            Directory.CreateDirectory(extractPath);

            //3. extract from sales_reports.zip into extracted
            string zipPath = @"..\..\sales_reports.zip";
            ZipFile.ExtractToDirectory(zipPath, extractPath);

            //4. get \extracted directories
            var subfolders = Directory.GetDirectories(extractPath);

            //5. foreach the directories 
            foreach (var dateFolderPath in subfolders)
            {
                string dateFolder = dateFolderPath.Split('\\').Last();

                //    parsing the date (parse fail = skip)
                DateTime saleDate;
                if (!DateTime.TryParse(dateFolder, out saleDate))
                {
                    continue;
                }

                //    get filenames
                var saleReportsByDealer = Directory.GetFiles(dateFolderPath);

                //    foreach the filenames
                foreach (var reportPath in saleReportsByDealer)
                {
                    // check if .xls extension (fail = skip)
                    var fileInfo = new FileInfo(reportPath);
                    string extension = fileInfo.Extension;
                    if (extension != ".xls")
                    {
                        continue;
                    }
                    // parse int for DealerId (fail = skip)
                    string filename = fileInfo.Name.Split('.').First();
                    int dealerId;
                    if (!int.TryParse(filename, out dealerId))
                    {
                        continue;
                    }

                    // connect via OleDb + pull the rows + close connection
                    string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + reportPath + ";"
                        + "Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";";

                    OleDbConnection db_Con = new OleDbConnection(connectionString);

                    #region Connection
                    db_Con.Open();
                    using (db_Con)
                    {

                        // this code is for discovering sheet names if they are not available
                        DataTable tables = db_Con.GetSchema("Tables");
                        var sheetNameList = new List<string>();
                        for (int i = 0; i < tables.Rows.Count; i++)
                        {
                            var name = tables.Rows[i][2].ToString();
                            sheetNameList.Add(name);
                        }

                        foreach (var sheetName in sheetNameList)
                        {
                            Sale sale = new Sale() { DealerId = dealerId, SaleDate = saleDate };
                            this.repo.Add<Sale>(sale);

                            string sqlString = "select * from [" + sheetName + "];";

                            OleDbCommand cmdGetRows = new OleDbCommand(sqlString, db_Con);
                            //cmdGetRows.Parameters.AddWithValue("@sheetName", sheetName);

                            OleDbDataReader reader = cmdGetRows.ExecuteReader();
                            using (reader)
                            {
                                SaleDetails details;
                                while (reader.Read())
                                {
                                    details = this.DetailsRow(this.repo, reader, sale);
                                    if (details != null)
                                    {
                                        this.repo.Add<SaleDetails>(details);
                                    }
                                }
                            }
                        }
                    }//using db_Con
                    #endregion
                }
            }
        }

        private SaleDetails DetailsRow(IVehicleVendorRepository repo, OleDbDataReader reader, Sale sale)
        {
            var detailQty = reader["Quantity"];
            if (detailQty != DBNull.Value)
            {
                var detailQtyInt = (int)(double)detailQty;
                var vehiceId = (int)(double)reader["VehicleId"];
                var details = new SaleDetails() { Quantity = (int)(double)detailQtyInt, VehicleId = vehiceId, Sale = sale };
                return details;
            }
            return null;
        }
    }
}