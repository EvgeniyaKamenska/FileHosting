using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileHostingModel
{
    public partial class FileEntity
    {
        [Key]
        public int FileId { get; set; }
        [Required]
        public string FileName { get; set; }
        public string Description { get; set; }
        [Required]
        public long Size { get; set; }
        [Required]
        public System.DateTime DateAdd { get; set; }
        public string Url { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }

        public virtual UserEntity UserEntity { get; set; }
    }
}
