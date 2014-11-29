namespace VehicleVendorConsole.Client
{
    using System;

    using PdfReportCreator;

    using VehicleVendor.Data;
    using VehicleVendor.Data.Repositories;
    using VehicleVendor.DataAceessData.Repository;
    using VehicleVendor.Reports;
    using VehicleVendor.Reports.JsonReportSQLServerGenerator;
    using VehicleVendor.Reports.MySqlDataJsonGenerator;
    using VehicleVendor.Reports.XmlReportSqlServerGenerator;
    using VehicleVendorConsole.Client.XmlInput;
    using VehicleVendorSqLite.Model;
    using VehicleVendorSqLite.Model.Repository;

    public class VehicleVendorConsoleClientEntryPoint
    {
        public static void Main()
        {
            var repo = new VehicleVendorRepository( new IVehicleVendorDbContext[] { new VehicleVendorDbContext() });
            var repoMySql = new VehicleVendorMySqlRepository();          
            var repoSqLite = new VehicleVendorSqLiteRepository(new SqLiteContext());
            var repoMongo = new VehicleVendorMongoRepository(new VehicleVendorMongoDb());
            var mongoLoader = new MongoLoader(repo, repoMongo);
            
            mongoLoader.LoadRepository();
            repo.SaveChanges();

            var xmlParser = new XmlParser(repo);
            var parseResult = xmlParser.ParseDiscounts(@"..\..\..\Discounts.xml", @"..\..\..\Discounts.xsd");
            var xmlLoader = new XmlLoader(repo, parseResult);
            xmlLoader.LoadRepository();
            repo.SaveChanges();

            var zipExLoader = new ZipExcelLoaderNonCom(repo);
            zipExLoader.LoadRepository();
            repo.SaveChanges();

            var pdfReporter = new PdfReportSQLServerGenerator(repo);
            pdfReporter.GenerateReport();

            var xmlReporter = new XmlReportGenerator(repo, new DateTime(2014, 01, 01), DateTime.Now);
            xmlReporter.GenerateReport();

            var jsonReporter = new JsonReportSQLServerGenerator(repo);
            jsonReporter.GenerateReport();

            var jsonToMySql = new MySqlDataJsonLoader(repo, repoMySql);
            jsonToMySql.WriteJsonsReportsToMySql();

            var excelReporter = new ExcelReportsSQLiteGenerator(repoMySql, repoSqLite, new DateTime(2014, 8, 1), new DateTime(2014, 9, 1));
            excelReporter.GenerateReport();
        }
    }
}