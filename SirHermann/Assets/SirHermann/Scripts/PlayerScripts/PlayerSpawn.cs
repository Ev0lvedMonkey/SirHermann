using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private SceneTransitionData _transitionData;


    private const string LastPlayerPosition = "LastPlayerPosition";

    void Start()
    {
        if (!PlayerPrefs.HasKey(LastPlayerPosition))
            transform.position = _transitionData.GetSpawnPosition((int)SceneLoader.GetActiveScene());
    }
}
