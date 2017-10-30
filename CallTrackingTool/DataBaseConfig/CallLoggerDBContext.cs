using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CallTrackingTool
{
    public class CallLoggerDBContext : DbContext
    {
        
        public DbSet<StaffMember> StaffMember { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Reports> Reports { get; set; }

        public CallLoggerDBContext()
            :base("name=CallLoggerDBContext")
        {
           // Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Calls Overides,  Create navigation and initate FK's

            modelBuilder.Entity<Call>()
                .HasRequired(c => c.Staff)
                .WithMany(s => s.Calls)
                .HasForeignKey(s => s.StaffId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Call>()
                .HasRequired(c => c.Client)
                .WithMany(cl => cl.Calls)
                .HasForeignKey(c => c.ClientId)
                .WillCascadeOnDelete(false);


            // StaffMember Overides

            modelBuilder.Entity<StaffMember>()
                .Property(S => S.StaffFirstName)
                .IsRequired();

            modelBuilder.Entity<StaffMember>()
                .Property(S => S.StaffLastName)
                .IsRequired();



            // Client Overides

            modelBuilder.Entity<Client>()
                .Property(C => C.FirstName)
                .IsRequired();

            modelBuilder.Entity<Client>()
                .Property(C => C.LastName)
                .IsRequired();

            modelBuilder.Entity<Client>()
                .Property(C => C.PostCode)
                .IsRequired();

            // Reports Overide

            modelBuilder.Entity<Reports>()
                .HasRequired(c => c.Staff)
                .WithMany(s => s.Reports)
                .HasForeignKey(r => r.StaffId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reports>()
                .HasMany(r => r.Calls)
                .WithMany(c => c.Reports)
                .Map(r => r.ToTable("ReportCalls"))
                .Map(r => r.MapLeftKey("ReportsId")
                .MapRightKey("CallId"));

            // Overide on Many to Many cascade convention to ensure delete does not cascade
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
                




            base.OnModelCreating(modelBuilder);
        }


    }
}
