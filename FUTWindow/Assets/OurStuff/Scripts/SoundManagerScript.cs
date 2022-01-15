using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip hook, uppercut, lowKick, backGroundSound, dead1, dead2, dead3, startVoice;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        uppercut = Resources.Load<AudioClip>("Upper");
        lowKick = Resources.Load<AudioClip>("LegKick");
        hook = Resources.Load<AudioClip>("Punch");
        backGroundSound = Resources.Load<AudioClip>("BCS1");
        dead1 = Resources.Load<AudioClip>("Dead1");
        dead2 = Resources.Load<AudioClip>("Dead2");
        dead3 = Resources.Load<AudioClip>("Dead3");
        startVoice = Resources.Load<AudioClip>("SRV");

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
                case "Dead1":
                audioSrc.PlayOneShot(dead1);
                break;
                case "Dead2":
                audioSrc.PlayOneShot(dead2);
                break;
                case "Dead3":
                audioSrc.PlayOneShot(dead3);
                break;
                case "SRV":
                audioSrc.PlayOneShot(startVoice);
                break;

        }
    }
}
