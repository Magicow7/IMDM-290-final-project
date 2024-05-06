using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTimer : MonoBehaviour
{

    public float aliveTime = 5f;
    public bool end = false; 
    public float buffer = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitThenDelete());
    }

    private IEnumerator WaitThenDelete(){
        yield return new WaitForSeconds(aliveTime - buffer);
        end = true; 
        yield return new WaitForSeconds(buffer);
        end = false; 
        Destroy(transform.gameObject);
    }

}
