using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenapiDemo
{
    static class AsrliteDemo
    {
        // 您的应用ID
        private static string APP_KEY = "";
        // 您的应用密钥
        private static string APP_SECRET = "";

        // 识别音频路径, 例windows路径：PATH = "C:\\youdao\\media.wav";
        private static string PATH = "";

        public static void Main()
        {
            // 添加请求参数
            Dictionary<String, String[]> paramsMap = createRequestParams();
            // 添加鉴权相关参数
            AuthV4Util.addAuthParams(APP_KEY, APP_SECRET, paramsMap);
            Dictionary<String, String[]> header = new Dictionary<string, string[]>() { { "Content-Type", new String[] { "application/x-www-form-urlencoded" } } };
            // 创建websocket连接
            Task task = WebsocketUtil.initConnectionWithParams("wss://openapi.youdao.com/stream_asropenapi", paramsMap);
            // 发送流式数据
            sendData(PATH, 6400);
            Task.WaitAll(task);
        }

        private static Dictionary<String, String[]> createRequestParams()
        {
            // note: 将下列变量替换为需要请求的参数
            // 取值参考文档: https://ai.youdao.com/DOCSIRMA/html/%E5%AE%9E%E6%97%B6%E8%AF%AD%E9%9F%B3%E7%BF%BB%E8%AF%91/API%E6%96%87%E6%A1%A3/%E5%AE%9E%E6%97%B6%E8%AF%AD%E9%9F%B3%E7%BF%BB%E8%AF%91%E6%9C%8D%E5%8A%A1/%E5%AE%9E%E6%97%B6%E8%AF%AD%E9%9F%B3%E7%BF%BB%E8%AF%91%E6%9C%8D%E5%8A%A1-API%E6%96%87%E6%A1%A3.html
            string from = "语种";
            string rate = "音频数据采样率, 推荐16000";

            return new Dictionary<string, string[]>() {
                { "from", new string[]{from}},
                {"rate", new string[]{rate}},
                {"format", new string[]{"wav"}},
                {"channel", new string[]{"1"}},
                {"version", new string[]{"v1"}}
            };
        }

        private static void sendData(string path, int step)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var buffer = new byte[step];
                    while (true)
                    {
                        int len = fs.Read(buffer, 0, buffer.Length);
                        if (len > 0)
                        {
                            WebsocketUtil.sendBinaryMessage(buffer);
                            if (len < step)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                        Thread.Sleep(200);
                    }
                    string endMessage = "{\"end\": \"true\"}";
                    WebsocketUtil.sendBinaryMessage(Encoding.UTF8.GetBytes(endMessage));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("send data error" + e);
            }
        }
    }
}