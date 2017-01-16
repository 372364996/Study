using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThoughtWorks.QRCode.Codec;

namespace qrcode.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult QrCode()
        {
            ViewBag.Message = "我要生成一个二维码";
            QRCodeEncoder qrcode = new QRCodeEncoder();
            //二维码背景颜色
            qrcode.QRCodeBackgroundColor = Color.White;
            //二维码的编码方式
            qrcode.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //每个小方格的宽度
            qrcode.QRCodeScale = 10;
            //二维码版本号
            qrcode.QRCodeVersion = 5;
            //纠错等级
            qrcode.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            string url = "http://www.baidu.com";
            Bitmap bitmap = qrcode.Encode(url);
            string strSaveDir = Request.MapPath("/QRcode/");
            if (!Directory.Exists(strSaveDir))
            {
                Directory.CreateDirectory(strSaveDir);
            }
            string strSavePath = Path.Combine(strSaveDir,"QrCode.png");
            if (!System.IO.File.Exists(strSavePath))
            {
                bitmap.Save(strSavePath);
            }
            ViewBag.img = strSavePath;
            return View();
        }
    }
}