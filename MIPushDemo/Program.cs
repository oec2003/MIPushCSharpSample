using com.xiaomi.xmpush.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIPushCSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Constants.useOfficial();//正式环境
                //Constants.useSandbox();//测试环境，只针对IOS
                string messagePayload = "这是一个消息";
                string title = "通知标题";
                string description = "通知说明" + DateTime.Now;

                #region 安卓发送

                Sender androidSender = new Sender("YNaLUDPuBZSmNgrtaptqBw==");//你的AppSecret

                com.xiaomi.xmpush.server.Message androidMsg = new com.xiaomi.xmpush.server.Message.Builder()
                    .title(title)
                    .description(description)//通知栏展示的通知描述
                    .payload(messagePayload)//透传消息
                    .passThrough(0)//设置是否透传1:透传, 0通知栏消息
                    .notifyId(new java.lang.Integer(Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds)))//取时间戳，避免通知覆盖
                    .restrictedPackageName("com.ican.smartspace")//包名
                    .notifyType(new java.lang.Integer(1)) //使用默认提示音提示
                    .notifyType(new java.lang.Integer(2)) //使用默认震动
                    .notifyType(new java.lang.Integer(3)) //使用默认LED灯光
                    .timeToLive(3600000 * 336)//服务器默认保留两周（毫秒）
                    .extra("data", "测试extra11111")//字符数不能超过1024最多十组
                    .build();
                //广播
                com.xiaomi.xmpush.server.Result androidPushResult = androidSender.broadcastAll(androidMsg, 3);

                //针对每个用户注册的registerid
                string regId = "";
                com.xiaomi.xmpush.server.Result androidPushResult1 = androidSender.send(androidMsg, regId, 3);
                #endregion
                //result.rows = androidPushResult;
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
