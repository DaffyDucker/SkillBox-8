using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace SkillBox_Домашнее_Задание_8._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Methods numb = new Methods();
            numb.Numbers();
            numb.Print();
            numb.Range();
            numb.Print();
        }
    }
}
