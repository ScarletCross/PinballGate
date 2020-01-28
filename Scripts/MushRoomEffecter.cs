using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoomEffecter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnCollisionEnter(Collision other)
    {
        //  ボールが触れてきたら
        if (other.gameObject.tag == "BallTag")
        {
            GetComponent<ParticleSystem>().Play();
        }
    }

}
