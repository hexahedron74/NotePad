using Microsoft.Win32;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Noting_
{
    static class Program
    {
        [DllImport("Shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread] 
        static void Main(string[] args)
        {
            if(!IsAssociated())
            {

            }
            else
            {
                Associate();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length == 0)
            {
                Application.Run(new Form2());
            }
            else
            {
                Application.Run(new Form2(args[0]));
            }
            
        }

        public static bool IsAssociated()
        {
            return(Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.txt", false) == null);
        }

        public static void Associate()
        {
            RegistryKey FileReg = Registry.CurrentUser.CreateSubKey("Software\\Classes\\.txt");
            RegistryKey AppReg = Registry.CurrentUser.CreateSubKey("Software\\Classes\\Applications\\Noting!.exe");
            RegistryKey AppAssoc = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.txt");

            FileReg.CreateSubKey("DefaultIcon").SetValue("", "D:\\Program Resources\\take a note\\note.ico");
            FileReg.CreateSubKey("PercievedType").SetValue("", "Text");

            AppReg.CreateSubKey("Shell\\open\\command").SetValue("", "\"" + Application.ExecutablePath + "\" %1");
            AppReg.CreateSubKey("Shell\\edit\\command").SetValue("", "\"" + Application.ExecutablePath + "\" %1");
            AppReg.CreateSubKey("DefaultIcon").SetValue("", "D:\\Program Resources\\take a note\\note.ico");

            AppAssoc.CreateSubKey("UserChoice").SetValue("Progid", "Applications\\Noting!.exe");
            SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
