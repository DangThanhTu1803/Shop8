using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model.EF;
using PagedList;

namespace Model.Dao
{
    public class UserDao
    {
        Shop8DbContext db = null;
        public UserDao()
        {
            db = new Shop8DbContext();
        }
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.GroupID = entity.GroupID;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                user.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Username.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.Username == userName);
        }
        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }
        public int Login(string username, string password, bool isLoginAdmin = false)
        {
            var result = db.Users.SingleOrDefault(x => x.Username == username);
            if (result == null)
            {
                return 0; 
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.GroupID == CommonConstants.ADMIN_GROUP )
                    {
                        if (!result.Status)
                        {
                            return -1; 
                        }
                        else
                        {
                            if (result.Password == password)
                            {
                                return 1; 
                            }
                            else
                            {
                                return -2; 
                            }
                        }
                    }
                    else
                    {
                        return -3; 
                    }
                }
                else
                {
                    if (!result.Status)
                    {
                        return -1; 
                    }
                    else
                    {
                        if (result.Password == password)
                        {
                            return 1; 
                        }
                        else
                        {
                            return -2;
                        }
                    }
                }
            }
        }
        public bool Delete(long id)
        {
            try 
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            } 
            catch(Exception e) 
            {
                return false;
            }
        }
        public bool ChangeStatus(long id)
        {
            var Entity = db.Users.Find(id);
            Entity.Status = !Entity.Status;
            db.SaveChanges();

            return Entity.Status;
        }
        public bool CheckUsername(string userName)
        {
            return db.Users.Count(x => x.Username == userName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
    }
}
