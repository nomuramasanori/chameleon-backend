using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.IO;
using System.Reflection;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chameleon.Application.Controllers
{
    [Authorize]
    [AllowAnonymous]
    [Area("Application")]
    public class ApiController : Controller
    {
        private readonly IDbConnection _connection;
        private readonly ApplicationManager _applicationManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        private string ImageFolder => Path.Combine(_hostingEnvironment.WebRootPath, "image");

        public ApiController(IDbConnection connection, IOptions<ApplicationManager> options, IHostingEnvironment environment)
        {
            _connection = connection;
            _applicationManager = options.Value;
            _hostingEnvironment = environment;
        }

        [HttpGet]
        [Route("application")]
        public ActionResult ApplicationInfomation()
        {
            var test = new { ID = Guid.NewGuid().ToString(), Name = _applicationManager.Name, Description = _applicationManager.Description };
            return Content(JsonConvert.SerializeObject(test), "application/json", System.Text.Encoding.UTF8);
        }

        [HttpGet]
        [Route("image")]
        public ActionResult Image(string function)
        {
            string fileName = function ?? Assembly.GetEntryAssembly().FullName.Split(',')[0];

            //return Content(Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(this.ImageFolder, fileName + ".png"))));
            return Content(Convert.ToBase64String(this.GetImage(fileName + ".png")));
        }
        
        private byte[] GetImage(string file)
        {
            var fullName = Path.Combine(this.ImageFolder, file);

            if (System.IO.File.Exists(fullName))
            {
                return System.IO.File.ReadAllBytes(fullName);
            }
            else
            {
                return System.IO.File.ReadAllBytes(Path.Combine(this.ImageFolder, "image-not-found.png"));
            }
        }

        [HttpGet]
        [Route("contents")]
        public ActionResult GetContents()
        {
            var outlines = new ArrayList();

            _applicationManager.GetContents().ForEach(content => {
                outlines.Add(new {
                    id = content.GetType().FullName,
                    name = content.Name,
                    description = content.Description,
                    showAsMenu = content.ShowAsMenu
                });
            });

            return Content(JsonConvert.SerializeObject(outlines), "application/json");
        }

        [HttpPost]
        [Route("{func}/layout")]
        public ActionResult GetEditorLayout(string func, [FromBody]Test condition)
        {
            return Content(JsonConvert.SerializeObject(this._applicationManager.CreateFunction(func, condition.condition, HttpContext.Request.Host.Value).GetLayout()), "application/json");
        }

        [HttpPost]
        [Route("{func}/{block}/columns")]
        public ActionResult GetColumnProperty(string func, string block, [FromBody]Test condition)
        {
            //return Content(JsonConvert.SerializeObject(this._applicationManager.CreateFunction(func, condition.condition, _connection).GetBlockProperty(block, _connection)), "application/json");
            return Content(JsonConvert.SerializeObject(this._applicationManager.CreateFunction(func, condition.condition, HttpContext.Request.Host.Value).GetColumnProperty(block, _connection)), "application/json");
        }

        [HttpPost]
        [Route("{func}/{block}/grid")]
        public ActionResult GetGridProperty(string func, string block, [FromBody]Test condition)
        {
            var inspectionTarget = this._applicationManager.CreateFunction(func, condition.condition, HttpContext.Request.Host.Value).GetBlock(block);

            if (inspectionTarget.GetType().GetInterfaces().Contains(typeof(IGridOption)))
            {
                var gridOption = inspectionTarget as IGridOption;
                return Content(JsonConvert.SerializeObject(gridOption), "application/json");
            }

            return null;
        }

        [HttpPost]
        [Route("{func}/{block}/linkable")]
        public ActionResult HasTransition(string func, string block, [FromBody]Test condition)
        {
            return Content(JsonConvert.SerializeObject(this._applicationManager.CreateFunction(func, condition.condition, HttpContext.Request.Host.Value).GetLinkers(block)), "application/json");
        }

        [HttpPost]
        [Route("{func}/{block}/authentication")]
        public ActionResult IsAuth(string func, string block, [FromBody]Test condition)
        {
            return Content(JsonConvert.SerializeObject(this._applicationManager.CreateFunction(func, condition.condition, HttpContext.Request.Host.Value).GetBlock(block).AuthenticateUrl), "application/json");
        }

        [HttpPost]
        [Route("{func}/data")]
        public ActionResult GetEditorData(string func, [FromBody]Test condition)
        {
            string serializeObject = JsonConvert.SerializeObject(_applicationManager.CreateFunction(func, condition.condition, HttpContext.Request.Host.Value).GetData(_connection));
            return Content(serializeObject, "application/json");
        }

        [HttpPost]
        [Route("{func}/recalculate")]
        public ActionResult RecalculateEditor(string func, [FromBody]Test test)
        {
            return Content(JsonConvert.SerializeObject(_applicationManager.CreateFunction(func, test.condition, HttpContext.Request.Host.Value).Recalculate(test.data, _connection)), "application/json");
        }

        [HttpPost]
        [Route("{func}/save")]
        public ActionResult Save(string func, [FromBody]Test test)
        {
            JArray editedDataJArray = (JArray)JsonConvert.DeserializeObject(test.data);
            JArray imagesJArray = (JArray)JsonConvert.DeserializeObject(test.images);

            var editor = _applicationManager.CreateFunction(func, test.condition, HttpContext.Request.Host.Value);

            // imageを保存してそのURLを編集済みデータに反映します
            foreach (var image in imagesJArray)
            {
                var blockName = image["block"].ToString();
                var columnName = image["column"].ToString();
                var imageString = image["image"].ToString();
                var type = image["type"].ToString();

                var rowType = editor.GetBlock(blockName).GetRecordType();
                var column = rowType.GetProperties().First(col => col.Name == columnName);
                var imageAttribute = Attribute.GetCustomAttribute(column, typeof(ImageAttribute)) as ImageAttribute;

                var row = editedDataJArray.Where(blocks => blocks["gridId"].ToString() == blockName).First()["data"][0];
                var typeSpecifiedRow = row.ToObject(rowType);

                byte[] imageBytes = Convert.FromBase64String(imageString);
                using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    var postedFile = new PostedFile(ms, this.Request.Host.Value, this._hostingEnvironment.WebRootPath, type);
                    var imageUrl = rowType.GetMethod(imageAttribute.SaveMethod).Invoke(typeSpecifiedRow, new object[] { postedFile }) as string;
                    row[columnName] = imageUrl;
                }
            }

            return Content(JsonConvert.SerializeObject(editor.Save(this._connection, editedDataJArray.ToString())), "application/json");
        }

        [HttpPost]
        [Route("{func}/{block}/{column}/listitem")]
        public ActionResult GetEditorListItem(string func, string block, string column, [FromBody]TransitionParameter tp)
        {
            return Content(JsonConvert.SerializeObject(_applicationManager.CreateFunction(func, tp.condition, HttpContext.Request.Host.Value).GetListItem(block, column, tp.rowData, _connection)), "application/json");
        }

        [HttpPost]
        [Route("{func}/{block}/{column}/listitem/all")]
        public ActionResult GetEditorAllListItem(string func, string block, string column, [FromBody]TransitionParameter condition)
        {
            return Content(JsonConvert.SerializeObject(_applicationManager.CreateFunction(func, condition.condition, HttpContext.Request.Host.Value).GetListItem(block, column, _connection)), "application/json");
        }

        //[HttpPost]
        //[Route("{func}/{block}/transition")]
        //public ActionResult GetTransition(string func, string condition, string block, string rowData)
        //{
        //    return Content(JsonConvert.SerializeObject(_applicationManager.CreateFunction(func, condition, _connection).GetBlock(block).Transition.CreateTransitionParameter(rowData)), "application/json");
        //}
        [HttpPost]
        [Route("{func}/{block}/link")]
        public ActionResult GetLink(string func, string block, [FromBody]TransitionParameter param)
        {
            return Content(JsonConvert.SerializeObject(_applicationManager.CreateFunction(func, param.condition, HttpContext.Request.Host.Value).GetBlock(block).GetLink(param.id).CreateLink(param.rowData)), "application/json");
        }

        //[HttpPost]
        //[Route("{content}/{block}/upload-file")]
        //public async Task<IActionResult> PostFiles(string content, string block, IFormFile file)
        //{
        //    var physicalPath = "";

        //    using (var stream = new MemoryStream())
        //    {
        //        await file.CopyToAsync(stream);
        //        stream.Position = 0;
        //        physicalPath = _applicationManager.CreateFunction(content, null).GetBlock(block).SavePicture(this.ImageFolder, "https://" + HttpContext.Request.Host.Value + "/image/", stream);
        //    }

        //    return Content(physicalPath);
        //}

        [HttpGet]
        [Route("/hoge")]
        public IActionResult hoge()
        {
            Console.WriteLine(User.Claims);
            return Ok("hogeeeeeee");
        }
    }

    public class Test
    {
        public string condition { get; set; }
        public string data { get; set; }
        public string images { get; set; }
    }

    public class TransitionParameter
    {
        public string id { get; set; }
        public string condition { get; set; }
        public string rowData { get; set; }
    }
}
