using Beymen.ECommerce.Common.Domain.SeedWorks;
using MediatR;
using MicroMarinCase.Domain.AggregateRootModels.RecordModels;
using MicroMarinCase.Domain.SeedWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MicroMarinCase.Infrastructure.Persistence.Context
{
    public class DynamicDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        public DynamicDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }
        public DbSet<Record> Records { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>().HasKey(x => x.Id);
            modelBuilder.Entity<Record>().HasIndex(x => x.Id).IsUnique();

            modelBuilder.Entity<Record>().Property(x => x.ParentId).IsRequired(false);
            modelBuilder.Entity<Record>().Property(x => x.RecordType).IsRequired(true);

            modelBuilder.Entity<Record>().HasOne(x => x.Parent).WithMany(x => x.Childs).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.HasDbFunction(() => DbFunctionExtensions.JsonValue(default(string), default(string))).HasName("JSON_VALUE");

            base.OnModelCreating(modelBuilder);
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var domainEntities = this.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(x => x.Entity._domainEvents != null && x.Entity._domainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity._domainEvents)
                .ToList();

            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent);


            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
