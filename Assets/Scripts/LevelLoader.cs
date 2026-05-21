using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{
    [Header("Stars")]
    [SerializeField] public int totalStars = 2; //But set this in inspector DONT FORGET TO CHANGE THE UI TEXT TOO

    [Header("Fade Settings")]
    [SerializeField] public float duration = 1f;
    [SerializeField] public Image fadeImage;
    //[SerializeField] public UnityEngine.UI.Image fadeImage;



    //Once the number of stars collected is equal to the number of stars in the level, load the next level
    public void LoadNextLevel () //checking if the level is actually complete
    {
        //when the current star count is equal to the total star count, then we want to load the next level
        //totalStars = FindAnyObjectByType<PlayerInventory>().TotalStars; //get the total number of stars in the level from the player inventory

        if (FindAnyObjectByType<PlayerInventory>().NumberOfStars == totalStars) //check if it is equal to
        {
            //fade to black first
            //StartCoroutine(FadeToBlack)
            StartCoroutine(ImageFade()); //starting a timed fade


            //load the next level

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  ----- this loads the next level IMMEDIATELY


            //LoadNextLevel(); //call the function again to check if the next level is also completed ---- NOT HERE this is what I THINK froze the game
        }

        //wait a bit before switching levels? OR fade - UX - the player might feel rushed / not get a nice visual end to the level they just completed.

    }

    IEnumerator ImageFade() //starts a timed fade
    {
        //timer starts at 0, and goes up, so its how long it willl eventually take
        float timer = 0f;

        Color startColor = fadeImage.color;
        Color endColor = fadeImage.color;

        //.a = alpaha = transparency
        startColor.a = 0f; //invisible
        endColor.a = 1f; //fully visible

        while (timer < duration) //keep fading while timer is not finishes e.g time is 2 seconds, then loop will continue until 2 seconds is up
        {
            timer += Time.deltaTime; //how much time passed this frame
            fadeImage.color = Color.Lerp(startColor, endColor, timer / duration); //this is the actual fade
            //lerp - linear interpolation - moves between two values smoothly
            // timer / duration ----> timer 1. duration 2. 1/2 = 0.5. So lerp will return at 50% between the start and end colour.


            //wait one frame, then continue
            yield return null; // without this, the loop will run endlessly
        }

        //happens after the fade is done
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //yield return null;
        //fade over time
        //We need a start value for the alpha
        // then we need an end value for the alpha
        //timer for how long it takes to go from the start value to the end value

        //then load next level?
    }

}
