using Akka.Actor;
using OX.Bapps;
using OX.Cryptography.ECC;
using OX.Ledger;
using OX.Network.P2P;
using OX.Network.P2P.Payloads;
using OX.Persistence.LevelDB;
using OX.SmartContract;
using OX.Wallets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OX.WebPort;
using OX.WebPort.Config;
using Settings = OX.WebPort.Settings;
namespace OX.WebPort
{
    public abstract class BaseWebPortBlockHandler : UntypedActor
    {
        protected static Props _Instance = default;

        public OXSystem oxsystem { get; protected set; }
        public abstract void OnStart();
        public abstract void OnStop();
        protected abstract void OnReceived(object message);
        protected abstract void OnBlockPersistCompleted(Block block);
        protected abstract void OnFlashMessageCaptured(FlashMessage flashMessage);

        public BaseWebPortBlockHandler(OXSystem system)
        {
            this.oxsystem = system;
        }
        protected override void OnReceive(object message)
        {
            if (message is Blockchain.PersistCompleted completed)
            {
                OnBlockPersistCompleted(completed.Block);
            }
            else if (message is Blockchain.FlashMessageCaptured flashMessageCaptured)
            {
                Bapp.OnFlashMessageCaptured(flashMessageCaptured.FlashMessage);
                OnFlashMessageCaptured(flashMessageCaptured.FlashMessage);
            }
            else if (message is IInventory inventory)
            {
                this.Relay(inventory);
            }
            else
            {
                OnReceived(message);
            }
        }
        public virtual void Start()
        {
            this.OnStart();
            Context.System.EventStream.Subscribe(Self, typeof(Blockchain.PersistCompleted));
            Context.System.EventStream.Subscribe(Self, typeof(Blockchain.FlashMessageCaptured));
        }
        public virtual void Stop()
        {
            Context.System.EventStream.Unsubscribe(Self, typeof(Blockchain.PersistCompleted));
            Context.System.EventStream.Unsubscribe(Self, typeof(Blockchain.FlashMessageCaptured));
            this.OnStop();
        }
        public void Relay(IInventory inventory)
        {
            this.oxsystem.LocalNode.Tell(new LocalNode.Relay { Inventory = inventory });
        }
        public void SendDirectly(IInventory inventory)
        {
            this.oxsystem.LocalNode.Tell(new LocalNode.SendDirectly { Inventory = inventory });
        }
        public void RelayFlash(FlashMessage flashMessage)
        {
            this.oxsystem.LocalNode.Tell(new LocalNode.RelayFlash { FlashMessage = flashMessage });
        }
    }
    public class WebPortBlockHandler : BaseWebPortBlockHandler
    {
        public static event BlockChainHandler<Block> SyncBlocksCompleted;
        public static event BlockChainHandler<Block> BlockCompleted;
        public static event BlockChainHandler<FlashMessage> FlashMessageCaptured;
        public WebPortBlockHandler(OXSystem oxsystem) : base(oxsystem)
        {
            this.Start();
        }
        static IActorRef _instance;
        public static IActorRef Instance
        {
            get
            {
                if (_instance == default)
                {
                    _instance = Build();
                }
                return _instance;
            }
        }
        static IActorRef Build()
        {
            var seeds = Settings.Default.SeedNode.Seeds;
            var extseeds = NodeConfig.Instance.ExtSeedPeers.Select(m => m.ToString()).ToArray();  //Settings.Default.ExtSeeds;
            if (extseeds != default && extseeds.Length > 0)
            {
                seeds = extseeds.Union(seeds).ToArray();
            }
            var collectSeeds = NodeConfig.Instance.CollectPeers.Select(m => m.ToString()).ToArray();
            if (collectSeeds != default && collectSeeds.Length > 0)
            {
                seeds = collectSeeds.Union(seeds).ToArray();
            }

            ProtocolSettings.InitSeed(seeds, Settings.Default.P2P.OnlySeed);
            LevelDBStore store = default;
            try
            {
                store = new LevelDBStore(Settings.Default.Paths.Chain);
            }
            catch
            {
                var path = Settings.Default.Paths.Chain;
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                store = new LevelDBStore(Settings.Default.Paths.Chain);
            }
            var oxsystem = new OXSystem(store);
            return oxsystem.ActorSystem.ActorOf(Akka.Actor.Props.Create(() => new WebPortBlockHandler(oxsystem)));
        }

        public override void OnStart()
        {
            this.oxsystem.StartNode(Settings.Default.P2P.Port, Settings.Default.P2P.WsPort, 0);
        }
        public override void OnStop()
        {
            if (_instance != null)
                oxsystem.ActorSystem.Stop(_instance);
        }
        protected override void OnReceived(object message)
        {
        }
        protected override void OnBlockPersistCompleted(Block block)
        {
            BlockCompleted?.Invoke(block);
            if (Blockchain.Singleton.Height == Blockchain.Singleton.HeaderHeight)
                SyncBlocksCompleted?.Invoke(block);
        }
        protected override void OnFlashMessageCaptured(FlashMessage flashMessage)
        {
            FlashMessageCaptured?.Invoke(flashMessage);
        }

    }
}
