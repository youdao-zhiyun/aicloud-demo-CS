using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

// 网易有道智云流式语音合成服务api调用demo
// api接口: wss://openapi.youdao.com/stream_tts
namespace OpenapiDemo
{
    static class StreamTtsDemo
    {
        // 您的应用ID
        private static string APP_KEY = "";
        // 您的应用密钥
        private static string APP_SECRET = "";

        // 语音合成文本
        private static string TEXT = "语音合成文本";

        public static void Main()
        {
            // 添加请求参数
            Dictionary<String, String[]> paramsMap = createRequestParams();
            // 添加鉴权相关参数
            AuthV4Util.addAuthParams(APP_KEY, APP_SECRET, paramsMap);
            // 创建websocket连接
            Task task = WebsocketUtil.initConnectionWithParams("wss://openapi.youdao.com/stream_tts", paramsMap);
            // 发送流式数据
            sendData();
            Task.WaitAll(task);
        }

        private static Dictionary<String, String[]> createRequestParams()
        {
            // note: 将下列变量替换为需要请求的参数
            string langType = "语言类型";
            string voice = "发言人";
            string rate = "音频数据采样率";
            string format = "音频格式";
            string volume = "音量, 取值0.1-5";
            string speed = "语速, 取值0.5-2";

            return new Dictionary<string, string[]>() {
                {"langType", new string[]{langType}},
                {"voice", new string[]{voice}},
                {"rate", new string[]{rate}},
                {"format", new string[]{format}},
                {"volume", new string[]{volume}},
                {"speed", new string[]{speed}}
            };
        }

        private static void sendData()
        {
            try
            {
                WebsocketUtil.sendTextMessage(String.Format("{{\"text\":\"{0}\"}}", TEXT));
                string endMessage = "{\"end\": \"true\"}";
                WebsocketUtil.sendBinaryMessage(Encoding.UTF8.GetBytes(endMessage));
            }
            catch (Exception e)
            {
                Console.WriteLine("send data error" + e);
            }
        }
    }
}