using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
//using System.Text.Json;
//using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SkillBox_Домашнее_Задание_8_2
{
    struct Owners
    {
        public JObject maintree;
        private string lastmiddlename;
        public string LastMiddleName
        {
            get { return lastmiddlename; }
            set { lastmiddlename = value; }
        }
        public string GetName()
        {
            Console.Write($"ФИО :");
            LastMiddleName = Convert.ToString(Console.ReadLine());
            
            Console.Clear();
            return LastMiddleName;
        }
        private string phonenumber;
        private string str;
        public string PhoneNumber
        {
            get
            {
                if ((!(phonenumber.Contains("+7")) && (!string.IsNullOrWhiteSpace(phonenumber))))
                {
                    this.phonenumber = phonenumber.Insert(0, "+7");
                }
                return this.phonenumber;
            }

            set { this.phonenumber = value; }

        }
        public List<string> PhoneNumberList;
        public List<string> GetPhoneNumber()
        {
            PhoneNumberList = new List<string>();
        begin1:
            Console.Write($"Номер Телефона: +7");
            if (long.TryParse(Console.ReadLine(), out long numb)) this.PhoneNumber = numb.ToString();
            else
            {
                Console.WriteLine($"Неккоректный ввод!!!Только числа!!!"); goto begin1;
            }
            while (!(String.IsNullOrWhiteSpace(PhoneNumber)))
            {
                PhoneNumberList.Add(PhoneNumber);
            begin2:
                Console.Write($"Добавить новый номер(д/н)?");
                str = Console.ReadLine();
                if (str == "д") goto begin1;
                else if (str == "н") break;
                else Console.Write($"Неправильная команда!!!(д/н)"); goto begin2;
            }
            Console.Clear();
            return PhoneNumberList;
        }

        /// <summary>
        /// Метод для управления всеми процессами программы
        /// </summary>
        public void Menu()
        {
            
            Console.Clear();
            string answer = Console.ReadLine();
            Console.Clear();
            switch (answer)                 ///переключатель функций программы
            {
                case "New":
                    GetName();
                    GetPhoneNumber();
                    BookPhone();
                    Menu();
                    break;

                case "Search":
                    Search();
                    Menu();
                    break;
                case "Print":
                    Print();
                    Menu();
                    break;
                case "Save":
                    SaveDb();
                    Menu();
                    break;
                case "Close":
                    ActionBeforeClose();
                    break;
                default:
                    Console.WriteLine(
                                      "New - Добавить Новый Контакт \n" +
                                      "Close - Закрыть программу \n" +
                                      "Search - Поиск владельца по номеру  \n" +
                                      "Save - Сохранение данных в формате json  \n" +
                                      "Print - Отобразить Все Контакты \n"
                                      );
                    Menu();
                    break;

            }
        }
        public void Print()
        {
            foreach (KeyValuePair<List<string>, string> e in book)
            {
                Console.Write($"\nКонтакт :{e.Value} Моб.Телефон: ");
                foreach (var number in e.Key) Console.Write($" {number}");

            }
        }



        public void Search()
        {
            Console.Write($"Номер Телефона: +7");
            PhoneNumber = Convert.ToString(Console.ReadLine());
            Console.Clear();
            if (!(book.TryGetValue(PhoneNumberList, out str)))
            {
                Console.WriteLine($"\n\"Номер телефона :" + PhoneNumber + "\n\"Владелец не зарегистрирован");
            }
            Console.WriteLine($"\n\"Номер телефона: " + PhoneNumber + "\n\"Владелец - " + str);

        }
        private Dictionary<List<string>, string> book;
        public Dictionary<List<string>, string> BookPhone()
        {
            if (book == null) book = new Dictionary<List<string>, string>();
            book.Add(PhoneNumberList, LastMiddleName);
            return book;
        }
        public void SaveDb()
        {

            if (maintree == null) maintree = new JObject();
            JArray PersonData = new JArray();
            foreach (var number in book.Keys)
            { 
                JObject jPerson = new JObject();// обьект с именем+номерами 

                JArray arrMobPhones = new JArray();// Массив с номерами

                arrMobPhones.Add(number);

                if (!(book.TryGetValue(number, out str))) jPerson["Name"] = $"пусто";

                    jPerson["Name"] = str;
                    jPerson["NumberPhones"] = arrMobPhones;
   
                    PersonData.Add(jPerson);
            }
            maintree["BookPhones"] = PersonData;
            Console.WriteLine(maintree.ToString());
            

            #region Хотел чтобы сериализиролось не в строчку а в развенутом формате как Console.WriteLine(maintree.ToString());
            //var options = new JsonSerializerOptions
            //{
            //    WriteIndented = true
            //};

            //string json = JsonSerializer.Serialize<JObject>(maintree,options);
            #endregion
            File.WriteAllText("BookPhones.json", JsonConvert.SerializeObject(maintree));
        }
        public void LoadDb()
        {
            if (File.Exists("BookPhones.json"))
            {
                string json = File.ReadAllText("BookPhones.json");
                Console.WriteLine(json); Console.Clear();

                var Numbers = JObject.Parse(json)["BookPhones"].ToArray();
                 
                foreach (var item in Numbers)
                {
                    this.LastMiddleName = item["Name"].ToString();
                    PhoneNumberList = new List<string>();
                    foreach (string i in item["NumberPhones"].ToArray())
                    {
                        Console.WriteLine(i);
                        PhoneNumberList.Add(i);
                    }
                    BookPhone();
                }

            }
        }

        /// <summary>
        /// Метоз для напоминания сохранения данных пользовтелю
        /// </summary>
        public void ActionBeforeClose()
        {
            Console.Write("Сохранить Сотрудников?(да/нет)");
            string answer = Console.ReadLine();
            if (answer == "да")
            {
                SaveDb();
                Environment.Exit(0);
            }
            else if (answer == "нет") Environment.Exit(0);
            else
            {
                Console.WriteLine("Неизвестная команда!!!");
                ActionBeforeClose();
            }
        }
    }
}

