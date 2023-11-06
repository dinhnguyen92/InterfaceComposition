using InterfaceComposition.DataFilters;
using InterfaceComposition.REST.UrlParameters;

namespace InterfaceComposition.UserManagement
{
    public class UserAccountUrlParamFilter : IEntityUrlParamFilter
    {
        public int? RoleId { get; set; }
        public string? Email { get; set; }

        private UserAccountUrlParamFilter(
            int? roleId,
            string? email)
        {
            RoleId = roleId;
            Email = email;
        }

        public static UserAccountUrlParamFilter ByRoleId(int roleId) => new(roleId, null);

        public static UserAccountUrlParamFilter ByEmailAddress(string emailAddress) => new(null, emailAddress);

        public bool IsEmpty() => string.IsNullOrEmpty(Email) && !RoleId.HasValue;

        public UrlParameterList ToUrlParameters()
        {
            var parameters = new UrlParameterList();
            if (RoleId != null)
            {
                parameters.Add(new UrlParameter(nameof(RoleId), RoleId));
            }
            if (!string.IsNullOrEmpty(Email))
            {
                parameters.Add(new UrlParameter(nameof(Email), Email));
            }
            return parameters;
        }
    }
}
