using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThresholdHandler : MonoBehaviour
{
    [System.Serializable]
    public class serializableClass
    {
        public List<GameObject> sampleList;
    }

    public int level = -1;
    public Material skybox1;
    public Material skybox2;
    public Material skybox3;
    public Material skybox4;

    public static ThresholdHandler instance;
    public List<serializableClass> nestedList = new List<serializableClass>();
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        updateThreshold(true);
        changeSkybox(level);
    }

    public void updateThreshold(bool increase){
        if(increase){
            level++;
        }else{
            level--;
        }

        for(int i = 0; i <= level; i++){
            foreach(GameObject g in nestedList[i].sampleList){
                changeSkybox(level);
                /*if (g.tag == "Particle")
                {

                }*/
                g.SetActive(true);
            }
        }
        for(int i = level; i < nestedList.Count; i++){
            foreach(GameObject g in nestedList[i].sampleList){
                g.SetActive(false);
            }
        }
    }

    public void changeSkybox(int level) 
    {
        if (level < 3)
        {
            RenderSettings.skybox = skybox1;
        }
         else if (level >= 3)
        {
            RenderSettings.skybox = skybox2;
        }
    }
}
