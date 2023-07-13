using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Diagnostics;

using Debug = UnityEngine.Debug;

public sealed class Utility
{
    #region IO Helpers

    public static string ReadFile(string filePath)
    {
        string content = string.Empty;
        try
        {
            content = File.ReadAllText(filePath);
            //Debug.Log("IO: Read text file: " + content);
        }
        catch
        {
            Debug.LogError("IO: Error reading file from " + filePath);
        }
        return content;
    }

    public static void WriteToFile(string filePath, string content)
    {
        try
        {
            File.WriteAllText(filePath, content);
#if UNITY_IPHONE
                UnityEngine.iOS.Device.SetNoBackupFlag(filePath);
#endif
        }
        catch
        {
            Debug.LogError("IO: Error writing to file at " + filePath);
        }
    }

    public static void WriteToFileBinary(string filePath, byte[] contents)
    {
        try
        {
            File.WriteAllBytes(filePath, contents);
#if UNITY_IPHONE
                UnityEngine.iOS.Device.SetNoBackupFlag(filePath);
#endif
        }
        catch
        {
            Debug.LogError("IO: Error writing to file at " + filePath);
        }
    }

    public static void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("File: Error deleting file by path: '{0}' err msg: '{1}'",
                    filePath, ex.Message);
            }
        }
    }

    public static void DeleteDirectory(string dirPath)
    {
        DeleteDirectory(dirPath, false);
    }
    public static void DeleteDirectory(string dirPath, bool recursive)
    {
        if (Directory.Exists(dirPath))
        {
            try
            {
                Directory.Delete(dirPath, recursive);
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("Directory: Error deleting directory by path: '{0}' err msg: '{1}'",
                    dirPath, ex.Message);
            }
        }
    }

    public static void CopyFile(string sourceFileName, string destFileName)
    {
        try
        {
            File.Copy(sourceFileName, destFileName);
        }
        catch
        {
            Debug.LogError("File: Error copy file from " + sourceFileName);
        }
    }

    public static string ReadAllTextFromFileStreamingAssets(string filePath)
    {
        string content = string.Empty;

        //try
        //{

        if (Application.platform == RuntimePlatform.Android)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            WWW reader = new WWW(filePath);
#pragma warning restore CS0618 // Type or member is obsolete
            while (!reader.isDone) { }

            content = reader.text;
        }
        else
        {
            content = File.ReadAllText(filePath);
        }
        //Debug.Log("[Utility][ReadAllTextFromFileStreamingAssets()] Read text file: " + content);

        //}
        //catch
        //{
        //    throw new Exception(string.Format("[Utility][ReadAllTextFromFileStreamingAssets()] Error reading file from '{0}'", filePath));
        //}

        return content;
    }

    public static byte[] ReadAllBytesFromFileStreamingAssets(string filePath)
    {
        byte[] content = null;

        //////try
        //////{

        if (Application.platform == RuntimePlatform.Android)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            WWW reader = new WWW(filePath);
#pragma warning restore CS0618 // Type or member is obsolete
            while (!reader.isDone) { }

            content = reader.bytes;
        }
        else
        {
            content = File.ReadAllBytes(filePath);
        }
        //Debug.Log("[Utility][ReadAllBytesFromFileStreamingAssets()] Read text file: " + content);

        ////////}
        ////////catch
        ////////{
        //////////    throw new Exception(string.Format("[Utility][ReadAllBytesFromFileStreamingAssets()] Error reading file from '{0}'", filePath));
        ////////}

        return content;
    }
    #endregion


    /// <summary>
    /// Puts the string into the Clipboard.
    /// </summary>
    public static void CopyToClipboard(string str)
    {
        var textEditor = new TextEditor();
        textEditor.text = str;
        textEditor.SelectAll();
        textEditor.Copy();
    }

    public static IEnumerator CheckInternetConnectionCoroutine(Action<bool> onSyncResult, int requestTimeout = 5)
    {
        bool result;
        var request = UnityWebRequest.Head("https://google.com");
        request.timeout = requestTimeout;
        yield return request.SendWebRequest();
        result = !request.isNetworkError && !request.isHttpError && (request.responseCode == 200 || request.responseCode == 204);
        request.Dispose();

        onSyncResult(result);
    }
}