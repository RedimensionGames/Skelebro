using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BGScroll : MonoBehaviour
{
    public float scrollSpeed;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(scrollSpeed, 0);
    }


    private void Update()
    {
       if(MainManager.instance.currentGameState != MainManager.GameState.Game)
        {
            rb2d.velocity = Vector2.zero;
        }
    }
    /*public float rightEdge;
    public float leftEdge;
    public Vector3 distanceBetweenEdges;

    /*private void Start()
    {
        CalculateEdges();
        distanceBetweenEdges = new Vector3(rightEdge - leftEdge, 0f, 0f);
    }

    void CalculateEdges()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rightEdge = transform.localPosition.x + spriteRenderer.bounds.extents.x / 5f;
        leftEdge = transform.localPosition.x - spriteRenderer.bounds.extents.x / 5f;
    }

    private void Update()
    {
        ScrollBG();
    }

    public void ScrollBG()
    {
        transform.localPosition += scrollSpeed * Vector3.right * Time.deltaTime;
        if(PassedEdge())
        {
            Debug.Log("edgepassed!");
            MoveSpriteToEdge();
        }
    }

    public bool PassedEdge()
    {
        
        return (scrollSpeed > 0 && transform.position.x > rightEdge) || (scrollSpeed < 0 && transform.position.x < leftEdge);
    }

    void MoveSpriteToEdge()
    {
        if (scrollSpeed > 0)
        {

            transform.localPosition -= distanceBetweenEdges;
        }
        else
        {
            transform.localPosition += distanceBetweenEdges;
        }
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
        {
            Gizmos.DrawCube(new Vector3(rightEdge, 0f, 0f), new Vector3(0.1f, 2f, 0.1f));
            Gizmos.DrawCube(new Vector3(leftEdge, 0f, 0f), new Vector3(0.1f, 2f, 0.1f));
        }
    }*/

}
