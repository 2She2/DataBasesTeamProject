namespace VehicleVendor.DataAceessData	
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Telerik.OpenAccess;

    public class DataAccessIncome
	{
        private int id;
        private int dealerId;
        private DateTime date;
        private decimal amount;
        private DataAccessDealer dataAccessDealer;

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
		
		[Required()]
        [ForeignKeyAssociation()]
		public virtual int DealerId
		{
			get
			{
				return this.dealerId;
			}
			set
			{
				this.dealerId = value;
			}
		}
		
		[DataType(DataType.DateTime)]
		[Required()]
		public virtual DateTime Date
		{
			get
			{
				return this.date;
			}
			set
			{
				this.date = value;
			}
		}
		
		[Required()]
		public virtual decimal Amount
		{
			get
			{
				return this.amount;
			}
			set
			{
				this.amount = value;
			}
		}
		
		public virtual DataAccessDealer DataAccessDealer
		{
			get
			{
				return this.dataAccessDealer;
			}
			set
			{
				this.dataAccessDealer = value;
			}
		}
	}
}
