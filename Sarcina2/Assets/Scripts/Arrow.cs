using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector3 target;  
    public float speed;
    public int damage;


    private void Start()
    {
        sampleTime = 0f;
        control = Vector3.Lerp(transform.position, target, 0.5f) + new Vector3(0, 4, 0);
        // straight line
        transform.forward = target - transform.position;

        Invoke("DestroyArrow", 1f);
    }


    private void Update()
    {
        // straight line
        //transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);

        sampleTime += Time.deltaTime * speed;
        transform.position = evaluate(sampleTime);
        transform.forward = evaluate(sampleTime + 0.1f) - transform.position;

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



    // curve stuff

    public float arrowTrajectorySpeed = 5f;
    private float sampleTime;

    Vector3 control; 
    private Vector3 evaluate(float t)
    {
        
        Vector3 ac = Vector3.Lerp(transform.position, control, t);
        Vector3 cb = Vector3.Lerp(control, target, t);
        return Vector3.Lerp(ac, cb, t);
    }

}
