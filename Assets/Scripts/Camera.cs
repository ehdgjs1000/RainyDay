using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public float camDistance = -10f;
    // Start is called before the first frame update
    void Start()
    {

    }

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, 0, camDistance);
    }

}
