﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using Web3Multitool.Commands;
using Web3Multitool.Dialogs;
using Web3MultiTool.Domain.Models;
using Web3Multitool.Stores;

namespace Web3Multitool.ViewModels;

public class ViewTabViewModel : BaseViewModel
{
    private readonly AccountInfosStore _accountInfosStore;
    public MainViewModel MainViewModel;

    private readonly ObservableCollection<AccountInfo> _accountInfos;
    public IEnumerable<AccountInfo> AccountInfos => _accountInfos;

    public ICommand ImportAccountsFromFileCommand { get; }
    public ICommand ExportAccountsToFileCommand { get; }
    public ICommand ClearAccountInfosCommand { get; }
    public ICommand EditCexAddressCommand { get; }
    public ICommand GenerateAccountsCommand { get; }
    public ICommand LoadAccountInfosCommand { get; }

    public ICommand SyncAccountsDataCommand { get; }
    

    public ViewTabViewModel(AccountInfosStore accountInfosStore)
    {
        _accountInfosStore = accountInfosStore;
        _accountInfos = new ObservableCollection<AccountInfo>();
        
        _accountInfosStore.AccountInfosGenerated += AccountInfosStoreOnAccountInfosGenerated;
        _accountInfosStore.AccountInfoDeleted += AccountInfosStoreOnAccountInfoDeleted;
        _accountInfosStore.AccountInfosCleared += AccountInfosStoreOnAccountInfosCleared;
        _accountInfosStore.AccountInfosLoaded += AccountInfosStoreOnAccountInfosLoaded;
        _accountInfosStore.AccountInfoUpdated += AccountInfosStoreOnAccountInfoUpdated;
        _accountInfosStore.AccountInfosImported += AccountInfosStoreOnAccountInfosImported;
        
        LoadAccountInfosCommand = new LoadAccountInfosCommand(this, _accountInfosStore);
        ImportAccountsFromFileCommand = new ImportAccountsFromFileCommand(this, _accountInfosStore);
        ExportAccountsToFileCommand = new ExportAccountsToFileCommand(this, _accountInfosStore);
        ClearAccountInfosCommand = new ClearAccountInfosCommand(this, _accountInfosStore);
        EditCexAddressCommand = new EditCexAddressCommand(this, _accountInfosStore);
        GenerateAccountsCommand = new GenerateAccountsCommand(this, _accountInfosStore);
        SyncAccountsDataCommand = new SyncAccountsDataCommand(this, accountInfosStore);
    }

    protected override void Dispose()
    {
        _accountInfosStore.AccountInfosGenerated -= AccountInfosStoreOnAccountInfosGenerated;
        _accountInfosStore.AccountInfoDeleted -= AccountInfosStoreOnAccountInfoDeleted;
        _accountInfosStore.AccountInfosCleared -= AccountInfosStoreOnAccountInfosCleared;
        _accountInfosStore.AccountInfosLoaded -= AccountInfosStoreOnAccountInfosLoaded;
        _accountInfosStore.AccountInfoUpdated += AccountInfosStoreOnAccountInfoUpdated;
        _accountInfosStore.AccountInfosImported -= AccountInfosStoreOnAccountInfosImported;

        base.Dispose();
    }
    
    private void AccountInfosStoreOnAccountInfosImported(IEnumerable<AccountInfo> accountInfos)
    {
        foreach (var accountInfo in accountInfos)
        {
            _accountInfos.Add(accountInfo);
        }
        OnPropertyChanged(nameof(AnyAccountExists));
    }
    
    private void AccountInfosStoreOnAccountInfoUpdated(AccountInfo accountInfo)
    {
        _accountInfos.FirstOrDefault(x => x.Id == accountInfo.Id).CexAddress = accountInfo.CexAddress;
    }

    private void AccountInfosStoreOnAccountInfosLoaded()
    {
        _accountInfos.Clear();

        foreach (var accountInfo in _accountInfosStore.AccountInfos)
        {
            _accountInfos.Add(accountInfo);
        }
        
        OnPropertyChanged(nameof(AnyAccountExists));
    }

    private void AccountInfosStoreOnAccountInfosCleared()
    {
        _accountInfos.Clear();
        OnPropertyChanged(nameof(AnyAccountExists));
    }

    private void AccountInfosStoreOnAccountInfoDeleted(string address)
    {
        var accountInfo = _accountInfos.FirstOrDefault(accInfo => accInfo.Address == address);
        
        if (accountInfo != null)
            _accountInfos.Remove(accountInfo);
        
        OnPropertyChanged(nameof(AnyAccountExists));
    }

    private void AccountInfosStoreOnAccountInfosGenerated(IEnumerable<AccountInfo> accountInfos)
    {
        _accountInfos.Clear();
        
        foreach (var accountInfo in accountInfos)
        {
            _accountInfos.Add(accountInfo);
        }
        
        OnPropertyChanged(nameof(AnyAccountExists));
    }

    private string _generateInputAmount;

    public string GenerateInputAmount
    {
        get => _generateInputAmount;
        set
        {
            SetField(ref _generateInputAmount, value);
            OnPropertyChanged(nameof(CanGenerate));
        }
    }

    private bool _isLoading;

    public bool IsLoading
    {
        get => _isLoading;
        set => SetField(ref _isLoading, value);
    }

    public bool CanGenerate => int.TryParse(GenerateInputAmount, out _);

    public bool AnyAccountExists => AccountInfos.Any();

    public static ViewTabViewModel LoadViewModel(AccountInfosStore accountInfosStore)
    {
        var viewModel = new ViewTabViewModel(accountInfosStore);
        
        viewModel.LoadAccountInfosCommand.Execute(null);

        return viewModel;
    }
}