using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class AsteroidController : MonoBehaviour
{
    //pick point in distance, all at same dist. from player
    //randomize size slightly (& shape? how?)
    //initialize asteroid in distance (fade into view)
    //make path toward player (???) *

    public float speed = 10f;
    public float sizeScale; //sets max size (*1) 
    public GameObject rock;
    public float frequency;
    public float amplitude;
    public float rockAmount = 5f; //1-10 scale?
    public Vector3 player = new Vector3(0, 0, 1);
    public float fadeAfter = 10f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject nRock = CreateRock();
        Vector3 startPos = nRock.transform.position;

        GameObject projectile = Instantiate(nRock, startPos, nRock.transform.rotation);
        Vector3 average = Vector3.Lerp(startPos, player, 0.5f);
        projectile.GetComponent<Rigidbody>().AddForce(average * speed, ForceMode.Force);

        var rand = UnityEngine.Random.Range(0, 30);
        if (rand < rockAmount)
        {
            //CreateRock();
            StartCoroutine(Shoot(nRock));    //should start shoot movement And initialize rock?    
        }
    }

    private IEnumerator Shoot(GameObject projectile)
    {
        Vector3 startPos = projectile.transform.position;
        GameObject nRock = Instantiate(projectile, startPos, projectile.transform.rotation);
        Vector3 average = Vector3.Lerp(startPos, player, 0.5f);
        nRock.GetComponent<Rigidbody>().AddForce(average * speed, ForceMode.Force);

        yield return new WaitForSeconds(fadeAfter);
        StartCoroutine(FadeOut(nRock));
    }

    public IEnumerator FadeOut(GameObject obj)
    {
        MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
        Color colour = meshRenderer.materials[0].color;

        while (colour.a > 0)
        {
            colour.a -= 0.1f;
            meshRenderer.materials[0].color = colour;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitUntil(() => meshRenderer.materials[0].color.a <= 0f);
        Destroy(obj); 
    }

    private GameObject CreateRock()     //randomizes size and start location
    {
        float size = sizeScale * UnityEngine.Random.Range((float).7, 1); //random range determines how much size variation there will be
        GameObject newRock = rock;
        newRock.transform.localScale.Set(size, size, size);

        float x = Mathf.Cos(Time.time * frequency) * amplitude;
        float y = Mathf.Sin(Time.time * frequency) * amplitude;
        float z = UnityEngine.Random.Range(0, 10);
        newRock.transform.position = new Vector3(x, y, z);
        return newRock;
    }   
}
