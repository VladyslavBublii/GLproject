using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BL.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace BL.Services
{
    public class S3Bucket : IS3Bucket
    {
        private readonly string _url;
        //private readonly string _accessKeyId;
        //private readonly string _secretKey;
        //private readonly string _region;
        //private readonly string _bucketName;

        public S3Bucket()
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var servicesDirectory = Path.GetFullPath(Path.Combine(basePath));

            var configFilePath = Path.Combine(servicesDirectory, "appsettings.json");

            // Reading data from a configuration file
            var configFileContent = File.ReadAllText(configFilePath, System.Text.Encoding.UTF8);
            Console.WriteLine(configFileContent);

            var configJson = JObject.Parse(configFileContent);
            var awsConnection = configJson["AWSConnection"];
            _url = awsConnection["Url"].ToString();
            //_accessKeyId = awsConnection["AccessKeyId"].ToString();
            //_secretKey = awsConnection["SecretKey"].ToString();
            //_region = awsConnection["Region"].ToString();
            //_bucketName = awsConnection["BucketName"].ToString();
        }

        // Method for getting a link to a picture by name
        public string GetImageLink(string imageName)
        {
            var url = $"{_url}/{imageName}";
            return url;
        }

        // Method for getting links to images from a list
        public IEnumerable<string> GetImagesLinks(IEnumerable<string> imageNames)
        {
            foreach (var imageName in imageNames)
            {
                yield return GetImageLink(imageName);
            }
        }
    }
}
