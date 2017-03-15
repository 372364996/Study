using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Study.MVC.Models
{
    public class Books
    {
      public  int Id { get; set; }
        [Display(Name = "书名")]
        [Required]
        public string BookName { get; set; }
        public int BookPrice { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public int C{ get; set; }
        public int D { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class Person
    {
        public int Id { get; set; }
        [Display(Name ="姓名")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "年龄")]
        [Required]
        public int Age { get; set; }

    }
    public class BookDBContext: DbContext
    {

        public DbSet<Books> Books { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}