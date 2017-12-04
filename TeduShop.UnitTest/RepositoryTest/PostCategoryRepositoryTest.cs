using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.UnitTest.RepositoryTest
{
    [TestClass]
    public class PostCategoryRepositoryTest
    {
        private IDbFactory dbFactory;
        private IUnitOfWork unitOfWork;
        private IPostCategoryRepository postCategoryRepository;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            unitOfWork = new UnitOfWork(dbFactory);
            postCategoryRepository = new PostCategoryRepository(dbFactory);
        }

        [TestMethod]
        public void PostCategory_Repository_GetAll()
        {
            var result = postCategoryRepository.GetAll().ToList();
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void PostCategory_Repository_Create()
        {
            PostCategory postCategory = new PostCategory();
            postCategory.Name = "TEST CLASS";
            postCategory.Alias = "TEST-CLASS";
            postCategory.Status = true;

            var result = postCategoryRepository.Add(postCategory);
            unitOfWork.Commit();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.ID);
        }
    }
}