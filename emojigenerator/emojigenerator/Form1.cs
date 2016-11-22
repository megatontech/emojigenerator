using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace emojigenerator
{
    public partial class UCCU : Form
    {
        public UCCU()
        {
            InitializeComponent();
            this.pictureBox1.AllowDrop = true;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.DragDrop += new DragEventHandler(pictureBox1_DragDrop);
            this.pictureBox1.DragEnter += new DragEventHandler(pictureBox1_DragEnter);
        }

        /// <summary>
        /// clear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DisposeImage();
        }

        /// <summary>
        /// save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (pictureBox1.Image != null)
            {
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                if (this.Jpeg.Checked) { bmp.Save(dir + "\\" + Guid.NewGuid().ToString().Replace("-", "").Substring(0,4) + ".jpg", ImageFormat.Jpeg); }
                if (this.Gif.Checked) { bmp.Save(dir + "\\" + Guid.NewGuid().ToString().Replace("-", "").Substring(0,4) + ".gif", ImageFormat.Gif); }
                if (this.Png.Checked) { bmp.Save(dir + "\\" + Guid.NewGuid().ToString().Replace("-", "").Substring(0,4) + ".png", ImageFormat.Png); }
                if (this.Bmp.Checked) { bmp.Save(dir + "\\" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 4) + ".bmp", ImageFormat.Bmp); }
            }
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                String[] strfileNames =
                    (String[])e.Data.GetData(DataFormats.FileDrop);
                Image dragImage = Image.FromFile(strfileNames[0]);
                if (dragImage != null)
                {
                    this.DisposeImage();
                    this.pictureBox1.Image = ZoomImage(dragImage);
                }
            }
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            bool bflag = e.Data.GetDataPresent(DataFormats.FileDrop);
            e.Effect = DragDropEffects.Copy;
        }

        #region Methods

        /// <summary>
        /// 销毁
        /// </summary>
        public void DisposeImage()
        {
            if (this.pictureBox1.Image != null)
            {
                this.pictureBox1.Image.Dispose();
                this.pictureBox1.Image = null;
            }
        }

        /// <summary>
        /// 缩放
        /// </summary>
        /// <param name="oldImage"></param>
        /// <returns></returns>
        private Image ZoomImage(Image oldImage)
        {
            if (null == oldImage)
            {
                return null;
            }
            int width = oldImage.Width;
            int height = oldImage.Height;
            int newWidth = 0;
            int newHeight = 0;
            float ratioXY = (float)width / (float)height;
            if ((width > this.pictureBox1.Width) ||
                 (height > this.pictureBox1.Height))
            {
                if (ratioXY >= 1.0)
                {
                    newWidth = this.pictureBox1.Width;
                    newHeight = (int)((float)newWidth / ratioXY) + 1;
                }
                else
                {
                    newHeight = this.pictureBox1.Height;
                    newWidth = (int)(newHeight * ratioXY);
                }
                Bitmap bitmap = new Bitmap(newWidth,
                     newHeight, oldImage.PixelFormat);
                Graphics g = Graphics.FromImage(bitmap);
                g.Clear(Color.Transparent);
                g.DrawImage(oldImage,
                    new RectangleF(0, 0, newWidth, newHeight));
                return Image.FromHbitmap(bitmap.GetHbitmap());
            }
            return oldImage;
        }

        #endregion Methods
    }
}