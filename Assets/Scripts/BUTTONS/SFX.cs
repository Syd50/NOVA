using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioSource clickAudio;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void playButton()
    {
        clickAudio.Play();
        
    }

  
}
