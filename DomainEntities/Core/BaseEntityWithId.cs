namespace InterfaceComposition.DomainEntities.Core
{
    public abstract class BaseEntityWithId<TEntityId> :
        IEquatable<BaseEntityWithId<TEntityId>>
        where TEntityId : notnull
    {
        public TEntityId Id { get; }

        public Type IdType => typeof(TEntityId);

        protected BaseEntityWithId(TEntityId id)
        {
            Id = id;
        }

        public bool Equals(BaseEntityWithId<TEntityId>? other)
        {
            return other != null && other.Id.Equals(Id);
        }
    }
}
