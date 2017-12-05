using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        IPostCategoryService _postCategoryService;
        public PostCategoryController(IErrorService errorService, IPostCategoryService postCatagoryService):base(errorService)
        {
            this._postCategoryService = postCatagoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage requets)
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
                    var listCategory = _postCategoryService.GetAll();
                    response = requets.CreateResponse(HttpStatusCode.OK, listCategory);
                }

                return response;
            });
        }

        public HttpResponseMessage Post(HttpRequestMessage requets, PostCategory postCategory)
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
                    var category = _postCategoryService.Add(postCategory);
                    _postCategoryService.Save();
                    response = requets.CreateResponse(HttpStatusCode.Created, postCategory);
                }

                return response;
            });
        }

        public HttpResponseMessage Put(HttpRequestMessage requets, PostCategory postCategory)
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
                    _postCategoryService.Update(postCategory);
                    _postCategoryService.Save();
                    response = requets.CreateResponse(HttpStatusCode.OK, postCategory);
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