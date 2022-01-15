using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip hook, uppercut, lowKick, backGroundSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        uppercut = Resources.Load<AudioClip>("Upper");
        lowKick = Resources.Load<AudioClip>("LegKick");
        hook = Resources.Load<AudioClip>("Punch");
        backGroundSound = Resources.Load<AudioClip>("BCS1");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Upper":
                audioSrc.PlayOneShot(uppercut);
                break;
                case "LegKick":
                audioSrc.PlayOneShot(lowKick);
                break;
                case "Punch":
                audioSrc.PlayOneShot(hook);
                break;
                case "BCS1":
                audioSrc.PlayOneShot(backGroundSound);
                break;

        }
    }
}
