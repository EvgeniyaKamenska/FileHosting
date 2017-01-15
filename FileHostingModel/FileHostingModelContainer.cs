using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace FileHostingModel
{
    public partial class FileHostingModelContainer : DbContext
    {
        public FileHostingModelContainer()
            : base("name=FileHostingDb")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<FileHostingModelContainer>(null);
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<FileHostingModelContainer>());

        }

        public virtual DbSet<UserEntity> UserEntitySet { get; set; }
        public virtual DbSet<FileEntity> FileEntitySet { get; set; }
    }
}
