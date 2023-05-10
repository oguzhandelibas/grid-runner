using UnityEngine;
using GridRunner.Runner.CoreGameModule.Signals;

public class PlayerSpawnPosition : MonoBehaviour
{
    private void OnEnable()
    {
        CoreGameSignals.Instance.onSetPlayerSpawnPosition?.Invoke(this.transform.position);
    }
}
