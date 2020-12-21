using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser_controls : MonoBehaviour
{
    [SerializeField]
    private int laserSpeed = 10;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * laserSpeed);

        if (transform.position.y >= 5.6)
        {
            Destroy(this.gameObject);
        }
    }
}
