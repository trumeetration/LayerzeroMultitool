﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Nethereum.Web3;

namespace Web3Multitool.Utils;

public class Web3Utils
{
    private static readonly string _baseABI =
        @"[{""inputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""name"":""balanceOf"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""},{""internalType"":""address"",""name"":"""",""type"":""address""}],""name"":""allowance"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""to"",""type"":""address""},{""internalType"":""uint256"",""name"":""value"",""type"":""uint256""}],""name"":""transfer"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""stateMutability"":""nonpayable"",""type"":""function""}]"; 
    
    private Dictionary<Chain, Dictionary<string, CoinInfo>> CoinInfosDictionary { get; } = new()
    {
        {
            Chain.Fantom, new()
            {
                { "USDT", new() { ContractAddress = "0x049d68029688eabf473097a2fc38ef61633a3c7a", ABI = _baseABI } },
                { "USDC", new() { ContractAddress = "0x04068DA6C83AFCFA0e13ba15A6696662335D5B75", ABI = _baseABI } }
            }
        },
        {
            Chain.Arbitrum, new()
            {
                { "USDT", new() { ContractAddress = "0xFd086bC7CD5C481DCC9C85ebE478A1C0b69FCbb9", ABI = _baseABI } },
                { "USDC", new() { ContractAddress = "0xFF970A61A04b1cA14834A43f5dE4533eBDDB5CC8", ABI = _baseABI } }
            }
        },
        {
            Chain.Avalanche, new()
            {
                { "USDT", new() { ContractAddress = "0x9702230A8Ea53601f5cD2dc00fDBc13d4dF4A8c7", ABI = _baseABI } },
                { "USDC", new() { ContractAddress = "0xB97EF9Ef8734C71904D8002F8b6Bc66Dd9c48a6E", ABI = _baseABI } }
            }
        },
        {
            Chain.Optimism, new()
            {
                { "USDT", new() { ContractAddress = "0x94b008aa00579c1307b0ef2c499ad98a8ce58e58", ABI = _baseABI } },
                { "USDC", new() { ContractAddress = "0x7f5c764cbc14f9669b88837ca1490cca17c31607", ABI = _baseABI } }
            }
        },
        {
            Chain.Polygon, new()
            {
                { "USDT", new() { ContractAddress = "0xc2132d05d31c914a87c6611c10748aeb04b58e8f", ABI = _baseABI } },
                { "USDC", new() { ContractAddress = "0x2791bca1f2de4661ed88a30c99a7a9449aa84174", ABI = _baseABI } }
            }
        }
    };
    public Dictionary<Chain, ChainInfo> ChainInfosDictionary { get; set; } = new();

    public Web3Utils(Dictionary<Chain, string> rpcDictionary)
    {
        foreach (var (chainId, rpcUrl) in rpcDictionary)
        {
            try
            {
                var w3 = new Web3(rpcUrl);
                var networkId = w3.Net.Version.SendRequestAsync().Result;
                Debug.WriteLine(networkId);
                ChainInfosDictionary.Add(chainId, new ChainInfo
                {
                    Endpoint = rpcUrl,
                    Provider = w3,
                    UsdtInfo = CoinInfosDictionary[chainId]["USDT"],
                    UsdcInfo = CoinInfosDictionary[chainId]["USDC"]
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Problem with rpc {rpcUrl}: {e}");
            }
        }
    }

    public class ChainInfo
    {
        public string Endpoint { get; set; }
        public Web3 Provider { get; set; }
        public CoinInfo UsdtInfo { get; set; }
        public CoinInfo UsdcInfo { get; set; }
    }

    public class CoinInfo
    {
        public string? ContractAddress { get; set; }
        public string? ABI { get; set; }
    }
}