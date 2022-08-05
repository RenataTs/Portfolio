using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    public GameObject ExplosioEffect;
    public static bool timerIsActive = false;
    [SerializeField] float moveSpeed = 1;

    private Vector2 initialPosition;
    private SpriteRenderer spriteRenderer;

    private GameObject shield;

    private Gun[] guns;
    private VerticalGun[] verticalGuns;
    private SpiralGun[] spiralGuns;

    private float speedMultiplier = 1f;
    private float invincibleTimer = 0f;
    private float invincibleTime = 2f;

    private int hits = 2;
    private int helth = 3;
    private int powerUpGunLevel = 0;
    private int powerUpVerticalGunLevel = 0;
    private int powerUpSpiralGunLevel = 0;

    private bool invincible = false;

    private bool moveUp;
    private bool moveDown;
    private bool moveLeft;
    private bool moveRight;
    private bool shoot;
    private bool verticalShoot;
    private bool spiralShoot;
    [SerializeField] GameObject audio;
    [SerializeField] AudioSource shootSound;
    [SerializeField] AudioSource bonusSound;
    [SerializeField] AudioSource hitSound;

    public Animator anim;

    private void Awake()
    {
        anim.SetBool("Start", true);
        initialPosition = transform.position;
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(audio.gameObject);
        gameObject.SetActive(true);
    }

    void Start()
    {
        PlayerPrefs.SetInt("Helth", helth);
        shield = transform.Find("Shield").gameObject;
        DeactiveShield();
        guns = transform.GetComponentsInChildren<Gun>();
        verticalGuns = transform.GetComponentsInChildren<VerticalGun>();
        spiralGuns = transform.GetComponentsInChildren<SpiralGun>();

        foreach (Gun gun in guns)
        {   
            gun.isActive = true;

            if (gun.powerUpLevelRequirement != 0)
            {
                gun.gameObject.SetActive(false);
            }
        }

        foreach (VerticalGun verticalGun in verticalGuns)
        {
            verticalGun.isActive = true;

            if (verticalGun.powerUpLevelRequirement != 0)
            {
                verticalGun.gameObject.SetActive(false);
            }
        }

        foreach (SpiralGun spiralGun in spiralGuns)
        {
            spiralGun.isActive = true;

            if (spiralGun.powerUpLevelRequirement != 0)
            {
                spiralGun.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {

        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        shoot = Input.GetKeyDown(KeyCode.LeftControl);
        verticalShoot = Input.GetKeyDown(KeyCode.Space);

        if (shoot)
        {
            foreach (Gun gun in guns)
            {
                if (gun.gameObject.activeSelf)
                {
                    gun.Shoot();
                    shootSound.Play();
                }
            }
        }
        else if (verticalShoot)
        {
            verticalShoot = false;

            foreach (VerticalGun verticalGun in verticalGuns)
            {
                if (verticalGun.gameObject.activeSelf)
                {
                    verticalGun.Shoot();
                    shootSound.Play();
                }
            }
        }

        if (spiralShoot)
        {
            spiralShoot = false;

            foreach (SpiralGun spiralGun in spiralGuns)
            {
                if (spiralGun.gameObject.activeSelf)
                {
                    spiralGun.SpiralShoot();
                }
            }
            
        }

        if (invincible)
        {
            if (invincibleTimer >= invincibleTime)
            {
                invincibleTimer = 0;
                invincible = false;
                spriteRenderer.enabled = true;
            }
            else 
            {
                invincibleTimer += Time.deltaTime;
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        float moveAmount = moveSpeed * speedMultiplier * Time.fixedDeltaTime;
        Vector2 move = Vector2.zero;
        transform.position = pos;

        if (moveUp)
        {
            move.y += moveAmount;
            anim.SetBool("IsUp", true);
        }
        else if (moveDown)
        {
            move.y -= moveAmount;
            anim.SetBool("IsDown", true);
        }
        else if (moveLeft)
        {
            move.x -= moveAmount;
        }
        else if (moveRight)
        {
            move.x += moveAmount;
        }
        else
        {
            anim.SetBool("IsUp", false);
            anim.SetBool("IsDown", false);
        }

        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);

        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }

        pos += move;

        if (pos.x <= -8.3f)
        {
            pos.x = -8.3f;
        }
        if (pos.x >= 8.3f)
        {
            pos.x = 8.3f;
        }
        if (pos.y <= -4.4f)
        {
            pos.y = -4.4f;
        }
        if (pos.y >= 4.4f)
        {
            pos.y = 4.4f;
        }
        transform.position = pos;
    }

    void ActivateShield()
    {
        shield.SetActive(true);
    }

    void DeactiveShield()
    {
        shield.SetActive(false);
    }

    bool HasShield()
    {
        return shield.activeSelf;
    }

    void AddGuns()
    {
        powerUpGunLevel++;
        foreach (Gun gun in guns)
        {
            if (gun.powerUpLevelRequirement <= powerUpGunLevel)
            {
                gun.gameObject.SetActive(true);
            }
            else
            {
                gun.gameObject.SetActive(false);
            }
        }
    }

    void AddVerticalGuns()
    {
        powerUpVerticalGunLevel++;
        foreach (VerticalGun verticalGun in verticalGuns)
        {
            if (verticalGun.powerUpLevelRequirement <= powerUpGunLevel)
            {
                verticalGun.gameObject.SetActive(true);
            }
            else
            {
                verticalGun.gameObject.SetActive(false);
            }
        }
    }

    void AddSpiralGuns()
    {
        powerUpSpiralGunLevel++;
        foreach (SpiralGun spiralGun in spiralGuns)
        {
            if (spiralGun.powerUpLevelRequirement <= powerUpSpiralGunLevel)
            {
                spiralGun.gameObject.SetActive(true);
            }
            else
            {
                spiralGun.gameObject.SetActive(false);
            }
        }
    }

    public void AddHelth()
    {
        helth++;
        PlayerPrefs.SetInt("Helth", helth);
    }

    void SetSpeedMultiplier(float mult)
    {
        speedMultiplier = mult;
    }

    void ResetShip()
    {
        transform.position = initialPosition;
        DeactiveShield();
        powerUpGunLevel = -1;
        powerUpVerticalGunLevel = -1;
        AddGuns();
        AddVerticalGuns();
        SetSpeedMultiplier(1);
        hits = 2;

        --helth;
        PlayerPrefs.SetInt("Helth", helth);
    }

    public void Hit(GameObject gameObjectHit)
    {
        hitSound.Play();

        if (helth == 0)
        {
            GameObject ExplosionEffectIns = Instantiate(ExplosioEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
            Destroy(ExplosionEffectIns, 1);
        }

        if (HasShield())
        {
            DeactiveShield();
        }
        else
        {
            if (!invincible)
            {
                hits--;
                if (hits == 0)
                {
                    ResetShip();
                }
                else
                {
                    invincible = true;
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Hit(gameObject);
        }

        Bullet bullet = collision.GetComponent<Bullet>();

        if (bullet != null)
        {
            if (bullet.isEnemy)
            {
                Hit(bullet.gameObject);
            }
        }

        Destructable destructable = collision.GetComponent<Destructable>();
        if (destructable != null)
        {
            Hit(destructable.gameObject);
        }

        BombScript bomb = collision.GetComponent<BombScript>();
        if (bomb != null)
        {
            Destroy(bomb.gameObject);
            Hit(bomb.gameObject);
        }

        PowerUp powerUp = collision.GetComponent<PowerUp>();
        if (powerUp)
        {
            bonusSound.Play();

            if (powerUp.activateShield)
            {
                ActivateShield();
            }

            if (powerUp.addGuns)
            {
                AddGuns();
                AddVerticalGuns();
            }

            if (powerUp.spiralGuns)
            {
                Ship.timerIsActive = true;
                AddSpiralGuns();
            }

            if (powerUp.increaseSpeed)
            {
                SetSpeedMultiplier(speedMultiplier + 1);
            }

            if (powerUp.addHelth)
            {
                AddHelth();
            }

            Destroy(powerUp.gameObject);
        }
    }
}

