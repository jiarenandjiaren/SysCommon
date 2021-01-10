using System;
using System.Collections.Generic;
using System.IO;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SysCommon.Service.Interface;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;

namespace SysCommon.Service
{
    /// <summary>
    /// 文件
    /// </summary>
    public class FileService : BaseService<UploadFile>
    {
        private ILogger<FileApp> _logger;
        private string _filePath;
        public string _dbFilePath;   //数据库中的文件路径
        private string _dbThumbnail;   //数据库中的缩略图路径

        public FileService( IOptions<AppSetting> setOptions, IUnitWork unitWork, IRepository<UploadFile> repository, ILogger<FileApp> logger, IAuth auth)
            :base(unitWork, repository, auth)
        {
            _logger = logger;
            _filePath = setOptions.Value.UploadPath;
            if (string.IsNullOrEmpty(_filePath))
            {
                _filePath = AppContext.BaseDirectory;
            }
        }

        public List<UploadFile> Add(IFormFileCollection files)
        {
            var result = new List<UploadFile>();
            foreach (var file in files)
            {
                result.Add(Add(file));
            }

            return result;
        }

        public UploadFile Add(IFormFile file)
        {
            if (file != null)
            {
                _logger.LogInformation("收到新文件: " + file.FileName);
                _logger.LogInformation("收到新文件: " + file.Length);
            }
            else
            {
                _logger.LogWarning("收到新文件为空");
            }
            if (file != null && file.Length > 0 && file.Length < 10485760)
            {
                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var data = binaryReader.ReadBytes((int)file.Length);
                    UploadFile(fileName, data);

                    var filedb = new UploadFile
                    {
                        FilePath = _dbFilePath,
                        Thumbnail = _dbThumbnail,
                        FileName = fileName,
                        FileSize = file.Length,
                        FileType = Path.GetExtension(fileName),
                        Extension = Path.GetExtension(fileName)
                    };
                    Repository.Add(filedb);
                    return filedb;
                }
            }
            else
            {
                throw new Exception("文件过大");
            }
        }

        private void UploadFile(string fileName, byte[] fileBuffers)
        {
            string folder = "File/"+DateTime.Now.ToString("yyyyMM");
            if (fileName.Contains(".jpg") || fileName.Contains(".jpeg") || fileName.Contains(".png") || fileName.Contains(".bmp") || fileName.Contains(".gif"))
            {
                folder = folder + "/Img";
            }
            else
                folder = folder + "/Other";
            //判断文件是否为空
            if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception("文件名不能为空");
            }

            //判断文件是否为空
            if (fileBuffers.Length < 1)
            {
                throw new Exception("文件不能为空");
            }

            var uploadPath = Path.Combine(_filePath , folder );
            _logger.LogInformation("文件写入：" + uploadPath);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var ext = Path.GetExtension(fileName).ToLower();
            string newName = GenerateId.GenerateOrderNumber() + ext;

            using (var fs = new FileStream(Path.Combine(uploadPath , newName), FileMode.Create))
            {
                fs.Write(fileBuffers, 0, fileBuffers.Length);
                fs.Close();

                //生成缩略图
                if (ext.Contains(".jpg") || ext.Contains(".jpeg") || ext.Contains(".png") || ext.Contains(".bmp") || ext.Contains(".gif"))
                {
                    string thumbnailName = GenerateId.GenerateOrderNumber() + ext;
                    ImgHelper.MakeThumbnail(Path.Combine(uploadPath , newName), Path.Combine(uploadPath , thumbnailName));
                    _dbThumbnail = Path.Combine(folder , thumbnailName);
                }


                _dbFilePath = Path.Combine(folder , newName);
            }
        }
        /// <summary>
        /// 删除文件夹下（包含嵌套文件夹里），所有.exe,.lib,.dll文件   _filePath
        /// </summary>
        /// <param name="file_path">文件路径</param>
        public void DeleteFile(string file_path)
        {
            file_path = _filePath+file_path;
            try
            {
                File.Delete(file_path);
                //foreach (string file in Directory.GetFileSystemEntries(file_path))
                //{
                //    if (Directory.Exists(file))
                //    {
                //        DeleteFile(file);
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}