using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Entity;
using FileHostingModel;
using System.IO;

namespace FileHostingService
{
    public class FileHostingService : IFileHostingService
    {
        public User RegistrNewUser(User newUser)
        {
            FileHostingModelContainer context = new FileHostingModelContainer();

            UserEntity userFined = context.UserEntitySet.Include(u => u.FileEntity)
                .Where(u => u.Login == newUser.Login).FirstOrDefault();

            if (userFined != null)
                return null;

            UserEntity user = (UserEntity)newUser;

            context.UserEntitySet.Add(user);
            context.SaveChanges();
            return (User)user;
        }

        public User CheckEnterUserPassword(string userName, string password)
        {
            FileHostingModelContainer context = new FileHostingModelContainer();
             UserEntity user = context.UserEntitySet.Include(u => u.FileEntity)
                .Where(u => u.Login == userName && u.Password == password).FirstOrDefault();

             return user == null ? null : (User)user;
        }

        public void UpdateFileInfo(File editFile)
        {
            FileHostingModelContainer context = new FileHostingModelContainer();
            FileEntity fileE = (FileEntity)editFile;

            context.FileEntitySet.Attach(fileE);
            context.Entry<FileEntity>(fileE).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteFile(int fileId, string fileName, int userId)
        {
            FileHostingModelContainer context = new FileHostingModelContainer();
            FileEntity file = new FileEntity
            {
                FileId = fileId
            };

            context.FileEntitySet.Attach(file);
            context.FileEntitySet.Remove(file);
            context.SaveChanges();

            string appDataPath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
            string pathDirectory = Path.Combine(appDataPath, "FileHostingDirectory");
            string path = Path.Combine(pathDirectory, userId.ToString());
            string url = Path.Combine(path, fileName);
            System.IO.File.Delete(url);
        }

        public File AddNewFile(File newFile)
        {
            string appDataPath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
            string pathDirectory = Path.Combine(appDataPath, "FileHostingDirectory");

            if (!Directory.Exists(pathDirectory))
                Directory.CreateDirectory(pathDirectory);

            string path = Path.Combine(pathDirectory, newFile.UserId.ToString());

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string url = Path.Combine(path, newFile.FileName);
            using (FileStream fileStream = new FileStream(url, FileMode.Create, FileAccess.Write))
            {
                fileStream.Write(newFile.Bytes, 0, newFile.Bytes.Length);
            }

            newFile.Url = url;
            FileHostingModelContainer context = new FileHostingModelContainer();
            FileEntity file = (FileEntity)newFile;

            context.FileEntitySet.Add(file);
            context.SaveChanges();
            return (File)file;
        }

        public byte[] DownloadFile(string fileName, int userId)
        {
            string appDataPath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
            string pathDirectory = Path.Combine(appDataPath, "FileHostingDirectory");
            string path = Path.Combine(pathDirectory, userId.ToString());
            string url = Path.Combine(path, fileName);
            return System.IO.File.ReadAllBytes(url);
        }

    }
}
