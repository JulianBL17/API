using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers{

[ApiController]
[Route("[controller]")]
    



public class StudentsController : ControllerBase {

    private readonly IStudentsRepository  _studentRepository;

    public StudentsController(IStudentsRepository studentRepository){

        _studentRepository = studentRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Students>> GetStudents(){
        return await _studentRepository.Get();

    }

     [HttpGet("{id}")]
        public async Task<ActionResult<Students>> GetStudents(int id)
        {
            return await _studentRepository.Get(id);

        }

    [HttpPost]
    public async Task<ActionResult<Students>>PostStudents([FromBody]Students students ){
        var newStudents=await _studentRepository.Create(students);
        return CreatedAtAction(nameof(GetStudents), new {id=newStudents.Id},newStudents);
    }

    [HttpPut]
    public async Task<ActionResult>PutStudents(int id ,[FromBody]Students students){
        if(id !=students.Id){
            return BadRequest();
        }
         await _studentRepository.Update(students);

            return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete (int id)
        {
            var studentsToDelete = await _studentRepository.Get(id);
            if (studentsToDelete == null)
                return NotFound();

            await _studentRepository.Delete(studentsToDelete.Id);
            return NoContent();
        }

} 

}