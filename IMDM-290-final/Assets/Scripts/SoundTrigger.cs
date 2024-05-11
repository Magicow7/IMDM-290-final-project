using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public bool soundOn = false;

    public GameObject constellationLines;
    public ParticleSystem particleEffect;
    public AudioSource player;
    public AudioSource soundEffectSource;
    public AudioClip soundEffect;

    private bool toggleable = true;
    // Start is called before the first frame update
    void Awake()
    {
        toggleable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(){
        if(toggleable == false){
            return;
        }
        soundOn = !soundOn;
        if(soundOn){
            particleEffect.Play();
            soundEffectSource.PlayOneShot(soundEffect);
        }
        
        StartCoroutine(volumeChange(soundOn));
        
        //player.mute = !soundOn;
    }

    public IEnumerator volumeChange(bool on){
        StartCoroutine(cooldown());
        ThresholdHandler.instance.updateThreshold(soundOn);
        constellationLines.SetActive(soundOn);
        for(int i = 0; i < 10; i++){
            yield return new WaitForSeconds(0.1f);
            if(on){
                player.volume = i*0.1f;
            }else{
                player.volume = 1 - (i*0.1f);
            }
        }
        if(on){
                player.volume = 1.0f;
            }else{
                player.volume = 0f;
            }
    }

    public IEnumerator cooldown(){
        toggleable = false;
        yield return new WaitForSeconds(5f);
        toggleable = true;

    }
}
