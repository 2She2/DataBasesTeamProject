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
    public partial class XmlDealerModel
    {
        private XmlDaySaleReportModel[] daySaleField;

        private string nameField;

        /// <remarks/>
        [XmlElementAttribute("day_sale")]
        public XmlDaySaleReportModel[] DaySale
        {
            get
            {
                return this.daySaleField;
            }
            set
            {
                this.daySaleField = value;
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