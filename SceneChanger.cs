using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public bool fade;
    public float alphaColor;
    public Image fadeImage; //Черная картинка растянутая на весь экран, с 0 прозрачностью и выключенным рейкаст таргетом

    [SerializeField]
    private GameObject player = null;
    [SerializeField]
    private AudioClip explosion_sound = null;
    [SerializeField]
    private GameObject playerExplosionPrefab = null;
    public void Change_scene()
    { //Вызывается из UI
        fade = true;
    }

    private void StartScene()
    { 
        SceneManager.LoadScene(2);
    }

    private void Update()
    {
        if (fade)
        {
            alphaColor = Mathf.Lerp(fadeImage.color.a, 1, Time.deltaTime * 1.5f);
            fadeImage.color = new Color(0, 0, 0, alphaColor);
        }

        if (alphaColor > 0.95 && fade)
        {
            fade = false;
            StartScene();
        }

        if (player != null)
        {
            if (player.GetComponent<Player>().playerLives < 1)
            {
                SaveSystem.SetInt("Score", (int)player.GetComponent<Score>().score);
                Instantiate(playerExplosionPrefab, player.transform.position, Quaternion.identity);
                Destroy(player);
                Change_scene();
                AudioSource.PlayClipAtPoint(explosion_sound, Camera.main.transform.position, 1.0f);
            }
        }
    }
}
