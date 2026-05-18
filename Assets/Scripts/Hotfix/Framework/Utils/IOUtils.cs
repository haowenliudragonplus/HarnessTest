using System;
using System.IO;
using System.Diagnostics;
using System.Security.AccessControl;
using Debug = UnityEngine.Debug;

public static class IOUtils
{
    /// <summary>
    /// 是否为文件路径
    /// </summary>
    public static bool IsFilePath(string path)
    {
        return !string.IsNullOrEmpty(Path.GetExtension(path));
    }

    /// <summary>
    /// 是否为文件夹路径
    /// </summary>
    public static bool IsFolderPath(string path)
    {
        return string.IsNullOrEmpty(Path.GetExtension(path));
    }

    /// <summary>
    /// 重新整理路径
    /// </summary>
    public static string ReconstructPath(string path)
    {
        path = path.Replace('\\', '/').Replace("//", "/");
        return path;
    }

    /// <summary>
    /// 打开文件夹
    /// </summary>
    public static void OpenFolder(string dir, string locationPath = "")
    {
        if (!DirectoryExist(dir))
        {
            Debug.LogError($"文件夹不存在：{dir}");
            return;
        }

        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.UseShellExecute = true;
        if (string.IsNullOrEmpty(locationPath))
        {
            // 仅打开文件夹
            startInfo.FileName = dir;
        }
        else
        {
            if (!FileExist(locationPath))
            {
                Debug.LogWarning($"定位文件不存在：{locationPath}，只打开文件夹");
                startInfo.FileName = dir;
            }
            else
            {
                // 定位到文件
#if UNITY_EDITOR_WIN
                startInfo.FileName = "explorer.exe";
                startInfo.Arguments = "/select, \"" + locationPath.Replace("/", "\\") + "\"";
#elif UNITY_EDITOR_OSX
                startInfo.FileName = "open";
                startInfo.Arguments = "-R \"" + locationPath + "\"";
#else
                Debug.LogWarning("该平台不支持定位功能");
                startInfo.FileName = dir;
#endif
            }
        }

        try
        {
            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            Debug.LogError("无法打开文件夹：" + ex.Message);
        }
    }

    /// <summary>
    /// 拷贝文件
    /// </summary>
    /// destPath：目标文件路径或目标文件夹路径
    public static bool CopyFile(string srcFilePath, string destPath, bool destroySrcFile = false, bool overwrite = true)
    {
        if (!IsFilePath(srcFilePath))
        {
            Debug.LogError($"原始文件路径有误，srcFilePath：{srcFilePath}");
            return false;
        }
        if (!File.Exists(srcFilePath))
        {
            Debug.LogError($"原始文件不存在，srcFilePath：{srcFilePath}");
            return false;
        }
        string destFilePath = "";
        string destDirPath = "";
        if (IsFilePath(destPath))
        {
            destFilePath = destPath;
            destDirPath = Path.GetDirectoryName(destPath);
        }
        else
        {
            destFilePath = Path.Combine(destPath, Path.GetFileName(srcFilePath));
            destDirPath = destPath;
        }
        if (!string.IsNullOrEmpty(destDirPath) && !Directory.Exists(destDirPath))
        {
            Directory.CreateDirectory(destDirPath);
        }
        File.Copy(srcFilePath, destFilePath, overwrite);
        if (destroySrcFile)
        {
            File.Delete(srcFilePath);
        }
        return true;
    }

    /// <summary>
    /// 拷贝文件夹
    /// </summary>
    public static bool CopyFolder(string srcDir, string destDir, bool containRootDir = true, bool overwrite = true)
    {
        if (!IsFolderPath(srcDir))
        {
            Debug.LogError($"原始路径有误，不是文件夹路径，srcDirPath：{srcDir}");
            return false;
        }
        if (!IsFolderPath(destDir))
        {
            Debug.LogError($"目标路径有误，不是文件夹路径，destDirPath：{destDir}");
            return false;
        }
        if (!Directory.Exists(srcDir))
        {
            Debug.LogError($"原始路径不存在，srcDirPath：{srcDir}");
            return false;
        }
        destDir = containRootDir ? Path.Combine(destDir, Path.GetFileName(srcDir)) : destDir;
        if (!Directory.Exists(destDir))
        {
            Directory.CreateDirectory(destDir);
        }
        string[] filePaths = Directory.GetFileSystemEntries(srcDir);
        foreach (var temp in filePaths)
        {
            if (IsFilePath(temp))
            {
                string destFilePath = Path.Combine(destDir, Path.GetFileName(temp));
                File.Copy(temp, destFilePath, overwrite);
            }
            else
            {
                CopyFolder(temp, destDir);
            }
        }
        return true;
    }

    /// <summary>
    /// 获取文件名称
    /// </summary>
    public static string GetFileName(string path, bool withoutExtension = true)
    {
        string fileName = string.Empty;
        if (withoutExtension)
        {
            fileName = Path.GetFileNameWithoutExtension(path);
        }
        else
        {
            fileName = Path.GetFileName(path);
        }
        return fileName;
    }

    /// <summary>
    /// 获取文件夹路径
    /// </summary>
    public static string GetFolderPath(string path)
    {
        string dirPath = Path.GetDirectoryName(path);
        return dirPath;
    }

    public static bool WirteToFile(string filePath, string fileContent, bool overwrite = true)
    {
        if (!IsFilePath(filePath))
            return false;
        string dirPath = GetFolderPath(filePath);
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        if (File.Exists(filePath))
        {
            if (overwrite)
            {
                File.WriteAllText(filePath, fileContent);
                return true;
            }
            return false;
        }
        else
        {
            File.WriteAllText(filePath, fileContent);
            return true;
        }
    }

    public static bool FileExist(string filePath)
    {
        bool exist = File.Exists(filePath);
        return exist;
    }

    public static bool DirectoryExist(string dir)
    {
        bool exist = Directory.Exists(dir);
        return exist;
    }

    public static bool DeleteFile(string filePath)
    {
        if (!FileExist(filePath))
            return false;
        File.Delete(filePath);
        return true;
    }

    public static bool DeleteDirectory(string dir, bool recursive = true)
    {
        if (!DirectoryExist(dir))
            return false;
        Directory.Delete(dir, recursive);
        return true;
    }

    public static bool CreateDirectory(string dir)
    {
        if (!DirectoryExist(dir))
            return false;
        Directory.CreateDirectory(dir);
        return true;
    }

    /// <summary>
    /// 删除某个文件夹内的所有文件
    /// </summary>
    public static bool DeleteAllFile(string dir)
    {
        if (!DirectoryExist(dir))
            return false;
        string[] filePathList = Directory.GetFiles(dir);
        foreach (string filePath in filePathList)
        {
            File.Delete(filePath);
        }
        // 递归删除子文件夹及其内容
        string[] directories = Directory.GetDirectories(dir);
        foreach (string directory in directories)
        {
            Directory.Delete(directory, true);
        }
        return true;
    }
    
    public static string GetExtensionName(string path)
    {
        var extension = Path.GetExtension(path).Replace(".", "");
        return extension;
    }
}