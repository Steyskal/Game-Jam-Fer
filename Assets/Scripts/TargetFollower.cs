using UnityEngine;
using System.Collections;

public class TargetFollower : MonoBehaviour {

    public Transform target;
    public float yOffset;

    void Update(){
        transform.position = new Vector3(target.position.x, target.position.y + yOffset, target.position.z);
    }
}
