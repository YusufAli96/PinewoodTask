using PinewoodTask.Constants;
using PinewoodTask.Models;

namespace PinewoodTask.Components.Pages.Customers
{
    public partial class List
    {
        private List<Customer> _customers = [];
        private bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await Load();
            await base.OnInitializedAsync();
        }

        private async Task Load()
        {
            _customers = await _customerDAL.GetAll();
            _isLoading = false;
            StateHasChanged();
        }

        private void Edit(Guid customerID)
        {
            _navigationManager.NavigateTo($"/Form/{customerID}");
        }

        private async Task Delete(Customer customer)
        {
            _isLoading = true;

            bool deleted = await _customerDAL.SoftDelete(customer);

            if (deleted)
                await Load();

            _isLoading = false;
        }

        private Func<Customer, int, string> _rowStyleFunc => (x, i) =>
        {
            if (x.AuditAction == AuditActionTypes.Delete)
                return "background-color:#EEE";

            return "";
        };
    }
}