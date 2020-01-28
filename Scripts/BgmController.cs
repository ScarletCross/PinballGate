using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmController : MonoBehaviour
{
    public AudioSource audioSource;

    //  ボールコントローラーのボールを入れる
    GameObject ball;

    //  ボールコントローラーscriptを入れる
    BallController script;


    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
        ball = GameObject.Find("Ball");
        script = ball.GetComponent<BallController>();

    }

    // Update is called once per frame
    void Update()
    {
       // if(ball.)
    }
}
