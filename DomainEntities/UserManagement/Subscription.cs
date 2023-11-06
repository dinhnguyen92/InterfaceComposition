using InterfaceComposition.DomainEntities.Core;
using System;

namespace InterfaceComposition.DomainEntities.UserManagement
{
    public class Subscription : BaseEntityWithGuid
    {
        public Guid UserAccountId { get; }
        public float MonthlyRate { get; }

        public Subscription(
            Guid userAccountId,
            float monthlyRate) :
            base()
        {
            UserAccountId = userAccountId;
            MonthlyRate = monthlyRate;
        }
    }
}
