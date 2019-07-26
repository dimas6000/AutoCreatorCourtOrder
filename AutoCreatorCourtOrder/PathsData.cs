using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCreatorCourtOrder
{
    /// <summary>
    /// Хранит пути к файлам и папкам.
    /// </summary>
    class PathsData
    {
        /// <summary>
        /// Путь к шаблону приказа.
        /// </summary>
        public static string PathToTemplate { get; set; }

        /// <summary>
        /// Путь к обрабатываемому файлу.
        /// </summary>
        public static string PathToFileBeingProcessed { get; set; }

        /// <summary>
        /// Путь к папке с приказами для обработки всех сразу.
        /// </summary>
        //  public static string PathToFolder { get; set; }

    }
}
