namespace IEProject_Final.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class IEProjectModels : DbContext
    {
        public IEProjectModels()
            : base("name=IEProjectModels")
        {
        }

        public virtual DbSet<Housing> Housings { get; set; }
        public virtual DbSet<School> Schools { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Housing>()
                .Property(e => e.Suburb)
                .IsUnicode(false);

            modelBuilder.Entity<School>()
                .Property(e => e.Suburb)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<IEProject_Final.Models.House_Rating> House_Rating { get; set; }

        public System.Data.Entity.DbSet<IEProject_Final.Models.Updated_Result> Updated_Result { get; set; }
    }
}
