using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioToLight : MonoBehaviour
{
    /*
    0:heart
    1:bass 1
    */
    public List<AudioSource> audioSources;
    public List<Material> materials;

    public List<Vector4> functions;

	public float updateStep = 0.1f;
	public int sampleDataLength = 1024;

    public float forcedIntensity = 1;
    public bool forcedIntensityOn = false;

	private float currentUpdateTime = 0f;

	private float clipLoudness;
	private float[] clipSampleData;

	// Use this for initialization
	void Awake () {
		clipSampleData = new float[sampleDataLength];

	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		currentUpdateTime += Time.deltaTime;
		if (currentUpdateTime >= updateStep) {
			currentUpdateTime = 0f;
            for(int i = 0; i < audioSources.Count; i++){
                audioSources[i].clip.GetData(clipSampleData, audioSources[i].timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
                clipLoudness = 0f;
                foreach (var sample in clipSampleData) {
                    clipLoudness += Mathf.Abs(sample);
                }
                clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
                
                //should be 0-0.1
                float emissiveIntensity = Mathf.Clamp((clipLoudness * functions[i].x) + functions[i].y, functions[i].z, functions[i].w);
                //if(i == 1 || i == 0){Debug.Log(audioSources[i] + " " +clipLoudness);}
                if(forcedIntensityOn){
                    materials[i].SetColor("_EmissionColor", (new Color(255,255,255)) * forcedIntensity);
                }else{
                    materials[i].SetColor("_EmissionColor", (new Color(255,255,255)) * emissiveIntensity);
                }
                
            }
			
		}

	}
}
