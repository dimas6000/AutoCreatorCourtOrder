using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreatorCourtOrder
{
    static class Data
    {
        /// <summary>
        /// Получает строку, делает каждое слово с заглавной буквы.
        /// </summary>
        public static string FirstUpper(string str)
        {
            //Код функции целиком скопирован с cyberforum.com
            str = str.ToLower();
            string[] s = str.Split(' ');

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Length > 1)
                    s[i] = s[i].Substring(0, 1).ToUpper() + s[i].Substring(1, s[i].Length - 1).ToLower();
                else s[i] = s[i].ToUpper();
            }
            return string.Join(" ", s);
        }

        /// <summary>
        /// Производит расчет госпошлины
        /// </summary>
        public static decimal StateDuty(int AllDebt)
        {
            if (AllDebt <= 10000)
                return 200;
            if (AllDebt <= 20000)
                return (AllDebt * 0.02m);
            if (AllDebt <= 100000)
                return (400 + ((AllDebt - 20000) * 0.015m));
            if (AllDebt <= 200000)
                return (1600 + ((AllDebt - 100000) * 0.01m));

            return (2600 + ((AllDebt - 200000) * 0.005m));
        }

        private static string _fullName;
        /// <summary>
        /// ФИО
        /// </summary>
        public static string FullName { get { return _fullName; } set { _fullName = FirstUpper(value); } }

        /// <summary>
        /// Адрес
        /// </summary>
        public static string Address { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public static string DOB { get; set; }
        /// <summary>
        /// Место рождения
        /// </summary>
        public static string BPL { get; set; }
        /// <summary>
        /// ИНН
        /// </summary>
        public static string INN { get; set; }
        /// <summary>
        /// Общая структура задолженности, целиком текст из начального файла
        /// </summary>
        public static string DebtStructure { get; set; }
        /// <summary>
        /// Общая сумма долга, для расчета госпошлины
        /// </summary>
        public static int AllDebt { get; set; }
        /// <summary>
        /// Реквизиты, целиком хранится кусок текста, вроде меняется только КБК
        /// </summary>
        public static string BankDetails { get; set; }

        /// <summary>
        /// Путь к шаблону приказа
        /// </summary>
        public static string PathToTemplate { get; set; }


    }
}
