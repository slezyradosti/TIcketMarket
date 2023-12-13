using Domain.Models.Catalogues;
using Domain.Models.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.EFInitial
{
    public class DataContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<EventCategory> EventCategory { get; set; }
        public DbSet<EventTable> EventTable { get; set; }
        public DbSet<EventType> EventType { get; set; }
        public DbSet<TicketType> TicketType { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Ticket> Ticket { get; set; }

        public DbSet<TicketOrder> TicketOrder { get; set; }
        public DbSet<TableEvent> TableEvent { get; set; }
        public DbSet<UserEvent> UserEvent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
    
            base.OnModelCreating(modelBuilder);
        }
    }
}
