﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TgDownloaderBlazor.Common;

public abstract class TgPageComponentBase : ComponentBase
{
    #region Public and private fields, properties, constructor

    [Inject] protected IJSRuntime JsRuntime { get; set; } = default!;
    [Inject] protected NavigationManager UriHelper { get; set; } = default!;
    [Inject] protected DialogService DialogService { get; set; } = default!;
    [Inject] protected TooltipService TooltipService { get; set; } = default!;
    [Inject] protected ContextMenuService ContextMenuService { get; set; } = default!;
    [Inject] protected NotificationService NotificationService { get; set; } = default!;
    [Inject] public IDbContextFactory<TgEfContext> DbFactory { get; set; } = default!;

    protected virtual TgLocaleHelper TgLocale => TgLocaleHelper.Instance;
    protected virtual bool IsLoading { get; set; } = true;

    #endregion
}