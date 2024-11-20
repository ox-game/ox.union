
using Microsoft.AspNetCore.Components;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using OX.Wallets;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using OX.Network.P2P.Payloads;
using OX;
using OX.IO;
using OX.Cryptography.ECC;
using OX.Ledger;
using OX.SmartContract;
using OX.Cryptography;
using OX.Bapps;
using AntDesign;
using OX.MetaMask;
using Microsoft.AspNetCore.Http;
using Nethereum.Util;
using Akka.IO;
using OX.Network.P2P;
using OX.Tablet.Config;

namespace OX.Tablet.Pages
{
    public class FormItemLayout
    {
        public ColLayoutParam LabelCol { get; set; }
        public ColLayoutParam WrapperCol { get; set; }
    }
    public class DepositModel
    {
        public string PoolEthAddress { get; set; }
        public string FromEthAddress { get; set; }
        public string OxAddress { get; set; }
        public decimal Amount { get; set; }
    }

    public partial class GoDeposit : IDisposable
    {
        public override string PageTitle => UIHelper.LocalString("入金", "Deposit");

        [Parameter]
        public string dealerethaddressHex { get; set; }
        string msg;
        DepositModel model { get; set; } = new DepositModel();
        UInt160 OTCDealerOXPoolScriptHash;
        string OTCDealerOXPoolAddress;
        Fixed8 OTCDealerOXPoolBalance = Fixed8.Zero;
        bool success = false;
        string ethtxid = string.Empty;
        string revertEthHash = string.Empty;
        private readonly FormItemLayout _formItemLayout = new FormItemLayout
        {
            LabelCol = new ColLayoutParam
            {
                Xs = new EmbeddedProperty { Span = 24 },
                Sm = new EmbeddedProperty { Span = 7 },
            },

            WrapperCol = new ColLayoutParam
            {
                Xs = new EmbeddedProperty { Span = 24 },
                Sm = new EmbeddedProperty { Span = 12 },
                Md = new EmbeddedProperty { Span = 10 },
            }
        };

        private readonly FormItemLayout _submitFormLayout = new FormItemLayout
        {
            WrapperCol = new ColLayoutParam
            {
                Xs = new EmbeddedProperty { Span = 24, Offset = 0 },
                Sm = new EmbeddedProperty { Span = 10, Offset = 7 },
            }
        };
        protected override void OnStateInit()
        {
            if (dealerethaddressHex.IsNotNullAndEmpty())
            {
                try
                {
                    var dealerethaddress = System.Text.Encoding.UTF8.GetString(dealerethaddressHex.HexToBytes());
                    this.model.PoolEthAddress = dealerethaddress;
                    var st = dealerethaddress.BuildOTCDealerTransaction();
                    OTCDealerOXPoolScriptHash = st.GetContract().ScriptHash;
                    OTCDealerOXPoolAddress = OTCDealerOXPoolScriptHash.ToAddress();
                    var account = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(OTCDealerOXPoolScriptHash);
                    if (account.IsNotNull() && account.Balances.TryGetValue(Mining.USDT_Asset, out OTCDealerOXPoolBalance))
                    {

                    }

                    if (this.EthID.IsNotNull())
                    {
                        this.model.FromEthAddress = this.EthID.EthAddress;
                        this.model.OxAddress = this.EthID.MapAddress.ToAddress();
                        if (this.Valid && this.ValidChain)
                        {
                            var n = string.Join(',', this.ValidChains.Select(m => m.ToString()));
                            this.msg = this.WebLocalString($"仅支持  {n}", $"Only supported {n}");
                        }
                    }
                }
                catch
                {

                }
            }
        }
        public override void OnDispose()
        {
            IMetaMaskService.AccountChangedEvent -= MetaMaskService_AccountChangedEvent;
            IMetaMaskService.ChainChangedEvent -= MetaMaskService_ChainChangedEvent;
            IMetaMaskService.OnConnectEvent -= IMetaMaskService_OnConnectEvent;
            IMetaMaskService.OnDisconnectEvent -= IMetaMaskService_OnDisconnectEvent;
        }
        protected override void IMetaMaskService_OnConnectEvent()
        {

        }

        protected override void IMetaMaskService_OnDisconnectEvent()
        {

        }

        protected override async Task MetaMaskService_ChainChangedEvent((long, Chain) arg)
        {
            await Task.CompletedTask;
        }


        protected override async Task MetaMaskService_AccountChangedEvent(string arg)
        {
            if (this.EthID.IsNotNull())
            {
                this.model.FromEthAddress = this.EthID.EthAddress;
                this.model.OxAddress = this.EthID.MapAddress.ToAddress();
            }
            await InvokeAsync(StateHasChanged);
        }
        public string WebLocalString(string str, string engstr)
        {
            return UIHelper.LocalString(str, engstr);
        }
        async Task HandleSubmit()
        {
            this.success = false;
            this.ethtxid = string.Empty;
            if (this.Valid && this.ValidChain)
            {
                var sh = this.model.OxAddress.ToScriptHash();
                try
                {
                    var r = await this.MetaMaskService.TrySimpleDeposit(this.model.PoolEthAddress, OTCDealerOXPoolScriptHash, this.model.Amount);
                    if (r.IsNotNullAndEmpty())
                    {
                        this.ethtxid = r;
                        if (OTCDealerHelper.DoSimpleDeposit(r, out var tx))
                        {
                             Program.BlockHandler.Tell(tx);
                            foreach (var coin in tx.Inputs)
                            {
                                SecureHelper.BlockIndex.UnconfirmCoins[coin] = Blockchain.Singleton.Height + 100;
                            }
                            this.success = true;
                        }

                        StateHasChanged();
                    }
                }
                catch (UserDeniedException e)
                {
                    this.msg = this.WebLocalString($"已经拒绝交易", $"Transaction has been rejected");
                    StateHasChanged();
                }
            }
        }
    }
}
