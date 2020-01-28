using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public GameObject Ball;


    // Start is called before the first frame update
    void Start()
    {
        Ball = GameObject.Find("Ball");
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
