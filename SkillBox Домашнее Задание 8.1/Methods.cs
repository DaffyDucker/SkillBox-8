using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SkillBox_Домашнее_Задание_8._1
{
    struct Methods
    {
        public List<int> numbers;
        public Random r;

        public List<int> Numbers()
        {
            numbers = new List<int>();
            r = new Random();
            for (int i = 0; i < 100; i++)
            {
                numbers.Add(r.Next(100));
            }
            return numbers;
        }

        public List<int> Range()
        {
            numbers.RemoveAll(numbers=>numbers >25&&numbers<50);
            return numbers;
        }
       
      

        public void Print ()
        {
            Console.Clear();
            foreach (var item in numbers)
            {
                Console.WriteLine(item);

            }

            Console.ReadLine();
        }
    }
}
