using System;
using System.Collections.Generic;
using System.IO;

namespace OpenapiDemo
{
    static class CutQuestionDemo
    {
        // 您的应用ID
        private static string APP_KEY = "";
        // 您的应用密钥
        private static string APP_SECRET = "";

        // 图片路径, 例windows路径：PATH = "C:\\youdao\\media.png";
        private static string PATH = "";

        public static void Main()
        {
            // 添加请求参数
            Dictionary<String, String[]> paramsMap = createRequestParams();
            // 添加鉴权相关参数
            AuthV3Util.addAuthParams(APP_KEY, APP_SECRET, paramsMap);
            Dictionary<String, String[]> header = new Dictionary<string, string[]>() { { "Content-Type", new String[] { "application/x-www-form-urlencoded" } } };
            // 请求api服务
            byte[] result = HttpUtil.doPost("https://openapi.youdao.com/cut_question", header, paramsMap, "application/json");
            // 打印返回结果
            if (result != null)
            {
                string resStr = System.Text.Encoding.UTF8.GetString(result);
                Console.WriteLine(resStr);
            }
        }

        private static Dictionary<String, String[]> createRequestParams()
        {
            string q = readFileAsBaes64(PATH);
            
            return new Dictionary<string, string[]>() {
                { "q", new string[]{q}},
                {"imageType", new string[]{"1"}},
                {"docType", new string[]{"json"}}
            };
        }

        private static string readFileAsBaes64(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (BinaryReader br = new BinaryReader(fs))
                {
                    var length = br.BaseStream.Length;
                    var bytes = br.ReadBytes((int)length);
                    return Convert.ToBase64String(bytes);
                }
            }
            catch
            {
                Console.WriteLine("read file error");
                return null;
            }

        }
    }
}