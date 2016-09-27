using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables;
using SavingVariables.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SavingVariables.Models;
using System.Data.Entity;

namespace SavingVariables.Tests.DAL
{
    [TestClass]
    public class SavingVariablesRepositoryTest
    {
        Mock<SavingVariablesDbContext> mock_context { get; set; }
        Mock<DbSet<charValue>> mock_dbset { get; set; }
        List<charValue> variable_datastore { get; set; }
        SavingVariablesRepository repo { get; set; }

        

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<SavingVariablesDbContext>();
            mock_dbset = new Mock<DbSet<charValue>>();
            variable_datastore = new List<charValue>();//fake database
            repo = new SavingVariablesRepository(mock_context.Object);//use the dependancy Injection
            //.Object returns the instance

            var queryable_list = variable_datastore.AsQueryable();//type change

            // Lie to LINQ make it think that our new Queryable List is a Database table.
            //the real dbset doesn't have Provider, Expression method...
            //return only return once, callback could be used for many times
            //IQuerable only used in LINQ
            mock_dbset.As<IQueryable<charValue>>().Setup(m => m.Provider).Returns(queryable_list.Provider);//where data from
            mock_dbset.As<IQueryable<charValue>>().Setup(m => m.Expression).Returns(queryable_list.Expression);//e.g. SQL query is an expression; a big expression could be seperate into two expressions
            mock_dbset.As<IQueryable<charValue>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);//key words is a element type, e.g. SELECT, FROM; * simbal; table, 3 element type
            mock_dbset.As<IQueryable<charValue>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_list.GetEnumerator());//could loop over ordered

            //mock context return the mock_variable_table when someone calls the SavingVariableContext.charValueDb
            mock_context.Setup(c => c.charValueDb).Returns(mock_dbset.Object);

            //capture when use Add function, instead use variable_datastore
            mock_dbset.Setup(t => t.Add(It.IsAny<charValue>())).Callback((charValue a/*capture the variable sent*/) => variable_datastore.Add(a)/*add it to a list*/);
            mock_dbset.Setup(t => t.Remove(It.IsAny<charValue>())).Callback((charValue a) => variable_datastore.Remove(a));
 
        }

        [TestCleanup]
        public void ClearUp()
        {
            repo = null;
        }

        [TestMethod]
        public void repositoryInstanceIsNotNull()
        {
            //put this sentence in everytime
            repo = new SavingVariablesRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void EnsureRepoHasContext()
        {
            repo = new SavingVariablesRepository();
            SavingVariablesDbContext actual_context = repo.Context;
            Assert.IsInstanceOfType(actual_context, typeof(SavingVariablesDbContext));
            //Assert.IsNotNull(actual_context);
        }


        [TestMethod]
        //test at first is empty
        public void RepoShowItAllWhenEmpty()
        {
            List<charValue> found_variables = repo.showItAll();
            int actual_record_count = found_variables.Count;
            Assert.AreEqual(0, actual_record_count);
            
        }
        
        //Add & show all
        [TestMethod]
        public void AddThenShowCount()
        {
            repo.SavingAValueToDB("x", 5);

            int actual_record_count = repo.showItAll().Count;
            Assert.AreEqual(1, actual_record_count);
        }

        //show all
        [TestMethod]
        public void ShowItAllReturnTypeIsList()
        {
            repo.SavingAValueToDB("c", 7);
            repo.SavingAValueToDB("v", 6);

            List<charValue> list = repo.showItAll();
            Assert.AreEqual(typeof(List<charValue>), list.GetType());
        }

        [TestMethod]
        public void showAll_showFirstValue()
        {
            repo.SavingAValueToDB("c", 7);
            repo.SavingAValueToDB("v", 6);

            List<charValue> records = repo.showItAll();
            int first_record_value = records.First().value;
            Assert.AreEqual(7, first_record_value);
        }

        //show single
        [TestMethod]
        public void showSingleFunctionTest()
        {
            repo.SavingAValueToDB("c", 7);
            repo.SavingAValueToDB("v", 6);

            charValue record = repo.showSingleRecord("c");
            int record_value = record.value;
            Assert.AreEqual(7, record_value);
        }

        [TestMethod]
        public void showANotExistingVariableReturnsNull()
        {
            repo.SavingAValueToDB("c", 7);
            repo.SavingAValueToDB("v", 6);

            var record = repo.showSingleRecord("d");
            Assert.IsNull(record);
        }

        //clear single
        [TestMethod]
        public void ClearSingleFunctionCount()
        {
            repo.SavingAValueToDB("c", 7);
            repo.SavingAValueToDB("v", 6);
            repo.SavingAValueToDB("b", 22);

            repo.ClearSingleRecord("c");
            int left_record_count = repo.showItAll().Count;
            Assert.AreEqual(2, left_record_count);
        }

        [TestMethod]
        public void ClearANotExistingValueReturnsNull()
        {
            repo.SavingAValueToDB("c", 7);
            repo.SavingAValueToDB("v", 6);
            repo.SavingAValueToDB("b", 22);

            var returnedResult= repo.ClearSingleRecord("d");
            Assert.IsNull(returnedResult);
        }

        //clear all
        [TestMethod]
        public void ClearAllFunctionTestCount()
        {
            repo.SavingAValueToDB("c", 7);
            repo.SavingAValueToDB("v", 6);
            repo.SavingAValueToDB("b", 22);

            repo.ClearAllFunction();
            int actual_left_count = repo.showItAll().Count;
            Assert.AreEqual(0, actual_left_count);
        }

    }
}
