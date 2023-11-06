using InterfaceComposition.DomainEntities.Core;
using InterfaceComposition.Utils;
using System.Net.Mail;

namespace InterfaceComposition.DomainEntities.KnowledgeEngine.Contact
{
    public class EmailAddress : BaseEntityWithId<string>, IEquatable<EmailAddress>
    {
        public string FullAddress { get; private set; }
        public string Username { get; private set; }
        public string DomainName { get; private set; }

        public EmailAddress(string fullAddress) : base(fullAddress)
        {
            if (!IsValidEmailAddress(fullAddress)) throw new Exception($"{fullAddress} is not a valid email address");
            FullAddress = fullAddress.ToLowerInvariant();
            var addressComponents = FullAddress.Split('@');
            Username = addressComponents[0];
            DomainName = addressComponents[1];
        }

        public static bool IsValidEmailAddress(string emailAddress)
        {
            try
            {
                var address = new MailAddress(emailAddress);
                return address.Address == emailAddress;
            }
            catch
            {
                return false;
            }
        }

        public bool Equals(EmailAddress? other)
        {
            return other != null && IsSameEmailAddress(other.FullAddress);
        }

        public bool IsSameEmailAddress(string emailAddress)
        {
            return FullAddress.EqualsIgnoreCase(emailAddress);
        }
    }
}
