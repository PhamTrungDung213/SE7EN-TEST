using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamOffset : MonoBehaviour
{
    public static CamOffset Instance;
    private GameObject player;
    public Transform target;

    [SerializeField] private Vector3 offset = new Vector3(0f, 10f, -8f);
    [SerializeField] private Vector3 rotate = new Vector3(50f, 0f, 0f);
    [SerializeField] private float smoothSpeed = 3f; 

    private bool follow = false;


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        StartCoroutine(CamWait());
        player = GameObject.Find("Jammo_LowPoly");
        target = player.transform;
    }

    void LateUpdate()
    {
        if (!follow) return;

        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.Euler(rotate);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
    }

    IEnumerator CamWait()
    {
        yield return new WaitForSeconds(1f);
        follow = true;
    }

    public void FollowBall(Transform ballTransform)
    {
        target = ballTransform;
        StartCoroutine(WaitAndReturn());
    }

    private IEnumerator WaitAndReturn()
    {
        yield return new WaitForSeconds(2f); 
        target = player.transform;                     
    }
}