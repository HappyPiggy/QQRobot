using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Coco.Framework.SDK;

namespace myplu
{
    public class Class1:Plugin
    {

        public String SweetInterface = "http://www.haosf921.com/qsx.php?url=";
        public String YKAccountInterface = "http://www.mdouvip.com/youku/";
        public String AQYAccountInterface = "http://www.mdouvip.com/aiqiyi";
        public String XLAccountInterface = "http://www.mdouvip.com/xunlei";
        public String LSAccountInterface = "http://www.aiqiyivip.com/forum-41-1.html";
        public String WeatherInterface = "http://api.map.baidu.com/telematics/v3/weather?location=";
        public String MovieInterface = "http://www.ashvsash.com/?s=";
        public String SimChatInterface = "http://api.douqq.com/?key=UD15TCs0cHd3dURSOGU9SWlsWFZpT0dUR0FZQUFBPT0&msg=";

        public String refer1 = "http://www.54qiu.cn/";
        public String refer3 = "http://www.iking.pw/";
        public String refer2 = "http://www.haosf921.com/";

        public String refer = "http://www.haosf921.com/";

        private int isSuccess = -1;   /* 未知错误 0成功 1,2今日满 3这周满 */
        private int choose = 1;    /* 0是黑夜 1是白天 */
        private int isWhichVip = 1;
        public int isOpenSweet = 1;
        public String WeatherKey = "ADcaebd39d8157c9a5cded8e4f19736a";
        private String PlayerName = "";
        private String resultContent = "";
        private String resultContent2 = "";
        public String movieKey = "电影:";
        public String movieKey2 = "电影：";
        public String ads = "出租小西瓜机器人 15元/月 QQ296056428";


        public Class1()
        {
            PluginName = "测试插件";
            Description = "test";
            Author = "胖蜀黍";
        }

        #region HTTPGET

        public String HttpGet(String url, String code, String refer)
        {
            /* string url1 = url; */
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            /*  request.Method = "Get"; */
            webRequest.Method = "GET";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 BIDUBrowser/7.6 Safari/537.36";
            //  webRequest.Accept = "application/json, text/javascript, */*; q=0.01";
            //  webRequest.Headers.Add("Cookie", "_5t_trace_sid=2c7cfabff51c2ab1cda06219f46085ec; _5t_trace_tms=1");
            // webRequest.Headers.Add("DNT","1");
            webRequest.Referer = refer;
            /* webRequest.Connection = "keep-alive"; */


            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            Stream s = webResponse.GetResponseStream();
            StreamReader sr = new StreamReader(s, Encoding.GetEncoding(code));
            /*  MessageBox.Show(sr.ReadToEnd()); */
            String temp = StreamToString(sr);

            sr.Dispose();
            sr.Close();
            s.Dispose();
            s.Close();
            return (temp);
        }

        //public String HttpGet(String url, String code)
        //  {
        //      /* string url1 = url; */
        //      HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
        //      /*  request.Method = "Get"; */
        //      webRequest.Method = "GET";
        //      webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 BIDUBrowser/7.6 Safari/537.36";
        //    //  webRequest.Accept = "application/json, text/javascript, */*; q=0.01";
        //   //  webRequest.Headers.Add("Cookie", "_5t_trace_sid=2c7cfabff51c2ab1cda06219f46085ec; _5t_trace_tms=1");
        //     // webRequest.Headers.Add("DNT","1");
        //      webRequest.Referer = "http://www.haosf921.com/";
        //      /* webRequest.Connection = "keep-alive"; */


        //      HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
        //      Stream s = webResponse.GetResponseStream();
        //      StreamReader sr = new StreamReader(s, Encoding.GetEncoding(code));
        //      /*  MessageBox.Show(sr.ReadToEnd()); */
        //      String temp = StreamToString(sr);

        //      sr.Dispose();
        //      sr.Close();
        //      s.Dispose();
        //      s.Close();
        //      return (temp);
        //  }

        public String HttpGet2(String url, String code)
        {
            /* string url1 = url; */
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            /*  request.Method = "Get"; */
            webRequest.Method = "GET";
            //  webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 BIDUBrowser/7.6 Safari/537.36";
            // webRequest.Accept = "application/json, text/javascript, */*; q=0.01";
            //  webRequest.Headers.Add("Cookie", "_5t_trace_sid=2c7cfabff51c2ab1cda06219f46085ec; _5t_trace_tms=1");
            // webRequest.Headers.Add("DNT","1");
            // webRequest.Referer = "http://www.iking.pw/";
            /* webRequest.Connection = "keep-alive"; */


            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            Stream s = webResponse.GetResponseStream();
            StreamReader sr = new StreamReader(s, Encoding.GetEncoding(code));
            /*  MessageBox.Show(sr.ReadToEnd()); */
            String temp = StreamToString(sr);

            sr.Dispose();
            sr.Close();
            s.Dispose();
            s.Close();
            return (temp);
        }


