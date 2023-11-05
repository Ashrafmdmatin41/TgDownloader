﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TgDownloaderWinDesktop.ViewModels;

[DebuggerDisplay("{ToDebugString()}")]
public sealed partial class TgItemSourceViewModel : TgPageViewModelBase, INavigationAware
{
    #region Public and private fields, properties, constructor

    public TgSqlTableSourceViewModel ItemSourceVm { get; private set; } = new();
    public TgPageViewModelBase? ViewModel { get; set; }
    private Guid SourceUid { get; set; }

    public TgItemSourceViewModel() { }

	#endregion

	#region Public and private methods

	public void OnNavigatedTo()
	{
        _ = Task.Run(InitializeViewModelAsync).ConfigureAwait(true);
    }

	public void OnNavigatedFrom() { }

    protected override async Task InitializeViewModelAsync()
    {
        await base.InitializeViewModelAsync();

        await OnGetSourceFromStorageAsync();
    }

    public void SetItemSourceVm(TgSqlTableSourceViewModel itemSourceVm) => 
        SetItemSourceVm(itemSourceVm.Source, itemSourceVm.Source.Uid);

    public void SetItemSourceVm(TgSqlTableSourceModel source, Guid? uid = null)
    {
        ItemSourceVm.Source.Fill(source, uid);
        TgSqlTableSourceViewModel itemBackup = ItemSourceVm;
        ItemSourceVm = new()
        {
            Source = itemBackup.Source,
        };
    }

    /// <summary>
    /// Create new download settings.
    /// </summary>
    /// <param name="sourceVm"></param>
    /// <returns></returns>
    public TgDownloadSettingsModel CreateDownloadSettings(TgSqlTableSourceViewModel sourceVm) =>
        new()
        {
            SourceVm = new TgSqlTableSourceViewModel()
            {
                SourceId = sourceVm.Source.Id,
                SourceFirstId = sourceVm.Source.FirstId,
                SourceDirectory = sourceVm.Source.Directory
            }
        };

    // GetSourceFromStorageCommand
    [RelayCommand]
    public async Task OnGetSourceFromStorageAsync()
    {
        await TgDesktopUtils.RunFuncAsync(ViewModel ?? this, async () =>
        {
            if (ItemSourceVm.SourceUid != Guid.Empty)
                SourceUid = ItemSourceVm.SourceUid;
            TgSqlTableSourceModel source = await ContextManager.SourceRepository.GetAsync(SourceUid);
            SetItemSourceVm(source, source.Uid);
        }, true);
    }

    // UpdateSourceFromTelegramCommand
    [RelayCommand]
    public async Task OnUpdateSourceFromTelegramAsync()
    {
        if (!CheckClientReady())
            return;

        await TgDesktopUtils.RunFuncAsync(ViewModel ?? this, async () =>
        {
            if (ItemSourceVm.SourceUid != Guid.Empty)
                SourceUid = ItemSourceVm.SourceUid;
            // Collect chats from Telegram.
            if (!TgDesktopUtils.TgClient.DicChatsAll.Any())
                await TgDesktopUtils.TgClient.CollectAllChatsAsync();
            // Download settings.
            TgDownloadSettingsModel tgDownloadSettings = CreateDownloadSettings(ItemSourceVm);
            // Update source from Telegram.
            await TgDesktopUtils.TgClient.UpdateSourceDbAsync(ItemSourceVm, tgDownloadSettings);
            await ContextManager.SourceRepository.SaveAsync(ItemSourceVm.Source);
            // Message.
            await TgDesktopUtils.TgClient.UpdateStateMessageAsync(TgDesktopUtils.TgLocale.SettingsSource);
            await TgDesktopUtils.TgClient.UpdateStateSourceAsync(ItemSourceVm.Source.Id, ItemSourceVm.Source.FirstId, TgDesktopUtils.TgLocale.SettingsSource);
        }, false);

        await OnGetSourceFromStorageAsync();
    }

    // DownloadSourceCommand
    [RelayCommand]
    public async Task<bool> OnDownloadSourceAsync()
    {
        if (!CheckClientReady())
            return false;

        await OnUpdateSourceFromTelegramAsync();

        bool result = true;
        await TgDesktopUtils.RunFuncAsync(ViewModel ?? this, async () =>
        {
            // Check directory.
            if (!Directory.Exists(ItemSourceVm.Source.Directory))
            {
                await TgDesktopUtils.TgClient.UpdateStateSourceAsync(ItemSourceVm.Source.Id, ItemSourceVm.Source.FirstId,
                    $"Directory is not exists! {ItemSourceVm.Source.Directory}");
                result = false;
                return;
            }

            // Download settings.
            TgDownloadSettingsModel tgDownloadSettings = CreateDownloadSettings(ItemSourceVm);
            // Job.
            await TgDesktopUtils.TgClient.DownloadAllDataAsync(tgDownloadSettings);
            await TgDesktopUtils.TgClient.UpdateStateMessageAsync(TgDesktopUtils.TgLocale.SettingsSource);
        }, true);

        await OnGetSourceFromStorageAsync();

        return result;
    }

    // ClearViewCommand
    [RelayCommand]
    public async Task OnClearViewAsync()
    {
        await TgDesktopUtils.RunFuncAsync(ViewModel ?? this, async () =>
        {
            if (ItemSourceVm.SourceUid != Guid.Empty)
                SourceUid = ItemSourceVm.SourceUid;
            ItemSourceVm.Source = await ContextManager.SourceRepository.GetNewAsync();
        }, false);
    }

    // SaveSourceCommand
    [RelayCommand]
    public async Task OnSaveSourceAsync()
    {
        await TgDesktopUtils.RunFuncAsync(ViewModel ?? this, async () =>
        {
            await ContextManager.SourceRepository.SaveAsync(ItemSourceVm.Source, true);
        }, false);
    }

    // ReturnToSectionSourcesCommand
    [RelayCommand]
    public async Task OnReturnToSectionSourcesAsync()
    {
        await TgDesktopUtils.RunActionAsync(this, () =>
        {
            if (Application.Current.MainWindow is MainWindow navigationWindow)
            {
                navigationWindow.ShowWindow();
                navigationWindow.Navigate(typeof(TgSourcesPage));
            }
        }, false);
    }

    #endregion
}