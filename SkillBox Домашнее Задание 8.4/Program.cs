using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SkillBox_Домашнее_Задание_8._4
{
    class Program
    {
        static void Main(string[] args)
        {
            
            UserInfo Person = new UserInfo();
            Person.xData();
            Person.ParseData();
            
           
        }
    }
}
