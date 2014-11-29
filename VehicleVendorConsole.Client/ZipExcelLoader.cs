namespace VehicleVendorConsole.Client
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Data.OleDb;
    using System.Data;
    using System.Collections.Generic;
    using VehicleVendor.Data.Repositories;
    using VehicleVendor.Models;
    
    public class ZipExcelLoader : IRepositoryLoader
    {
        private IVehicleVendorRepository repo;
        public ZipExcelLoader(IVehicleVendorRepository repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Reads MS Excel (.xls) file through the OLE DB data provider. 
        /// CAUTION: This method does not call SaveChanges() to the repo.
        /// </summary>
        public void LoadRepository()
        {
            string fileLocation = @"..\..\datafile.xls";
            FileInfo dataFile = new FileInfo(fileLocation);
            
            string zipPath = @"..\..\zipfile.zip";
            string extractPath = @"..\..\";
            
            if (!dataFile.Exists)
            {
                ZipFile.ExtractToDirectory(zipPath, extractPath);
            }
            
            
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";"
                + "Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";";

            OleDbConnection db_Con = new OleDbConnection(connectionString);

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
                    string sqlString = "select * from [" + sheetName + "];";

                    OleDbCommand cmdGetRows = new OleDbCommand(sqlString, db_Con);

                    OleDbDataReader reader = cmdGetRows.ExecuteReader();

                    using (reader)
                    {
                        reader.Read();
                        Sale sale;
                        SaleDetails details;
                        var dealer = reader["DealerId"];
                        if (dealer != DBNull.Value)
                        {
                            var dealerInt = (int)(double)dealer;
                            var saleDate = (DateTime)reader["SaleDate"];
                            sale = new Sale() { DealerId = dealerInt, SaleDate = saleDate };
                            this.repo.Add<Sale>(sale);

                            details = this.DetailsRow(this.repo, reader, sale);
                            if (details != null)
                            {
                                this.repo.Add<SaleDetails>(details);
                            }

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
                }
            }//using db_Con
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
