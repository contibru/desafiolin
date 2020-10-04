using DesafioLin.Domain.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DesafioLin.Domain.Entities

{
    public class User : IEntityBase
    {
        public User()
        {
        }

        [Key]
        public virtual int Id { get; set; }

        public virtual string Login { get; set; }
        public virtual string Password { get; set; }
        public virtual ICollection<Authorization> authorizations { get; set; }
    }
}