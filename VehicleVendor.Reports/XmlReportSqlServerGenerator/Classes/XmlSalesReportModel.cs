﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
// 
// This source code was auto-generated by xsd, Version=4.0.30319.33440.
// 
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
    [XmlRootAttribute("sales_report", Namespace = "http://nissan.com/ReportSchema.xsd", IsNullable = false)]
    public partial class XmlSalesReportModel
    {
        /// <remarks/>
        private XmlCountryModel[] countryField;

        [XmlElementAttribute("country")]
        public XmlCountryModel[] Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }
    }
}