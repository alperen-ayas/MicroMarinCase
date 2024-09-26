using MediatR;

namespace MicroMarinCase.Domain.SeedWorks
{
    public abstract class AggregateRoot : Entity
    {
        public List<INotification> _domainEvents;


        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }
    }
}
