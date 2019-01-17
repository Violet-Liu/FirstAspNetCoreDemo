using Aliyun.OSS;
using Aliyun.OSS.Common;
using Aliyun.OSS.Util;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Upgrade.Cloud.Web.Options;

namespace Upgrade.Cloud.Web.Service
{
    public class OSSBaseProvider : IOSSBaseProvider
    {
        private readonly string _accessKeyId;
        private readonly string _accessKeySecret;
        private readonly string _endpoint;
        private OssClient _ossClient;
        private readonly ILogger<OSSBaseProvider> _logger;
        public OSSBaseProvider(IOptionsMonitor<OSSOptions> options,ILogger<OSSBaseProvider> logger)
        {
            var option = options.CurrentValue;
            _accessKeyId = option.AccessKeyId;
            _accessKeySecret = option.AccessKeySecret;
            _endpoint = option.Endpoint;
            _logger = logger;
            _ossClient = new OssClient(_endpoint, _accessKeyId, _accessKeySecret);
        }

        public bool CreateBucket(string bucketName,ref string msg)
        {
            try
            {
                _ossClient.CreateBucket(bucketName);

                return true;
            }
            catch (OssException ex)
            {
                msg = ex.Message;
                _logger.LogError("Failed with error info: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                                  ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                _logger.LogError("Failed with error info: {0}", ex.Message);
            }

            return false;
        }

        public PutObjectResult PutObjectFromFile(string bucketName,string key,string filename,Stream content)
        {
            try
            {
                var metadata = new ObjectMetadata();
                if(metadata.ContentType==null)
                {
                    metadata.ContentType = HttpUtils.GetContentType(key, filename);
                }
                var result = _ossClient.PutObject(bucketName, key, content, metadata);

                _logger.LogInformation("Put object:{0} succeeded", key);
                return result;
            }
            catch (OssException ex)
            {
                _logger.LogError("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed with error info: {0}", ex.Message);
            }
            return default(PutObjectResult);
        }

        public List<string> ListAllBuckets()
        {
            try
            {
                var buckets = _ossClient.ListBuckets();

                return buckets?.OrderByDescending(d => d.CreationDate).Select(t => t.Name).ToList();

            }
            catch (OssException ex)
            {
                _logger.LogError("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                                  ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
                return new List<string>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed with error info: {0}", ex.Message);
                return new List<string>();
            }
        }

        public bool DoesBucketExist(string bucketName,ref string msg)
        {
            try
            {
                var exist = _ossClient.DoesBucketExist(bucketName);
                return exist;
            }
            catch (OssException ex)
            {
                msg = ex.Message;
                _logger.LogError("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                _logger.LogError("Failed with error info: {0}", ex.Message);
            }
            return false;
        }
    }
}
