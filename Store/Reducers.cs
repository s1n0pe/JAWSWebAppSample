using Fluxor;

namespace JAWSWebApp.Store
{
    public class Reducers
    {
        [ReducerMethod]
        public static SheetDataState ReduceGetSheetDataAction(SheetDataState state, GetSheetDataAction action)
        {
            return new SheetDataState(action.Value);
        }
    }
}
