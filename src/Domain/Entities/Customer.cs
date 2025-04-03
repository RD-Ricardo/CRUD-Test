using CrossCutting.DomainObjects;
using Domain.Enums;

namespace Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Document { get; private set; }
        public DocumentTypeEnum DocumentType { get; private set; }
        public ICollection<Address> Addresses { get; set; } = null!;
        public Customer(string name, string email, string document, DocumentTypeEnum documentType)
        {
            Name = name;
            Email = email;
            Document = document;
            DocumentType = documentType;
        }

        public void Update(string name, string email)
        {
            Name = name;
            Email = email;
            UpdateAt();
        }
    }
}
