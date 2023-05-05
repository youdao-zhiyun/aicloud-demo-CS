using System;
using System.Collections.Generic;
using System.IO;

// 网易有道智云通用OCR服务api调用demo
// api接口: https://openapi.youdao.com/ocrapi
namespace OpenapiDemo
{
    static class OcrDemo
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
            byte[] result = HttpUtil.doPost("https://openapi.youdao.com/ocrapi", header, paramsMap, "application/json");
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
            // 取值参考文档: https://ai.youdao.com/DOCSIRMA/html/%E6%96%87%E5%AD%97%E8%AF%86%E5%88%ABOCR/API%E6%96%87%E6%A1%A3/%E9%80%9A%E7%94%A8%E6%96%87%E5%AD%97%E8%AF%86%E5%88%AB%E6%9C%8D%E5%8A%A1/%E9%80%9A%E7%94%A8%E6%96%87%E5%AD%97%E8%AF%86%E5%88%AB%E6%9C%8D%E5%8A%A1-API%E6%96%87%E6%A1%A3.html
            string langType = "要识别的语言类型";
            string detectType = "识别类型";
            string angle = "是否进行360角度识别";
            string column = "是否按多列识别";
            string rotate = "是否需要获得文字旋转角度";
            string docType = "json";
            string imageType = "1";

            // 数据的base64编码
            string img = readFileAsBaes64(PATH);
            return new Dictionary<string, string[]>() {
                {"img", new string[]{img}},
                {"langType", new string[]{langType}},
                {"detectType", new string[]{detectType}},
                {"angle", new string[]{angle}},
                {"column", new string[]{column}},
                {"rotate", new string[]{rotate}},
                {"docType", new string[]{docType}},
                {"imageType", new string[]{imageType}},
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