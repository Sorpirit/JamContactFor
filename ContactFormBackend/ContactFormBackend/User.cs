using System.ComponentModel.DataAnnotations;

namespace ContactFormBackend
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        protected bool Equals(User other)
        {
            return Email == other.Email;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User)obj);
        }

        public override int GetHashCode()
        {
            return (Email != null ? Email.GetHashCode() : 0);
        }
    }
}