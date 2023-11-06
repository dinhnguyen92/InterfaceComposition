using System;

namespace InterfaceComposition.DomainEntities.Core
{
    public abstract class BaseEntityWithGuid : BaseEntityWithId<Guid>
    {
        protected BaseEntityWithGuid() : base(Guid.NewGuid()) { }

        protected BaseEntityWithGuid(Guid id) : base(id) { }
    }
}
