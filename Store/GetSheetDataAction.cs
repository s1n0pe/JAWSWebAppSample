using JAWSWebApp.Models;

namespace JAWSWebApp.Store
{
    public class GetSheetDataAction
    {
        public GetSheetDataAction(List<SheetDataModel> data)
        {
            Value = data;
        }

        public List<SheetDataModel> Value { get; set; }
    }
}
