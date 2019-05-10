using ITIndeed.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITIndeed.BL
{
     public class User
    {
        public Guid BaseUserID { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }


        public User()
        {

        }

        public User(Guid baseUserID, string userName, string password)
        {
            BaseUserID = baseUserID;
            UserName = userName;
            Password = password;
        }

        public User(Guid baseUserID, string userName)
        {
            BaseUserID = baseUserID;
            UserName = userName;
        }

        public bool UserLoadById(Guid baseUserId)
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblUser user = dc.tblUsers.Where(u => u.Id == baseUserId).FirstOrDefault();

                    if (user != null)
                    {
                        this.BaseUserID = user.Id;
                        this.UserName = user.UserName;
                        this.Password = user.Password;

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetHash()
        {
            using (var hash = new System.Security.Cryptography.SHA1Managed())
            {
                var hashbytes = System.Text.Encoding.UTF8.GetBytes(this.Password);
                return Convert.ToBase64String(hash.ComputeHash(hashbytes));
            }
        }

        public bool UserLogin()
        {
            try
            {
                if (UserName != null && UserName != string.Empty)
                {
                    if (Password != null && Password != string.Empty)
                    {
                        ITIndeedEntities dc = new ITIndeedEntities();
                        tblUser user = dc.tblUsers.FirstOrDefault(u => u.UserName == this.UserName);
                        if (user != null)
                        {
                            if (user.Password == this.GetHash()) // Checks Password Correct
                            {
                                // Successful login
                                BaseUserID = user.Id;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UserLoadByUserName(string username)
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblUser user = dc.tblUsers.Where(u => u.UserName == username).FirstOrDefault();

                    if (user != null)
                    {
                        this.BaseUserID = user.Id;
                        this.UserName = user.UserName;

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UserInsert()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    if (dc.tblUsers.Where(u => u.UserName == this.UserName).FirstOrDefault() == null)
                    {
                        tblUser user = new tblUser();
                        user.Id = Guid.NewGuid();                         
                        user.UserName = this.UserName;
                        user.Password = GetHash();

                        dc.tblUsers.Add(user);
                        dc.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UserUpdate()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblUser user = dc.tblUsers.Where(u => u.Id == this.BaseUserID).FirstOrDefault();

                    if (user != null)
                    {
                        user.UserName = this.UserName;
                        user.Password = GetHash();

                        dc.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UserDelete()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblUser user = dc.tblUsers.Where(u => u.Id == this.BaseUserID).FirstOrDefault();

                    if (user != null)
                    {
                        dc.tblUsers.Remove(user);

                        dc.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class UserList : List<User>
    {
        public void UserListLoad()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    dc.tblUsers.ToList().ForEach(u => Add(new User(u.Id, u.UserName, u.Password)));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
