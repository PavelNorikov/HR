using System.Data.SqlClient;
using ConsoleApp1;
using ConsoleApp1.EF;

static void MyAdoNet()
{
    var connection = new SqlConnection(@"Server = WIN-AV0EFDDEH5B; Database = HR; Trusted_Connection = True;");
    Console.WriteLine("\n Вывести список соискателей, отсортированных по названию вакансии, внутри вакансии по зарплате.");
    connection.Open();
    var command = connection.CreateCommand();
    command.CommandText = "select applicants.Name, vacancies.Title, applicants.Salary , Exp from applicants, vacancies where applicants.Vacancy = vacancies.ID order by Vacancy, Salary";
    var reader = command.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine($"Name = {reader.GetString(0)} Title = {reader.GetString(reader.GetOrdinal("Title"))} Salary = {reader.GetDecimal(reader.GetOrdinal("Salary"))} ");
    }
    connection.Close();


    Console.WriteLine("\n Вывести первых двух наиболее привлекательных соискателей программистов, (привлекательность считать как стаж, делённый на зарплатные ожидания)");
    connection.Open();
    command = connection.CreateCommand();
    command.CommandText = "select TOP (2) applicants.Name from applicants where applicants.Vacancy = 1 order by [Exp]/Salary DESC";
    reader = command.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine($"Name = {reader.GetString(0)} ");
    }
    connection.Close();


    Console.WriteLine("\n Вывести список соискателей, с зарплатными ожиданиями “выше среднего по базе”, вместе с их специальностями.");
    connection.Open();
    command = connection.CreateCommand();
    command.CommandText = "select applicants.Name, vacancies.Title, applicants.Salary from applicants, vacancies where applicants.Vacancy = vacancies.ID and applicants.Salary > (select AVG(Salary) from applicants)";
    reader = command.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine($"Name = {reader.GetString(0)} Title = {reader.GetString(reader.GetOrdinal("Title"))} Salary = {reader.GetDecimal(reader.GetOrdinal("Salary"))}");
    }
    connection.Close();


    Console.WriteLine("\n Вывести пары: {специальность, среднее зарплатное ожидание}, отсортировать от более высокооплачиваемых специальностей к менее.");
    connection.Open();
    command = connection.CreateCommand();
    command.CommandText = "select distinct vacancies.Title, (select AVG(Salary) from applicants where applicants.Vacancy = vacancies.ID) AS avgSalary from applicants INNER join vacancies on applicants.Vacancy = vacancies.ID order by avgSalary DESC";
    reader = command.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine($"Name = {reader.GetString(0)} avgSalary = {reader.GetDecimal(reader.GetOrdinal("avgSalary"))}");
    }
    connection.Close();


    Console.WriteLine("\n Взять кандидатов с наименьшем стажем в каждой из специальностей – удалить из базы.");
    Console.WriteLine("До:");
    connection.Open();
    command = connection.CreateCommand();
    command.CommandText = "select applicants.Name, vacancies.Title, applicants.Salary , Exp from applicants, vacancies where applicants.Vacancy = vacancies.ID order by Vacancy, Salary";
    reader = command.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine($"Name = {reader.GetString(0)} Title = {reader.GetString(reader.GetOrdinal("Title"))} Salary = {reader.GetDecimal(reader.GetOrdinal("Salary"))} Exp = {reader.GetInt32(reader.GetOrdinal("Exp"))}");
    }
    connection.Close();

    Console.WriteLine("");
    connection.Open();
    command = connection.CreateCommand();
    command.CommandText = "select applicants.ID from applicants, vacancies where applicants.Vacancy = vacancies.ID and Exp = (select MIN(Exp) from applicants where applicants.Vacancy = vacancies.ID)";
    reader = command.ExecuteReader();
    while (reader.Read())
    {
        var connection2 = new SqlConnection(@"Server = WIN-AV0EFDDEH5B; Database = HR; Trusted_Connection = True;");
        connection2.Open();
        var command2 = connection2.CreateCommand();
        command2.CommandText = $"delete from applicants where ID = {reader.GetInt32(reader.GetOrdinal("ID"))}";
        command2.ExecuteReader();
        connection2.Close();
    }
    connection.Close();

    Console.WriteLine("\nПосле:");
    connection.Open();
    command = connection.CreateCommand();
    command.CommandText = "select applicants.Name, vacancies.Title, applicants.Salary , Exp from applicants, vacancies where applicants.Vacancy = vacancies.ID order by Vacancy, Salary";
    reader = command.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine($"Name = {reader.GetString(0)} Title = {reader.GetString(reader.GetOrdinal("Title"))} Salary = {reader.GetDecimal(reader.GetOrdinal("Salary"))} Exp = {reader.GetInt32(reader.GetOrdinal("Exp"))}");
    }
    connection.Close();



    Console.WriteLine("\n Найти кандидата с максимальным ожиданием зарплаты, уменьшить сумму на 15%.");
    Console.WriteLine("До:");
    connection.Open();
    command = connection.CreateCommand();
    command.CommandText = "select applicants.Name, vacancies.Title, applicants.Salary , Exp from applicants, vacancies where applicants.Vacancy = vacancies.ID order by Vacancy, Salary";
    reader = command.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine($"Name = {reader.GetString(0)} Title = {reader.GetString(reader.GetOrdinal("Title"))} Salary = {reader.GetDecimal(reader.GetOrdinal("Salary"))} Exp = {reader.GetInt32(reader.GetOrdinal("Exp"))}");
    }
    connection.Close();

    connection.Open();
    command = connection.CreateCommand();
    command.CommandText = "select applicants.ID from applicants where Salary = (select MAX(Salary) from applicants)";
    reader = command.ExecuteReader();
    while (reader.Read())
    {
        var connection2 = new SqlConnection(@"Server = WIN-AV0EFDDEH5B; Database = HR; Trusted_Connection = True;");
        connection2.Open();
        var command2 = connection2.CreateCommand();
        command2.CommandText = $"update applicants set Salary = Salary * 0.85 where ID = {reader.GetInt32(reader.GetOrdinal("ID"))}";
        command2.ExecuteReader();
        connection2.Close();
    }
    connection.Close();

    Console.WriteLine("\nПосле:");
    connection.Open();
    command = connection.CreateCommand();
    command.CommandText = "select applicants.Name, vacancies.Title, applicants.Salary , Exp from applicants, vacancies where applicants.Vacancy = vacancies.ID order by Vacancy, Salary";
    reader = command.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine($"Name = {reader.GetString(0)} Title = {reader.GetString(reader.GetOrdinal("Title"))} Salary = {reader.GetDecimal(reader.GetOrdinal("Salary"))} Exp = {reader.GetInt32(reader.GetOrdinal("Exp"))}");
    }
    connection.Close();




    Console.WriteLine("\n Найти специальность с наименьшим числом кандидатов и добавить в неё нового");
    Console.WriteLine("До:");
    connection.Open();
    command = connection.CreateCommand();
    command.CommandText = "select applicants.Name, vacancies.Title, applicants.Salary , Exp from applicants, vacancies where applicants.Vacancy = vacancies.ID order by Vacancy, Salary";
    reader = command.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine($"Name = {reader.GetString(0)} Title = {reader.GetString(reader.GetOrdinal("Title"))} Salary = {reader.GetDecimal(reader.GetOrdinal("Salary"))} Exp = {reader.GetInt32(reader.GetOrdinal("Exp"))}");
    }
    connection.Close();

    connection.Open();
    command = connection.CreateCommand();
    command.CommandText = "select top(1) applicants.Vacancy from applicants GROUP BY Vacancy ORDER BY COUNT(Vacancy)";
    reader = command.ExecuteReader();
    while (reader.Read())
    {
        var connection2 = new SqlConnection(@"Server = WIN-AV0EFDDEH5B; Database = HR; Trusted_Connection = True;");
        connection2.Open();
        var command2 = connection2.CreateCommand();
        command2.CommandText = $"INSERT INTO [applicants] VALUES ('Тест Тестов Тестович', 99, 9999999, {reader.GetInt32(reader.GetOrdinal("Vacancy"))} )";
        command2.ExecuteReader();
        connection2.Close();
    }
    connection.Close();

    Console.WriteLine("\nПосле:");
    connection.Open();
    command = connection.CreateCommand();
    command.CommandText = "select applicants.Name, vacancies.Title, applicants.Salary , Exp from applicants, vacancies where applicants.Vacancy = vacancies.ID order by Vacancy, Salary";
    reader = command.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine($"Name = {reader.GetString(0)} Title = {reader.GetString(reader.GetOrdinal("Title"))} Salary = {reader.GetDecimal(reader.GetOrdinal("Salary"))} Exp = {reader.GetInt32(reader.GetOrdinal("Exp"))}");
    }
    connection.Close();
}


