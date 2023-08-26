using JAWSWebApp.Models;

namespace JAWSWebApp.Store
{
    public class SheetDataState
    {
        public SheetDataState(List<SheetDataModel> sheetData)
        {
            SheetData = sheetData;
        }

        public List<SheetDataModel> SheetData { get; }
    }
}
