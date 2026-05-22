using UnityEngine;

//this script might end up being the audio manager later
public class AudioCrossfade : MonoBehaviour
{
    //type: double offers higher precision for something like timing audio
    public double musicDuration; //how long current clip lasts
    public double goalTime; //stores the exact dsp time when the next clip should begin
    public AudioSource[] _audioSources; //array of audio sources
    public int audioToggle; // just switches between audio sources
    public AudioClip currentClip; // stores music file itself
    public AudioSource audioSource; //audio source reference

    //current system plays one after the other, I need to crossfade, so add an overlap of sorts
    public double overlapTime = 1.0;

    private void Start()
    {
        OnPlayMusic();
    }
    private void OnPlayMusic()
    {
        //uinty's audio clock, its really accurate and is different from game time
        //+0.5 starts music half a second from now
        goalTime = AudioSettings.dspTime + 0.5;
        //assign audio clip
        audioSource.clip = currentClip;
        //instead of PLAY this one plays at a very specific dsp time
        audioSource.PlayScheduled(goalTime);

        //how many seconds long is this clip, total audio samples and frequency samples per second
        musicDuration = (double)currentClip.samples / currentClip.frequency;
        //the next clip should be right after this clip
        //goalTime = goalTime + musicDuration;
        goalTime = goalTime + musicDuration - overlapTime;
    }

    private void Update()
    {
        //if we are withihn x amount of seconds from the next loop point then prepare / start the next scheduled clip
        if (AudioSettings.dspTime > goalTime - 2)
        {   
            //this clip - this the overlap logic
            PlayScheduledClip();
        }
    }

    private void PlayScheduledClip()
    {
        //_audioSources[audioToggle] - pcik sorce 1 or 0. It depends on the toggle (in inspector)
        _audioSources[audioToggle].clip = currentClip; // load music into chosen source
        _audioSources[audioToggle].PlayScheduled(goalTime); //start it exactly at the goal time (in the future)

        //preparing the next loop again
        musicDuration = (double)currentClip.samples / currentClip.frequency;
        //goalTime = goalTime + musicDuration;
        goalTime = goalTime + musicDuration - overlapTime; 

        //I THINK (DOUBLE CHECK LATER) this bit alternates between 0, 1 then again 0,1,0,1
        audioToggle = 1 - audioToggle;
    }

    //this should let other scripts change music
    public void SetCurrentClip(AudioClip clip)
    {
        currentClip = clip;
    }


    //maybe this might let the sound continue on while the scene changes? Changing scene seems to cut off the sound
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}

//layering other tracks
// nothing shhould be destroyed when changing level
// and change to new track - current audio becomes a new clip once changed level
// but not waiting for the current track to reach the end
