using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrepTeach.Models;
using PrepTeach.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace PrepTeach.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Tovarlar bilan ishlash")]
    public class ProductController : Controller
    {
        private readonly MyDbContext db;
        private readonly ILogger<ProductController> logger;

        public ProductController(ILogger<ProductController> _logger, MyDbContext _db)
        {
            db = _db;
            logger = _logger;
        }

        [HttpPost()]
        [SwaggerOperation("Yangi Tovar Qo'shish")]
        public async Task<ResponseView<Product>> Post([FromBody] Product payload)
        {
            ResponseView<Product> response = new();
            Product product = new()
            {
                Image = payload.Image,
                Name = payload.Name,
                Price = payload.Price,
                Quantity = payload.Quantity
            };
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
            response.Data = product;
            response.Status = 1;
            return response;
        }

        [HttpPut()]
        [SwaggerOperation("Tovarni O'zgartirish")]
        public async Task<ResponseView<Product>> Put([FromBody] Product payload)
        {
            ResponseView<Product> response = new();
            Product? product = db.Products.FirstOrDefault(p => p.Id == payload.Id);
            if (product != null)
            {
                product.UpdateDate = DateTime.UtcNow;
                product.Name = payload.Name;
                product.Price = payload.Price;
                product.Image = payload.Image;
                await db.SaveChangesAsync();
                response.Data = product;
                response.Status = 1;
            }
            else
            {
                response.Message = "Not Found";
            }
            return response;
        }

        [HttpPatch("sold")]
        [SwaggerOperation("Tovarni Sotilgan Deb Beliglash")]
        public async Task<ResponseView<int>> PatchToSolid([FromBody] IdView idView)
        {
            ResponseView<int> response = new();
            Product? product = db.Products.FirstOrDefault(p => p.Id == idView.Id);
            if (product != null)
            {
                product.UpdateDate = DateTime.UtcNow;
                product.IsSold = true;
                await db.SaveChangesAsync();
                response.Data = 1;
                response.Status = 1;
            }
            else
            {
                response.Message = "Not Found";
            }
            return response;
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Tovarni O'chirib Tashlash")]
        public async Task<ResponseView<int>> Delete([FromRoute] int id)
        {
            Product? old = db.Products.FirstOrDefault(x => x.Id == id);
            ResponseView<int> response = new();
            if (old != null)
            {
                db.Products.Remove(old);
                await db.SaveChangesAsync();
                response.Data = id;
                response.Status = 1;
            }
            else
            {
                response.Message = "Not Found";
            }
            return response;
        }

        [HttpGet()]
        [AllowAnonymous]
        [SwaggerOperation("Hamma Tovarlarni Olish")]
        public ResponseView<IEnumerable<Product>> GetAll()
        {
            IEnumerable<Product> products = db.Products.ToArray();
            return new ResponseView<IEnumerable<Product>>
            {
                Data = products,
                Status = 1,
            };
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [SwaggerOperation("Bitta Tovarni Olish")]
        public ResponseView<Product?> GetId([FromRoute] int id)
        {
            Product? product = db.Products.FirstOrDefault(x => x.Id == id);
            bool isExist = product != null;
            return new ResponseView<Product?>
            {
                Data = product,
                Status = isExist ? 1 : 0,
                Message = isExist ? null : "Not Found"
            };
        }
    }
}
