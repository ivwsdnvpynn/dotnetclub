﻿using Microsoft.AspNet.Testing;
using System;
using System.IO;
using System.Linq;

namespace Discussion.Web.Tests.Utils
{
    public static class TestEnv
    {
        public static string TestProjectPath()
        {
            var args = Environment.GetCommandLineArgs();
            var appBaseIndex = Array.IndexOf(args, "--appbase");

            var path = appBaseIndex >= 0 ? args[appBaseIndex + 1] : Environment.CurrentDirectory;
            return path.NormalizeSeparatorChars();
        }

        public static string DnxPath()
        {
            var dnxPathFragment = string.Concat(".dnx", Path.DirectorySeparatorChar, "runtimes");
            var dnxCommand = TestPlatformHelper.IsWindows ? "dnx.exe" : "dnx";
            var envPathSeparator = TestPlatformHelper.IsWindows ? ';' : ':';

            var envPath = Environment.GetEnvironmentVariable("PATH");
            var runtimeBin = envPath.Split(new char[] { envPathSeparator }, StringSplitOptions.RemoveEmptyEntries)
                .Where(c => c.Contains(dnxPathFragment)).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(runtimeBin))
            {
                throw new Exception("Runtime not detected on the machine.");
            }

            return Path.Combine(runtimeBin, dnxCommand);
        }
    }

    static class StringPathExtensions
    {
        public static string NormalizeSeparatorChars(this string path)
        {
            return path.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
        }
    }
}