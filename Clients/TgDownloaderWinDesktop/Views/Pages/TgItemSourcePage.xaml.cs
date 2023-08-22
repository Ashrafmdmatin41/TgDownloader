﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TgDownloaderWinDesktop.Views.Pages;

/// <summary>
/// Interaction logic for TgItemSourceVm.xaml
/// </summary>
public partial class TgItemSourcePage
{
	#region Public and private fields, properties, constructor

	public TgItemSourcePage()
	{
		TgDesktopUtils.TgItemSourceVm.AddUpdateUi(TgEnumUpdateType.Page, UpdatePage);
		TgDesktopUtils.TgItemSourceVm.AddUpdateUi(TgEnumUpdateType.Window, UpdateMainWindow);
		TgDesktopUtils.TgItemSourceVm.AddUpdateUi(TgEnumUpdateType.Application, UpdateApplication);
		TgDesktopUtils.TgItemSourceVm.OnNavigatedTo();
		InitializeComponent();
	}

	#endregion
}