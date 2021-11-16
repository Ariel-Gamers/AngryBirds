using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;

    public Vector3 currentPosition;

    public float maxLength;

    public float bottomBoundary;
    public float upperBoundary=1000;
    public float mouse_z_POS = 10;
    bool isMouseDown;


    public GameObject birdPrefab;

    public float birdPositionOffset;

    Rigidbody2D bird;
    Collider2D birdCollider;

    public float force;

    void Start()
    {
        //alligns the strips to the right place
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);
        
        //Create bird (from script)
        CreateBird();
        
    }

    void CreateBird()
    {
        bird = Instantiate(birdPrefab).GetComponent<Rigidbody2D>();
        birdCollider = bird.GetComponent<Collider2D>();
        birdCollider.enabled = false;        
        bird.isKinematic = true;   
        ResetStrips();
    }

    void Update()
    {
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition; //get mouse position
            mousePosition.z = mouse_z_POS; //otherwise breaks the throw of the bird

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition); //offset to world space
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength); //set the correct position of the strips

            currentPosition = ClampBoundary(currentPosition);

            SetStrips(currentPosition); //set the strips provided with currentPosition

            if (birdCollider)
            {
                birdCollider.enabled = true; //enable the collider for the bird if there is a collider
            }
        }
        else
        {
            ResetStrips();
        }
    }
       

    private void OnMouseDown() //detects mouse press
    {
        isMouseDown = true;
    }

    private void OnMouseUp() // detect mouse release
    {
        isMouseDown = false;
        Shoot();
        currentPosition = idlePosition.position;
    }

    void Shoot()
    {
        bird.isKinematic = false; //enables the bird to use physical functions
        Vector3 birdForce = (currentPosition - center.position) * force * -1; //formula for the bird to "fly"
        bird.velocity = birdForce; //applying the force

        bird.GetComponent<Bird>().Release();

        bird = null;
        birdCollider = null;        
    }

    void ResetStrips() //reset the strips of the sligshot where they need to be 
    {
        currentPosition = idlePosition.position; //define the strips to idle (original) position
        SetStrips(currentPosition); //set the original position
    }

    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position); //set the first (1) line to the given position
        lineRenderers[1].SetPosition(1, position); //set the second (1) line to the given position

        if (bird) //puts the bird to the correct place when setting up the straps
        {
            Vector3 dir = position - center.position;
            bird.transform.position = position + dir.normalized * birdPositionOffset;
            bird.transform.right = -dir.normalized;
        }
    }

    Vector3 ClampBoundary(Vector3 vector) //makes sure that the player cant "strech" the bird in the slingshot too far
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, upperBoundary);
        return vector;
    }
}
