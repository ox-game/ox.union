using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OX.Tablet
{
    public class WebBoxComponentBase : AntDomComponentBase
    {
        [Inject]
        protected IHttpContextAccessor HttpContextAccessor { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {

            await base.OnInitializedAsync();
            this.OnInitWebBox();
        }
        protected virtual void OnInitWebBox()
        {

        }

    }
}
