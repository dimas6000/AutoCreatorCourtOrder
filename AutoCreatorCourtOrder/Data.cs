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
    }
}
