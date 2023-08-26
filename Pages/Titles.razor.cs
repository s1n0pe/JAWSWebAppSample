using Fluxor;
using JAWSWebApp.Models;
using JAWSWebApp.Store;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace JAWSWebApp.Pages
{
    public partial class Titles
    {
        [Inject]
        private IState<SheetDataState> SheetDataState { get; set; }
        [Inject] public IDispatcher Dispatcher { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var client = new HttpClient();
            var result = await client.GetAsync(@"https://xxxxxx.com/getSheet");

            var resultStr = await result.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<SheetDataModel>>(resultStr);
            var action = new GetSheetDataAction(data);
            Dispatcher.Dispatch(action);

            await InvokeAsync(StateHasChanged);

            base.OnInitialized();
        }
    }
}
