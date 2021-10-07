using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        var targetPosition = target.position;
        transform.position = new Vector3(targetPosition.x, targetPosition.y + 6, targetPosition.z - 3);
    }
}
