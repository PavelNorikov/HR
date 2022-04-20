using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1.EF
{

    internal class MyCont : DbContext
    {
        public DbSet<cont_vacancy> cont_vacancies {get; set;}
        public DbSet<cont_applicant> cont_applicants { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = WIN-AV0EFDDEH5B; Database = Test_HR; Trusted_Connection = True;");
            base.OnConfiguring(optionsBuilder); 
        }

        public void CreateDbIfNotExist()
        {
            this.Database.EnsureCreated();
        }

        public void DropDB()
        {
            this.Database.EnsureDeleted();
        }

        public class cont_vacancy
        {
            public int Id { get; set; }
            public string? Title { get; set; }
        }

        public class cont_applicant
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int Exp { get; set; }
            public Decimal Salary { get; set; }
            public cont_vacancy Vacancy { get; set; }
        }





        public void AddVacancies()
        {

            cont_vacancies.AddRange(entities: new cont_vacancy[]
            {
                new cont_vacancy { Title = "Программист" },
                new cont_vacancy { Title = "Менеджер" },
                new cont_vacancy { Title = "Консультант" }
            });
        }


        public void AddApplicants()
        {

            cont_applicants.AddRange(entities: new cont_applicant[]
            {
                new cont_applicant { Name = "Разуваев Александр Юринович", Exp = 22, Salary = 38000, Vacancy = cont_vacancies.First(t => t.Id==1)},
                new cont_applicant { Name = "Низовцева Галина Марковна", Exp = 17, Salary = 45000, Vacancy = cont_vacancies.First(t => t.Id==2)},
                new cont_applicant { Name = "Кочетов Марк Тимофеевич", Exp = 3, Salary = 68000, Vacancy = cont_vacancies.First(t => t.Id==3)},
                new cont_applicant { Name = "Ефимова Вера Егоровна", Exp = 5, Salary = 63000, Vacancy = cont_vacancies.First(t => t.Id==1)},
                new cont_applicant { Name = "Широнина Рада Алексеевна", Exp = 11, Salary = 47000, Vacancy = cont_vacancies.First(t => t.Id==2)},
                new cont_applicant { Name = "Бузыцкова Лана Марковна", Exp = 21, Salary = 88000, Vacancy = cont_vacancies.First(t => t.Id==3)},
                new cont_applicant { Name = "Караваев Евгений Юлианович", Exp = 1, Salary = 29000, Vacancy = cont_vacancies.First(t => t.Id==1)},
                new cont_applicant { Name = "Грачева Алла Феоктистовна", Exp = 18, Salary = 59000, Vacancy = cont_vacancies.First(t => t.Id==2)},
                new cont_applicant { Name = "Горбунов Герасим Венедиктович", Exp = 9, Salary = 100000, Vacancy = cont_vacancies.First(t => t.Id==3)},
                new cont_applicant { Name = "Красильников Емельян Георгиевич", Exp = 16, Salary = 75000, Vacancy = cont_vacancies.First(t => t.Id==1)}
            });
        }






    }
}
