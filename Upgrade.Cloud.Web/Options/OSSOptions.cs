using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upgrade.Cloud.Web.Options
{
    public class OSSOptions
    {
        private string _accessKeyId;

        public string AccessKeyId
        {
            get { return _accessKeyId; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(AccessKeyId)} must be set.");
                }
                _accessKeyId = value;
            }
        }


        private string _accessKeySecret;
        public string AccessKeySecret
        {
            get { return _accessKeySecret; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(AccessKeySecret)} must be set.");
                }
                _accessKeySecret = value;
            }
        }

        private string _endpoint { get; set; } = "http://oss-cn-shenzhen.aliyuncs.com";

        public string Endpoint
        {
            get { return _endpoint; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(Endpoint)} must be set.");
                }
                _endpoint = value;
            }
        }

        public string CallbackServer { get; set; }

        public string DirToDownload { get; set; } = "D:\\OSSDownload";

        public string FileToUpload { get; set; } = "D:\\OSSUpload";

        public string BigFileToUpload { get; set; } = "D:\\OSSBigUpload";

        public string ImageFileToUpload { get; set; } = "D:\\OSSImgUpload";
    }
}
