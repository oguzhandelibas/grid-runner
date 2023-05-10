using UnityEngine;

public class LevelLoaderCommand
{
    #region Self Variables

    #region Private Variables

    private GameObject _levelHolder;

    #endregion

    #endregion

    public LevelLoaderCommand(ref GameObject levelHolder)
    {
        Debug.Log("LevelLoaderCommand()");
        _levelHolder = levelHolder;
    }

    public void Execute()
    {
        Object.Instantiate(Resources.Load<GameObject>($"Levels/RunnerGameLevel {1}"),
            _levelHolder.transform);
    }
}
