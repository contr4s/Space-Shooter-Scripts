using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    static private Transform _PLAYERLASER_ANCHOR;
    static Transform PLAYERLASER_ANCHOR
    {
        get
        {
            if (_PLAYERLASER_ANCHOR == null)
            {
                GameObject go = new GameObject("PlayerLaserAnchor");
                _PLAYERLASER_ANCHOR = go.transform;
            }
            return _PLAYERLASER_ANCHOR;
        }
    }

    [SerializeField]
    private int laserSpeed = 10;

    void Start()
    {
        transform.SetParent(PLAYERLASER_ANCHOR, true);
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
