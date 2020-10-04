using DesafioLin.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DesafioLin.Domain.Entities

{
    public class Authorization : IEntityBase
    {
        public Authorization()
        {
        }

        [Key]
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
        public virtual bool Value { get; set; }

        public virtual Usuario usuario { get; set; }
    }
}