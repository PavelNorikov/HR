using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class vacancy
    {
        public int Id;
        public string Title;
    }

    public class applicant
    {
        public int Id;
        public string Name;
        public int Exp;
        public Decimal Salary;
        public vacancy Vacancy;
    }

    public class Database
    {
        public List<vacancy> vacancies;
        public List<applicant> applicants;

        public Database()
        {
            vacancies = new List<vacancy>
            {
                new vacancy {Id = 1, Title = "Программист"},
                new vacancy {Id = 2, Title = "Менеджер"},
                new vacancy {Id = 3, Title = "Консультант"}
            };

            applicants = new List<applicant>
            {
                new applicant {Id = 1, Name = "Разуваев Александр Юринович", Exp = 22, Salary = 38000, Vacancy = vacancies[0]},
                new applicant {Id = 2, Name = "Низовцева Галина Марковна", Exp = 17, Salary = 45000, Vacancy = vacancies[1]},
                new applicant {Id = 3, Name = "Кочетов Марк Тимофеевич", Exp = 3, Salary = 68000, Vacancy = vacancies[2]},
                new applicant {Id = 4, Name = "Ефимова Вера Егоровна", Exp = 5, Salary = 63000, Vacancy = vacancies[0]},
                new applicant {Id = 5, Name = "Широнина Рада Алексеевна", Exp = 11, Salary = 47000, Vacancy = vacancies[1]},
                new applicant {Id = 6, Name = "Бузыцкова Лана Марковна", Exp = 21, Salary = 88000, Vacancy = vacancies[2]},
                new applicant {Id = 7, Name = "Караваев Евгений Юлианович", Exp = 1, Salary = 29000, Vacancy = vacancies[0]},
                new applicant {Id = 8, Name = "Грачева Алла Феоктистовна", Exp = 18, Salary = 59000, Vacancy = vacancies[1]},
                new applicant {Id = 9, Name = "Горбунов Герасим Венедиктович", Exp = 9, Salary = 100000, Vacancy = vacancies[2]},
                new applicant {Id = 10, Name = "Красильников Емельян Георгиевич", Exp = 16, Salary = 75000, Vacancy = vacancies[0]},
            };

        
        }
        

    }




}
