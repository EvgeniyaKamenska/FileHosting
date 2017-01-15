using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Entity;

namespace FileHostingService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IFileHostingService
    {
        [OperationContract]
        User RegistrNewUser(User newUser);
        [OperationContract]
        User CheckEnterUserPassword(string userName, string password);
        [OperationContract]
        void UpdateFileInfo(File editFile);
        [OperationContract]
        void DeleteFile(Int32 fileId, string fileName, int userId);
        [OperationContract]
        File AddNewFile(File newFile);
        [OperationContract]
        byte[] DownloadFile(string fileName, int userId);
    }

}