        String StreamToString(StreamReader st)
        {
            String temp = string.Empty;
            while (st.Peek() > -1)
            {
                String input = st.ReadLine();
                temp += input;
            }
            return (temp);
        }

        #endregion
        #region 刷棒棒糖
        void GetSweet(String URL)
        {
            String myURL = SweetInterface + URL;
            String result = HttpGet(myURL, "UTF-8", refer);
            String playerNameRules = "\"account\":\"(.*?)\"";
            String messageRules = "\"code\":\"(.*?)\"";
            String errorRules = "{\"code\":(.*?)}";
            String errorMsg = RulesPackage(errorRules, result);

            if (errorMsg.Equals("-1"))
            {
                isSuccess = -1;
            }
            else
            {

                PlayerName = RulesPackage(playerNameRules, result);
                PlayerName = System.Text.RegularExpressions.Regex.Unescape(PlayerName);
                isSuccess = Int32.Parse(RulesPackage(messageRules, result));
            }
        }

        void AutoGetSweets()
        {
            resultContent2 = "";
            string strLine;
            try
            {
                FileStream aFile = new FileStream("VIPUser.txt", FileMode.Open);
                StreamReader sr = new StreamReader(aFile);
                strLine = sr.ReadLine();
                while (strLine != null)
                {
                    /* Console.WriteLine(strLine); */
                    if (!strLine.Equals(""))
                    {
                        GetSweet(strLine.Trim());
                        VIPReply();
                    }
                    strLine = sr.ReadLine();
                }
                sr.Close();

            }
            catch (IOException ex)
            {
                Console.WriteLine("An IOException has been thrown!");
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
                return;
            }
        }


        void AutoGetSweets2()
        {
            resultContent2 = "";
            string strLine;
            try
            {
                FileStream aFile = new FileStream("VIPUser2.txt", FileMode.Open);
                StreamReader sr = new StreamReader(aFile);
                strLine = sr.ReadLine();
                while (strLine != null)
                {
                    /* Console.WriteLine(strLine); */
                    if (!strLine.Equals(""))
                    {
                        GetSweet(strLine.Trim());
                        VIPReply();
                    }
                    strLine = sr.ReadLine();
                }
                sr.Close();

            }
            catch (IOException ex)
            {
                Console.WriteLine("An IOException has been thrown!");
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
                return;
            }
        }

        void VIPReply()
        {
            String msg = "";

            if (isSuccess == 0)
            {
                msg = "（成功）";
            }
            else if (isSuccess == 2 || isSuccess == 1)
            {
                msg = "（日上限）";
            }
            else if (isSuccess == 3)
            {
                msg = "（周上限）";
            }

            resultContent2 += PlayerName + msg + "\n";
        }

        void GetSweetReply()
        {
            if (isSuccess == 0)
            {
                resultContent = "成功获得5个棒棒糖！";
            }
            else if (isSuccess == 2 || isSuccess == 1)
            {
                resultContent = "今日已经领取过了！";
            }
            else if (isSuccess == 3)
            {
                resultContent = "本周已经达到上限！";
            }
            /*
             * else if (isSuccess == -1)
             * {
             *    resultContent = "刷棒棒糖系统暂时挂掉了~群主正在努力抢修中...\n 如果着急获取棒棒糖，可以看群公告的第二种获取方法~";
             * }
             */
            else
            {
                resultContent = "未知错误！请亲再发一遍喽~~  ";
            }

            /*
             *  SendMessage(resultContent);
             * MessageBox.Show(result);
             */
        }

        #endregion
        String RulesPackage(String rules, String content)
        {
            Regex r = new Regex(rules, RegexOptions.None);
            Match mc = r.Match(content);
            String temp = mc.Groups[1].Value.Trim();
            return temp;
        }

        public override bool Start()
        {
            ReceiveClusterIM += GroupMsg;
            ReceiveNormalIM += AdminInfo;
            return base.Start();
        }

