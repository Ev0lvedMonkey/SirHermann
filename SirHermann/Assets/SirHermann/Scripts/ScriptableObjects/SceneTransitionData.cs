using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scene Transition Data")]
public class SceneTransitionData : ScriptableObject
{
    [SerializeField] private  List<Vector3> _spawnPositions = new ();

    public Vector3 GetSpawnPosition(int positionIndex)
    {
        return _spawnPositions[positionIndex];
    }

}
