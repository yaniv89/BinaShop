using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BinaShop.Core.Models
{
    public class PayPalLogger
    {
        public static string LogDirectoryPath=Environment.CurrentDirectory;
        public static void Log(String messages)
        {
            try
            {
                StreamWriter strw = new StreamWriter(LogDirectoryPath + "//PayPalError.log", true);
                strw.WriteLine("{0}---->{1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") , messages);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
