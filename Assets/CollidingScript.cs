using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidingScript : MonoBehaviour
{

    public int numOfBooksOnDesk = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BookOutOfZone"))
        {
            other.gameObject.tag = "BookInSideZone";
            numOfBooksOnDesk++;
        }
    }
}
