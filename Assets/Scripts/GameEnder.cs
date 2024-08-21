using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(EndGame), 5.0f);
            
        }
    }

    private void EndGame()
    {
        Application.Quit();
        Debug.Log("End");
    }

}
