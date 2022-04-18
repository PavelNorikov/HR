CREATE TABLE [applicants] (
  ID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
  [Name] nvarchar(60),
  [Exp] int,
  [Salary] money,
  [Vacancy] int
)
GO

CREATE TABLE [vacancies] (
  ID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
  [Title] nvarchar(30)
  )
  GO

  ALTER TABLE [applicants] ADD FOREIGN KEY ([Vacancy]) REFERENCES [vacancies] (ID)
GO


INSERT INTO [vacancies]
       VALUES ('Программист'), ('Менеджер'), ('Консультант');


delete from applicants
INSERT INTO [applicants]
       VALUES
            ('Разуваев Александр Юринович', 22, 38000, 1),
			('Низовцева Галина Марковна', 17, 45000, 2),
			('Кочетов Марк Тимофеевич', 3, 68000, 3),
			('Ефимова Вера Егоровна', 5, 63000, 1),
			('Широнина Рада Алексеевна', 11, 47000, 2),
			('Бузыцкова Лана Марковна', 21, 88000, 3),
			('Караваев Евгений Юлианович', 1, 29000, 1),
			('Грачева Алла Феоктистовна', 18, 59000, 2),
			('Горбунов Герасим Венедиктович', 9, 100000, 3),
			('Красильников Емельян Георгиевич', 16, 75000, 1);


  --Вывести список соискателей, отсортированных по названию вакансии, внутри вакансии по зарплате.
select applicants.Name, vacancies.Title, applicants.Salary , Exp from applicants, vacancies where applicants.Vacancy = vacancies.ID order by Vacancy, Salary
  --Вывести первых двух наиболее привлекательных соискателей программистов, (привлекательность считать как стаж, делённый на зарплатные ожидания)
select TOP (2) applicants.Name from applicants where applicants.Vacancy = 1 order by [Exp]/Salary DESC
  --Вывести список соискателей, с зарплатными ожиданиями “выше среднего по базе”, вместе с их специальностями.
select applicants.Name, vacancies.Title, applicants.Salary from applicants, vacancies where applicants.Vacancy = vacancies.ID and applicants.Salary > (select AVG(Salary) from applicants)
  --Вывести пары: {специальность, среднее зарплатное ожидание}, отсортировать от более высокооплачиваемых специальностей к менее.
select distinct vacancies.Title, (select AVG(Salary) from applicants where applicants.Vacancy = vacancies.ID) AS avgSalary from applicants INNER join vacancies on applicants.Vacancy = vacancies.ID order by avgSalary DESC
  --Взять кандидатов с наименьшем стажем в каждой из специальностей – удалить из базы.
select applicants.Name from applicants, vacancies where applicants.Vacancy = vacancies.ID and Exp = (select MIN(Exp) from applicants where applicants.Vacancy = vacancies.ID)
  --Найти кандидата с максимальным ожиданием зарплаты, уменьшить сумму на 15%.
select applicants.ID from applicants where Salary = (select MAX(Salary) from applicants)
  --Найти специальность с наименьшим числом кандидатов и добавить в неё нового
select top(1) applicants.Vacancy from applicants GROUP BY Vacancy ORDER BY COUNT(Vacancy)

