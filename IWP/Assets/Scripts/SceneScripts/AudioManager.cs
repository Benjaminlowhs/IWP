using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip AttackingGrunt;
    public AudioClip bossAttackDouble;
    public AudioClip bossAttackSwing;
    public AudioClip getHitGrunt;
    public AudioClip hit;
    public AudioClip slash;
    public AudioClip background;
    public AudioClip earthquake;
    public AudioClip walk;
    public AudioClip jump;



    // Start is called before the first frame update
    void Start()
    {
        //musicSource.clip = background;
        //musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
