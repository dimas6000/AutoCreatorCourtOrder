using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyriller;

namespace AutoCreatorCourtOrder
{
    static class ExtractedData
    {
        /// <summary>
        /// Получает строку, возвращает каждое слово с заглавной буквы.
        /// </summary>
        private static string _firstUpper(string str)
        {
            // Код функции без изменений с cyberforum.com
            str = str.ToLower();
            string[] s = str.Split(' ');

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Length > 1)
                    s[i] = s[i].Substring(0, 1).ToUpper() + s[i].Substring(1, s[i].Length - 1).ToLower();
                else
                    s[i] = s[i].ToUpper();
            }
            return string.Join(" ", s);
        }
        
        /// <summary>
        /// Производит расчет госпошлины. 
        /// </summary>
        /// <returns>Размер госпошлины.</returns>
        public static decimal CalculateStateDuty()
        {
            // Магические числа берутся из формулы расчета госпошлины в мировой суд для 
            // заявления о вынесении судебного приказа (50% от стандартной госпошлины).
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
        /// ФИО.
        /// </summary>
        public static string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = _firstUpper(value);

                //Сразу склоняем ФИО в родительный падеж с помощью Cyriller'a.
                string[] splFullName = FullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var toGenitive = new CyrName();
                string[] genitiveFullName = toGenitive.Decline(splFullName[0], splFullName[1], splFullName[2], 2);
                FullNameGenitive = genitiveFullName[0] + " " + genitiveFullName[1] + " " + genitiveFullName[2];
            }
        }

        /// <summary>
        /// ФИО в родительном падеже.
        /// </summary>
        public static string FullNameGenitive { get; private set; }

        /// <summary>
        /// Адрес.
        /// </summary>
        public static string Address { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public static string DateOfBirth { get; set; }

        /// <summary>
        /// Место рождения.
        /// </summary>
        public static string BirthPlace { get; set; }

        /// <summary>
        /// ИНН.
        /// </summary>
        public static string Inn { get; set; }

        /// <summary>
        /// Общая структура задолженности, целиком текст из начального файла.
        /// </summary>
        public static string DebtStructure { get; set; }

        /// <summary>
        /// Общая сумма долга, для расчета госпошлины.
        /// </summary>
        public static int AllDebt { get; set; }

        /// <summary>
        /// Реквизиты, целиком хранится кусок текста, вроде в них меняется 
        /// только КБК, но это не точно, поэтому сохраняем целиком.
        /// </summary>
        public static string BankDetails { get; set; }
    }
}
