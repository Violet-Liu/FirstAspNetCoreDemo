using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Upgrade.Cloud.Web.Controllers.Common;
using Upgrade.Cloud.Web.Models;
using Upgrade.Core.DomainModels;
using Upgrade.Core.Interfaces;
using Upgrade.Infrastructure.Interfaces;
using Mall.Common.Extension;
using System.IO;
using Upgrade.Cloud.Web.Service;
using System.Net;

namespace Upgrade.Cloud.Web.Controllers
{
    public class HomeController : RepositoryRangeBaseController<HomeController>
    {
        private readonly IOSSBaseProvider _ossProvider;
        private readonly IRepository<ClientSet> _clientSetRepository;
        private readonly IRepository<Park> _parkRepository;

        public HomeController(ICoreService<HomeController> coreService,
            IRepository<UpgradeItem> upgradeItemRepository,
            IRepository<ClientUpgradeItem> clientUpgradeItemRepository,
            IRepository<UpgradeFiles> upgradeFilesRepository,
            IRepository<ClientSet> clientSetRepository,
            IRepository<Park> parkRepository,
            IOSSBaseProvider oSSBase) : base(coreService, upgradeItemRepository, clientUpgradeItemRepository, upgradeFilesRepository)
        {
            _ossProvider = oSSBase;
            _clientSetRepository = clientSetRepository;
            _parkRepository = parkRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetUpgradeItems(int limit, int offset, string search, string sort, string order)
        {
            var records = _upgradeItemRepository.GetAll(); 
            if (!string.IsNullOrEmpty(search))
            {
                records = records.Where(d => d.Name.Contains(search) || d.Content.Contains(search));
            }
            if (!string.IsNullOrEmpty(sort))
                records = records.DataSorting(sort, order == "asc");
            else
                records = records.DataSorting("Id", false);

            var total = records.Count();
            var rows = records.Skip(offset).Take(limit).ToList();
            return Json(new { total = total, rows = rows });

        }

        public JsonResult GetUpgradeFiles(int limit, int offset, string search, string sort, string order, int itemId)
        {
            var records = _upgradeFilesRepository.GetAll().Where(d => d.UpgradeItemId == itemId);
            if (!string.IsNullOrEmpty(search))
            {
                records = records.Where(d => d.FileName.Contains(search) || d.BucketName.Contains(search));
            }
            if (!string.IsNullOrEmpty(sort))
                records = records.DataSorting(sort, order == "asc");
            else
                records = records.DataSorting("Id", false);

            var total = records.Count();
            var rows = records.Skip(offset).Take(limit).ToList();
            return Json(new { total = total, rows = rows });

        }

        public JsonResult GetUpgradeClients(int limit, int offset, string search, string sort, string order, int itemId)
        {
            var records = _clientUpgradeItemRepository.GetAll().Where(d => d.UpgradeItemId == itemId);
            if (!string.IsNullOrEmpty(search))
            {
                records = records.Where(d => d.ParkId.Contains(search));
            }
            if (!string.IsNullOrEmpty(sort))
                records = records.DataSorting(sort, order == "asc");
            else
                records = records.DataSorting("Id", false);

            var total = records.Count();
            var rows = records.Skip(offset).Take(limit).ToList();
            return Json(new { total = total, rows = rows });

        }

        public JsonResult GetParks(int limit, int offset, string search, string sort, string order)
        {
            var records = _parkRepository.GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                records = records.Where(d => d.ParkId.Contains(search) || d.ParkName.Contains(search));
            }
            if (!string.IsNullOrEmpty(sort))
                records = records.DataSorting(sort, order == "asc");
            else
                records = records.DataSorting("Id", false);

            var total = records.Count();
            var rows = records.Skip(offset).Take(limit).ToList();
            return Json(new { total = total, rows = rows });

        }

        public JsonResult GetClientSets(int limit, int offset, string search, string sort, string order, int itemId)
        {
            var records = _clientSetRepository.GetAll().Where(d => d.UpgradeItemId == itemId);
            if (!string.IsNullOrEmpty(search))
            {
                records = records.Where(d => d.ParkId.Contains(search));
            }
            if (!string.IsNullOrEmpty(sort))
                records = records.DataSorting(sort, order == "asc");
            else
                records = records.DataSorting("Id", false);

            var total = records.Count();
            var rows = records.Skip(offset).Take(limit).ToList();
            return Json(new { total = total, rows = rows });

        }

        public async Task<JsonResult> Edit(UpgradeItem item)
        {
            var entity = await _upgradeItemRepository.GetByIdAsync(item.Id);
            if (entity.IsNull())
                return Json(new { result = false, msg = "后台未找到对应数据" });
            entity.Name = item.Name;
            entity.Content = item.Content;
            entity.IsValid = item.IsValid;
            _upgradeItemRepository.Update(entity);
            if (await UnitOfWork.SaveAsync())
                return Json(new { result = true });
            else
                return Json(new { result = false, msg = "修改失败" });
            
        }

