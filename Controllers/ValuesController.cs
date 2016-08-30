using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ValuesController : ApiController
    {
        Repasitory rep = null;
        private static string currenPath=  HttpContext.Current.Server.MapPath("~/");
        private string basePath=HttpContext.Current.Server.MapPath("~/");
        private static List<string> dirPath = new List<string>();
        // GET api/values
        public object Get()
        {
            
            rep = new Repasitory();
            var fileAndDir = rep.GetAllFileAndDir(currenPath);
            return new { Dir = fileAndDir.directory, file=fileAndDir.nameOfFile, countS = fileAndDir.countS, countX = fileAndDir.countX, countXL = fileAndDir.countXL, currentPathDir = currenPath };

        }

        // GET api/values/5
        public object GetByDir(string id, [FromUri]bool privious)
        {
            rep = new Repasitory();
            MyFolder fileAndDir = new MyFolder();
            bool isChild = true;
            string currentDir = null;
            if (privious)
            {
                currentDir = dirPath.Last();
                fileAndDir = rep.GetReviousDir(currentDir, ref currenPath);             
                dirPath.RemoveAt(dirPath.LastIndexOf(dirPath.Last()));
              
            }
            else
            {
                dirPath.Add(id);
                fileAndDir = rep.GetAllFileAndDirByName(id,ref currenPath);
                currentDir = dirPath.Last();


            }
            if (basePath.Equals(currenPath))
                isChild = false;
            return new { Dir = fileAndDir.directory, file = fileAndDir.nameOfFile,countS= fileAndDir.countS, countX = fileAndDir.countX, countXL = fileAndDir.countXL, currentPathDir= currenPath, currentDir = currentDir, parentDir=privious,isChild= isChild};
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
