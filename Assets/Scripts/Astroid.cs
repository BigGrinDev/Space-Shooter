
using UnityEngine;

public class Astroid : MonoBehaviour
{
    private SpawnManager spawnManager;


    [SerializeField]
    private float _rotationSpeed = 3;
    [SerializeField]
    private GameObject _ExplosionPrefab;
    [SerializeField]
    private bool isDestroyed = false;

    [SerializeField]
    private AudioSource _AsteroidAudio;
    [SerializeField]
    private AudioClip _Explosion;
    void Start()
    {
        spawnManager = GameObject.FindObjectOfType<SpawnManager>().GetComponent<SpawnManager>();

        if(spawnManager == null)
        {
            Debug.LogError("SpawnManager Not Found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    //check for Laser Collisions (trigger)
    //insstantiate explosion at asteriods pos (this)
    //destroy the explosion after 3 seconds.

    private void OnTriggerEnter2D(Collider2D other)
    {
 
        if (other.CompareTag("Laser") && !isDestroyed)
        {
            Destroy(other.gameObject);

            Instantiate(_ExplosionPrefab, transform.position, Quaternion.identity);// as GameObject; //GameObject go =

            spawnManager.StartSpawning();

            _AsteroidAudio.PlayOneShot(_Explosion);

            Destroy(gameObject,1f);

            isDestroyed = true;
        }
    }

    
}
