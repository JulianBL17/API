using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories{

public interface IStudentsRepository{

Task<IEnumerable<Students>> Get();
        Task<Students> Get(int id);
        Task<Students> Create(Students students);
        Task Update(Students students);
        Task Delete(int id);



}



}
