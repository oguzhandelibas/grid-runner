using UnityEngine.Events;

namespace GridRunner.Grid.GridModule.Signals
{
    public class GridSignals : AbstractSingleton<GridSignals>
    {
        public UnityAction<int> onCreateGrid = delegate { };
    }
}
