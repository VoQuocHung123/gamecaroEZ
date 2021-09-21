using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caro_VoQuocHung
{
    class Cls_QuocHung
    {
        public static Stack<Button> hightLight = new Stack<Button>();
        public static Stack<Button> undoPlay1 = new Stack<Button>();
        public static Stack<Button> undoPlay2 = new Stack<Button>();
        public static int playerX = 60;
        public static int playerO = 60;
        public static bool checkX = true;
        public static bool checkO = true;
        /*
        private string name;

        public string Name { get => name; set => name = value; }
        public Cls_QuocHung(String name)
        {
            this.Name = name;
        }
        */

    }
}
   
