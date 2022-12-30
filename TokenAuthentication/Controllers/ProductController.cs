using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TokenAuthentication.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {

        [HttpGet]
        [Route("GetAllProducts")]
        public string GetAllProducts()
        { 
            return "All Products";
        }


        [HttpPost]
        [Route("SaveProduct")]
        public object SaveProduct(string productsName)
        {
            return Array.Empty<object>();
        }

    }
}
