namespace LeaveMate.Others
{
    public class ErrorLog
    {
        public static void Print(String Error)
        {
            try
            {
                string ErrorLogDirectory = Path.GetDirectoryName(FixedValues.ErrorLogFilePath);
                if (!Directory.Exists(ErrorLogDirectory))
                {
                    Directory.CreateDirectory(ErrorLogDirectory);
                }

                string ErrorLogMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {Error}{Environment.NewLine}{Environment.NewLine}";
                File.AppendAllText(FixedValues.ErrorLogFilePath, ErrorLogMessage);
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ErrorLog writing failed : {Ex.Message}");
            }
        }
    }
}