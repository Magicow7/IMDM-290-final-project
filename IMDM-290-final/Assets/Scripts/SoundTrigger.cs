using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public bool soundOn = false;

    public GameObject constellationLines;
    public AudioSource player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(){
        soundOn = !soundOn;
        ThresholdHandler.instance.updateThreshold(soundOn);
        constellationLines.SetActive(soundOn);
        StartCoroutine(volumeChange(soundOn));
        //player.mute = !soundOn;
    }

    private IEnumerator volumeChange(bool on){
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
}
