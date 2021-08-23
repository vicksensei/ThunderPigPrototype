using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    int SceneToLoad;
    [SerializeField]
    bool LoadOnStart = false;
    // Start is called before the first frame update
    void Start()
    {
        if (LoadOnStart)
        {
            LoadScene();
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
