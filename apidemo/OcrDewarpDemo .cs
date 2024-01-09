using System;
using System.Collections.Generic;
using System.IO;

// 网易有道智云图像矫正服务api调用demo
// api接口: https://openapi.youdao.com/ocr_dewarp
namespace OpenapiDemo
{
    static class OcrDewarpDemo
    {
        // 您的应用ID
        private static string APP_KEY = "";
        // 您的应用密钥
        private static string APP_SECRET = "";

        // 待识别图片路径, 例windows路径：PATH = "C:\\youdao\\media.jpg";
        private static string PATH = "";

        public static void Main()
        {
            // 添加请求参数
            Dictionary<String, String[]> paramsMap = createRequestParams();
            // 添加鉴权相关参数
            AuthV3Util.addAuthParams(APP_KEY, APP_SECRET, paramsMap);
            Dictionary<String, String[]> header = new Dictionary<string, string[]>() { { "Content-Type", new String[] { "application/x-www-form-urlencoded" } } };
            // 请求api服务
            byte[] result = HttpUtil.doPost("https://openapi.youdao.com/ocr_dewarp", header, paramsMap, "application/json");
            // 打印返回结果
            if (result != null)
            {
                string resStr = System.Text.Encoding.UTF8.GetString(result);
                Console.WriteLine(resStr);
            }

        }

        private static Dictionary<String, String[]> createRequestParams()
        {
            // note: 将下列变量替换为需要请求的参数
            // 取值参考文档: https://ai.youdao.com/DOCSIRMA/html/ocr/api/txjz/index.html
            string enhance = "0";  // 是否进行图像增强预处理
            string angle = "0"; // 是否进行360角度识别
            string docDetect = "1"; // 是否进行图像检测
            string docDewarp = "1"; // 是否进行图像矫正,同时将自动跳过轮廓分割

            // 数据的base64编码
            string q = readFileAsBaes64(PATH);
            return new Dictionary<string, string[]>() {
                {"q", new string[]{q}},
                {"enhance", new string[]{enhance}},
                {"angle", new string[]{angle}},
                {"docDetect", new string[]{docDetect}},
                {"docDewarp", new string[]{docDewarp}},
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