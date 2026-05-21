using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditButton : MonoBehaviour
{
   public void switchScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
