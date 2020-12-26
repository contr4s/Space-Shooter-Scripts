using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    static private Transform _ENEMYLASER_ANCHOR;
    static Transform ENEMYLASER_ANCHOR
    {
        get
        {
            if (_ENEMYLASER_ANCHOR == null)
            {
                GameObject go = new GameObject("EnemyLaserAnchor");
                _ENEMYLASER_ANCHOR = go.transform;
            }
            return _ENEMYLASER_ANCHOR;
        }
    }


    private float laserSpeed;

    [SerializeField]
    private GameObject explosionPrefab = null;
    [SerializeField]
    private AudioClip explosionSound = null;
    [SerializeField]
    private GameObject enemyPrefab = null;

    void Start()
    {
        transform.SetParent(ENEMYLASER_ANCHOR, true);
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * laserSpeed);

        laserSpeed = enemyPrefab.GetComponent<Enemy>().speed * 2;

        if (transform.position.y <= -5.6)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            Destroy(collision.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 1.0f);
        }
        else if (collision.tag == "Player")
        {
            Player player_Controls = collision.GetComponent<Player>();

            if (player_Controls != null)
            {
                GameManager.gm.lifeSubstraction(1);
            }
            Destroy(this.gameObject);
        }
    }
}
