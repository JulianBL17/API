using API.Models;
using API.Repositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dapper;

namespace API.Repositories{

    public class StudentsRepository : IStudentsRepository
    {
      
       private readonly string _connectionString;

        public StudentsRepository(IConfiguration configuration){
             _connectionString = configuration.GetConnectionString("Default");
        }
 
        public async Task<Students> Create(Students students)
        {
            var Query = "INSERT INTO Students(nombre,apellido,edad,correo) VALUES (@Nombre, @Apellido,@Edad,@Correo)";
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(Query, new 
                {
                    students.Nombre,
                    students.Apellido,
                    students.Edad,
                    students.Correo
                });

                return students;
            }
        }
        

            public async Task Delete(int id)
        {
            var sqlQuery = $"DELETE from Students WHERE Id={id}";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery);
            }
        }
        

        

         public async Task<IEnumerable<Students>> Get()
        {
            var sqlQuery = "SELECT * FROM Students";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryAsync<Students>(sqlQuery);
            } 
        }


        public async Task<Students> Get(int id)
        {
            var sqlQuery = "SELECT * FROM Students WHERE Id=@StudentsId";

            using (var connection = new SqliteConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Students>(sqlQuery, new { StudentsId = id });
            }
        }

        public async Task Update(Students students)
        {
            var sqlQuery = "UPDATE Students SET nombre=@Nombre, apellido=@Apellido, edad=@Edad,correo=@Correo WHERE id=@Id";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new
                {
                    students.Id,
                    students.Nombre,
                    students.Apellido,
                    students.Edad,
                    students.Correo

                });
            }
        }

}
}

