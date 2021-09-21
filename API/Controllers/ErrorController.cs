using API.Error;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // overide thực thi lai phương thức Route , chạy khi bị nhận lỗi
    [Route("errors/{statusCode}")] 
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController  : BaseApiController
    {
        public IActionResult Error(int statusCode) {
            return new ObjectResult(new ApiResponse(200));
        }
    }
}