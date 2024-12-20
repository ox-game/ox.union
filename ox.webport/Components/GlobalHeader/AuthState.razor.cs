﻿using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using Microsoft.AspNetCore.Components.Authorization;
using OX.Wallets.States;
using Blazored.LocalStorage;
using OX.Wallets.Authentication;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.ABI.Model;
using OX.MetaMask;
using OX.Wallets.Eths;
using Nethereum.Util;
using OX.Wallets.Messages;
using OX.Network.P2P.Payloads;
using OX.Wallets.Mnemonics;

namespace OX.Mix.Components
{
    public partial class AuthState : IDisposable
    {
        [Inject]
        protected IStateDispatch StateDispatcher { get; set; }
        [Inject]
        public IMetaMaskService MetaMaskService { get; set; } = default!;
        [Inject]
        protected IHttpContextAccessor HttpContextAccessor { get; set; }
        //[Inject]
        //protected IEthereumContext EthereumContext { get; set; }
        [CascadingParameter]
        public EventCallback LanguageCallBack { get; set; }
        public EthID EthID { get; set; }
        public Block LastBlock { get; set; }
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


        protected override async Task OnInitializedAsync()
        {
            //Subscribe to events
            IMetaMaskService.AccountChangedEvent += MetaMaskService_AccountChangedEvent;
            IMetaMaskService.ChainChangedEvent += MetaMaskService_ChainChangedEvent;
            IMetaMaskService.OnConnectEvent += IMetaMaskService_OnConnectEvent;
            IMetaMaskService.OnDisconnectEvent += IMetaMaskService_OnDisconnectEvent;
            StateDispatcher.MixStateNotice += StateDispatcher_MixStateNotice;
            HasMetaMask = await MetaMaskService.HasMetaMask();
            if (HasMetaMask)
                await MetaMaskService.ListenToEvents();

            bool isSiteConnected = await MetaMaskService.IsSiteConnected();
            if (isSiteConnected)
            {
                await GetSelectedAddress();
                await GetSelectedNetwork();
            }
            if (this.Language.IsNotNullAndEmpty())
            {
                await this.LanguageCallBack.InvokeAsync(this.Language);
            }
        }

        private void StateDispatcher_MixStateNotice(IMixStateMessage message)
        {
            if (message.StateMessageKind == MixStateMessageKind.NewBlock)
            {
                NewBlockMessage msg = message as NewBlockMessage;
                this.LastBlock = msg.Block;
                InvokeAsync(StateHasChanged);
            }
        }

        private void IMetaMaskService_OnDisconnectEvent()
        {
            Console.WriteLine("Disconnect");
        }

        private void IMetaMaskService_OnConnectEvent()
        {
            Console.WriteLine("Connect");
        }

        private async Task MetaMaskService_ChainChangedEvent((long, Chain) arg)
        {
            Console.WriteLine("Chain Changed");
            await GetSelectedNetwork();
            StateHasChanged();
        }

        private async Task MetaMaskService_AccountChangedEvent(string arg)
        {
            Console.WriteLine("Account Changed");
            await GetSelectedAddress();
            StateHasChanged();
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
            Console.WriteLine($"Address: {SelectedAddress}");
        }

        public async Task GetSelectedNetwork()
        {
            var chainInfo = await MetaMaskService.GetSelectedChain();
            ChainID = (int)chainInfo.chainId;
            Chain = chainInfo.chain;

            SelectedChain = $"ChainID: {chainInfo.chainId}, Name: {chainInfo.chain.ToString()}";
            Console.WriteLine($"ChainID: {chainInfo.chainId}");
        }



        public new void Dispose()
        {
            IMetaMaskService.AccountChangedEvent -= MetaMaskService_AccountChangedEvent;
            IMetaMaskService.ChainChangedEvent -= MetaMaskService_ChainChangedEvent;
            IMetaMaskService.OnConnectEvent -= IMetaMaskService_OnConnectEvent;
            IMetaMaskService.OnDisconnectEvent -= IMetaMaskService_OnDisconnectEvent;
            StateDispatcher.MixStateNotice -= StateDispatcher_MixStateNotice;
            base.Dispose();
        }
        public async void ChangeLanguage(string language)
        {
            var u = language == "English" ? "en-us" : "zh-cn";
            this.Language = u;
            await this.SetLocalStorage("_ox_box_language", u);
            if (this.Language.IsNotNullAndEmpty())
            {
                await this.LanguageCallBack.InvokeAsync(this.Language);
            }
            await InvokeAsync(StateHasChanged);
        }
    }
}