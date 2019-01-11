using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Text;
using System.Drawing.Imaging;

namespace SitioBase.clases
{
    /// <summary>
    /// Summary description for Captcha
    /// </summary>
    public class Captcha
    {
        private static Random rand = new Random();
        //string static strTextoSinFormato = string.Empty;
        public static Bitmap CreateImage(string pTexto)
        {
            string code = pTexto;// GetRandomText();
            Bitmap bitmap = new Bitmap(180, 70, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Color.Yellow);
            Rectangle rect = new Rectangle(0, 0, 180, 70);
            SolidBrush b = new SolidBrush(Color.DarkKhaki);
            SolidBrush blue = new SolidBrush(Color.Blue);
            int counter = 0;
            g.DrawRectangle(pen, rect);
            g.FillRectangle(b, rect);
            g.FillRectangle(new SolidBrush(Color.White), rect);
            SolidBrush SoColor;
            int randColor = rand.Next(0, 6);
            int randFuente = rand.Next(0, 6);
            for (int i = 0; i < code.Length; i++)
            {
                int ranAux = rand.Next(0, 6);
                while (randColor == ranAux)
                {
                    ranAux = rand.Next(0, 6);
                }
                randColor = ranAux;
                switch (randColor.ToString())
                {
                    case "0":
                        SoColor = new SolidBrush(Color.Green);
                        break;
                    case "1":
                        SoColor = new SolidBrush(Color.Black);
                        break;
                    case "2":
                        SoColor = new SolidBrush(Color.Red);
                        break;
                    case "3":
                        SoColor = new SolidBrush(Color.Maroon);
                        break;
                    case "4":
                        SoColor = new SolidBrush(Color.Orange);
                        break;
                    case "5":
                        SoColor = new SolidBrush(Color.DodgerBlue);
                        break;
                    case "6":
                        SoColor = new SolidBrush(Color.Yellow);
                        break;
                    default:
                        SoColor = new SolidBrush(Color.Blue);
                        break;
                }

                int ranAuxFuenta = rand.Next(0, 6);
                while (randFuente == ranAuxFuenta)
                {
                    ranAuxFuenta = rand.Next(0, 6);
                }
                randFuente = ranAuxFuenta;
                int Tamaño = 10 + rand.Next(6, 25);
                switch (rand.Next(0, 6).ToString())
                {
                    case "0":
                        g.DrawString(code[i].ToString(), new Font("Verdena", Tamaño, FontStyle.Italic), SoColor, new PointF(10 + counter, 10));
                        break;
                    case "1":
                        g.DrawString(code[i].ToString(), new Font("Lucida Fax", Tamaño), SoColor, new PointF(10 + counter, 10));
                        break;
                    case "2":
                        g.DrawString(code[i].ToString(), new Font("Arial", Tamaño), SoColor, new PointF(10 + counter, 10));
                        break;
                    case "3":
                        g.DrawString(code[i].ToString(), new Font("Times New Roman", Tamaño, FontStyle.Bold), SoColor, new PointF(10 + counter, 10));
                        break;
                    case "4":
                        g.DrawString(code[i].ToString(), new Font("Hobo Std", Tamaño, FontStyle.Italic), SoColor, new PointF(10 + counter, 10));
                        break;
                    case "5":
                        g.DrawString(code[i].ToString(), new Font("Tahoma", Tamaño), SoColor, new PointF(10 + counter, 10));
                        break;
                    case "6":
                        g.DrawString(code[i].ToString(), new Font("Broadway", Tamaño, FontStyle.Regular), SoColor, new PointF(10 + counter, 10));
                        break;
                    default:
                        g.DrawString(code[i].ToString(), new Font("Verdena", Tamaño, FontStyle.Regular), SoColor, new PointF(10 + counter, 10));
                        break;
                }
                if (Tamaño >= 18)
                {
                    counter += rand.Next(18, Tamaño);
                }
                else
                {
                    counter += rand.Next(20, 25);
                }

            }
            DrawRandomLines(g);
            HttpContext.Current.Response.ContentType = "image/gif";
            bitmap.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Gif);

            return bitmap;
        }
        public static string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "abcdefghijklmnopqrstuvwxyz";
            Random r = new Random();
            for (int j = 0; j <= 5; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            string strTextoSinFormato = randomText.ToString();
            return strTextoSinFormato;
        }
        private static void DrawRandomLines(Graphics g)
        {
            SolidBrush SoColor = new SolidBrush(Color.Green);
            int randColor = rand.Next(0, 6);
            for (int i = 0; i < 30; i++)
            {
                int ranAux = rand.Next(0, 6);
                while (randColor == ranAux)
                {
                    ranAux = rand.Next(0, 6);
                }
                randColor = ranAux;
                switch (randColor.ToString())
                {
                    case "0":
                        SoColor = new SolidBrush(Color.BlueViolet);
                        break;
                    case "1":
                        SoColor = new SolidBrush(Color.Black);
                        break;
                    case "2":
                        SoColor = new SolidBrush(Color.DeepPink);
                        break;
                    case "3":
                        SoColor = new SolidBrush(Color.DodgerBlue);
                        break;
                    case "4":
                        SoColor = new SolidBrush(Color.MediumPurple);
                        break;
                    case "5":
                        SoColor = new SolidBrush(Color.DodgerBlue);
                        break;
                    case "6":
                        SoColor = new SolidBrush(Color.Bisque);
                        break;
                    default:
                        SoColor = new SolidBrush(Color.Tan);
                        break;
                }

                g.DrawLines(new Pen(SoColor, 1), GetRandomPoints());
            }
        }
        private static Point[] GetRandomPoints()
        {
            Point punto = new Point(rand.Next(0, 150), rand.Next(0, 150));
            Point[] points = { punto, new Point(punto.X + rand.Next(1, 10), punto.Y + rand.Next(1, 10)) };
            return points;
        }

    }
    public class ReCaptchaClass
    {
        static string PrivateKey = System.Configuration.ConfigurationManager.AppSettings["reCAPTCHA_ClaveSecreta"];
        public static bool Validate(string EncodedResponse)
        {
            bool result = false;
            try
            {
                var client = new System.Net.WebClient();
                string GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));
                if (!string.IsNullOrEmpty(GoogleReply) && GoogleReply.ToLower().Contains("true"))
                    result = true;
            }
            catch (Exception ex)
            {
                // throw;
            }
            return result;
        }
        public string success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }

        private string m_Success;
        private List<string> m_ErrorCodes;
    }
}