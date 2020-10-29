using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _trippleShotPrefab;
    [SerializeField]
    private GameObject _Shield;
    [SerializeField]
    private GameObject _leftEngine, _rightEngine;
    [SerializeField]
    private GameObject explosionprefab;
    
    
    private SpawnManager _spawnManager;
    private UI_Manager UI;

    [SerializeField]
    private bool trippleShotActive;
    [SerializeField]
    private bool ShieldActive;

    [SerializeField]
    private float _speed = 10;
    [SerializeField]
    private float MaxSpeed = 30;
    [SerializeField]
    private float _SpeedMultiplier=2;
    [SerializeField]
    private int _Lives = 3;

    [SerializeField]
    private int _score;

    [SerializeField]
    private AudioSource _playerAudio;
    [SerializeField]
    private AudioClip _laserShot;
    [SerializeField]
    private AudioClip _explosion;


    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        UI = GameObject.Find("Canvas UI_Manager").GetComponent<UI_Manager>();

        

        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is not in scene");
        }



        if(UI ==null)
        {
            Debug.LogError("UI_manager is not in scene");
        }

    }
    void Update()
    {
        

        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }


        CalculateMovement();
   
    }


    //method for getting hit by laser
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EnemyLaser"))
        {
            Damage();
        }
           
    }


    public void AddScore(int Points)
    {
        //add 10 to score if enemy was killed
        _score += Points;
        UI.UpdateScore(_score);

    }






    public void Damage()
    {
        if(ShieldActive)
        {
            ShieldActive = false;
            _Shield.SetActive(false);
            return;
        }
        else
        {
            _Lives -= 1;

            if(_Lives ==2)
            {
                _rightEngine.SetActive(true);
            }
            else if(_Lives ==1)
            {
                _leftEngine.SetActive(true);
            }


            UI.UpdateLives(_Lives);
            if (_Lives < 1)
            {
                _Lives = 0;
                _spawnManager.OnPlayerDeath();
                _playerAudio.PlayOneShot(_explosion);
                Instantiate(explosionprefab, transform.position,Quaternion.identity);
                Destroy(this.gameObject);
                print("Your lost...");
            }
            
            
        }
            

        
    }

    


    public  void ActivateShieldPowerUp()
   {
        ShieldActive = true;
        _Shield.SetActive(true);
   }




   public void ActivateTrippleShot()
   {
        trippleShotActive = true;
        StartCoroutine(TrippleShotPowerdownRoutine());
   }
    IEnumerator TrippleShotPowerdownRoutine()
    {
        yield return new WaitForSeconds(5f);
        trippleShotActive = false;
    }





   public  void ActivateSpeedPowerUp()
   {
       
        _speed *= _SpeedMultiplier;
        StartCoroutine(SpeedPowerDownRoutine());
   }
    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _speed /= _SpeedMultiplier;
    }





    void FireLaser()
    {
        
        
      _canFire = Time.time + _fireRate;



        if (trippleShotActive)
        {
            Instantiate(_trippleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        _playerAudio.PlayOneShot(_laserShot);

    }






    void CalculateMovement()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(HorizontalInput, VerticalInput, 0) * _speed * Time.deltaTime);


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0));

        //Or

        //if (transform.position.y >= 0)
        //{
        //    transform.position = new Vector3(transform.position.x, 0, 0);
        //}
        //else if (transform.position.y <= -3.8f)
        //{
        //    transform.position = new Vector3(transform.position.x, -3.8f, 0);
        //}


        if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x <= -11)
        {
            transform.position = new Vector3(11, transform.position.y);
        }
    }
}