using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBox_Домашнее_Задание_8._3
{
    struct HashSet
    {
        private int usernumber;
        public int UserNumber
        {
            get {return this.usernumber;}
            set { this.usernumber = value;}
        }
        public int GetUserNumber()
        {
            Console.Clear();
            begin1:
            Console.Write($"Число: ");
            if (!int.TryParse(Console.ReadLine(),out int UserNumber))
            {
                Console.WriteLine($"Неккоректный ввод!!!Только числа!!!"); goto begin1;
            }
            return UserNumber;
        }
        private bool Status;
        public bool CheckStatus()
        {
            if (Status=NumbersUser.Contains(UserNumber))
            {
                Console.WriteLine($"\n\"Число Уже Добавлялось");
            }
            Console.WriteLine($"\n\"Число Успешно Сохранено");
            return Status;
        }
        private HashSet<int> NumbersUser;
        public HashSet<int> CollectionNumber()
        {
            if (NumbersUser == null) NumbersUser = new HashSet<int>();
            CheckStatus();
            if (!Status) NumbersUser.Add(UserNumber);
            return NumbersUser;
        }
    }
}
