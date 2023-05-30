using System;
using System.Collections.Generic;
using System.IO;

namespace OpenapiDemo
{
    static class TtsDemo
    {
        // 您的应用ID
        private static string APP_KEY = "";
        // 您的应用密钥
        private static string APP_SECRET = "";

        // 合成音频保存路径, 例windows路径：PATH = "C:\\tts\\media.mp3";
        private static string PATH = "";

        public static void Main()
        {
            // 添加请求参数
            Dictionary<String, String[]> paramsMap = createRequestParams();
            // 添加鉴权相关参数
            AuthV3Util.addAuthParams(APP_KEY, APP_SECRET, paramsMap);
            Dictionary<String, String[]> header = new Dictionary<string, string[]>() { { "Content-Type", new String[] { "application/x-www-form-urlencoded" } } };
            // 请求api服务
            byte[] result = HttpUtil.doPost("https://openapi.youdao.com/ttsapi", header, paramsMap, "audio");
            // 打印返回结果
            if (result != null)
            {
                saveFile(PATH, result);
            }
        }

        private static Dictionary<String, String[]> createRequestParams()
        {
            // note: 将下列变量替换为需要请求的参数
            string q = "待合成文本";
            string voiceName = "发言人名称";
            string format = "mp3";

            return new Dictionary<string, string[]>() {
                { "q", new string[]{q}},
                {"voiceName", new string[]{voiceName}},
                {"format", new string[]{format}}
            };
        }

        private static void saveFile(string path, byte[] data)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(data, 0, data.Length);
                    Console.WriteLine("save path:  " + path);
                }
            }
            catch
            {
                Console.WriteLine("save file error");
            }
        }
    }
}