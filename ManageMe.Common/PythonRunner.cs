using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.Common
{
    public class PythonRunner
    {
        public async Task<string> RunPythonScriptAsync(string scriptPath, string inputData)
        {
            string pythonExePath = @"C:\Users\RaduI\AppData\Local\Programs\Python\Python312\python.exe";
            string scriptDir = Path.GetDirectoryName(scriptPath);
            string scriptName = Path.GetFileName(scriptPath);

            // Ensure the script exists
            if (!File.Exists(scriptPath))
            {
                throw new FileNotFoundException("The specified Python script could not be found.", scriptPath);
            }

            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = pythonExePath,
                Arguments = $"\"{scriptPath}\" \"{inputData}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(start))
            {
                // Write input to the Python script if needed (for example via standard input)
                // However, we are passing the input as a command line argument here

                string result = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();

                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new Exception($"Python script failed with error: {error}");
                }

                return result;
            }
        }
    }
}
