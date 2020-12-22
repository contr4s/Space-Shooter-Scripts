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

    public GameObject playerFire1 = null;
    public Animator fireAnim = null;
    public GameObject playerFire2 = null;
    public Animator fireAnim2 = null;

    public Animator playerAnimator = null;

    public int playerLives = 5;

    public static int playerSpeed = 10;

    private AudioSource laserShot;

    [SerializeField]
    public VariableJoystick variableJoystick;

    private Vector3 mousePos;

    private bool isWaited;

    private Rigidbody2D _rigid;
    void Start()
    {
        transform.position = new Vector3(0, -3, 0);
        isWaited = false;

        laserShot = GetComponent<AudioSource>();
        playerFire1.SetActive(false);
        playerFire2.SetActive(false);

        _rigid = GetComponent<Rigidbody2D>();
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

    public void lifeSubstraction(int damage)
    {
        playerLives -= damage;
        GameManager.gm.playerHP -= damage;
        playerFire1.SetActive(true);
        fireAnim.Play("fire1");
        if (playerLives < 2)
        {
            playerFire2.SetActive(true);
            fireAnim2.Play("fire1");
        }
        playerAnimator.SetTrigger("Start Player hurt");
        StartCoroutine(stopAnimationPlayerHurt());
        HealthBar.AdjustCurrentValue(-1);
    }

    private void spaceMovement() 
    {
        float horizon_input = Input.GetAxis("Horizontal");
        float vert_input = Input.GetAxis("Vertical");

        Vector3 vel = new Vector3(horizon_input, vert_input);
        if (vel.magnitude > 1)
        {
            // Avoid speed multiplying by 1.414 when moving at a diagonal
            vel.Normalize();
        }

        _rigid.velocity = vel * playerSpeed;

        Vector3 direction = Vector3.up * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        if (Mathf.Abs(direction.magnitude) > 0.001)
            _rigid.velocity = direction * playerSpeed;

        if (transform.position.y > 4)
            transform.position = new Vector3(transform.position.x, 4, 0);
        else if (transform.position.y < -4)
            transform.position = new Vector3(transform.position.x, -4, 0);

        if (transform.position.x > 8.2)
            transform.position = new Vector3(8.2f, transform.position.y, 0);
        else if (transform.position.x < -8.2)
            transform.position = new Vector3(-8.2f, transform.position.y, 0);
    }

    public IEnumerator stopAnimationPlayerHurt()
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
                playerAnimator.SetTrigger("Stop Player hurt");
                yield break;
            }
        }
    }
}
