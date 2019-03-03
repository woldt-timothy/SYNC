﻿using ITIndeed.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIndeed.BL
{
    public class User
    {
        // Properties


        /// <summary>
        /// I CHANGED ALL OF THE IDs to be GUIDs - taw - 03022019
        /// </summary>

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        // Constructors

        public User()
        {

        }

        public User(Guid id, string userName, string password)
        {
            Id = id;
            UserName = userName;
            Password = password;
        }

        // Methods
        public bool LoadById(Guid id)
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblUser user = dc.tblUsers.Where(u => u.Id == id).FirstOrDefault();

                    if (user != null)
                    {
                        this.Id = user.Id;
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

        public bool Login()
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
                            if (user.Password == this.GetHash()) // Checked if password is correct
                            {
                                // Successful login
                                Id = user.Id;
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







        public bool LoadByUserName(string username)
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblUser user = dc.tblUsers.Where(u => u.UserName == username).FirstOrDefault();

                    if (user != null)
                    {
                        this.Id = user.Id;
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
        public bool Insert()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {

                    ///CHECKS TO SEE IF THERE IS A USER IN THE DATABASE IF NOT THEN INSERTS A NEW USER WITH A GUID AS THE Id
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
        public void Update()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblUser user = dc.tblUsers.Where(u => u.Id == this.Id).FirstOrDefault();

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
        public void Delete()
        {
            try
            {
                using (ITIndeedEntities dc = new ITIndeedEntities())
                {
                    tblUser user = dc.tblUsers.Where(u => u.Id == this.Id).FirstOrDefault();

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
        public void Load()
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
