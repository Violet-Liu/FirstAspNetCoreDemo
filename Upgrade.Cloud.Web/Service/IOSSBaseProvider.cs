using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Upgrade.Cloud.Web.Service
{
    public interface IOSSBaseProvider
    {
        bool CreateBucket(string bucketName,ref string msg);

        PutObjectResult PutObjectFromFile(string bucketName, string key, string filename, Stream content);

        List<string> ListAllBuckets();

        bool DoesBucketExist(string bucketName,ref string msg);
    }
}
