namespace WinFormsApp1;
using System.Drawing.Imaging;
using System.Windows.Forms;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {
        String name1 = textBox1.Text;
        String name2 = textBox2.Text;
        String name3 = textBox3.Text;
        String name4 = textBox4.Text;
        String[] names = new string[4];
        names[0] = name1;
        names[1] = name2;
        names[2] = name3;
        names[3] = name4;
        Array.Sort(names);
        label1.Text = names[0];
        label2.Text = names[1];
        label3.Text = names[2];
        label4.Text = names[3];

    }

    private void button3_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void textBox4_TextChanged(object sender, EventArgs e)
    {

    }

    private void textBox3_TextChanged(object sender, EventArgs e)
    {

    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {

    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {

    }

    private void button2_Click(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"; // Filter for image files

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            pictureBox1.Image = new Bitmap(ofd.FileName); // Display image in PictureBox
        }
    }


    private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
    {
        Bitmap bmp = new Bitmap(pictureBox1.Image);
        pictureBox1.Image = bmp;
        Color col = bmp.GetPixel(e.X, e.Y);
        int red = col.R;
        int green = col.G;
        int blue = col.B;

        MessageBox.Show($"Red: {red}, Green: {green}, Blue: {blue}", "Pixel Color");

    }

    private void button4_Click(object sender, EventArgs e)
    {
        Bitmap bmp = new Bitmap(pictureBox1.Image);
        for (int i = 0; i < bmp.Width; i++)
        {
            for (int j = 0; j < bmp.Height; j++)
            {
                Color color = bmp.GetPixel(i, j);
                int red = color.R;
                int green = color.G;
                int blue = color.B;
                int avg = (red + green + blue) / 3;
                red = avg + 50;
                red = 255;

                bmp.SetPixel(i, j, Color.FromArgb(red, avg, avg));


            }
        }
        pictureBox1.Image = bmp;
    }

    private void button5_Click(object sender, EventArgs e)
    {
        Bitmap bmp = new Bitmap(pictureBox1.Image);
        for (int i = 0; i < bmp.Width; i++)
        {
            for (int j = 0; j < bmp.Height; j++)
            {
                Color color = bmp.GetPixel(i, j);
                int red = 255 - color.R;
                int green = 255 - color.G;
                int blue = 255 - color.B;
                bmp.SetPixel(i, j, Color.FromArgb(red, green, blue));



            }
        }
        pictureBox1.Image = bmp;
    }

    private void button6_Click(object sender, EventArgs e)
    {

        Bitmap bmp = new Bitmap(pictureBox1.Image);
        Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
        for (int i = 1; i < bmp.Width - 1; i++)
        {
            for (int j = 1; j < bmp.Height - 1; j++)
            {
                int total = Math.Abs(bmp.GetPixel(i, j).R - bmp.GetPixel(i - 1, j - 1).R) +
                     Math.Abs(bmp.GetPixel(i, j).G - bmp.GetPixel(i - 1, j - 1).G) +
                     Math.Abs(bmp.GetPixel(i, j).B - bmp.GetPixel(i - 1, j - 1).B);
                if (total > 120)
                {
                    bmp2.SetPixel(i, j, Color.Black);

                }
            }

        }
        pictureBox1.Image = bmp2;

    }

    //Convert the image into text 
    private void button7_Click(object sender, EventArgs e)
    {
        string filename = "./file.txt";

        if (pictureBox1.Image == null)
        {
            MessageBox.Show("No image selected.", "Error");
            return;
        }

        Bitmap bmp = new Bitmap(pictureBox1.Image);
        Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);

        List<string> pixelData = new List<string>(); // Store pixel data for writing later

        for (int i = 1; i < bmp.Width - 1; i++)
        {
            for (int j = 1; j < bmp.Height - 1; j++)
            {
                int total = Math.Abs(bmp.GetPixel(i, j).R - bmp.GetPixel(i - 1, j - 1).R) +
                            Math.Abs(bmp.GetPixel(i, j).G - bmp.GetPixel(i - 1, j - 1).G) +
                            Math.Abs(bmp.GetPixel(i, j).B - bmp.GetPixel(i - 1, j - 1).B);

                pixelData.Add($"Pixel({i}, {j}): {total}");

                bmp2.SetPixel(i, j, total > 120 ? Color.Black : Color.White);
            }
        }

        File.WriteAllLines(filename, pixelData);

        pictureBox1.Image = bmp2;
    }

    private void button8_Click(object sender, EventArgs e)
    {
        Bitmap bmp = new Bitmap(pictureBox1.Image);
        BitmapData data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height)
            , ImageLockMode.ReadWrite
            , PixelFormat.Format24bppRgb
            );
        System.IntPtr s = data.Scan0;
        unsafe
        {
            byte* p = (byte*)s;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    int blue = p[0];
                    int green = p[1];
                    int red = p[2];
                    int avg = (blue + green + red) / 3;
                    p[0] = p[1] = p[2] = (byte)avg;
                    p += 3;
                }
            }
            bmp.UnlockBits(data);
            pictureBox1.Image = bmp;
        }
    }

    private void button9_Click(object sender, EventArgs e)
    {
        Bitmap bmp = new Bitmap(pictureBox1.Image);
        //empty bitmap for sketch
        Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
        BitmapData data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb
            );
        //empty data2 for sketch
        BitmapData data2 = bmp2.LockBits(
           new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite,
           PixelFormat.Format24bppRgb
           );
        System.IntPtr s = data.Scan0;
        //system pointer pointing to empty bitmap
        System.IntPtr s2 = data2.Scan0;
        int mw = data.Stride;
        //built in variable for 3 times width of image
        unsafe
        {
            byte* p = (byte*)s;
            //byte pointer pointing to empty bitmap
            byte* p2 = (byte*)s2;
            for (int i = 1; i < bmp.Width - 1; i++)
            {
                for (int j = 1; j < bmp.Height - 1; j++)
                {
                    int total = Math.Abs(p[0] - p[3 + mw]) +
                         Math.Abs(p[1] - p[4 + mw]) +
                         Math.Abs(p[2] - p[5 + mw]);
                    if (total > 120)
                    {
                        p2[3 + mw] = p2[4 + mw] = p2[5 + mw] = 255;
                    }
                    p += 3;
                    p2 += 3;
                }
            }
            bmp.UnlockBits(data);
            bmp2.UnlockBits(data2);
            pictureBox1.Image = bmp2;
        }
    
    }

    private void button10_Click(object sender, EventArgs e)
    {
        Bitmap bmp = new Bitmap(pictureBox1.Image);
        BitmapData data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height)
            , ImageLockMode.ReadWrite
            , PixelFormat.Format24bppRgb
            );
        System.IntPtr s = data.Scan0;
        unsafe
        {
            byte* p = (byte*)s;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    int blue = 255 - p[0];
                    int green = 255 - p[1];
                    int red = 255 - p[2];
                    p[0] = (byte)blue;
                    p[1] = (byte)green;
                    p[2] = (byte)red;
                    p += 3;
                }
            }
            bmp.UnlockBits(data);
            pictureBox1.Image = bmp;
        }
    }
}