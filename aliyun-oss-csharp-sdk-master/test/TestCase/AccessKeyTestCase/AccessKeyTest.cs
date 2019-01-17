﻿using Aliyun.OSS;
using Aliyun.OSS.Common;
using Aliyun.OSS.Test.Util;

using NUnit.Framework;

namespace Aliyun.OSS.Test.TestClass.AccessKeyTestClass
{
    [TestFixture]
    public class AccessKeyTest
    {
        [Test]
        public void InvalidAccessKeyIdTest()
        {
            try
            {
                //Key Id is invalid
                var ossClient = new OssClient(Config.Endpoint, "invalidKeyId", Config.AccessKeySecret);
                ossClient.ListBuckets();
                Assert.Fail("Invalid key Id should not initialize OssClient successfully");
            }
            catch (OssException e)
            {
                Assert.AreEqual(OssErrorCode.InvalidAccessKeyId, e.ErrorCode);
            }
        }

        [Test]
        public void InvalidAccessKeySecretTest()
        {
            try
            {
                //Key secret is invalid
                var ossClient = new OssClient(Config.Endpoint, Config.AccessKeyId, "invalidKeySecret");
                ossClient.ListBuckets();
                Assert.Fail("Invalid key secret should not initialize OssClient successfully");
            }
            catch (OssException e)
            {
                Assert.AreEqual(OssErrorCode.SignatureDoesNotMatch, e.ErrorCode);
            }
        }

        [Test]
        public void DisabledAccessKeyTest()
        {
            try
            {
                //Key id/secret is valid but disabled
                var ossClient = new OssClient(Config.Endpoint, Config.DisabledAccessKeyId, Config.DisabledAccessKeySecret);
                ossClient.ListBuckets();
                Assert.Fail("Disabled access key should not initialize OssClient successfully");
            }
            catch(OssException e)
            {
                Assert.AreEqual(OssErrorCode.InvalidAccessKeyId, e.ErrorCode);
            }
        }
    }
}
