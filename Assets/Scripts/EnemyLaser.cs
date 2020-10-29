using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8;
   
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 8 || transform.position.y <= -5)
        {
            if(this.transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

    public void ChangeSpeed(int speed)
    {
        _speed = speed;
    }
}
