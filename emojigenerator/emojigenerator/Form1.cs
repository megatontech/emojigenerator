using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace emojigenerator
{
    
    public partial class UCCU : Form
    {
        private FormTextLabel label1 = new FormTextLabel();
        private FormTextLabel label2 = new FormTextLabel();
        private FormTextLabel label3 = new FormTextLabel();
        private bool isDrag = false;
        private Control currentMoveControl = new Control();
        private Point currcur = new Point();
        private Point oldcur = new Point();
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
                if (this.Jpeg.Checked) { bmp.Save(dir + "\\" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 4) + ".jpg", ImageFormat.Jpeg); }
                if (this.Gif.Checked) { bmp.Save(dir + "\\" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 4) + ".gif", ImageFormat.Gif); }
                if (this.Png.Checked) { bmp.Save(dir + "\\" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 4) + ".png", ImageFormat.Png); }
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextLabel1.Text = this.textBox1.Text.Trim();
            label1.text = this.textBox1.Text.Trim();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextLabel2.Text = this.textBox2.Text.Trim();
            label2.text = this.textBox2.Text.Trim();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            TextLabel3.Text = this.textBox3.Text.Trim();
            label3.text = this.textBox3.Text.Trim();
        }

        private void UCCU_MouseDown(object sender, MouseEventArgs e)
        {
            isDrag = true; currcur = new Point(e.X, e.Y);
            if (sender is Label) { ((Label)sender).BackColor = Color.DarkRed; currentMoveControl = ((Label)sender); }
            currcur = currentMoveControl.Location ;
            oldcur = e.Location;
        }

        private void UCCU_MouseUp(object sender, MouseEventArgs e)
        {
            isDrag = false;
            currcur = new Point((Cursor.Position.X), (Cursor.Position.Y));
            Label l = new Label();
            l.Location = currcur;
            this.Controls.Add(l);
            l.Text = currentMoveControl.Text+"~!!";
            if (currentMoveControl.Name.Contains("1")) { TextLabel1.Location = currcur; label1.xPos = currcur.X; label1.yPos = currcur.Y; }
            if (currentMoveControl.Name.Contains("2")) { TextLabel2.Location = currcur; label2.xPos = currcur.X; label2.yPos = currcur.Y; }
            if (currentMoveControl.Name.Contains("3")) { TextLabel3.Location = currcur; label3.xPos = currcur.X; label3.yPos = currcur.Y; }
            currcur.X = currentMoveControl.Location.X;
            currcur.Y = currentMoveControl.Location.Y;
            this.Refresh();
        }

        private void UCCU_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left)
            {
                Point pTemp = new Point(Cursor.Position.X, Cursor.Position.Y);
                pTemp = this.PointToClient(pTemp);
                currentMoveControl.Location = new Point(pTemp.X - currcur.X, pTemp.Y - currcur.Y);

                this.Refresh();
            }
        }

        #region Methods

        private Point getPointToForm(Point p)
        {
            return p;
        }
        
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
    /// <summary>
    /// 显示文本
    /// </summary>
    public class FormTextLabel
    {
        public string font { get; set; }
        public bool isVertical { get; set; }
        public int opcaity { get; set; }
        public int size { get; set; }
        public string text { get; set; }
        public int xPos { get; set; }
        public int yPos { get; set; }
    }

}