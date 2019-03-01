using System;
using System.Collections.Generic;
using System.Text;

namespace ITIndeed.BL
{
    public class User
    {
        // Properties
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        // Constructors
        public User()
        {

        }

        public User(int id, string userName, string password)
        {
            Id = id;
            UserName = userName;
            Password = password;
        }

        // Methods

        public void LoadById(int id)
        {
            try
            {
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Insert()
        {
            try
            {

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

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
