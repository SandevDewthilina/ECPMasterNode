using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ECPMaster.AnsibleCLI
{
    public interface IAnsibleRepository
    {
        Task<string> GetAgentList();
    }

    public class AnsibleRepository : IAnsibleRepository
    {
        async Task<string> RunCommandAndGetOutputAsync(string command, string workingDirectory = "/")
        {
            var output = new StringBuilder();
            var commandFileName = "cmd.exe";

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                commandFileName = "bash";
            }

            try
            {
                using var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = commandFileName, // Use "bash" for Linux/macOS
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WorkingDirectory = workingDirectory, // Specify the working directory if needed
                    },
                    EnableRaisingEvents = true,
                };

                process.OutputDataReceived += (sender, args) =>
                {
                    if (args.Data != null)
                    {
                        output.AppendLine(args.Data);
                    }
                };

                process.ErrorDataReceived += (sender, args) =>
                {
                    if (args.Data != null)
                    {
                        output.AppendLine(args.Data);
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                await using (var streamWriter = process.StandardInput)
                {
                    if (streamWriter.BaseStream.CanWrite)
                    {
                        // Send your command to the standard input, e.g., "ls" for Linux/macOS or "dir" for Windows
                        await streamWriter.WriteLineAsync(command);
                    }
                }

                process.WaitForExit(); // Use WaitForExitAsync() from a custom extension method
            }
            catch (Exception ex)
            {
                output.AppendLine($"Error: {ex.Message}");
            }

            return output.ToString();
        }

        public async Task<string> GetAgentList()
        {
            // ansible all -m setup
            return await RunCommandAndGetOutputAsync("dir");
        }
    }
}