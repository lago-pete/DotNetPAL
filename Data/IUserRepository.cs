using DotnetAPI.Models;


namespace DotnetAPI.Data
{
    public interface IUserRepository
    {
        public bool SaveChanges();
       
        public void AddEntity<T>(T entityToAdd);

        public void RemoveEntity<T>(T entityToRemove);
       
        public IEnumerable<User> GetUser();

        public User GetSingleUser(int userId);

        public IEnumerable<UserJobInfo> GetAllUserJobs();
        public UserJobInfo GetUsersJob(int userId);

        public IEnumerable<UserSalary> GetAllUserSalaries();

        public UserSalary GetUserSalary(int userId);

        public IEnumerable<UserComplete> GetFULLUser();

        public UserComplete GetSingleFULLUser(int userId);

    }
}