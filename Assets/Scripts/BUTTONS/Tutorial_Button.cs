using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_Button : MonoBehaviour
{
    public void switchScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
