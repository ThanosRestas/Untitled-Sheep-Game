using System.Collections;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float movementDuration = 2.0f;
    public float waitBeforeMoving = 5.0f;
    private bool hasArrived = false;
    private Animator anim;
    public GameObject sheep;
    private bool isMoving = false;
    public float turnSpeed = 0.1f;
    [SerializeField]
    private float distanceToDestination;
    [SerializeField]
    private float distanceLeft;
    private bool poopedAlready = false;

    private void Start ()
    {
        anim = gameObject.GetComponent<Animator> ();

    }

    private void Update ()
    {

        if (distanceLeft < 0.15)
        {
            //Debug.Log ("I'm near my destination");
            isMoving = false;
        }

        if (isMoving)
        {

            //Debug.Log ("Sheep is walking");
            anim.SetBool ("Idle", false);
            anim.SetBool ("Walk", true);
            poopedAlready = false;
        }
        else
        {
            //Debug.Log ("Sheep is idle");

            anim.SetBool ("Idle", true);
            anim.SetBool ("Walk", false);
            if (poopedAlready == false)
            {
                poopedAlready = true;
                //sheep.GetComponent<MouseOver> ().poop ();
            }

        }

        if (hasArrived == false)
        {
            hasArrived = true;

            // Pick a random point in scene and move to
            float randX = Random.Range (-5.0f, 5.0f);
            float randZ = Random.Range (-5.0f, 5.0f);
            StartCoroutine (MoveToPoint (new Vector3 (randX, 0f, randZ)));
        }

    }

    
    private IEnumerator MoveToPoint (Vector3 targetPos)
    {
        float timer = 0.0f;
        Vector3 startPos = transform.position;

        while (timer < movementDuration)
        {
            isMoving = true;
            timer += Time.deltaTime;
            float t = timer / movementDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            Quaternion petRotation = Quaternion.LookRotation (targetPos - startPos);
            transform.rotation = Quaternion.Slerp (transform.rotation, petRotation, turnSpeed);
            transform.position = Vector3.Lerp (startPos, targetPos, t);
            distanceToDestination = Vector3.Distance (startPos, targetPos);
            distanceLeft = Vector3.Distance (transform.position, targetPos);

            anim.SetFloat ("MovementSpeedMultiplier", distanceToDestination / 5);

            yield return null;
            isMoving = false;
        }
        yield return new WaitForSeconds (waitBeforeMoving);
        hasArrived = false;

    }
}