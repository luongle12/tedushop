using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Models;
using TeduShop.Web.Infrastructure.Extensions;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        IPostCategoryService _postCategoryService;
        public PostCategoryController(IErrorService errorService, IPostCategoryService postCatagoryService) : base(errorService)
        {
            this._postCategoryService = postCatagoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage requets)
        {
            return CreateHttpResponse(requets, () =>
            {
                HttpResponseMessage response = null;

                var listCategory = _postCategoryService.GetAll();
                var listCategoryViewModel = Mapper.Map<List<PostCategoryViewModel>>(listCategory);

                response = requets.CreateResponse(HttpStatusCode.OK, listCategoryViewModel);

                return response;
            });
        } 

        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage requets, PostCategoryViewModel postCategoryViewModel)
        {
            return CreateHttpResponse(requets, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    requets.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    PostCategory newPostCategory = new PostCategory();
                    newPostCategory.UpdatePostCategory(postCategoryViewModel);

                    var category = _postCategoryService.Add(newPostCategory);
                    _postCategoryService.Save();

                    response = requets.CreateResponse(HttpStatusCode.Created, category);
                }

                return response;
            });
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage requets, PostCategoryViewModel postCategoryViewModel)
        {
            return CreateHttpResponse(requets, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    requets.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var postCategoryDb = _postCategoryService.GetByID(postCategoryViewModel.ID);
                    postCategoryDb.UpdatePostCategory(postCategoryViewModel);

                    _postCategoryService.Update(postCategoryDb);
                    _postCategoryService.Save();

                    response = requets.CreateResponse(HttpStatusCode.OK, postCategoryDb);
                }

                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage requets, int id)
        {
            return CreateHttpResponse(requets, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    requets.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var category = _postCategoryService.Delete(id);
                    _postCategoryService.Save();
                    response = requets.CreateResponse(HttpStatusCode.Created, id);
                }

                return response;
            });
        }
    }
}