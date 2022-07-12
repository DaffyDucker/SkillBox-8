using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace SkillBox_Домашнее_Задание_8_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Owners dict = new Owners();
            dict.LoadDb();
            dict.Menu();


        }
    }
}
