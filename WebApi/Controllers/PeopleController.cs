using Accela.DataAccess;
using Accela.DataAccess.DataModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebApi.Controllers
{
    [ApiController]
    public class PeopleController : Controller
    {
        [HttpGet]
        [Route("api/people/getall")]
        public ActionResult GetPeople()
        {
            AccelaDbContext dbContext = new AccelaDbContext();

            var result = dbContext.People.ToList();

            return Ok(result);
        }

        [HttpPost]
        [Route("api/people/addperson")]
        public ActionResult AddPerson([FromBody] Person req)
        {
            AccelaDbContext dbContext = new AccelaDbContext();
            dbContext.People.Add(req);
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("api/people/editperson")]
        public ActionResult EditPerson([FromBody] Person req)
        {
            int personId = req.PersonId;

            AccelaDbContext dbContext = new AccelaDbContext();

            Person personToUpdate = dbContext.People.Where(x => x.PersonId == personId).FirstOrDefault();

            if (personToUpdate != null)
            {
                personToUpdate.Title = req.Title;
                personToUpdate.FirstName = req.FirstName;
                personToUpdate.LastName = req.LastName;
                personToUpdate.Email = req.Email;
                personToUpdate.Telephone = req.Telephone;
                personToUpdate.DateOfBirth = req.DateOfBirth;

                dbContext.SaveChanges();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("api/people/deleteperson/{personId}")]
        public ActionResult DeleteReport(int personId)
        {
            AccelaDbContext dbContext = new AccelaDbContext();

            Person personToRemove = dbContext.People.Where(x => x.PersonId == personId).FirstOrDefault();

            if (personToRemove != null)
            {
                dbContext.People.Remove(personToRemove);
                dbContext.SaveChanges();
            }

            return Ok();
        }
    }
}
