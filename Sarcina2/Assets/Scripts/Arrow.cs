using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector3 target;  
    public float speed;
    public int damage;


    private void Start()
    {
        transform.forward = target - transform.position;

        Invoke("DestroyArrow", 1f);
    }


    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
    }


    public bool isArrowShotByEnemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Enemy")  && !isArrowShotByEnemy || other.tag == "Player" && isArrowShotByEnemy || other.tag == "BuildingModel" && isArrowShotByEnemy || other.tag == "Ally" && isArrowShotByEnemy)
        {
            DestroyArrow();
        }
    }

    private void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
