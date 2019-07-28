using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AutoCreatorCourtOrder
{
    // Справочник по регуляркам: https://docs.microsoft.com/ru-ru/dotnet/standard/base-types/regular-expression-language-quick-reference
    /// <summary>
    /// Хранит все регулярные выражения и функцию для работы с ними.
    /// </summary>
    static class MyRegex
    {
        // @ перед строковым литералом позволяет переходить на новую строку, использовать табуляцию
        // и т.п. без управляющих последовательностей (кроме ", она записывается как "").

        /// <summary>
        /// Находит ФИО. Три слова после слова "Должник", они и являются ФИО.
        /// </summary>
        public static string FullName { get { return @"(?<=Должник.*)[а-яё]+\s+[а-яё]+\s+[а-яё]+"; } }

        /// <summary>
        /// Находит адрес. Подстрока от первого алфавитно-цифрового 
        /// символа после ФИО и до пробела перед словом "Дата".
        /// </summary>
        public static string Address { get { return @"(?<=" + ExtractedData.FullName + @"\s*)\w.+?(?=\s*Дата)"; } }

        /// <summary>
        /// Находит дату рождения. Три числа после слов "Дата рождения" разделенные любым одиночным символом.
        /// </summary>
        public static string DateOfBirth { get { return @"(?<=Дата\s*рождения.\s*)\d+.\d+.\d+"; } }

        /// <summary>
        /// Находит место рождения. Подстрока от первого алфавитно-цифрового символа
        /// после слов "Место рождения" и до пробела перед словом "Общая".
        /// </summary>
        public static string BirthPlace { get { return @"(?<=Место\s*рождения.\s*)\w.+?(?=\s*Общая)"; } }

        /// <summary>
        /// Находит ИНН. Число после слова "ИНН". 
        /// </summary>
        public static string Inn { get { return @"(?<=\bИНН\s*)\d+"; } }

        /// <summary>
        /// Находит описание задолженностей. Подстрока от слов "Недоимки по" до "В соотвествии со статьёй 123.8". 
        /// Не игнорировать регистр, иначе может работать некорректно. Позднее исправлю т.к. регистр может быть любой.
        /// </summary>
        public static string DebtStructure { get { return @"(?<=недоимки\s*по.\s*)\S(.|\s)+?(?=\s*В\sсоответ\S(.|\s)+?123.8)"; } }

        /// <summary>
        /// Находит общую сумму задолженности для расчета госпошлины БЕЗ учета копеек. 
        /// Целое число от слов "Общая сумма", если число будет дробное, то копейки проигнорируются.
        /// </summary>
        public static string AllDebt { get { return @"(?<=Общая\s*сумма.\s*)\d+"; } }

        /// <summary>
        /// Находит КБК\реквизиты. Весь текст от слова "Получатель" (включительно) до "Приложение".
        /// </summary>
        public static string BankDetails { get { return @"Получат(.|\s)+?(?=\s*Прилож)"; } }

        /// <summary>
        /// Поиск данных во входном документе с помощью регулярных выражений.
        /// </summary>
        /// <param name="textForSearch">Текст в котором осуществляется поиск.</param>
        /// <param name="regexPattern">Паттерн регулярного выражения</param>
        /// <param name="regexOption">Если нужно игнорировать регистр, то передаем сюда RegexOptions.IgnoreCase</param>
        /// <returns></returns>
        public static string FindDataWithRegex(string textForSearch, string regexPattern, RegexOptions regexOption = RegexOptions.None)
        {
            try
            {
                // Время поиска в регулярном выражении ограничивается 5 секундами т.к. обрабатываемые документы маленькие. 
                Regex regex = new Regex(regexPattern, regexOption, TimeSpan.FromSeconds(5));
                if (regex.IsMatch(textForSearch))
                    return regex.Match(textForSearch).Value;
                else
                {
                    MessageBox.Show("Данные не найдены в тексте, возможно вы пытаетесь использовать неподходящий документ.");
                    return "!!!ДАННЫЕ НЕ ОБНАРУЖЕНЫ!!!";
                }
            }
            catch (RegexMatchTimeoutException)
            {
                MessageBox.Show("Данные не найдены в тексте, возможно вы пытаетесь использовать неподходящий документ.");
                return "!!!ДАННЫЕ НЕ ОБНАРУЖЕНЫ!!!";
            }
        }

    }
}

