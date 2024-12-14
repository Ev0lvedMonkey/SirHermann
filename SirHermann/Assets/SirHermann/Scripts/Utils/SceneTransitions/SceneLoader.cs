using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    OpenScene = 0,
    HouseScene = 1
}

public static class SceneLoader
{
    public static Scenes GetActiveScene()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;

        if (System.Enum.TryParse(activeSceneName, out Scenes activeScene))
        {
            return activeScene;
        }
        else
        {
            Debug.LogWarning($"—цена с именем {activeSceneName} не найдена в перечислении Scene.");
            return Scenes.OpenScene; 
        }
    }


    public static void Load(Scenes targetScene)
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
