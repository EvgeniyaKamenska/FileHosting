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
    public class File
    {
        int fileId;
        string fileName;
        string description;
        long size;
        string password;
        string url;
        DateTime dateAdd;
        //DateTime dateModif;
        int userId;
        byte[] bytes;

        [DataMember]
        public int FileId
        {
            get { return fileId; }
            set { fileId = value; }
        }
        [DataMember]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        [DataMember]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        [DataMember]
        public long Size
        {
            get { return size; }
            set { size = value; }
        }
        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        [DataMember]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        [DataMember]
        public DateTime DateAdd
        {
            get { return dateAdd; }
            set { dateAdd = value; }
        }
        [DataMember]
        public byte[] Bytes
        {
            get { return bytes; }
            set { bytes = value; }
        }
        //[DataMember]
        //public DateTime DateModif
        //{
        //    get { return dateModif; }
        //    set { dateModif = value; }
        //}


        [DataMember]
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public static explicit operator FileEntity(File f)
        {
            FileEntity file = new FileEntity();
            file.FileName = f.FileName;
            file.FileId = f.FileId;
            file.Description = f.Description;
            file.Size = f.Size;
            file.Url = f.Url;
            file.Password = f.Password;
            file.DateAdd = f.DateAdd;
            file.UserId = f.UserId;

            return file;
        }

        public static explicit operator File(FileEntity f)
        {
            File file = new File();
            file.UserId = f.UserId;
            file.FileId = f.FileId;
            file.FileName = f.FileName;
            file.Description = f.Description;
            file.Size = f.Size;
            file.Url = f.Url;
            file.Password = f.Password;
            file.DateAdd = f.DateAdd;

            return file;
        }
    }
}
