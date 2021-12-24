using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim101.Entity
{
    [Table("tblNotlarimUsers")]
    public class NotlarimUser : MyEntityBase
    {
        [DisplayName("Ad") ,StringLength(30, ErrorMessage = "{0}) alani max {1} karakter olmalıdır."), Required]
        public string Name { get; set; }
        [DisplayName("Soyad"), StringLength(30, ErrorMessage = "{0}) alani max {1} karakter olmalıdır."), Required]
        public string Surname { get; set; }
        [DisplayName("Kullamıcı Adı") ,StringLength(30, ErrorMessage = "{0}) alani max {1} karakter olmalıdır."), Required]
        public string Username { get; set; }
        [DisplayName("E-mail"), StringLength(100, ErrorMessage = "{0}) alani max {1} karakter olmalıdır."), Required]
        public string Email { get; set; }
        [DisplayName("Şifre"),StringLength(100, ErrorMessage = "{0}) alani max {1} karakter olmalıdır."), Required]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public Guid ActivateGuid { get; set; }
        public bool IsAdmin { get; set; }
        [StringLength(30)]
        public string ProfileImageFileName { get; set; }

        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }

    }
}
