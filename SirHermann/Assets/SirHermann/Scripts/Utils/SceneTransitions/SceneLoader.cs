using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    OpenScene = 0,
    HouseScene = 1
}

public static class SceneLoader
{
    public static Scene GetActiveScene()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;

        if (System.Enum.TryParse(activeSceneName, out Scene activeScene))
        {
            return activeScene;
        }
        else
        {
            Debug.LogWarning($"—цена с именем {activeSceneName} не найдена в перечислении Scene.");
            return Scene.OpenScene; // ¬озвращаем значение по умолчанию
        }
    }


    public static void Load(Scene targetScene)
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
