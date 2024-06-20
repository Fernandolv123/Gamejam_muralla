using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{
    public float health;
    public List<Transform> WayPoints;
    private Transform nextPosition;
    private Transform initialposition;
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine("Patrol"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (nextPosition == null || initialposition == null) return;
        // Debug.Log("{VECTOR A}" + transform.position);
        // Debug.Log("{VECTOR B}" + nextPosition.position);
        // Debug.Log(Vector3.Distance(transform.position,nextPosition.position));
        transform.position = Vector3.Lerp(initialposition.position,nextPosition.position,Time.deltaTime);
        //transform.Translate(new Vector3(nextPosition.position.x-initialposition.position.x,0,0)*Time.deltaTime);
    }

    public IEnumerator Patrol(){
        while(true){
        foreach(Transform waypoint in WayPoints){
            initialposition = transform;
            nextPosition= waypoint;
            yield return new WaitUntil(() => Vector3.Distance(transform.position,waypoint.position)<0.2);
        }
        yield return null;
        }
    }
}
