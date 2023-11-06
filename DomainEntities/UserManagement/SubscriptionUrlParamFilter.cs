using InterfaceComposition.DataFilters;
using InterfaceComposition.REST.UrlParameters;
using System;

namespace InterfaceComposition.DomainEntities.UserManagement
{
    public class SubscriptionUrlParamFilter : IEntityUrlParamFilter
    {
        public Guid UserAccountId { get; set; }

        public SubscriptionUrlParamFilter(Guid userAccountId)
        {
            UserAccountId = userAccountId;
        }

        public bool IsEmpty() => false;

        public UrlParameterList ToUrlParameters() => new (nameof(UserAccountId), UserAccountId);
    }
}
