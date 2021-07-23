using System.Drawing;
using System.Windows.Media.Imaging;

namespace Laboratory.Classes
{
    class Captcha
    {
        public int Width;
        public int Height;
        public static string Text;
        public Bitmap Image;

        public Captcha(string text, int width, int height)
        {
            Text = text;
            Width = width;
            Height = height;
            GenerateImage();
        }

        private void GenerateImage()
        {
            Bitmap bitmap = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                graphics.DrawString(Text, new Font("Trebuchet MS", Height / 2, FontStyle.Strikeout), Brushes.Red, new Rectangle(0, 0, Width, Height), format);
            }

            Image = bitmap;
        }


    }
}
