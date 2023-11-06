using InterfaceComposition.DomainEntities.Core;
using InterfaceComposition.DomainEntities.KnowledgeEngine.Contact;

namespace InterfaceComposition.DomainEntities.UserManagement
{
    public class UserAccount : BaseEntityWithGuid
    {
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string FullName => $"{FirstName} {LastName}".Trim();
        public string? AvatarUrl { get; private set; }
        public EmailAddress Email { get; private set; }

        public UserAccount(
            string email,
            string? firstName,
            string? lastName,
            string? avatarUrl) :
            base()
        {
            Email = new EmailAddress(email);
            FirstName = firstName;
            LastName = lastName;
            AvatarUrl = avatarUrl;
        }
    }
}
