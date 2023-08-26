using Fluxor;
using JAWSWebApp.Models;

namespace JAWSWebApp.Store
{
    public class Feature : Feature<SheetDataState>
    {
        public override string GetName()
        {
            return "SheetData";
        }

        protected override SheetDataState GetInitialState()
        {
            var initState = new List<SheetDataModel>();
            return new SheetDataState(initState);
        }
    }
}
