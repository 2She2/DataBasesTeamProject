namespace VehicleVendor.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
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
    using VehicleVendorConsole.Client;
    using VehicleVendorSqLite.Model.Repository;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IVehicleVendorRepository repo;
        private IVehicleVendorMySqlRepository repoMySql;
        private IVehicleVendorMongoRepository repoMongo;
        private IVehicleVendorSqLiteRepository repoSqLite;

        public MainWindow()
        {
            InitializeComponent();
            this.repo = new VehicleVendorRepository(new IVehicleVendorDbContext[] { new VehicleVendorDbContext() });
            this.repoMySql = new VehicleVendorMySqlRepository();
            this.repoMongo = new VehicleVendorMongoRepository(new VehicleVendorMongoDb());
            this.repoSqLite = new VehicleVendorSqLiteRepository(new SqLiteContext());
        }

        public void OnLoadMongoClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var mongoLoader = new MongoLoader(this.repo, this.repoMongo);
                mongoLoader.LoadRepository();
                repo.SaveChanges();
                Print("Countries, dealers and vehicle data from MongoDB successfully loaded.");
            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }

        public void OnXMLToSQLClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var xmlParser = new XmlParser(repo);
                var parseResult = xmlParser.ParseDiscounts(@"..\..\..\Discounts.xml", @"..\..\..\Discounts.xsd");
                var xmlLoader = new XmlLoader(repo, parseResult);
                xmlLoader.LoadRepository();
                repo.SaveChanges();
                Print("Discounts data from XML to SQL Server successfuly loaded.");
            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }

        public void OnXMLToMongoClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var xmlParser = new XmlParser(repo);
                var parseResult = xmlParser.ParseDiscounts(@"..\..\..\Discounts.xml", @"..\..\..\Discounts.xsd");
                var mongoLoader = new MongoLoader(this.repo, this.repoMongo);
                mongoLoader.LoadDiscountsInMongo(parseResult);
                Print("Discounts data from XML to MongoDB successfuly loaded.");
            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }

        public void OnLoadExcelZipClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var zipExLoader = new ZipExcelLoaderNonCom(repo);
                zipExLoader.LoadRepository();
                repo.SaveChanges();
                Print("Sales from zipped Excel 2003 files to SQL Server successfuly loaded.");
            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }

        public void OnGenerateExcelClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var excelReporter = new ExcelReportsSQLiteGenerator(repoMySql, repoSqLite, new DateTime(2014, 8, 1), new DateTime(2014, 9, 1));
                excelReporter.GenerateReport();
                Print("Excel 2007 report from SQLite and MySql is completed.");
            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }

        public void OnGeneratePdfReportClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var pdfReporter = new PdfReportSQLServerGenerator(repo);
                pdfReporter.GenerateReport();
                Print("PDF report from SQL is completed.");
            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }

        public void OnGenerateJSONClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var jsonReporter = new JsonReportSQLServerGenerator(repo);
                jsonReporter.GenerateReport();

                var jsonToMySql = new MySqlDataJsonLoader(repo, repoMySql);
                jsonToMySql.WriteJsonsReportsToMySql();
                Print("Data from SQL to JSON and then to MySQL is successfully transferred.");
            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }

        public void OnGenerateXMLReportClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var xmlReporter = new XmlReportGenerator(repo, new DateTime(2014, 01, 01), DateTime.Now);
                xmlReporter.GenerateReport();
                Print("XML Report from SQL is successfully genrated.");
            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }

        public void Print(string message)
        {
            this.Msg.Text = message;
        }
    }
}
