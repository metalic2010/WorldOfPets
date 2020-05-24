using System;
using System.ComponentModel.DataAnnotations;

namespace WorldOfPets.Models
{
    public class InfoUsers
    {
        [Key]
        public string Login { get; set; }
        public Byte STATUS { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public Int64 Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        //public DateTime DateOfCreation { get; set; }
        //public DateTime DateOfChange { get; set; }
        public string Timezone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        //public int Enabled { get; set; }
    }

    public class InfoAuth
    {
        [Key]
        public string Login { get; set; }
        public string RoleName { get; set; }
    }

    public class InfoReg
    {
        [Key]
        public string ResultRequest { get; set; }
    }
}
