using System;
using System.Collections.Generic;

namespace OpenapiDemo
{
    static class TextEmbeddingDemo
    {
        // 您的应用ID
        private static string APP_KEY = "";
        // 您的应用密钥
        private static string APP_SECRET = "";

        public static void Main()
        {
            // 1、请求version服务
            // 添加请求参数
            Dictionary<String, String[]> paramsMap1 = createRequestParams1();
            // 添加鉴权相关参数
            AuthV3Util.addAuthParams(APP_KEY, APP_SECRET, paramsMap1);
            Dictionary<String, String[]> header = new Dictionary<string, string[]>() { { "Content-Type", new String[] { "application/x-www-form-urlencoded" } } };
            // 请求api服务
            byte[] version = HttpUtil.doGet("https://openapi.youdao.com/textEmbedding/queryTextEmbeddingVersion", header, paramsMap1, "application/json");
            // 打印返回结果
            if (version != null)
            {
                string resStr = System.Text.Encoding.UTF8.GetString(version);
                Console.WriteLine("version: " + resStr);
            }

            // 2、请求embedding服务
            // 添加请求参数
            Dictionary<String, String[]> paramsMap2 = createRequestParams2();
            // 添加鉴权相关参数
            AuthV3Util.addAuthParams(APP_KEY, APP_SECRET, paramsMap2);
            // 请求api服务
            byte[] embedding = HttpUtil.doPost("https://openapi.youdao.com/textEmbedding/queryTextEmbeddings", header, paramsMap2, "application/json");
            // 打印返回结果
            if (embedding != null)
            {
                string resStr = System.Text.Encoding.UTF8.GetString(embedding);
                Console.WriteLine("embedding: " + resStr);
            }
        }

        private static Dictionary<String, String[]> createRequestParams1()
        {
            // note: 将下列变量替换为需要请求的参数
            // 注: q参数本身非必传, 但是计算签名时需要用作空字符串处理""
            string q = "";

            return new Dictionary<string, string[]>() {
                { "q", new string[]{q}}
            };
        }

        private static Dictionary<String, String[]> createRequestParams2()
        {
            // note: 将下列变量替换为需要请求的参数
            // 注: 包含16个q时效果最佳, 每个q不要超过500个token
            string q1 = "待输入文本1";
            string q2 = "待输入文本2";
            string q3 = "待输入文本3";
            // q4...

            return new Dictionary<string, string[]>() {
                { "q", new string[]{q1, q2, q3}}
            };
        }
    }
}