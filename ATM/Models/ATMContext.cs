using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class ATMContext : DbContext
    {
        public int Id { get; set; }
        public DbSet<AccountInfo> AccountInfo { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        
    }
}
