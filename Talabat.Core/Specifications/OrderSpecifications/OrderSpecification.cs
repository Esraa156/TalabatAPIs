using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications.OrderSpecifications
{
	public class OrderSpecification:BaseSpecifications<Order>
	{
        public OrderSpecification(string email)
            :base(O=>O.BuyerEmail== email)
        
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

            AddOrderByDesc(O => O.OrderDate);
        }
		public OrderSpecification(string email,int id)
		   : base(O => O.BuyerEmail == email && O.Id==id)

		{
			Includes.Add(O => O.DeliveryMethod);
			Includes.Add(O => O.Items);

		}
	}
}
