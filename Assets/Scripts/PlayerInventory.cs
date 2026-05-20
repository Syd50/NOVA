using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI starText; 

    //I've got the number of stars, but now I need the next thing - which is the total. Otherwise we don't know what 'NOT' to update
    public int NumberOfStars { get; private set; } //other scripts can get the value, but only this script can set the value
    

    //public int TotalStars { get; private set; }  //THIS VALUE NEEDS TO COME FROM SOMEWHERE 

    //every time the number changes, we need to rebuild the string? Might be a longer approach, but game is small

    //creting an event
    //this is what OTHER  scripts are going to listen for
    //public UnityEvent<PlayerInventory> OnStarCollected;

    //method called WHEN a star IS collected,
    public void StarCollected()
    {
        //increase the count by 1
        NumberOfStars++;

        //check if the level is complete when the collection part has happened
        //DO the event
        //invoke - trigger the event
        //this - sned THIS Player inventory as the thing that is being sent to the event listeners
        //OnStarCollected.Invoke(this);
        //but add to the first number, and keep the total number the same - only need to update one number
        starText.text = NumberOfStars.ToString() + " / " + FindAnyObjectByType<LevelLoader>().totalStars;

        //starText = NumberOfStars + "/" + TotalStars;
        FindAnyObjectByType<LevelLoader>().LoadNextLevel();
    }
}

//Star count is different now. Number / total - so we can cut the string up? Update x but not y? (x / y)
//OR, take a longer approach and write all possibilities for the star count - 0/3, 1/3, 2/3, 3/3
//an equation? startText = current numbert + "/" + total number
//then its updating, so it should be inside the start collected and we tell it to update the current one only
