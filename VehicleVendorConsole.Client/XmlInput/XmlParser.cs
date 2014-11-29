namespace VehicleVendorConsole.Client.XmlInput
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Schema;
    using VehicleVendor.Data.Repositories;

    public class XmlParser
    {
        private const string NullOrEmptyPath = "Specified path is incorrect (null or empty)";
        private const string FileNotFoundExceptionFormat = "The specified {0} file does not exist";
        private const string NoDealerInCountry = "No such dealer in this country: {0}";
        private const string NoDealer = "No Dealer: {0}";
        private const string XmlValidationErrorFormat = "Validation error: {0}";
        private const string DiscountKey = "discount";

        private readonly IVehicleVendorRepository repo;
        private XmlSchema schema;

        public XmlParser(IVehicleVendorRepository repo)
        {
            this.repo = repo;
        }

        public IDictionary<int, double> ParseDiscounts(string xmlLocation, string schemaFileLocation)
        {
            this.CheckFileExistence(xmlLocation);
            this.CheckFileExistence(schemaFileLocation);
            this.AddSchema(schemaFileLocation);
            
            // Result
            var result = new Dictionary<int, double>();
            
            // Set validation settings
            var settings = new XmlReaderSettings();
            settings.Schemas.Add(this.schema);
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += new ValidationEventHandler(this.DiscountsValidationEventHandler);

            // Read xml
            using (var discountsReader = XmlReader.Create(xmlLocation, settings))
            {
                var discountParameters = new Dictionary<string, string>();
                var ns = discountsReader.NamespaceURI;
                string lastElement = null;
                while (discountsReader.Read())
                {
                    switch (discountsReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (discountsReader.Name == string.Format("{0}{1}", ns, DiscountKey))
                            {
                                this.GenerateParseResult(discountParameters, result);
                            }
                            else
                            {
                                lastElement = discountsReader.LocalName;
                            }

                            break;
                        case XmlNodeType.Text:
                            var value = discountsReader.Value;
                            discountParameters.Add(lastElement, value);
                            break;
                        case XmlNodeType.EndElement:
                            this.GenerateParseResult(discountParameters, result);
                            break;
                    }
                }
            }

            return result;
        }

        private bool AddSchema(string schemaFileLocation)
        {
            using (var schemaReader = File.OpenRead(schemaFileLocation))
            {
                this.schema = XmlSchema.Read(schemaReader, this.DiscountsValidationEventHandler);
            }

            return true;
        }

        private void CheckFileExistence(string pathToFile)
        {
            if (string.IsNullOrEmpty(pathToFile))
            {
                throw new ArgumentException(NullOrEmptyPath);
            }

            if (!File.Exists(pathToFile))
            {
                throw new FileNotFoundException(string.Format(FileNotFoundExceptionFormat, "XSD schema"), pathToFile);
            }
        }
        
        private bool GenerateParseResult(IDictionary<string, string> dealerSchema, IDictionary<int, double> resultCollection)
        {
            if (dealerSchema.Count < 3)
            {
                return false;
            }

            //Find Dealer and generate Discount entry
            var currentCompany = dealerSchema["company"];
            var dealers = this.repo.Dealers.Where(d => d.Company == currentCompany);
            if (dealers == null || dealers.Count() == 0)
            {
                throw new ArgumentException(string.Format(NoDealer, currentCompany));
            }

            var currentCountry = dealerSchema["country"];
            var currentDealer = dealers.Where(d => d.Country.Name == currentCountry).FirstOrDefault();
            if (currentDealer == null)
            {
                throw new ArgumentException(string.Format(NoDealerInCountry, currentCountry));
            }

            double amount = double.Parse(dealerSchema["amount"]);
            resultCollection.Add(currentDealer.Id, amount);
            dealerSchema.Clear();
            return true;
        }

        private void DiscountsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    throw new XmlSchemaValidationException(string.Format(XmlValidationErrorFormat, e.Message));
                case XmlSeverityType.Warning:
                    // Could be logged.
                    break;
            }
        }
    }
}