using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InputsViewer
{
    internal class ButtonInterface
    {
        public String synonim;
        public String upStatePicString;
        public String downStatePicString;
        public Image upStateImage;
        public Image downStateImage;
        public Keys key;
        public PictureBox pBox;
 

        public ButtonInterface(String syn,String up,String down,Keys k, PictureBox pb)
        {
            synonim = syn;
            upStatePicString = up;
            downStatePicString = down;
            key = k;
            pBox = pb;
            LoadImages();
            pBox.Image = upStateImage;
        }

        public void  LoadImages()
        {
            upStateImage = Image.FromFile(@"C:\\Новая папка\" + upStatePicString + ".png");
            downStateImage = Image.FromFile(@"C:\\Новая папка\" + downStatePicString + ".png");
        }

        public String ButToString()
        {
            String s = synonim + " " + upStatePicString + " " + downStatePicString + " " + key.ToString();
            return s;
        }

    }
}
