using UnityEngine;

public class PlayerSpawn 
{
    private const string LastPlayerPosition = "LastPlayerPosition";

    private SceneTransitionData _transitionData;
    private Transform _transform;


    public PlayerSpawn(Transform transform, SceneTransitionData transitionData)
    {
        _transform = transform;
        _transitionData = transitionData;
    }


    public void Spawn()
    {
        _transform.position = _transitionData.GetSpawnPosition((int)SceneLoader.GetActiveScene());
    }
}
