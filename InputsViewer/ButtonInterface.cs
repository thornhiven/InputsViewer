using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InputsViewer
{
    internal class ButtonInterface
    {
        public String synonim;
        public String upStatePic;
        public String downStatePic;
        public Keys key;

        public ButtonInterface(String syn,String up,String down,Keys k)
        {
            synonim = syn;
            upStatePic = up;
            downStatePic = down;
            key = k;
        }

        public String ButToString()
        {
            String s = synonim + " " + upStatePic + " " + downStatePic + " " + key.ToString();
            return s;
        }

    }
}
