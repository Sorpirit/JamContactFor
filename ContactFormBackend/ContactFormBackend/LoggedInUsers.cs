using System.Collections.Generic;

namespace ContactFormBackend
{
    public class LoggedInUsers
    {
        private readonly HashSet<User> _registeredUsers = new();

        public HashSet<User> RegisteredUsers => _registeredUsers;
    }
}