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

        ///All User Functions//////

        public IEnumerable<UserComplete> GetFULLUser()
        {
            var users = _entityFrameWork.Users.ToList();
            var jobInfos = _entityFrameWork.UsersJobInfo.ToList();
            var salaries = _entityFrameWork.UsersSalary.ToList();

            IEnumerable<UserComplete> FULLusers = users.Select(user => new UserComplete
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
                Active = user.Active,
                JobTitle = jobInfos.FirstOrDefault(job => job.UserId == user.UserId)?.JobTitle,
                Department = jobInfos.FirstOrDefault(job => job.UserId == user.UserId)?.Department,
                Salary = salaries.FirstOrDefault(salary => salary.UserId == user.UserId)?.Salary ?? 0
            }).ToList();
            return FULLusers;
        }

        public UserComplete GetSingleFULLUser(int userId)
        {
            
            var user = _entityFrameWork.Users.FirstOrDefault(u => u.UserId == userId);
            var jobInfo = _entityFrameWork.UsersJobInfo.FirstOrDefault(j => j.UserId == userId);
            var salary = _entityFrameWork.UsersSalary.FirstOrDefault(s => s.UserId == userId);

           
            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found.");
            }

            // Map the fetched data to the UserComplete model
            var userComplete = new UserComplete
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
                Active = user.Active,
                JobTitle = jobInfo?.JobTitle,          // Use null propagation to avoid errors
                Department = jobInfo?.Department,     // Use null propagation to avoid errors
                Salary = salary?.Salary ?? 0          // Use null coalescing for default value
            };

            return userComplete;
        }





        ///All User Functions//////







    }





}










