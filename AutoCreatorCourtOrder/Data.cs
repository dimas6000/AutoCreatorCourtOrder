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
        /// ФИО
        /// </summary>
        public static string fullName { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        public static string address { get; set; }
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
        public static int allDebt { get; set; }
    }
}
