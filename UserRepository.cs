
namespace przypomnienie
{
    public class UserRepository
    {
        private Dictionary<string, Users> _users = new();

        // zapisane jako (id,uzytkownik)


        public void AddUser(string _id, Users _user)
        {
            if (_user is not null && !string.IsNullOrEmpty(_id))
            {
                this._users.TryAdd(_id, _user);
            }
        }
        public int checkCount()
        {
            return this._users.Count;
        }
        public List<string> GetAllids()
        {
            return _users.Keys.ToList();
        }
        public Dictionary<string,Users> initialUsers()
        {
            var initialUsers  = _users.OrderBy(x => x.Value.userBalance).Skip(_users.Count / 2).Take(3).ToDictionary();
            return initialUsers;

        }
        public Users getUserById(string _id)
        {
            var check = _users.TryGetValue(_id, out Users user);
            if (check)
            {
                return user;
            }
            return null;
        }
    }
}
