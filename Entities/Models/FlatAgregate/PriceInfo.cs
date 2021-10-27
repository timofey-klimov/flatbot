using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.FlatEntities
{
    public class PriceInfo : Entity<int>
    {
        public int FlatId { get; private set; }

        public int Price { get; private set; }

        public int? Deposit { get; private set; }

        public int? AgentFee { get; private set; }

        private PriceInfo() { }

        public PriceInfo(
            int price,
            int? deposit,
            int? agentFee)
        {
            Price = price;
            Deposit = deposit;
            AgentFee = agentFee;
        }
    }
}
