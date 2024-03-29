using DemoAPI.Data;
using DemoAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private ApplicationDbContext _db;

        public StudentsController(ApplicationDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public List<StudentEntity> GetAllStudents()
        {
            return _db.StudentRegister.ToList();
        }

        [HttpGet("GetStudentById")]
        public ActionResult<StudentEntity> GetStudentDetails(int Id)
        {
            if(Id == 0)
            {
                return BadRequest();
            }
            var StudentDetails = _db.StudentRegister.FirstOrDefault(x => x.Id == Id);

            if(StudentDetails == null)
            {
                return NotFound();
            }

            return StudentDetails;
        }

        [HttpPost]
        public ActionResult<StudentEntity> AddStudent([FromBody]StudentEntity studentDetails)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // add validated studentDetails to database
            _db.StudentRegister.Add(studentDetails);
            _db.SaveChanges();

            return Ok(studentDetails);
        }

        [HttpPut("UpdateStudentDetails")]
        public ActionResult<StudentEntity> UpdateStudent(int Id, [FromBody] StudentEntity studentDetails)
        {
            if (studentDetails == null)
            {
                return BadRequest(studentDetails);
            }

            // fetch the record to make sure that the record student ID class is valid
            var StudentDetails = _db.StudentRegister.FirstOrDefault(studentDetail => studentDetail.Id == Id);
            if(StudentDetails == null)
            {
                return NotFound();
            }

            // Code to update Student Detail

            StudentDetails.Name = studentDetails.Name;
            StudentDetails.Email = studentDetails.Email;
            StudentDetails.Age = studentDetails.Age;
            StudentDetails.Standard = studentDetails.Standard;

            _db.SaveChanges();

            return Ok(studentDetails);
        }


        [HttpDelete("DeleteStudent")]
        public ActionResult<StudentEntity> Delete(int Id)
        {
            // fetch the record to make sure that the record student ID class is valid
            var StudentDetails = _db.StudentRegister.FirstOrDefault(studentDetail => studentDetail.Id == Id);
            if (StudentDetails == null)
            {
                return NotFound();
            }

            _db.Remove(StudentDetails);
            _db.SaveChanges();

            return NoContent();

        }
    }
}
