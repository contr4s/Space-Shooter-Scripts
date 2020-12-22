using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private int speed = 5;

    [SerializeField]
    private GameObject explosionPrefab = null;
    [SerializeField]
    private AudioClip explosionSound = null;

    private float horizontalMovement;

    public GameObject player;

    void Start()
    {
        horizontalMovement = Random.Range(-1f, 1f);
        player = GameObject.Find("Player");
    }

   
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed + Vector3.right * horizontalMovement * Time.deltaTime * speed);

        if (transform.position.y < -6.7f)
        {
            transform.position = new Vector3(Random.Range(-7.5f, 7.5f), 5.8f, 0);
            horizontalMovement = Random.Range(-1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Laser":
                Destroy(collision.gameObject);
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 1.0f);
                GameManager.gm.AddPoints(5);
                break;
            case "enemy_laser":
                Destroy(collision.gameObject);
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 1.0f);
                break;
            case "Asteroid":
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
                if (transform.position.y < 4.5f)
                {
                    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                    AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 1.0f);
                }
                break;
            case "Enemy":
                if (transform.position.y < 4.5f)
                    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                break;
            case "Player":
                Player player_Controls = collision.GetComponent<Player>();

                if (player_Controls != null)
                {
                    GameManager.gm.lifeSubstraction(1);
                }
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 1.0f);
                break;
        }
    }
}
