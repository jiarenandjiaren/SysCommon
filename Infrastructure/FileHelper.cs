using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infrastructure
{
    public class FileHelper
    {
        /// <summary>
        /// 文件写入
        /// </summary>
        /// <param name="path"></param>
        /// <param name="context"></param>
        public static void Write(string path, string context)
        {
            StreamWriter f1 = new System.IO.StreamWriter(path, true, System.Text.Encoding.UTF8);
            f1.WriteLine(context);
            f1.Flush();
            f1.Close();
            f1.Dispose();
        }
        /// <summary>
        /// 获取指定文件夹下所有的文件名称
        /// </summary>
        /// <param name="foldername">指定文件夹名称,绝对路径</param>
        /// <param name="filefilter">文件类型过滤,根据文件后缀名,如:*,*.txt,*.xls</param>
        /// <param name="iscontainsubfolder">是否包含子文件夹</param>
        /// <returns>arraylist数组,为所有需要的文件路径名称</returns>
        public static ArrayList getallfilesbyfolder(string foldername, bool iscontainsubfolder)
        {
            ArrayList resarray = new ArrayList();

            string[] files = Directory.GetFiles(foldername);
            for (int i = 0; i < files.Length; i++)
            {
                resarray.Add(files[i].Replace(foldername + "\\", ""));
            }
            if (iscontainsubfolder)
            {
                string[] folders = Directory.GetDirectories(foldername);
                for (int j = 0; j < folders.Length; j++)
                {
                    //遍历所有文件夹
                    ArrayList temp = getallfilesbyfolder(folders[j], iscontainsubfolder);
                    resarray.AddRange(temp);
                }
            }
            return resarray;
        }
        /// <summary>From:www.uzhanbao.com
        /// 获取路径下所有文件以及子文件夹中文件
        /// </summary>
        /// <param name="path">全路径根目录</param>
        /// <param name="FileList">存放所有文件的全路径</param>
        /// <param name="RelativePath"></param>
        /// <returns></returns>
        public static Dictionary<string, long> GetFile(string path, Dictionary<string, long> FileList, string RelativePath)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] fil = dir.GetFiles();
            DirectoryInfo[] dii = dir.GetDirectories();
            foreach (FileInfo f in fil)
            {
                //int size = Convert.ToInt32(f.Length);
                long size = f.Length;
                FileList.Add(f.FullName, size);//添加文件路径到列表中
            }
            //获取子文件夹内的文件列表，递归遍历
            foreach (DirectoryInfo d in dii)
            {
                GetFile(d.FullName, FileList, RelativePath);
            }
            return FileList;
        }
        public static string[] listFiles(string dir, int level)
        {
            //获取文件列表
            string[] files = Directory.GetFiles(dir);

            String preStr = "";
            for (int i = 0; i < level; i++)
            {
                preStr += "    ";
            }

            foreach (string f in files)
            {
                if (f.LastIndexOf("\\") == -1)
                {
                    Console.WriteLine(preStr + f.Substring(f.LastIndexOf("/") + 1));
                }
                else
                {
                    Console.WriteLine(preStr + f.Substring(f.LastIndexOf("\\") + 1));
                }

            }

            //获取目录列表
            string[] dirs = Directory.GetDirectories(dir);
            foreach (string d in dirs)
            {
                if (d.LastIndexOf("\\") == -1)
                {
                    Console.WriteLine(preStr + d.Substring(d.LastIndexOf("/") + 1));
                }
                else
                {
                    Console.WriteLine(preStr + d.Substring(d.LastIndexOf("\\") + 1));
                }
                if (Directory.Exists(d))
                {
                    listFiles(d, level + 1);
                }
            }
            return dirs;
        }
    }
}
