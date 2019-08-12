using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyriller;
using Cyriller.Model;

namespace AutoCreatorCourtOrder
{
    public class ExtractedData
    {
        /// <summary>
        /// Производит расчет госпошлины. 
        /// </summary>
        /// <returns>Размер госпошлины.</returns>
        public decimal CalculateStateDuty()
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

        private string _fullName;
        /// <summary>
        /// ФИО.
        /// </summary>
        public string FullName
        {
            get { return _fullName; }
            set
            {
                var cyrName = new CyrName();
                // Cyriller при попытке склонения в именительный падеж просто вернет ФИО с нормальным форматированием.
                _fullName = cyrName.Decline(value, CasesEnum.Nominative).FullName;
                // Сразу склоняем ФИО в родительный падеж с помощью Cyriller'a.
                FullNameGenitive = cyrName.Decline(value, CasesEnum.Genitive).FullName;
            }
        }

        /// <summary>
        /// ФИО в родительном падеже.
        /// </summary>
        public string FullNameGenitive { get; private set; }

        /// <summary>
        /// Адрес.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Место рождения.
        /// </summary>
        public string BirthPlace { get; set; }
        
        /// <summary>
        /// ИНН.
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// Общая структура задолженности, целиком текст из начального файла.
        /// </summary>
        public string DebtStructure { get; set; }

        /// <summary>
        /// Общая сумма долга, для расчета госпошлины.
        /// </summary>
        public int AllDebt { get; set; }

        /// <summary>
        /// Реквизиты, целиком хранится кусок текста, вроде в них меняется 
        /// только КБК, но это не точно, поэтому сохраняем целиком.
        /// </summary>
        public string BankDetails { get; set; }
    }
}
