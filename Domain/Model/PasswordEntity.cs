using System;

namespace Domain.Model
{
    public class PasswordEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public DateTime DataCreated { get; set; }
    }
}
