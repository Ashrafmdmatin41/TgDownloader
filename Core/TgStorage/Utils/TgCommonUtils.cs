﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TgStorage.Utils;

public static class TgCommonUtils
{
	#region Public and private methods

	public static string GetIsFlag(bool isFlag, string positive, string negative) => $"{(isFlag ? positive : negative)}";

	public static string GetIsEnabled(bool isEnabled) => $"{(isEnabled ? "<Enabled>" : "<Disabled>")}";

    public static string GetIsExists(bool isExists) => $"{(isExists ? "<Exist>" : "<Non-existent>")}";

    public static string GetIsExistsDb(bool isExistsDb) => $"{(isExistsDb ? "<Exist DB>" : "<Non-existent DB>")}";

    public static string GetIsLoad(bool isLoad) => $"{(isLoad ? "<Loaded>" : "<Not loaded>")}";

    public static string GetIsUseProxy(bool isUseProxy) => $"{(isUseProxy ? "<Use proxy>" : "<Not use proxy>")}";

    public static string GetIsReady(bool isReady) => $"{(isReady ? "<Ready>" : "<Not ready>")}";

	#endregion
}