        public async Task<JsonResult> EditFile(UpgradeFiles item)
        {
            var entity = await _upgradeFilesRepository.GetByIdAsync(item.Id);
            if (entity.IsNull())
                return Json(new { result = false, msg = "后台未找到对应数据" });
            entity.FileName = item.FileName;
            entity.FilePath = item.FilePath;
            _upgradeFilesRepository.Update(entity);
            if (await UnitOfWork.SaveAsync())
                return Json(new { result = true });
            else
                return Json(new { result = false, msg = "修改失败" });

        }

        public async Task<JsonResult> DelFileById(int Id)
        {
            await _upgradeFilesRepository.DeleteByIdAsync(Id);

            if (await UnitOfWork.SaveAsync())
            {
                return Json(new { result = true, msg = "删除成功" });
            }
            else
                return Json(new { result = false, msg = "删除失败" });
                
        }

        public async Task<JsonResult> DeleteClientSetById(int Id)
        {
            await _clientSetRepository.DeleteByIdAsync(Id);

            if (await UnitOfWork.SaveAsync())
            {
                return Json(new { result = true, msg = "删除成功" });
            }
            else
                return Json(new { result = false, msg = "删除失败" });

        }

        

        public async Task<JsonResult> DelClientById(int Id)
        {
            await _clientUpgradeItemRepository.DeleteByIdAsync(Id);

            if (await UnitOfWork.SaveAsync())
            {
                return Json(new { result = true, msg = "删除成功" });
            }
            else
                return Json(new { result = false, msg = "删除失败" });

        }

        

        public async Task<JsonResult> AddItem([FromBody]ItemViewModel item)
        {
            if (string.IsNullOrEmpty(item.Name))
                return Json(new { result = false, msg = "名称不能为空" });
            if (string.IsNullOrEmpty(item.Content))
                return Json(new { result = false, msg = "更新内容不能为空" });

            var entity = new UpgradeItem
            {
                Name = item.Name,
                Content = item.Content,
                Creater = "sys",
                CreateTime = DateTimeOffset.Now.DateTime,
                IsValid = true,
            };

            _upgradeItemRepository.Add(entity);
            if (await UnitOfWork.SaveAsync())
                return Json(new { result = true, msg = "保存成功" });
            else
                return Json(new { result = false, msg = "服务器保存失败" });
        }

        public async Task<JsonResult> AddClientSet(ClientSetViewModel item)
        {
            var entity = new ClientSet
            {
                UpgradeItemId=item.ItemId,
                ParkId = item.ParkId,
                ParkName = item.ParkName,
                Creater = "sys",
                CreateTime = DateTimeOffset.Now.DateTime,
            };

            _clientSetRepository.Add(entity);
            if (await UnitOfWork.SaveAsync())
                return Json(new { result = true, msg = "保存成功" });
            else
                return Json(new { result = false, msg = "服务器保存失败" });
        }

        public JsonResult GetBucketNames()
        {
            var buckets = _ossProvider.ListAllBuckets();

            return Json(new { data = buckets, result = buckets.Count > 0 });
        }
        
        public JsonResult CreateBucket(string name)
        {
            var msg = string.Empty;
            if(string.IsNullOrEmpty(name))
                return Json(new { result = false, msg = "The bucketName can't be empty or null" });

            if (_ossProvider.DoesBucketExist(name, ref msg))
                return Json(new { result = false, msg = msg });

            if(_ossProvider.CreateBucket(name,ref msg))
                return Json(new { result = true, msg = "Build success" });
            else
                return Json(new { result = false, msg = msg });
        }


        public async Task<JsonResult> Upload(int Id,string filePath,string bucketName)
        {
            var context = HttpContext.Request.HttpContext;

            if (string.IsNullOrEmpty(filePath))
                return Json(new { result = false, msg = "上传文件路径不准为空" });

            if (string.IsNullOrEmpty(bucketName))
                return Json(new { result = false, msg = "上传存储空间不准为空" });

            var files = Request.Form.Files;
            if(!files.IsNullOrEmpty())
            {
                foreach(var formFile in files)
                {
                    if(formFile.Length>0)
                    {
                        var fileName= formFile.FileName;
                        var key = formFile.FileName;//formFile.FileName.Substring(0, formFile.FileName.LastIndexOf("."));
                        
                        using (var stream = new MemoryStream())
                        {
                            await formFile.CopyToAsync(stream);
                            stream.Position = 0;
                            var result = _ossProvider.PutObjectFromFile(bucketName, key, fileName, stream);
                            if (result.HttpStatusCode == HttpStatusCode.OK)
                            {
                                _upgradeFilesRepository.Add(new UpgradeFiles
                                {
                                    Key = key,
                                    FileName = fileName,
                                    BucketName = bucketName,
                                    Creater = "sys",
                                    CreateTime = DateTimeOffset.Now.DateTime,
                                    UpgradeItemId = Id,
                                    FilePath = filePath
                                });
                                if (await UnitOfWork.SaveAsync())
                                {
                                    return Json(new { result = true, msg = $"上传{fileName}文件成功" });
                                }
                            }
                        } 
                    }
                }
            }

            return Json(new { result = false, msg = "上传失败" });
        }
    }
}
