﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TgDownloaderCore.Locales;

public class LocaleQuestionHelper
{
    #region Design pattern "Lazy Singleton"

    private static LocaleQuestionHelper _instance;
    public static LocaleQuestionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    public string TypeTgChannelUserName => "Type the channel user name: ";
    public string TypeTgMessageCount => "Type the messages count (0 - unlimited): ";
    public string TypeTgMessageStartId => "Type the message start ID: ";

    #endregion
}