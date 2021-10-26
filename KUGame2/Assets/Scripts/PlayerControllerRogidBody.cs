using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerRogidBody : MonoBehaviour
{
    public float speed = 2f;
    public float rotSpeed = 20f;
    Rigidbody rb;
    float newRotY = 0;
    public GameObject prefabBullet;
    public Transform GunPosition;
    public float gunPower = 15f;
    public float gunCooldown = 2f;
    public float gunCooldownCount = 0;
    public bool hasGun = false;
    public int bulletCount = 0;

    public int coinCount = 0;
    //public Text uiTextCoin;
    public PlayGroundSceneManager manager;
    public AudioSource audioCoin;
    public AudioSource audioFire;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        manager = FindObjectOfType<PlayGroundSceneManager>();
        if(manager == null)
        {
            print("Manager not found!");
        }

        if (PlayerPrefs.HasKey("CoinCount"))
        {
            coinCount = PlayerPrefs.GetInt("CoinCount");
        }
        manager.SetTextCoin(coinCount);
        
    }

    /*void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(0, 0, speed, ForceMode.VelocityChange);
            newRotY = 0;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(0, 0, -speed, ForceMode.VelocityChange);
            newRotY = 180;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(speed, 0, 0, ForceMode.VelocityChange);
            newRotY = 90;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-speed, 0, 0, ForceMode.VelocityChange);
            newRotY = -90;
        }

        transform.rotation = Quaternion.Lerp(
            Quaternion.Euler(0, newRotY, 0),
            transform.rotation,
            rotSpeed * Time.deltaTime
            );

    }*/


    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed;
        float vertical = Input.GetAxis("Vertical") * speed;
        if(horizontal > 0)
        {
            newRotY = 90;
        }
        else if(horizontal < 0)
        {
            newRotY = -90;
        }
        if(vertical > 0)
        {
            newRotY = 0;
        }
        else if(vertical < 0)
        {
            newRotY = 180;
        }

        rb.AddForce(horizontal, 0, vertical, ForceMode.VelocityChange);
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, newRotY, 0),
            transform.rotation, rotSpeed * Time.deltaTime);

    }



   
    private void Update()
    {
        gunCooldownCount += Time.deltaTime;
        
        if (Input.GetButtonDown("Fire1") && (bulletCount > 0) && (gunCooldownCount >= gunCooldown) )
        {
            gunCooldownCount = 0;
            GameObject bullet = Instantiate(prefabBullet, GunPosition.position, GunPosition.rotation);
            Rigidbody bRb = bullet.GetComponent<Rigidbody>();
            bRb.AddForce(transform.forward * gunPower, ForceMode.Impulse);

            Destroy(bullet, 2f);
            bulletCount--;
            manager.SetTextBullet(bulletCount);
            audioFire.Play();
        }


    }

    




    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.tag == "Collectable")
        {
            Destroy(collision.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectable")
        {
            Destroy(other.gameObject);
            coinCount++;
            manager.SetTextCoin(coinCount);
            audioCoin.Play();
            PlayerPrefs.SetInt("CoinCount", coinCount);
            //uiTextCoin.text = coinCount.ToString();
        }
        if (other.gameObject.name == "Gun")
        {
            Destroy(other.gameObject);
            hasGun = true;
            bulletCount += 10;
            manager.SetTextBullet(bulletCount);
        }
    }
}
