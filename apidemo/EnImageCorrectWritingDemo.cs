using System;
using System.Collections.Generic;
using System.IO;

// 网易有道智云英文图片作文批改服务api调用demo
// api接口: https://openapi.youdao.com/v2/correct_writing_image
namespace OpenapiDemo
{
    static class EnImageCorrectWritingDemo
    {
        // 您的应用ID
        private static string APP_KEY = "";
        // 您的应用密钥
        private static string APP_SECRET = "";

        // 作文图片路径, 例windows路径：PATH = "C:\\youdao\\media.jpg";
        private static string PATH = "";

        public static void Main()
        {
            // 添加请求参数
            Dictionary<String, String[]> paramsMap = createRequestParams();
            // 添加鉴权相关参数
            AuthV3Util.addAuthParams(APP_KEY, APP_SECRET, paramsMap);
            Dictionary<String, String[]> header = new Dictionary<string, string[]>() { { "Content-Type", new String[] { "application/x-www-form-urlencoded" } } };
            // 请求api服务
            byte[] result = HttpUtil.doPost("https://openapi.youdao.com/v2/correct_writing_image", header, paramsMap, "application/json");
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
            // 取值参考文档: https://ai.youdao.com/DOCSIRMA/html/%E4%BD%9C%E6%96%87%E6%89%B9%E6%94%B9/API%E6%96%87%E6%A1%A3/%E8%8B%B1%E8%AF%AD%E4%BD%9C%E6%96%87%E6%89%B9%E6%94%B9%EF%BC%88%E5%9B%BE%E5%83%8F%E8%AF%86%E5%88%AB%EF%BC%89/%E8%8B%B1%E8%AF%AD%E4%BD%9C%E6%96%87%E6%89%B9%E6%94%B9%EF%BC%88%E5%9B%BE%E5%83%8F%E8%AF%86%E5%88%AB%EF%BC%89-API%E6%96%87%E6%A1%A3.html
            string grade = "作文等级";
            string title = "作文标题";
            string modelContent = "作文参考范文";
            string isNeedSynonyms = "是否查询同义词";
            string correctVersion = "作文批改版本：基础，高级";
            string isNeedEssayReport = "是否返回写作报告";

            // 数据的base64编码
            string q = readFileAsBaes64(PATH);
            return new Dictionary<string, string[]>() {
                { "q", new string[]{q}},
                {"grade", new string[]{grade}},
                {"title", new string[]{title}},
                {"modelContent", new string[]{modelContent}},
                {"isNeedSynonyms", new string[]{isNeedSynonyms}},
                {"correctVersion", new string[]{correctVersion}},
                {"isNeedEssayReport", new string[]{isNeedEssayReport}},
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