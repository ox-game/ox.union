using System;
using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Util;
using OX.MetaMask;
using System.Security.Claims;
using System.Threading.Tasks;
using OX.Bapps;
using System.Collections.Generic;
namespace OX.Tablet
{
    public abstract class StatesComponentBase : AuthComponentBase
    {
        protected bool Valid => EthID.IsNotNull();
        public EthID EthID { get; set; }
        public bool HaveEthID { get { return EthID.IsNotNull(); } }

        public bool HasMetaMask { get; set; }
        public string? InitializeMsg { get; set; }
        public string? SelectedChain { get; set; }
        public string? TransactionCount { get; set; }
        public string? SignedData { get; set; }
        public string? SignedDataV4 { get; set; }
        public string? PersonalSigned { get; set; }
        public string? FunctionResult { get; set; }
        public string? RpcResult { get; set; }
        public int? ChainID { get; set; }
        public Chain Chain { get; set; }


        protected bool ValidMainChain
        {
            get
            {
                return this.ChainID.HasValue && this.Chain == Chain.Mainnet;
            }
        }
        protected bool ValidChain
        {
            get
            {
                if (this.ChainID.HasValue)
                {
                    if (SecureHelper.BlockIndex.IsNotNull())
                    {
                        var miningIndex = SecureHelper.BlockIndex.GetSubBlockIndex<MiningBlockIndex>();
                        var settings = miningIndex.GetAllInvestSettings();
                        var setting = settings.FirstOrDefault(m => Enumerable.SequenceEqual(m.Key, new[] { InvestSettingTypes.ValidEthChain }));
                        if (!setting.Equals(new KeyValuePair<byte[], InvestSettingRecord>()))
                        {
                            if (setting.Value.Value.IsNotNullAndEmpty())
                            {
                                foreach (var c in setting.Value.Value.Split('-'))
                                {
                                    if (this.ChainID.Value.ToString() == c) return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }
        }
        protected Chain[] ValidChains
        {
            get
            {
                List<Chain> result = new List<Chain>();
                if (this.ChainID.HasValue)
                {
                    if (SecureHelper.BlockIndex.IsNotNull())
                    {
                        var miningIndex = SecureHelper.BlockIndex.GetSubBlockIndex<MiningBlockIndex>();
                        var settings = miningIndex.GetAllInvestSettings();
                        var setting = settings.FirstOrDefault(m => Enumerable.SequenceEqual(m.Key, new[] { InvestSettingTypes.ValidEthChain }));
                        if (!setting.Equals(new KeyValuePair<byte[], InvestSettingRecord>()))
                        {
                            if (setting.Value.Value.IsNotNullAndEmpty())
                            {
                                foreach (var c in setting.Value.Value.Split('-'))
                                {
                                    result.Add((Chain)int.Parse(c));
                                }
                            }
                        }
                    }
                }
                return result.ToArray();
            }
        }

        protected override async Task OnInit()
        {
            IMetaMaskService.AccountChangedEvent += MetaMaskService_AccountChangedEvent;
            IMetaMaskService.ChainChangedEvent += MetaMaskService_ChainChangedEvent;
            IMetaMaskService.OnConnectEvent += IMetaMaskService_OnConnectEvent;
            IMetaMaskService.OnDisconnectEvent += IMetaMaskService_OnDisconnectEvent;
            HasMetaMask = await MetaMaskService.HasMetaMask();
            if (HasMetaMask)
                await MetaMaskService.ListenToEvents();

            bool isSiteConnected = await MetaMaskService.IsSiteConnected();
            if (isSiteConnected)
            {
                await GetSelectedAddress();
                await GetSelectedNetwork();
            }
            this.OnStateInit();
        }



        public async Task ConnectMetaMask()
        {
            try
            {
                await MetaMaskService.ConnectMetaMask();

                await GetSelectedAddress();
            }
            catch (UserDeniedException)
            {
                InitializeMsg = "User Denied";
            }
            catch (Exception ex)
            {
                InitializeMsg = ex.ToString();
            }
        }

        public async Task GetSelectedAddress()
        {
            var SelectedAddress = await MetaMaskService.GetSelectedAddress();
            this.EthID = default;
            if (SelectedAddress.IsNotAnEmptyAddress())
            {
                this.EthID = new EthID(SelectedAddress);
            }
        }

        public async Task GetSelectedNetwork()
        {
            var chainInfo = await MetaMaskService.GetSelectedChain();
            ChainID = (int)chainInfo.chainId;
            Chain = chainInfo.chain;
            SelectedChain = $"ChainID: {chainInfo.chainId}, Name: {chainInfo.chain.ToString()}";
            Console.WriteLine($"ChainID: {chainInfo.chainId}");
        }

        protected abstract void OnStateInit();

        protected abstract void IMetaMaskService_OnConnectEvent();

        protected abstract void IMetaMaskService_OnDisconnectEvent();

        protected abstract Task MetaMaskService_ChainChangedEvent((long, Chain) arg);


        protected abstract Task MetaMaskService_AccountChangedEvent(string arg);





        public override void OnDispose()
        {
            IMetaMaskService.AccountChangedEvent -= MetaMaskService_AccountChangedEvent;
            IMetaMaskService.ChainChangedEvent -= MetaMaskService_ChainChangedEvent;
            IMetaMaskService.OnConnectEvent -= IMetaMaskService_OnConnectEvent;
            IMetaMaskService.OnDisconnectEvent -= IMetaMaskService_OnDisconnectEvent;
        }

    }
}
