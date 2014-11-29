namespace VehicleVendor.DataAceessData
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

	public class DataAccessDealer
	{
        private int id;
        private string company;
        private string address;
        private int countryId;

        private DataAccessCountry dataAccessCountry;
        private IList<DataAccessIncome> dataAccessIncomes;

        public DataAccessDealer()
        {
            this.dataAccessIncomes = new List<DataAccessIncome>();
        }

        [Required()]
        [Key()]
		public virtual int Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}
		
		public virtual string Company
		{
			get
			{
				return this.company;
			}
			set
			{
				this.company = value;
			}
		}
		
		public virtual string Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.address = value;
			}
		}
		
		[Required()]
		public virtual int CountryId
		{
			get
			{
				return this.countryId;
			}
			set
			{
				this.countryId = value;
			}
		}
		
		public virtual IList<DataAccessIncome> DataAccessIncomes
		{
			get
			{
				return this.dataAccessIncomes;
			}
		}
		
		public virtual DataAccessCountry DataAccessCountry
		{
			get
			{
				return this.dataAccessCountry;
			}
			set
			{
				this.dataAccessCountry = value;
			}
		}		
	}
}
