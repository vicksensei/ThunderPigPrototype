
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(scene);
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(scene.name);
    }

    public void NextLevel() { }
    public void MenuLevel() { }
}
