using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using OX.Bapps;
using System.Reflection;


namespace OX.WebPort
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ApiTagAttribute : RouteAttribute
    {
        public string ApiBoxName { get; set; }
        public string ApiModuleName { get; set; }
        public string ApiActionName { get; set; }

        public ApiTagAttribute(string boxName, string moduleName, string actionName)
            : base("/api/" + boxName + "/" + moduleName + "/" + actionName)
        {
            ApiBoxName = boxName;
            ApiModuleName = moduleName;
            ApiActionName = actionName;
        }
    }
    [ApiController]
    public class BappApiController : ControllerBase
    {

        static Dictionary<string, ApiBoxBuilder> ApiBox = new Dictionary<string, ApiBoxBuilder>();
        static BappApiController()
        {

            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (!type.IsSubclassOf(typeof(ApiBoxBuilder))) continue;
                if (type.IsAbstract) continue;

                ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                try
                {
                    var apiBuilder = constructor?.Invoke(null) as ApiBoxBuilder;
                    ApiBox[apiBuilder.ApiBoxName] = apiBuilder;
                }
                catch (Exception ex)
                {
                }
            }

        }

        static bool ok = false;
        [Route("api/state")]
        [HttpGet]
        //[ResponseCache(Duration = 60)]
        public IActionResult State()
        {
            return this.Content(ok.ToString());
        }
        [Route("api/close")]
        [HttpGet]
        //[ResponseCache(Duration = 60)]
        public IActionResult Close()
        {
            ok = false;
            return this.Content(ok.ToString());
        }
        [Route("api/open")]
        [HttpGet]
        //[ResponseCache(Duration = 60)]
        public IActionResult Open()
        {
            ok = true;
            return this.Content(ok.ToString());
        }

        [Route("api/{ApiBoxName}/{ApiModuleName}/{ApiActionName}/{arg?}/{flag?}")]
        [HttpGet]
        [ResponseCache(Duration = 60)]
        public IActionResult Get(string ApiBoxName, string ApiModuleName, string ApiActionName, string arg, string flag)
        {
            if (ApiBox.TryGetValue(ApiBoxName, out var boxBuilder))
            {
                var box = boxBuilder.Build();
                if (box.IsNull()) return StatusCode(500);
                return box.ProcessGet(this, ApiModuleName, ApiActionName, arg, flag);
            }
            return StatusCode(500);
        }
        [Route("api/{ApiBoxName}/{ApiModuleName}/{ApiActionName}/{arg?}/{flag?}")]
        [HttpPost]
        public IActionResult Post(string ApiBoxName, string ApiModuleName, string ApiActionName, string arg, string flag)
        {
            if (ApiBox.TryGetValue(ApiBoxName, out var boxBuilder))
            {
                var box = boxBuilder.Build();
                if (box.IsNull()) return StatusCode(500);
                return box.ProcessPost(this, ApiModuleName, ApiActionName, arg, flag);
            }
            return StatusCode(500);
        }

    }
}
