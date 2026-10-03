using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject confettiPrefab;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void KickToGoal(GameObject goal)
    {
        rb.isKinematic=false;

        Vector3 centerTarget = goal.GetComponent<Collider>().bounds.center;
        Vector3 velocity = centerTarget - transform.position; 
        velocity.y += 0.5f * Mathf.Abs(Physics.gravity.y);
        rb.velocity = velocity;

        CamOffset.Instance.FollowBall(transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            ParticleSystem ps = collision.gameObject.GetComponentInChildren<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
            }
        }
    }
}
