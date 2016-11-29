using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace emojigenerator
{
    
    public partial class UCCU : Form
    {
        private FormTextLabel label1 = new FormTextLabel() { font= "Verdana", color = Color.Black, size = 16, xPos = 0f,yPos=0f,text="" };
        private FormTextLabel label2 = new FormTextLabel() { font = "Verdana", color = Color.Black, size = 16, xPos = 0f, yPos = 0f, text = "" };
        private FormTextLabel label3 = new FormTextLabel() { font = "Verdana", color = Color.Black, size = 16, xPos = 0f, yPos = 0f, text = "" };
        private Control currentSelLabel = new Control();
        private Point realImageSize = new Point();
        public UCCU()
        {
            InitializeComponent();
            this.pictureBox1.AllowDrop = true;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.DragDrop += new DragEventHandler(pictureBox1_DragDrop);
            this.pictureBox1.DragEnter += new DragEventHandler(pictureBox1_DragEnter);
        }
        
        #region Event
        /// <summary>
        /// clear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DisposeImage();
            currentSelLabel = new Control();
            label1 = new FormTextLabel() { font = "Verdana", color = Color.Black, size = 16, xPos = 0f, yPos = 0f, text = "" };
            label2 = new FormTextLabel() { font = "Verdana", color = Color.Black, size = 16, xPos = 0f, yPos = 0f, text = "" };
            label3 = new FormTextLabel() { font = "Verdana", color = Color.Black, size = 16, xPos = 0f, yPos = 0f, text = "" };
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
                System.Drawing.Image image = pictureBox1.Image;
                if (label1.text.Trim().Length > 0 && label1.xPos > 0 && label1.yPos > 0)
                {
                    Graphics g = Graphics.FromImage(image);
                    g.DrawImage(image, 0, 0, image.Width, image.Height);
                    Font f = new Font(label1.font, label1.size);
                    Brush b = new SolidBrush(label1.color);
                    g.DrawString(label1.text.Trim(), f, b, label1.xPos, label1.yPos);
                    g.Dispose();
                }
                if (label2.text.Trim().Length > 0 && label2.xPos > 0 && label2.yPos > 0)
                {
                    Graphics g = Graphics.FromImage(image);
                    g.DrawImage(image, 0, 0, image.Width, image.Height);
                    Font f = new Font(label2.font, label2.size);
                    Brush b = new SolidBrush(label2.color);
                    g.DrawString(label2.text.Trim(), f, b, label2.xPos, label2.yPos);
                    g.Dispose();
                }
                if (label3.text.Trim().Length > 0 && label3.xPos>0&& label3.yPos>0)
                {
                    Graphics g = Graphics.FromImage(image);
                    g.DrawImage(image, 0, 0, image.Width, image.Height);
                    Font f = new Font(label3.font, label3.size);
                    Brush b = new SolidBrush(label3.color);
                    g.DrawString(label3.text.Trim(), f, b, label3.xPos, label3.yPos);
                    g.Dispose();
                }
                Bitmap bmp = new Bitmap(image);
                if (this.Jpeg.Checked) { bmp.Save(dir + "\\" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 4) + ".jpg", ImageFormat.Jpeg); }
                if (this.Gif.Checked) { bmp.Save(dir + "\\" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 4) + ".gif", ImageFormat.Gif); }
                if (this.Png.Checked) { bmp.Save(dir + "\\" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 4) + ".png", ImageFormat.Png); }
                if (this.Bmp.Checked) { bmp.Save(dir + "\\" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 4) + ".bmp", ImageFormat.Bmp); }
                MessageBox.Show("(＾o＾)ﾉ");
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
                    realImageSize.X = dragImage.Width;
                    realImageSize.Y = dragImage.Height;
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
            //isDrag = true; currcur = new Point(e.X, e.Y);
            //if (sender is Label) { ((Label)sender).BackColor = Color.DarkRed; currentMoveControl = ((Label)sender); }
            //currcur = currentMoveControl.Location ;
            //oldcur = e.Location;
        }

        private void UCCU_MouseUp(object sender, MouseEventArgs e)
        {
            //isDrag = false;
            //currcur = new Point((Cursor.Position.X), (Cursor.Position.Y));
            //Label l = new Label();
            //l.Location = currcur;
            //this.Controls.Add(l);
            //l.Text = currentMoveControl.Text+"~!!";
            //if (currentMoveControl.Name.Contains("1")) { TextLabel1.Location = currcur; label1.xPos = currcur.X; label1.yPos = currcur.Y; }
            //if (currentMoveControl.Name.Contains("2")) { TextLabel2.Location = currcur; label2.xPos = currcur.X; label2.yPos = currcur.Y; }
            //if (currentMoveControl.Name.Contains("3")) { TextLabel3.Location = currcur; label3.xPos = currcur.X; label3.yPos = currcur.Y; }
            //currcur.X = currentMoveControl.Location.X;
            //currcur.Y = currentMoveControl.Location.Y;
            //this.Refresh();
        }

        private void UCCU_MouseMove(object sender, MouseEventArgs e)
        {
            //if (e.Button==MouseButtons.Left)
            //{
            //    Point pTemp = new Point(Cursor.Position.X, Cursor.Position.Y);
            //    pTemp = this.PointToClient(pTemp);
            //    currentMoveControl.Location = new Point(pTemp.X - currcur.X, pTemp.Y - currcur.Y);

            //    this.Refresh();
            //}
        }
        /// <summary>
        /// set current sel label position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentSelLabel.Name == this.TextLabel1.Name)
            {
                label1.xPos = float.Parse(ProcessXPosition(e.X));
                label1.yPos = float.Parse(ProcessYPosition(e.Y));
                TextLabel1.BackColor = Color.Green;
            }
            if (currentSelLabel.Name == this.TextLabel2.Name)
            {
                currentSelLabel = this.TextLabel2;
                label2.xPos = float.Parse(e.X.ToString());
                label2.yPos = float.Parse(e.Y.ToString());
                TextLabel2.BackColor = Color.Green;
            }
            if (currentSelLabel.Name == this.TextLabel3.Name)
            {
                currentSelLabel = this.TextLabel3;
                label3.xPos = float.Parse(e.X.ToString());
                label3.yPos = float.Parse(e.Y.ToString());
                TextLabel3.BackColor = Color.Green;
            }
        }

        private void TextLabel1_Click(object sender, EventArgs e)
        {
            this.TextLabel1.BackColor = Color.Red;
            this.TextLabel2.BackColor = Color.Black;
            this.TextLabel3.BackColor = Color.Black;
            currentSelLabel = this.TextLabel1;
        }

        private void TextLabel2_Click(object sender, EventArgs e)
        {
            this.TextLabel1.BackColor = Color.Black;
            this.TextLabel2.BackColor = Color.Red;
            this.TextLabel3.BackColor = Color.Black;
            currentSelLabel = this.TextLabel2;
        }

        private void TextLabel3_Click(object sender, EventArgs e)
        {
            this.TextLabel1.BackColor = Color.Black;
            this.TextLabel2.BackColor = Color.Black;
            this.TextLabel3.BackColor = Color.Red;
            currentSelLabel = this.TextLabel3;
        }
        #endregion

        #region Methods
        public string ProcessXPosition(int pos)
        {
            double result = 0;
            double ratio = Math.Round( (double)pos / 400,4);
            result = ratio * realImageSize.X;
            return result.ToString();     
        }
        public string ProcessYPosition(int pos)
        {
            double result = 0;
            double ratio = Math.Round((double)pos / 300, 4);
            result = ratio * realImageSize.Y;
            return result.ToString();
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
        public Color color { get; set; }
        public string font { get; set; }
        public bool isVertical { get; set; }
        public int opcaity { get; set; }
        public int size { get; set; }
        public string text { get; set; }
        public float xPos { get; set; }
        public float yPos { get; set; }
    }

}