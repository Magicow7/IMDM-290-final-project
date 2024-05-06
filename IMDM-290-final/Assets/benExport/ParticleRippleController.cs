//ben
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRippleController : MonoBehaviour
{
    public ParticleSystem ripple;

    void Start()    
    {
        //ripple.Stop();
    }

    void Update()   
    {
        GameObject wandshot = GameObject.FindWithTag("WandParticle");
        bool over = wandshot.GetComponent<DeleteTimer>().end; 
        if (over)  
        {
            StartCoroutine(CreateWaitDelete(wandshot, ripple));
        }
    }

    private IEnumerator CreateWaitDelete(GameObject hit, ParticleSystem ripple)  //called after wand particle dissipates (when it hits target hopefully)
    {
        ParticleSystem rippleClone = ripple;
        RippleReposition(hit, rippleClone);
        rippleClone.Play();
        yield return new WaitForSeconds(rippleClone.duration + 0.5f);
        rippleClone.Stop();
    }

    private void RippleReposition(GameObject hit, ParticleSystem ripple)   
    {
        //initialize rotation/position perpendicular to the spot that was shot
        ripple.transform.position = hit.transform.position;
        var playerPos = new Vector3(0,0,0);
        var direction = playerPos - ripple.transform.position;
        Quaternion rotation = Quaternion.LookRotation(playerPos, direction);
        //rotation.z = rotation.z * -1;
        ripple.transform.rotation = rotation;
    }


    //ig this is so that the ripple only plays if u hit a target / under the same conditions as a sound being triggered
    //private void OnTriggerEnter()   //~from SoundTrigger -- add script to same object as SoundTrigger (??)
    //{

    //    //collisionPoint.transform.position = collide.transform.position; 
    //    //var collision = Collider.ClosestPoint(collisionPoint.transform.position);

    //    //initialize rotation/position perpendicular to the spot that was shot
    //    ripple.transform.position = hit.transform.position;
    //    var direction = player.transform.position - ripple.transform.position;
    //    rotation.z = rotation.z * -1;
    //    ripple.transform.rotation = rotation;
    //    ripple.Play();
    //}
}

