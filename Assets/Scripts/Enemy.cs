using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4;

    private Player player;
    private Animator anim;

    [SerializeField]
    private GameObject laserPrefab;

    [SerializeField]
    private AudioSource _EnemyAudio;
    [SerializeField]
    private AudioClip _explosion;
    [SerializeField]
    private AudioClip _laserShot;
    [SerializeField]
    private bool isDead;
    [SerializeField]
    private bool isShooting;
    void Start()
    {
        StartCoroutine(RandomFire());

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        anim = gameObject.GetComponent<Animator>();


        _EnemyAudio = GameObject.Find("Audio_Manager").GetComponentInChildren<AudioSource>();

        if (player == null)
        {
            Debug.LogError("Player is not in scene");
        }

        if (anim == null)
        {
            Debug.LogError("The anim component is not attached");
        }
    }


    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);



        if (transform.position.y <= -5.34f)
        {
            float Random_X = Random.Range(-8f, 8f);

            transform.position = new Vector3(Random_X, 6.9f, 0);
        }


    }


    IEnumerator RandomFire()
    {


        while (isShooting == true)
        {
            yield return new WaitForSeconds(Random.Range(0.2f,5)); //
            GameObject Laser = Instantiate(laserPrefab, transform.position + new Vector3(0, -0.8f, 0), Quaternion.identity) as GameObject;
            _EnemyAudio.pitch = 1.5f;
            _EnemyAudio.PlayOneShot(_laserShot);
            yield return new WaitForSeconds(0.7f);

        }

        //Fire laser every 3-7 seconds
        //instantiate Laser Prefab

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDead)
        {

            if (player != null)
            {
                player.Damage();

            }
            anim.SetTrigger("OnEnemyDeath");
            _speed = 1.0f;
            _EnemyAudio.PlayOneShot(_explosion);
            isDead = true;
            Destroy(this.gameObject, 1.8f);
        }

        if (other.CompareTag("EnemyLaser"))   //not finished
        {
            return;
        }
        else if (other.CompareTag("Laser") && !isDead)
        {
            Destroy(other.gameObject);
            StopAllCoroutines();

            if (player != null)
            {
                int RandomScore = Random.Range(5, 10);
                player.AddScore(RandomScore);
            }

            anim.SetTrigger("OnEnemyDeath");
            _speed = 2;
            _EnemyAudio.PlayOneShot(_explosion);
            isDead = true;
            Destroy(this.gameObject, 1.8f);

        }

    }
}
