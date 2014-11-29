namespace VehicleVendor.DataAceessData	
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

	public class DataAccessCountry
	{
        private int id;
        private string name;
        private int region;

        private IList<DataAccessDealer> dataAccessDealers;

        public DataAccessCountry()
        {
            this.dataAccessDealers = new List<DataAccessDealer>(); ;
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
		
		public virtual string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}
		
		[Required()]
		public virtual int Region
		{
			get
			{
				return this.region;
			}
			set
			{
				this.region = value;
			}
		}

		public virtual IList<DataAccessDealer> DataAccessDealers
		{
			get
			{
				return this.dataAccessDealers;
			}
		}
		
	}
}
