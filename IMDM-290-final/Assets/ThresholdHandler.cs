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

    public int level;

    public static ThresholdHandler instance;
    public List<serializableClass> nestedList = new List<serializableClass>();
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void updateThreshold(bool increase){
        if(increase){
            level++;
        }else{
            level--;
        }

        for(int i = 0; i < level; i++){
            foreach(GameObject g in nestedList[i].sampleList){
                g.SetActive(true);
            }
        }
        for(int i = level; i < nestedList.Count; i++){
            foreach(GameObject g in nestedList[i].sampleList){
                g.SetActive(false);
            }
        }
    }
}
