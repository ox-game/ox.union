using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using OX.MetaMask;
namespace OX.Tablet
{
    public abstract class AuthComponentBase : WebBoxComponentBase, IDisposable
    {
        public abstract string PageTitle { get; }
        [Inject]
        public IMetaMaskService MetaMaskService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            this.OnAuthInitialized();
            await this.OnInit();
            await base.OnInitializedAsync();
        }
        protected abstract Task OnInit();
        public abstract void OnDispose();
        protected virtual void OnAuthInitialized()
        {

        }



    }
}
