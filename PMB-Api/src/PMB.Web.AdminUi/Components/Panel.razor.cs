using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using PMB.Admin.Domain;
using PMB.Web.AdminUi.Models;
using PMB.Web.AdminUi.Services;

namespace PMB.Web.AdminUi.Components
{
    public class PanelRazor: ComponentBase
    {
        [Inject]
        protected AdminApiClient AdminApiClient { get; set; }
        
        [Inject]
        protected IMatToaster Toaster { get; set; }
        
        protected UserModel[] Users = Array.Empty<UserModel>();
        private string _expanded = string.Empty;

        protected bool DateChangeDialogIsOpen = false;
        protected DateTime DateChangeSelected = DateTime.Now;
        protected UserModel.KeyModel DateChangeDialogKeyModel = null;

        protected bool AddNewKeyDialogIsOpen = false;
        protected string AddNewKeyLoginSelected = string.Empty;
        protected DateTime AddNewKeyDateSelected = DateTime.Now;

        private string _searchValue = string.Empty;
        
        protected override async Task OnInitializedAsync()
        {
            await InitializeInternal();
            await base.OnInitializedAsync();
        }

        private async Task InitializeInternal()
        {
            SaveExpanded();

            var result = await AdminApiClient.Users(_searchValue, CancellationToken.None);

            var validResult = result.Validate(Toaster);
            if (validResult == null)
            {
                return;
            }
            
            Users = validResult?.Users.Select(x => x.Convert()).ToArray() ?? Array.Empty<UserModel>();

            foreach (var user in Users)
            {
                if (user.Login == _expanded)
                {
                    user.Expanded = true;
                }
            }
            
            await InvokeAsync(StateHasChanged);
        }

        protected async Task ChangeKeyState(bool toggled, string key, string login)
        {
            if (toggled)
            {
                await UnfreezeKey(login, key);
            }
            else
            {
                await FreezeKey(login, key);
            }
        }
    
        private async Task FreezeKey(string login, string key)
        {
            var result = await AdminApiClient.FreezeKey(new FreezeKeyCommand(login, key), CancellationToken.None);
            
            var validResult = result.Validate(Toaster);
            if (validResult == null)
            {
                return;
            }
            
            await InvokeAsync(InitializeInternal);
        }
    
        private async Task UnfreezeKey(string login, string key)
        {
            var result = await AdminApiClient.UnfreezeKey(new UnfreezeKeyCommand(login, key), CancellationToken.None);
            
            var validResult = result.Validate(Toaster);
            if (validResult == null)
            {
                return;
            }
            
            await InvokeAsync(InitializeInternal);
            
        }

        protected void DialogDateChangeOpen(UserModel.KeyModel model)
        {
            DateChangeDialogKeyModel = model;
            DateChangeSelected = model.KeyExpirationTime.ToLocalTime().DateTime;
            DateChangeDialogIsOpen = true;
        }

        protected void DialogDateChangeClose()
        {
            DateChangeDialogKeyModel = null;
            DateChangeSelected = DateTime.Now;
            DateChangeDialogIsOpen = false;
        }

        protected async Task DialogDateChangeOk()
        {
            var oldDate = DateChangeDialogKeyModel.KeyExpirationTime;

            var result = await AdminApiClient.ChangeKeyLifetime(
                new ChangeKeyLifetimeCommand(DateChangeDialogKeyModel.Login, DateChangeDialogKeyModel.Key, DateChangeSelected.WithDate(oldDate)), CancellationToken.None);

            var validResult = result.Validate(Toaster);
            if (validResult == null)
            {
                return;
            }
            
            DialogDateChangeClose();
            await InvokeAsync(InitializeInternal);
        }
    
        protected void DialogAddNewKeyOpen(string login)
        {
            AddNewKeyDateSelected = DateTime.Now;
            AddNewKeyLoginSelected = login;
            AddNewKeyDialogIsOpen = true;
        }
        
        protected void DialogAddNewKeyClose()
        {
             AddNewKeyDialogIsOpen = false;
             AddNewKeyDateSelected = DateTime.Now;
             AddNewKeyLoginSelected = string.Empty;
        }

        protected async Task DialogAddNewKeyOk()
        {
            var now = DateTimeOffset.Now;

            var result = await AdminApiClient.CreateKey(new CreateKeyCommand(AddNewKeyLoginSelected, AddNewKeyDateSelected.WithDate(now)), CancellationToken.None);
            
            var validResult = result.Validate(Toaster);
            if (validResult == null)
            {
                return;
            }
            
            DialogAddNewKeyClose();
            await InvokeAsync(InitializeInternal);
        }

        protected async Task RemoveKey(UserModel.KeyModel model)
        {
            var result = await AdminApiClient.RemoveKey(new RemoveKeyCommand(model.Login, model.Key), CancellationToken.None);

            var validResult = result.Validate(Toaster);
            if (validResult == null)
            {
                return;
            }

            await InvokeAsync(InitializeInternal);
        }

        protected async Task SearchByLoginCallback(ChangeEventArgs value)
        {
            var searchValue = value.Value as string;
            _searchValue = string.IsNullOrEmpty(searchValue) ? string.Empty : searchValue;
            
             await InvokeAsync(InitializeInternal);
        }
        
        protected string colStyle => "width:25%";

        protected string NormalizeDate(DateTimeOffset dt) => dt.ToLocalTime().DateTime.ToString("s").Replace("T", " ");
        
        private void SaveExpanded() => _expanded = Users?.FirstOrDefault(x => x.Expanded)?.Login;
    }
}