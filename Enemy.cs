using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPrefab = null;
    private float fireRate = 10f;
    private float nextFire;

    public float speed = 3;

    [SerializeField]
    private GameObject enemyExplosionPrefab = null;

    [SerializeField]
    private AudioClip explosionSound = null;

    [SerializeField]
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        if (player != null)
            speed = 3 + player.GetComponent<Score>().complexity * 1.5f;
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);

        if (transform.position.y < -6.7f)
            Destroy(this.gameObject);

        if (Time.time > nextFire)
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, -0.65f, 0), Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Laser":
                Destroy(collision.gameObject);
                Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 1.0f);
                player.GetComponent<Score>().score += 15;
                break;
            case "Asteroid":
                if (transform.position.y < 4.5f)
                {
                    Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
                    AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 1.0f);
                    Destroy(this.gameObject);
                }
                break;
            case "Player":
                Player player_Controls = collision.GetComponent<Player>();

                if (player_Controls != null)
                {
                    player_Controls.lifeSubstraction();
                }
                Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 1.0f);
                break;
            case "Enemy":
                if (transform.position.y > 4.5f)
                {
                    if (transform.position.x > collision.transform.position.x)
                        transform.position = new Vector3(collision.transform.position.x + 0.7f, transform.position.y, 0);
                    else
                        transform.position = new Vector3(collision.transform.position.x - 0.7f, transform.position.y, 0);
                    if (transform.position.x > 7.5f || transform.position.x < -7.5f)
                        Destroy(this.gameObject);
                }
                break;
        }
    }
}
