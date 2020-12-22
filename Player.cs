using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPrefab = null;
    private float fireRate = 0.3f;
    private float nextFire;

    [SerializeField]
    private GameObject playerFire1 = null;
    [SerializeField]
    private Animator fireAnim = null;
    [SerializeField]
    private GameObject playerFire2 = null;
    [SerializeField]
    private Animator fireAnim2 = null;

    [SerializeField]
    private Animator playerAnimator = null;

    public int playerLives = 5;

    private int playerSpeed = 10;
 
    private float joysticSpeed = 0.4f;

    private AudioSource laserShot;

    [SerializeField]
    public VariableJoystick variableJoystick;

    private Vector3 mousePos;

    private int turnRight = 2;

    private bool isWaited;
    void Start()
    {
        transform.position = new Vector3(0, -3, 0);
        isWaited = false;

        laserShot = GetComponent<AudioSource>();
        playerFire1.SetActive(false);
        playerFire2.SetActive(false);
        SaveSystem.Initialize("results");
    }

    void Update()
    {
        spaceMovement();

        shooting();
    }

    private void shooting()
    {
        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Time.time > nextFire && (mousePos.x > -5.5f || mousePos.y > 1.5f))
            {
                Instantiate(laserPrefab, transform.position + new Vector3(0, 1.34f, 0), Quaternion.identity);
                laserShot.Play();
                nextFire = Time.time + fireRate;
            }

        }
    }

    public void lifeSubstraction()
    {
        playerLives--;
        playerFire1.SetActive(true);
        fireAnim.Play("fire1");
        if (playerLives < 2)
        {
            playerFire2.SetActive(true);
            fireAnim2.Play("fire1");
        }
        playerAnimator.Play("player hurt");
        StartCoroutine(stopAnimationPlayerHurt());
        HealthBar.AdjustCurrentValue(-1);
    }
    private void spaceMovement() 
    {
        float horizon_input = Input.GetAxis("Horizontal");
        float vert_input = Input.GetAxis("Vertical");
        if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("player hurt"))
        {
            if (horizon_input < 0 || variableJoystick.Horizontal < 0)
            {
                playerAnimator.Play("player turn left");
                turnRight = 0;
            }
            else if (horizon_input > 0 || variableJoystick.Horizontal > 0)
            {
                playerAnimator.Play("player turn right");
                turnRight = 1;
            }
            else if (horizon_input == 0 || variableJoystick.Horizontal == 0)
            {
                if (turnRight == 1)
                    playerAnimator.Play("player turn right back");
                else if (turnRight == 0)
                    playerAnimator.Play("player turn left back");
            }
        }
        transform.Translate(Vector3.right * Time.deltaTime * playerSpeed * horizon_input);
        transform.Translate(Vector3.up * Time.deltaTime * playerSpeed * vert_input);

        Vector3 direction = Vector3.up * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        transform.Translate(direction * joysticSpeed);

        if (transform.position.y > 4)
            transform.position = new Vector3(transform.position.x, 4, 0);
        else if (transform.position.y < -4)
            transform.position = new Vector3(transform.position.x, -4, 0);

        if (transform.position.x > 8.2)
            transform.position = new Vector3(8.2f, transform.position.y, 0);
        else if (transform.position.x < -8.2)
            transform.position = new Vector3(-8.2f, transform.position.y, 0);
    }

    IEnumerator stopAnimationPlayerHurt()
    {
        while (true)
        {
            if (!isWaited)
            {
                isWaited = true;
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                isWaited = false;
                playerAnimator.Play("default");
                yield break;
            }
        }
    }
}
