using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;  

    private Vector3 offset; 

    void Start()
    {
        if (player != null)
        {
            
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            
            Vector3 newPosition = new Vector3(player.position.x + offset.x,
                                              player.position.y + offset.y,
                                              transform.position.z);
            transform.position = newPosition;
        }
    }
}
