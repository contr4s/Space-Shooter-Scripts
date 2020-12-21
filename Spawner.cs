using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject asteroidPrefab;

    public GameObject player;

    void Start()
    {
        StartCoroutine(Clone_enemy());
        StartCoroutine(Clone_asteroid());
    }

    IEnumerator Clone_enemy()
    {
        while (true)
        {
            Instantiate(enemyPrefab, new Vector3(Random.Range(-7.5f, 7.5f), 5.8f, 0), Quaternion.identity);
            if (player != null)
                yield return new WaitForSeconds(1 / player.GetComponent<Score>().complexity);
            else
                yield break;
        }
    }

    IEnumerator Clone_asteroid()
    {
        while (true)
        {
            Instantiate(asteroidPrefab, new Vector3(Random.Range(-7.5f, 7.5f), 5.8f, 0), Quaternion.identity);
            yield return new WaitForSeconds(2.5f);
        }
    }
}
