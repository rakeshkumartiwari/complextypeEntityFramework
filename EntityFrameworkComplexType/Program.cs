using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkComplexType
{
    class Program
    {
        static void Main(string[] args)
        {
            Seed();
            Read();
            Console.Read();
        }
        static void Seed()
        {
            var db = new SchoolDb();
            var student = new Student
            {
                Name = "Rajnish",
                Address = new Address { Line1 = "Hyderabad", Line2 = "Ameerpeth" },
                Subjects = new List<Subject>() { new Subject { Name = "Javascript" }, new Subject { Name = "HTML" }, },
                Gender = new Gender { GenderType="Male" }

            };
            db.Students.Add(student);
            db.SaveChanges();
        }
        static void Read()
        {
            var db = new SchoolDb();
            var StudentList = db.Students.Include(s => s.Address).Include(s => s.Subjects).ToList();
            foreach (var student in StudentList)
            {
                Console.WriteLine(student.Name);
                Console.WriteLine(student.Address.Line1 + " " + student.Address.Line2);
                foreach (var subject in student.Subjects)
                {
                    Console.WriteLine(subject.Name);
                }
                Console.WriteLine(student.Gender.GenderType);
            }
        }
    }
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public List<Subject> Subjects { get; set; }
        public Gender Gender { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
    }
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Gender 
    {
        public string GenderType { get; set; }
    
    }
    public class SchoolDb : DbContext
    {
        public SchoolDb()
            : base()
        {



            Database.SetInitializer<SchoolDb>(new DropCreateDatabaseAlways<SchoolDb>());
        }
        public IDbSet<Student> Students { get; set; }
        public IDbSet<Address> Addresses { get; set; }
       
    }
}