static void MyLinq()
{
    Database db = new Database();
    Console.WriteLine("Вывести список соискателей, отсортированных по названию вакансии, внутри вакансии по зарплате.");
    foreach (applicant a in db.applicants.OrderByDescending(a => a.Vacancy.Title).ThenBy(a => a.Salary))
    {
        Console.WriteLine($"{a.Name} {a.Vacancy.Title} {a.Salary}");
    }


    Console.WriteLine("\n\n Вывести первых двух наиболее привлекательных соискателей программистов, (привлекательность считать как стаж, делённый на зарплатные ожидания)");
    foreach (applicant a in db.applicants.Where(a => a.Vacancy.Title == "Программист").OrderByDescending(a => a.Exp/a.Salary).Take(2))
    {
        Console.WriteLine($"{a.Name}");
    }


    Console.WriteLine("\n\n Вывести список соискателей, с зарплатными ожиданиями “выше среднего по базе”, вместе с их специальностями.");
    foreach (applicant a in db.applicants.Where(a => a.Salary > db.applicants.Average(a => a.Salary)))
    {
        Console.WriteLine($"{a.Name} {a.Vacancy.Title} {a.Salary}");
    }


    Console.WriteLine("\n\n Вывести пары: {специальность, среднее зарплатное ожидание}, отсортировать от более высокооплачиваемых специальностей к менее.");
    foreach (var g in db.applicants.GroupBy(a => a.Vacancy).Select(x => new { Title = x.Key.Title, avg = db.applicants.Where(a => a.Vacancy.Id == x.Key.Id).Average(a => a.Salary)}).OrderByDescending(t => t.avg) )
    {
        Console.WriteLine($"{g.Title} {g.avg}");
    }




    Console.WriteLine("\n\n Взять кандидатов с наименьшем стажем в каждой из специальностей – удалить из базы.");
    Console.WriteLine("До:");
    foreach (applicant a in db.applicants)
    {
        Console.WriteLine($"{a.Name} {a.Exp}");
    }
    List<applicant> res;
    res = new List<applicant> { };
    Console.WriteLine("");
    foreach (var g in db.applicants.GroupBy(a => a.Vacancy).Select(x => new {Title = x.Key.Title, Min = db.applicants.Where(a => a.Vacancy.Id == x.Key.Id).Min(a => a.Exp) }))
    {
        foreach (applicant p in db.applicants.Where(p => p.Vacancy.Title == g.Title && p.Exp == g.Min))
        {
            res.Add(p);
        }
    }
    foreach (var t in res)
    {
        db.applicants.Remove(t);
    }
    Console.WriteLine("После:");
    foreach (applicant a in db.applicants)
    {
        Console.WriteLine($"{a.Name} {a.Exp}");
    }



    Console.WriteLine("\n\n Найти кандидата с максимальным ожиданием зарплаты, уменьшить сумму на 15%.");
    Console.WriteLine("До:");
    foreach (applicant a in db.applicants)
    {
        Console.WriteLine($"{a.Name} {a.Salary}");
    }
    Console.WriteLine("");
    foreach (var g in db.applicants)
    {
        if(g.Salary == db.applicants.Max(g => g.Salary))
        {
            g.Salary = g.Salary * Convert.ToDecimal(0.85);
        }
    }
    Console.WriteLine("После:");
    foreach (applicant a in db.applicants)
    {
        Console.WriteLine($"{a.Name} {a.Salary}");
    }


    Console.WriteLine("\n\n Найти специальность с наименьшим числом кандидатов и добавить в неё нового");
    Console.WriteLine("До:");
    foreach (applicant a in db.applicants)
    {
        Console.WriteLine($"{a.Name} {a.Salary} {a.Vacancy.Title}");
    }
    res = new List<applicant> { };
    Console.WriteLine("");

    foreach (var g in db.vacancies.Select(x => new { id = x.Id, count = db.applicants.Where(a => a.Vacancy.Id == x.Id).Count() }).OrderBy(c => c.count).Take(1))
    {
        db.applicants.Add(new applicant { Id = 99, Name = "Тест Тестов Тестович", Exp = 99, Salary = 9999999, Vacancy = db.vacancies[g.id - 1] });
    }   

    Console.WriteLine("После:");
    foreach (applicant a in db.applicants)
    {
        Console.WriteLine($"{a.Name} {a.Salary} {a.Vacancy.Title}");
    }
}


static void MyEF()
{
    MyCont db = new MyCont();
    db.CreateDbIfNotExist();
}

//MyEF();
MyAdoNet();
//MyLinq();