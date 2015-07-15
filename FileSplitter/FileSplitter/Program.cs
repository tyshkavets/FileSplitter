using System;
using System.IO;
using System.Text;

namespace Tyshkavets.FileSplitter
{
    class Program
    {
        private static ParametersContainer _parameters;

        static int Main(string[] args)
        {
            var validationResult = InitialValidation(args);

            if (!validationResult.IsValid)
            {
                Console.WriteLine(validationResult.ErrorMessage);
                return validationResult.ReturnCode;
            }

            var processedFilesCount = 0;
            using (var fileStream = new FileStream(_parameters.InputFileName, FileMode.Open, FileAccess.Read))
            using (var inputStream = new StreamReader(fileStream))
            {
                var stringToWrite = new StringBuilder();
                var rowCount = 0;
                while (!inputStream.EndOfStream)
                {
                    stringToWrite.AppendLine(inputStream.ReadLine());
                    rowCount++;

                    if (rowCount == _parameters.NumberOfLinesInOnePartition)
                    {
                        FlushFile(processedFilesCount, stringToWrite);
                        Console.WriteLine("Partition {0} processed", processedFilesCount);
                        processedFilesCount++;
                        rowCount = 0;
                        stringToWrite.Clear();
                    }
                }

                if (rowCount > 0)
                {
                    FlushFile(processedFilesCount, stringToWrite);
                }
            }

            return 0;
        }

        private static void FlushFile(int fileIndex, StringBuilder content)
        {
            var outputStream = new StreamWriter(String.Format("{1}{0}.sql", fileIndex, _parameters.PartitionNamePrefix));
            outputStream.Write(content);
            outputStream.Flush();
            outputStream.Close();
        }

        private static ErrorInformation InitialValidation(string[] args)
        {
            var result = new ErrorInformation();

            if (args.Length != 3)
            {
                result.ErrorMessage = "Usage: FileSplitTool <input_file> <number_of_lines_in_each_partition> <output_file_prefix>";
                result.ReturnCode = 1;
                return result;
            }

            int numberOfLinesInEachFile = 0;
            try
            {
                numberOfLinesInEachFile = Convert.ToInt32(args[1]);
            }
            catch (FormatException formatException)
            {
                result.ErrorMessage = "Number of lines in each partition is in incorrect format. Must be integer.";
                result.ReturnCode = 2;
            }
            catch (OverflowException overflowException)
            {
                result.ErrorMessage = "Number of lines in each partition is too big. Seriously?";
                result.ReturnCode = 3;
            }

            if (numberOfLinesInEachFile <= 0)
            {
                result.ErrorMessage = "Number of lines in each partition should be positive";
                result.ReturnCode = 4;
            }

            if (!File.Exists(args[0]))
            {
                result.ErrorMessage = "Input file not found.";
                result.ReturnCode = 5;
            }

            if (result.IsValid)
            {
                _parameters = new ParametersContainer(args[0], numberOfLinesInEachFile, args[2]);
            }

            return result;
        }
    }
}
