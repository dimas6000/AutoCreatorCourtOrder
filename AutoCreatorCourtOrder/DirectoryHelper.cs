using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoCreatorCourtOrder
{
    public static class DirectoryHelper
    {
        const string courtOrdersDirectoryName = "Приказы созданные программой";
        const string processedFilesDirectoryName = "Обработанные файлы";

        public static string PathToCourtOrdersDirectory { get; private set; }
        public static string PathToProcessedFilesDirectory { get; private set; }

        public static void CreateDirectories(FileInfo pathToTemplate)
        {
            // todo: Придумать как написать то же самое без дублирования кода.
            string directory = Path.Combine(pathToTemplate.DirectoryName, courtOrdersDirectoryName);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            PathToCourtOrdersDirectory = directory;

            directory = Path.Combine(pathToTemplate.DirectoryName, processedFilesDirectoryName);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            PathToProcessedFilesDirectory = directory;
        }
    }
}
