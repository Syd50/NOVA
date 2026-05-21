using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{


    public void switchScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

 

}
