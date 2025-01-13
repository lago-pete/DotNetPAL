using DotnetAPI.Models;



namespace DotnetAPI.Data
{
    public class UserRepository : IUserRepository
    {
        DataContextEF _entityFrameWork;
        public UserRepository(IConfiguration config)
        {
            _entityFrameWork = new DataContextEF(config);
        }

        public bool SaveChanges()
        {
            return _entityFrameWork.SaveChanges() > 0;
        }


        public void AddEntity<T>(T entityToAdd)
        {
            if (entityToAdd != null)
            {
                _entityFrameWork.Add(entityToAdd);
            }
        }

        public void RemoveEntity<T>(T entityToRemove)
        {
            if (entityToRemove != null)
            {
                _entityFrameWork.Remove(entityToRemove);
            }
        }



        ////User Functions//////

        public IEnumerable<User> GetUser()
        {
            IEnumerable<User> users = _entityFrameWork.Users.ToList<User>();
            return users;
        }

        public User GetSingleUser(int userId)
        {
            User? user = _entityFrameWork.Users.Where(u => u.UserId == userId).FirstOrDefault<User>();
            if (user != null)
            {
                return user;
            }
            throw new Exception("Failed to Get User");
        }

        ////User Functions//////


        ////Job Functions//////

        public IEnumerable<UserJobInfo> GetAllUserJobs()
        {
            IEnumerable<UserJobInfo> users = _entityFrameWork.UsersJobInfo.ToList<UserJobInfo>();
            return users;
        }


        public UserJobInfo GetUsersJob(int userId)
        {
            UserJobInfo? user = _entityFrameWork.UsersJobInfo.Where(u => u.UserId == userId).FirstOrDefault<UserJobInfo>();

            if (user != null)
            {
                return user;
            }

            throw new Exception("Failed to Get User");

        }



        ////Job Functions//////

        ////Salary Functions/////

        public IEnumerable<UserSalary> GetAllUserSalaries()
        {
            IEnumerable<UserSalary> users = _entityFrameWork.UsersSalary.ToList<UserSalary>();
            return users;
        }


        public UserSalary GetUserSalary(int userId)
        {
            UserSalary? user = _entityFrameWork.UsersSalary.Where(u => u.UserId == userId).FirstOrDefault<UserSalary>();

            if (user != null)
            {
                return user;
            }

            throw new Exception("Failed to Get User");
        }


        ////Salary Functions/////

        ///Auth Functions//////
        
  




        ///Auth Functions//////







    }





}










