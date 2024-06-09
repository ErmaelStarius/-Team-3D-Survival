using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour
{
    public float jumpForce = 10f;
    public string playerTag = "Player";//플레이어를 태그해야 한다



    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag(playerTag))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();//플레이어의 rigidbody를 가져와야한다.
            if (rb != null)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}



