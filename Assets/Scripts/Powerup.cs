using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;

    
    
    [SerializeField]
    private AudioClip _PowerupSound;
   
    [SerializeField]
    private int powerUpID = 0; //0 =TripleshotPowerUp, 1 = SpeedPowerUp, 2 = ShieldPowerUp

    private void OnEnable()
    {
        
    }
    private void Update()
    {
        //move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //if we leave the screen destroy object
        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }


        //_PowerUpAudio = GameObject.Find("Audio_Manager").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        //Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //or
        Player player = other.GetComponent<Player>();

        if (other.CompareTag("Player") && player != null)
        {

            AudioSource.PlayClipAtPoint(_PowerupSound, transform.position);

            switch (powerUpID)
            {
                case 0:
                    print("TrippleShotPowerUp");
                    player.ActivateTrippleShot();
                    
                    Destroy(gameObject);
                    break;
                case 1:
                    print("speedPowerUp");
                    player.ActivateSpeedPowerUp();
                    
                    Destroy(gameObject);
                    break;
                case 2:
                    player.ActivateShieldPowerUp();
                    
                    Destroy(gameObject);
                    break;
                default:
                    print("no PowerUp");
                    break;

            }
        }
    }    
}

