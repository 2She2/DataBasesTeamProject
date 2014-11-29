/// <remarks/>
namespace VehicleVendor.Reports.XmlReportSqlServerGenerator.Classes
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://nissan.com/ReportSchema.xsd")]
    [XmlRootAttribute(Namespace = "http://nissan.com/ReportSchema.xsd", IsNullable = false)]
    public partial class XmlCountryModel
    {
        private XmlDealerModel[] dealerField;

        private string nameField;

        /// <remarks/>
        [XmlElementAttribute("dealer")]
        public XmlDealerModel[] Dealer
        {
            get
            {
                return this.dealerField;
            }
            set
            {
                this.dealerField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute("name")]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }
}