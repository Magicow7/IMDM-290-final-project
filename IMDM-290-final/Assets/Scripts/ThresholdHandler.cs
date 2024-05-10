using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class ThresholdHandler : MonoBehaviour
{
    [System.Serializable]
    public class serializableClass
    {
        public List<GameObject> sampleList;
    }
    public int level;

    public static ThresholdHandler instance;
    public List<serializableClass> nestedList = new List<serializableClass>();

    public Material skybox1;
    public Material skybox2;
    public Material skybox3;
    public Material skybox4;

    public AsteroidController asteroidHandler;
    public ParticleSystem myParticleSystem;
    public GameObject startScreen;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //Update is called to turn on objects who are at threshold 0, and turn level to 0
        updateThreshold(true);
    }

    public void updateThreshold(bool increase){
        if(increase){
            level++;
        }else{
            level--;
        }
        //Turn off the start screen once anything is active
        if (level > 1)
        {
            startScreen.SetActive(false);
        }

        for(int i = 0; i <= level; i++){
            foreach(GameObject g in nestedList[i].sampleList){
                g.SetActive(true);
            }
        }
        for(int i = level; i < nestedList.Count; i++){
            foreach(GameObject g in nestedList[i].sampleList){
                g.SetActive(false);
            }
        }
        changeSkybox(level);
        changeAsteroids(level);
        changeParticles(level);
    }
    public void changeSkybox(int level)
    {
        if (level <= 3)
        {
            RenderSettings.skybox = skybox1;
        }
        if (level > 3 && level <= 6)
        {
            RenderSettings.skybox = skybox2;
        }
        if (level > 6 && level <= 9)
        {
            RenderSettings.skybox = skybox3;
        }
        if (level > 9)
        {
            RenderSettings.skybox = skybox4;
        }
    }
    public void changeAsteroids(int level)
    {
        asteroidHandler.asteroidWaitTime = 16 - level;
    }
    
    public void changeParticles(int level)
    {
        var emission = myParticleSystem.emission;
        emission.rateOverTime = level * 50;
        if (level <= 3)
        {
            emission.rateOverTime = 25;
        }
        if (level > 3 && level <= 6)
        {
            emission.rateOverTime = 150;
        }
        if (level > 6 && level <= 9)
        {
            emission.rateOverTime = 400;
        }
        if (level > 9)
        {
            emission.rateOverTime = 700;
        }
    }
}

