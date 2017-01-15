using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.Serialization;
using System.ServiceModel;
using FileHostingModel;

namespace FileHostingService
{
    [DataContract]
    public class User
    {
        int userId;
        //string firstName;
        //string lastName;
        string email;
        string login;
        string password;
        DateTime dateBeg;
        //DateTime dateEnd;
        List<File> files= new List<File>() ;

        [DataMember]
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        //[DataMember]
        //public string FirstName
        //{
        //    get { return firstName; }
        //    set { firstName = value; }
        //}
        //[DataMember]
        //public string LastName
        //{
        //    get { return lastName; }
        //    set { lastName = value; }
        //}
        [DataMember]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        [DataMember]
        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        [DataMember]
        public System.DateTime DateBeg
        {
            get { return dateBeg; }
            set { dateBeg = value; }
        }
        //[DataMember]
        //public System.DateTime DateEnd
        //{
        //    get { return dateEnd; }
        //    set { dateEnd = value; }
        //}

        [DataMember]
        public List<File> Files
        {
            get { return files; }
            set { files = value; }
        }

        public static explicit operator UserEntity(User u)
        {
            UserEntity user = new UserEntity();
            user.Email = u.Email;
            user.UserId = u.UserId;
            user.Login = u.Login;
            user.Password = u.Password;
            user.DateBeg = DateTime.Now;

            return user;
        }

        public static explicit operator User(UserEntity u)
        {
            User user = new User();
            user.UserId = u.UserId;
            user.Email = u.Email;
            user.Login = u.Login;
            user.Password = u.Password;
            user.DateBeg = u.DateBeg;
            user.Files = new List<File>();

            if (u.FileEntity != null)
            {
                foreach (FileEntity fileE in u.FileEntity)
                {
                    File file = (File)fileE;
                    user.Files.Add(file);
                }
            }
            return user;
        }
    }
}
