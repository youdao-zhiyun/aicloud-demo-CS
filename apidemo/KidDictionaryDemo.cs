using System;
using System.Collections.Generic;

namespace OpenapiDemo
{
    static class KidDictionaryDemo
    {
        // 您的应用ID
        private static string APP_KEY = "";
        // 您的应用密钥
        private static string APP_SECRET = "";

        public static void Main()
        {
            // 添加请求参数
            Dictionary<String, String[]> paramsMap = createRequestParams();
            // 添加鉴权相关参数
            AuthV3Util.addAuthParams(APP_KEY, APP_SECRET, paramsMap);
            Dictionary<String, String[]> header = new Dictionary<string, string[]>() { { "Content-Type", new String[] { "application/x-www-form-urlencoded" } } };
            // 请求api服务
            byte[] result = HttpUtil.doPost("https://openapi.youdao.com/v2/kid_dict", header, paramsMap, "application/json");
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
            // 取值参考文档: https://ai.youdao.com/DOCSIRMA/html/dictionary/api/secd/index.html
            string q = "待查询的词";
            string langType = "输入的语言";

            return new Dictionary<string, string[]>() {
                { "q", new string[]{q}},
                {"langType", new string[]{langType}}
            };
        }
    }
}