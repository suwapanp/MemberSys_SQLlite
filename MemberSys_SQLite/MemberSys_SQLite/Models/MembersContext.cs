using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MemberSys_SQLite.Models
{
    public class MembersContext : DbContext
    {
        public MembersContext(DbContextOptions<MembersContext> options) : base(options)
        {

        }
        public DbSet<Members> Members { get; set; }
    }
    public class Members
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    }
}
