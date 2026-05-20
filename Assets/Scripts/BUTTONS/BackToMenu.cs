using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void ClickToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
