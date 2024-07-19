using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector3 target;  
    public float speed;



    private void Start()
    {
        transform.forward = target - transform.position;

        Invoke("DestroyArrow", 1f);
    }


    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            DestroyArrow();
        }
    }

    private void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
