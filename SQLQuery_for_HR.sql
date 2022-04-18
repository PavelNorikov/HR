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
       VALUES ('�����������'), ('��������'), ('�����������');


delete from applicants
INSERT INTO [applicants]
       VALUES
            ('�������� ��������� ��������', 22, 38000, 1),
			('��������� ������ ��������', 17, 45000, 2),
			('������� ���� ����������', 3, 68000, 3),
			('������� ���� ��������', 5, 63000, 1),
			('�������� ���� ����������', 11, 47000, 2),
			('��������� ���� ��������', 21, 88000, 3),
			('�������� ������� ���������', 1, 29000, 1),
			('������� ���� ������������', 18, 59000, 2),
			('�������� ������� ������������', 9, 100000, 3),
			('������������ ������� ����������', 16, 75000, 1);


  --������� ������ �����������, ��������������� �� �������� ��������, ������ �������� �� ��������.
select applicants.Name, vacancies.Title, applicants.Salary , Exp from applicants, vacancies where applicants.Vacancy = vacancies.ID order by Vacancy, Salary
  --������� ������ ���� �������� ��������������� ����������� �������������, (����������������� ������� ��� ����, ������� �� ���������� ��������)
select TOP (2) applicants.Name from applicants where applicants.Vacancy = 1 order by [Exp]/Salary DESC
  --������� ������ �����������, � ����������� ���������� ����� �������� �� ����, ������ � �� ���������������.
select applicants.Name, vacancies.Title, applicants.Salary from applicants, vacancies where applicants.Vacancy = vacancies.ID and applicants.Salary > (select AVG(Salary) from applicants)
  --������� ����: {�������������, ������� ���������� ��������}, ������������� �� ����� ������������������ �������������� � �����.
select distinct vacancies.Title, (select AVG(Salary) from applicants where applicants.Vacancy = vacancies.ID) AS avgSalary from applicants INNER join vacancies on applicants.Vacancy = vacancies.ID order by avgSalary DESC
  --����� ���������� � ���������� ������ � ������ �� �������������� � ������� �� ����.
select applicants.Name from applicants, vacancies where applicants.Vacancy = vacancies.ID and Exp = (select MIN(Exp) from applicants where applicants.Vacancy = vacancies.ID)
  --����� ��������� � ������������ ��������� ��������, ��������� ����� �� 15%.
select applicants.ID from applicants where Salary = (select MAX(Salary) from applicants)
  --����� ������������� � ���������� ������ ���������� � �������� � �� ������
select top(1) applicants.Vacancy from applicants GROUP BY Vacancy ORDER BY COUNT(Vacancy)

