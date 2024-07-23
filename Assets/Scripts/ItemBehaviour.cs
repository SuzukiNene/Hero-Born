using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public GameBehaviour game;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("Game").GetComponent<GameBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.transform.parent.gameObject);
            //Debug.Log("Item collected!");
            game.Items += 1;
        }
    }
}
