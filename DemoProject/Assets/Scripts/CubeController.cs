using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].position.x < 100) transform.position += Vector3.left;

            if (Input.touches[0].position.x > 900) transform.position += Vector3.right;
        }
    }
}
