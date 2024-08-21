using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject gate;
    private SpriteRenderer sprite;
    private bool buttonPressed = false;
    public Sprite buttonOnState;
    public BoxCollider2D buttonOnColl;
    public BoxCollider2D buttonOffColl;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == box.name && !buttonPressed)
        {
            sprite.sprite = buttonOnState;
            buttonOffColl.enabled = false;
            buttonOnColl.enabled = true;
            gate.transform.position = new Vector3(gate.transform.position.x, gate.transform.position.y + 3.0f, gate.transform.position.z);
            
            buttonPressed = true;
        }
        
    }
}