        #region 检测个人消息
        void AdminInfo(object sender, ReceiveNormalIMQQEventArgs e)
        {

            //终极管理权限
            if (e.QQ == 296056428 || e.QQ == 1730761955)
                AdminOrder(e, e.QQ);

                

            

        }

        #endregion
        #region  管理函数
        void AdminOrder(ReceiveNormalIMQQEventArgs e1, uint e)
        {

            if (e1.Message.Contains("vip"))
            {
                //分段执行
                AutoGetSweets();
                this.SendMessage(e, resultContent2);
                AutoGetSweets2();
                this.SendMessage(e, resultContent2);
            }


            /* 刷棒棒糖开关 */
            if (e1.Message.Contains("棒棒糖开"))
            {
                isOpenSweet = 1;
                resultContent = "小西瓜刷棒棒糖功能已开启";
                this.SendMessage(e, resultContent);
            }

            else if (e1.Message.Contains("棒棒糖关"))
            {
                isOpenSweet = 0;
                resultContent = "小西瓜刷棒棒糖功能已关闭";
                this.SendMessage(e, resultContent);
            }



            if (e1.Message.Contains("接口1"))
            {
                SweetInterface = "http://www.54qiu.cn/qsx.php?url=";
                refer = refer1;
                resultContent = "接口成功修改为" + SweetInterface;
                this.SendMessage(e, resultContent);
            }
            else if (e1.Message.Contains("接口2"))
            {
                SweetInterface = "http://www.haosf921.com/qsx.php?url=";
                refer = refer2;
                resultContent = "接口成功修改为" + SweetInterface;
                this.SendMessage(e, resultContent);
            }
            else if (e1.Message.Contains("接口3"))
            {
                SweetInterface = "http://www.iking.pw/qsx.php?url=";
                refer = refer3;
                resultContent = "接口成功修改为" + SweetInterface;
                this.SendMessage(e, resultContent);
            }

            if (e1.Message.Contains("修改接口"))
            {
                String QQInfo = e1.Message.ToString();
                string[] sArray = QQInfo.Split(new string[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
                String newInterface = sArray[1];
                SweetInterface = newInterface;
                resultContent = "接口成功修改为" + SweetInterface;
                this.SendMessage(e, resultContent);
            }



        }

        #endregion
        
        private void GroupMsg(object sender,ReceiveClusterIMQQEventArgs e)
        {

            String QQgroupInfo = e.Cluster.ToString();
            string[] sArray = QQgroupInfo.Split(new string[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
            uint QQGroupNumber = uint.Parse(sArray[1]);
            String temp = e.Message.ToString();

            if (e.Message == "测试")
                SendClusterMessage(e.Cluster, e.ClusterMember, "测试成功！");


            if (QQGroupNumber == 546726116 || QQGroupNumber == 372472368)
            {
                if (e.Message.IndexOf("dwz") > -1)
                {

                    String contentRules = ".*?http.{0,10}dwz.cn..(.{6}).*?";
                    String newContentRules = ".*?http.{0,10}dwz.cn(.*)";
                    temp = e.Message;

                    if (e.Message.Contains(":"))
                    {
                        //新的链接
                        temp = RulesPackage(newContentRules, temp);
                        temp = System.Text.RegularExpressions.Regex.Unescape(temp).Trim();
                    }
                    else
                    {
                        temp = RulesPackage(contentRules, temp);
                        temp = "/" + System.Text.RegularExpressions.Regex.Unescape(temp).Trim();
                    }



                    GetSweet("http://dwz.cn" + temp);
                    GetSweetReply();


                    //               String temp2 = "http://dwz.cn/3sENri";
                    String test = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><msg serviceID=\"1\" templateID=\"1\" action=\"plugin\" brief=\"[Boom]\" sourcePublicUin=\"\" sourceMsgId=\"0\"  flag=\"2\" sType=\"\"> <item layout=\"0\"> <summary  size=\"30\" color=\"#FF0000\">用户名:" + PlayerName + "</summary><title size=\"30\" color=\"#7D26CD\">" + resultContent + "</title><hr hidden=\"false\"/><title size=\"30\" color=\"#2F4F2F\">欢迎使用西瓜机器人</title><title size=\"20\" color=\"#2F4F2F\">" + ads + "</title><hr hidden=\"false\"/><hr/></item><source name=\"萌萌哒小西瓜\" icon=\"http://dwz.cn/3jjnxQ\" action=\"\" appid=\"1\"/></msg>";
                    // String test = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><msg serviceID=\"1\" templateID=\"1\" action=\"plugin\" brief=\"棒棒糖到账信息\"sourcePublicUin=\"\" sourceMsgId=\"0\"  flag=\"2\" sType=\"\"> <item layout=\"0\"> <summary  size=\"30\" color=\"#FF0000\">用户名:" + PlayerName + "</summary><title size=\"30\" color=\"#7D26CD\">" + resultContent + "</title><hr hidden=\"false\"/><title size=\"30\" color=\"#2F4F2F\">欢迎使用西瓜机器人</title><title size=\"20\" color=\"#2F4F2F\">" + ads + "</title></item><hr hidden=\"false\" /></hr><item layout=\"0\"><button action=\"plugin\" url=\"http://dwz.cn/3sENri\">点击联系</button></item><source name=\"萌萌哒小西瓜\" icon=\"http://dwz.cn/3jjnxQ\"  url=\"http://dwz.cn/3sENri\"  action=\"plugin\" appid=\"0\"/></msg>";
                    this.SendClusterMessage(e.Cluster, e.ClusterMember, test);

                }

            }


//时间限制
            else if (e.Message.IndexOf("dwz") > -1 && isOpenSweet == 0)
            {
                resultContent = "现在无法使用刷棒棒糖功能！！可能情况为以下两点\n一、刷棒棒糖开放时间为8:00-22:00\n二、服务器阻塞，耐心等待~群主抢修中....\n\n如果急需刷棒棒糖请进入以下网站自行刷糖：\n http://www.54qiu.cn/ \n http://xzfuli.cn/";

                this.SendClusterMessage(e.Cluster, e.ClusterMember, resultContent);
            }
            else if (e.Message.IndexOf("dwz") > -1 && isOpenSweet == 1 && QQGroupNumber != 546726116)
            {

                String contentRules = ".*?http.{0,10}dwz.cn..(.{6}).*?";
                String newContentRules = ".*?http.{0,10}dwz.cn(.*)";
                temp = e.Message;

                if (e.Message.Contains(":"))
                {
                    //新的链接
                    temp = RulesPackage(newContentRules, temp);
                    temp = System.Text.RegularExpressions.Regex.Unescape(temp).Trim();
                }
                else
                {
                    temp = RulesPackage(contentRules, temp);
                    temp = "/" + System.Text.RegularExpressions.Regex.Unescape(temp).Trim();
                }



                GetSweet("http://dwz.cn" + temp);
                GetSweetReply();


                //               String temp2 = "http://dwz.cn/3sENri";
                String test = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><msg serviceID=\"1\" templateID=\"1\" action=\"plugin\" brief=\"[Boom]\" sourcePublicUin=\"\" sourceMsgId=\"0\"  flag=\"2\" sType=\"\"> <item layout=\"0\"> <summary  size=\"30\" color=\"#FF0000\">用户名:" + PlayerName + "</summary><title size=\"30\" color=\"#7D26CD\">" + resultContent + "</title><hr hidden=\"false\"/><title size=\"30\" color=\"#2F4F2F\">欢迎使用西瓜机器人</title><title size=\"20\" color=\"#2F4F2F\">" + ads + "</title><hr hidden=\"false\"/><hr/></item><source name=\"萌萌哒小西瓜\" icon=\"http://dwz.cn/3jjnxQ\" action=\"\" appid=\"1\"/></msg>";
                // String test = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><msg serviceID=\"1\" templateID=\"1\" action=\"plugin\" brief=\"棒棒糖到账信息\"sourcePublicUin=\"\" sourceMsgId=\"0\"  flag=\"2\" sType=\"\"> <item layout=\"0\"> <summary  size=\"30\" color=\"#FF0000\">用户名:" + PlayerName + "</summary><title size=\"30\" color=\"#7D26CD\">" + resultContent + "</title><hr hidden=\"false\"/><title size=\"30\" color=\"#2F4F2F\">欢迎使用西瓜机器人</title><title size=\"20\" color=\"#2F4F2F\">" + ads + "</title></item><hr hidden=\"false\" /></hr><item layout=\"0\"><button action=\"plugin\" url=\"http://dwz.cn/3sENri\">点击联系</button></item><source name=\"萌萌哒小西瓜\" icon=\"http://dwz.cn/3jjnxQ\"  url=\"http://dwz.cn/3sENri\"  action=\"plugin\" appid=\"0\"/></msg>";
                this.SendClusterMessage(e.Cluster, e.ClusterMember, test);
            }

        }
    }
}
