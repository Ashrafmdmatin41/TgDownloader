﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TgDownloader.Helpers;

public partial class TgClientHelper
{
    private void TryCatchAction(Action action, Action<string, bool>? refreshStatus = null, 
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            // It should be saved and asked to be sent to the developer.
            //string fileName = Path.GetFileName(filePath);
            if (refreshStatus is not null)
            {
                //refreshStatus($"Exception at | {nameof(fileName)}: {fileName} | {nameof(lineNumber)}: {lineNumber} | {nameof(memberName)}: {memberName}");
                refreshStatus(ex.Message, false);
                if (ex.InnerException is not null)
                    refreshStatus(ex.InnerException.Message, false);
            }
        }
    }
}