using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_laser_controls : MonoBehaviour
{
    private float laserSpeed;

    [SerializeField]
    private GameObject explosionPrefab = null;
    [SerializeField]
    private AudioClip explosionSound = null;
    [SerializeField]
    private GameObject enemyPrefab = null;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * laserSpeed);

        if (transform.position.y >= 5.6)
        {
            Destroy(this.gameObject);
        }

        laserSpeed = enemyPrefab.GetComponent<Enemy_controls>().speed * 2;
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
            Player_controls player_Controls = collision.GetComponent<Player_controls>();

            if (player_Controls != null)
            {
                player_Controls.lifeSubstraction();
            }
            Destroy(this.gameObject);
        }
    }
}
