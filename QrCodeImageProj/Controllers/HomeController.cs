using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QRCoder;

namespace QrCodeImageProj.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QrCodeImage(string str)
        {
            string QrUri = GetQrCodeImageByString(str);
            ViewBag.QrCodeUri = QrUri;
            return View();
        }

        private string GetQrCodeImageByString(string str)
        {
            string QrUri = "";

            if (!string.IsNullOrWhiteSpace(str))
            {
                QRCodeGenerator QrGenerator = new QRCodeGenerator();
                QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(str, QRCodeGenerator.ECCLevel.Q);
                QRCode QrCode = new QRCode(QrCodeInfo);
                Bitmap QrBitmap = QrCode.GetGraphic(60);

                MemoryStream ms = new MemoryStream();
                QrBitmap.Save(ms, ImageFormat.Jpeg);
                byte[] BitmapArray = ms.ToArray();
                QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
            }

            return QrUri;
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
    }
}