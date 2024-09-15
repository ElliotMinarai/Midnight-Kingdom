using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger entered by: " + collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player reached the end of the level");
            GameManager.instance.PlayerReachedEndOfLevel();
        }
    }
}