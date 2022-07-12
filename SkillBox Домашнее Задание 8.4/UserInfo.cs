using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace SkillBox_Домашнее_Задание_8._4
{
    struct UserInfo
    {
        #region Поля
        private string lastmiddlename;
        private string street;
        private byte housenumber;
        private byte apartmentnumber;
        private string mobilephone;
        private string homephone;
        #endregion
        private XElement Person;
        private XElement myStreet;
        private XElement myHouseNumber;
        private XElement myApartmentNumber;
        private XElement myMobilePhone;
        private XElement myHomePhone;
        private XElement Address;
        private XElement Phones;
        #region Подсказка пользователю, что надо вводить
        /// <summary>
        /// Переключатель  для добавления нового пользователя
        /// </summary>
        /// <param name="Item"></param>
        public void Message(string Item)
        {
            switch (Item)
            {
                case "Name":
                    Console.Write("ФИО Контакта: ");
                    break;
                case "Street":
                    Console.Write("Улица : ");
                    break;
                case "HouseNumber":
                    Console.Write("Номер Дома: ");
                    break;
                case "ApartmentNumber":
                    Console.Write("Квартира: ");
                    break;
                case "MobilePhone":
                    Console.Write("Мобильный Телефон: 8");
                    break;
                case "HomePhone":
                    Console.Write("Домашний Телефон: ");
                    break;
            }
        }
        #endregion
        #region Свойства
        public string LastMiddleName { get { return this.lastmiddlename; } set {this.lastmiddlename = value; } }
        public string Street { get { return this.street; } set {this.street = value; } }
        public byte HouseNumber { get { return this.housenumber; } set {
                if (value.ToString().StartsWith("0"))
                { Console.WriteLine($"Не может начинаться с нуля"); GetHouseNumber(); }
                else this.housenumber = value;
            } }
        public byte ApartmentNumber { get { return this.apartmentnumber; } set {
                if (value != 0) this.apartmentnumber = value;
                else
                { Console.WriteLine($"Не может начинаться с нуля"); 
                    GetApartmentNumber(); 
                } } }
        public string MobilePhone { 
            get {
                if (!(mobilephone.StartsWith("8")) && (!string.IsNullOrWhiteSpace(mobilephone)))
                {
                    this.mobilephone = mobilephone.Insert(0, "8");
                }
                //if (!(mobilephone.Contains("8")) && (!string.IsNullOrWhiteSpace(mobilephone)))this.mobilephone = mobilephone.Insert(0, "8");
                return this.mobilephone; } 
            set {
                if (value.Length == 10) this.mobilephone = value;
                else
                { 
                    Console.WriteLine($"Мобильный номер состоит из 10 цифр!"); GetMobilePhone(); 
                }
            } }
        public string HomePhone {
            get
            {
                return this.homephone;
            }
            set
            {
                if (value.Length == 9) this.homephone = value;
                else
                {
                    Console.WriteLine(value.Length);
                    Console.WriteLine($"Домашний номер состоит из 7 цифр!"); GetHomePhone();
                }
            }
        }
        #endregion



        public XElement GetUserName()
        {
            Message("Name");
            LastMiddleName = Convert.ToString(Console.ReadLine());
            Person = new XElement("Person");
            XAttribute AttributeName = new XAttribute("name", LastMiddleName);
            Person.Add(AttributeName);
            return Person;
        }
        public XElement GetStreet()
        {
            Message("Street");
            Street = Convert.ToString(Console.ReadLine());

            myStreet = new XElement("Street", Street);

            return myStreet;
        }
        public XElement GetHouseNumber()
        {
            beginHouseNumber:
            Message("HouseNumber");
            try { this.HouseNumber = Convert.ToByte(Console.ReadLine()); }
            catch { Console.WriteLine($"Вышли из диапазона!!! Не больше 255");goto beginHouseNumber; }

            myHouseNumber = new XElement("HouseNumber", HouseNumber);

            return myHouseNumber;
        }
        public XElement GetApartmentNumber ()
        {
            beginApatrment:
            Message("ApartmentNumber");
            try { this.ApartmentNumber = Convert.ToByte(Console.ReadLine()); }
            catch { Console.WriteLine($"Вышли из диапазона!!! Не больше 255"); goto beginApatrment; }

            myApartmentNumber= new XElement("FlatNumber", ApartmentNumber);
           
            return myApartmentNumber;
        }
        public XElement GetMobilePhone ()
        {
        beginmob:
            Message("MobilePhone");
            if (long.TryParse(Console.ReadLine(), out long numb))
            { this.MobilePhone = numb.ToString(); }
            else
            { Console.WriteLine($"Неккоректный ввод!!!Только числа!!!"); goto beginmob; }

            myMobilePhone = new XElement("MobilePhone",MobilePhone);

            return myMobilePhone;
        }

        public XElement  GetHomePhone()
        {
        beginHomeNumber:
            Message("HomePhone");
            if (long.TryParse(Console.ReadLine(), out long numb)) this.HomePhone = string.Format("{0:#-##-##}", numb);
            else 
            {
                Console.WriteLine($"Неккоректный ввод!!!Только числа!!!"); goto beginHomeNumber; 
            }

            myHomePhone = new XElement("FlatPhone",HomePhone);
            return myHomePhone;
        }
       
        public XElement xData()
        {
            GetHomePhone();
            GetMobilePhone();
            GetUserName();
            GetStreet();
            GetHouseNumber();
            GetApartmentNumber();
            
            

            
            Address = new XElement("Address", myStreet, myHouseNumber, myApartmentNumber);
            Phones = new XElement("Phones", myMobilePhone, myHomePhone);
            
            Person.Add(Address, Phones);
            Person.Save("Person.xml");
            return Person;

        }
        public void ParseData ()
        {
            string xml = System.IO.File.ReadAllText("Person.xml");


            var col = XDocument.Parse(xml)
                               .Descendants("Person")
                              
                               .ToList();
            foreach (var item in col)
            {
                Console.WriteLine(  "Ф.И.О: {0}" +
                                    "\nАдрес: ул.{1}, д.{2}, кв.{3}" +
                                    "\nТелефоны: Моб.телефон {4}, Дом.телефон {5}",
                                    item.Attribute("name").Value,
                                    item.Element("Address").Element("Street").Value,
                                    item.Element("Address").Element("HouseNumber").Value,
                                    item.Element("Address").Element("FlatNumber").Value,
                                    item.Element("Phones").Element("MobilePhone").Value,
                                    item.Element("Phones").Element("FlatPhone").Value);
               
            }
            Console.ReadLine(); Console.Clear();



        }
    }
}
