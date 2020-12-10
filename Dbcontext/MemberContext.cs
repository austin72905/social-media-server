using Microsoft.EntityFrameworkCore;
using SocialMedia.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Dbcontext
{
    public class MemberContext : DbContext
    {
        public MemberContext(DbContextOptions<MemberContext> options) :base(options)
        {

        }


        public DbSet<Member> Members { get; set; }

        public DbSet<Password> Passwords { get; set; }
        public DbSet<MemberInfo> MemberInfos { get; set; }
        public DbSet<Directory> Directorys { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Personality> Personalitys { get; set; }
        public DbSet<PreferType> PreferTypes { get; set; }
        public DbSet<MemberInterest> MemberInterests { get; set; }

        //可以使用 OnModelCreating 覆寫成想要的資料表名稱
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API
            modelBuilder.Entity<Member>().ToTable("Member");
            modelBuilder.Entity<Password>().ToTable("Password");
            modelBuilder.Entity<MemberInfo>().ToTable("MemberInfo");
            modelBuilder.Entity<Directory>().ToTable("Directory");

            modelBuilder.Entity<Interest>().ToTable("Interest");
            modelBuilder.Entity<Personality>().ToTable("Personality");
            modelBuilder.Entity<PreferType>().ToTable("PreferType");
            modelBuilder.Entity<MemberInterest>().ToTable("MemberInterest");
        }
    }
}
