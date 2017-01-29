using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class AccountInfo
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public double Withdraw { get; set; }
        public double Deposit { get; set; }
        public DateTime DateTime { get; set; }
        public virtual UserInfo UserInfo { get; set; }



    }
}
