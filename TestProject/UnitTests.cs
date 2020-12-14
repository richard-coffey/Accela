using Accela.DataAccess;
using Accela.DataAccess.DataModels;
using System;
using System.Linq;
using System.Transactions;
using Xunit;

namespace TestProject
{
    public class UnitTests :IDisposable
    {
        private AccelaDbContext dbContext;

        public UnitTests()
        {
            dbContext = new AccelaDbContext();
        }

        private int AddPerson(Person testPerson)
        {
            dbContext.People.Add(testPerson);

            dbContext.SaveChanges();

            return testPerson.PersonId;
        }

        [Fact]
        public void Test_AddPerson()
        {
            int personId = 0;

            Person testPerson = new Person
            {
                Title = "MR",
                FirstName = "John",
                LastName = "Smith",
                Email = "johnsmith@gmail.com",
                Telephone = "085-123-4567",
                DateOfBirth = new DateTime(1990, 01, 01)
            };

            // Update & Rollback [to isolate change from other tests]
            using (TransactionScope scope = new TransactionScope())
            {
                personId = AddPerson(testPerson);               
            }

            Assert.True(personId > 0, "The id was not greater than zero");
        }

        [Fact]
        public void Test_RemovePerson()
        {
            Person testPerson = new Person
            {
                Title = "MRS",
                FirstName = "Jane",
                LastName = "Doe",
                Email = "janedoe@gmail.com",
                Telephone = "087-321-7654",
                DateOfBirth = new DateTime(1995, 06, 15)
            };

            int personId = AddPerson(testPerson);

            Person personToRemove = dbContext.People.Where(x => x.PersonId == personId).FirstOrDefault();

            if (personToRemove != null)
            {
                dbContext.People.Remove(personToRemove);
                dbContext.SaveChanges();
            }

            Person checkTestPerson = dbContext.People.Where(x => x.FirstName == "Jane" && x.LastName == "Doe" && x.PersonId == personId).FirstOrDefault();

            Assert.Null(checkTestPerson);
        }

        [Fact]
        public void Test_UpdatePerson()
        {
            int personId = 0;
            Person checkTestPerson = null;

            Person testPerson = new Person
            {
                Title = "MR",
                FirstName = "Peter",
                LastName = "Parker",
                Email = "peterparker@gmail.com",
                Telephone = "086-231-7564",
                DateOfBirth = new DateTime(1979, 04, 24)
            };

            // Update & Rollback [to isolate change from other tests]
            using (TransactionScope scope = new TransactionScope())
            {
                personId = AddPerson(testPerson);

                checkTestPerson = dbContext.People.Where(x => x.FirstName == "Peter" && x.LastName == "Parker" && x.PersonId == personId).FirstOrDefault();

                checkTestPerson.Email = "spiderman@gmail.com";

                dbContext.SaveChanges();
            }            

            Assert.True(checkTestPerson.Email == "spiderman@gmail.com");
        }

        public void Dispose()
        {
            dbContext = null;
        }
    }
}
