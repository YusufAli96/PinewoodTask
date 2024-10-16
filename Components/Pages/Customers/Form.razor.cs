using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using PinewoodTask.Models;

namespace PinewoodTask.Components.Pages.Customers
{
    public partial class Form
    {
        [Parameter]
        public Guid? CustomerID { get; set; }

        Customer _customer = new();
        bool _valid = true;

        protected override async Task OnInitializedAsync()
        {
            _customer = new();
            _valid = true;

            await Load();

            await base.OnInitializedAsync();
        }

        private async Task Load()
        {
            if (CustomerID != null)
            {
                var customer = await _customerDAL.Get(CustomerID.Value);
                if (customer != null)
                {
                    _customer = customer;
                    StateHasChanged();
                }
                else
                {
                    _snackbar.Add($"Customer does not exist", Severity.Error);
                    _navigationManager.NavigateTo("/");
                }
            }
        }

        private void Invalid()
        {
            _valid = false;
        }

        private async Task Save()
        {
            _valid = true;

            Guid customerID;

            if (CustomerID == null)
                customerID = await _customerDAL.Create(_customer);
            else
                customerID = await _customerDAL.Update(_customer);

            if (customerID != Guid.Empty)
            {
                _snackbar.Add($"New customer {_customer.FullName} {(CustomerID == null ? "created" : "updated")}", Severity.Success);
                _navigationManager.NavigateTo("/");
            }
            else
            {
                _snackbar.Add($"Failed to {(CustomerID == null ? "create" : "update")} customer {_customer.FullName}", Severity.Error);
            }
        }
    }
}