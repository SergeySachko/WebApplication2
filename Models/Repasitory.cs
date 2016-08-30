using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Repasitory
    {
        private  MyFolder myfolder = null;      
        public  MyFolder GetAllFileAndDir(string name)
        {
            myfolder = new MyFolder();
            SetDir(name);
            return myfolder;
        }
       
        public MyFolder GetAllFileAndDirByName(string nameOfDir,ref string currenPath)
        {
            myfolder = new MyFolder();
            currenPath += "\\" + nameOfDir;
            SetDir(currenPath);
            return myfolder;
        }
        public MyFolder  GetReviousDir(string id, ref string currenPath)
        {
            myfolder = new MyFolder();
            currenPath = currenPath.Remove(currenPath.Length-id.Length-1,id.Length+1);
            SetDir(currenPath);
            return myfolder;
        }
        private void SetDir(string currenPath)
        {
            int countS = 0, countX = 0, countXL = 0;
            List<String> list = new List<String>();
            List<String> listOfDir = new List<String>();
            List<String> listOfFile = new List<String>();
            DirectoryInfo dr = new DirectoryInfo(currenPath);

            FileInfo[] files = dr.GetFiles();
            DirectoryInfo[] masdir = dr.GetDirectories();
            foreach (DirectoryInfo d in masdir)
            {
                listOfDir.Add(d.Name);
                countS += d.EnumerateFiles().Where(x => convertToMB(x.Length) < 10000 ).Count();
                countX += d.EnumerateFiles().Where(x => convertToMB(x.Length) >=10000 && convertToMB(x.Length) < 50000).Count();
                countXL += d.EnumerateFiles().Where(x => convertToMB(x.Length) >= 100000).Count();
            }
            foreach (FileInfo f in files)
            {
                listOfFile.Add(f.Name);
                if (convertToMB(f.Length) < 10000)
                    countS++;
                else if (convertToMB(f.Length) >= 10000 && convertToMB(f.Length) < 50000)
                    countX++;
                else if(convertToMB(f.Length) >= 100000)
                    countXL++;
            }

            myfolder.directory = listOfDir;
            myfolder.nameOfFile = listOfFile;
            myfolder.countS=countS.ToString();
            myfolder.countX=countX.ToString();
            myfolder.countXL=countXL.ToString();
        }
        double convertToMB(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
    }
}