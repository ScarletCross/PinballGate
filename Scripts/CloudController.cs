using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    private Transform tf_Cloud;

    private Rigidbody rb_Cloud;

    //最小サイズ
    private float min = 1.0f;

    //拡大・縮小スピード
    private float spreadSpeed = 10.0f;

    //拡大率
    private float magnification = 0.07f;

    //  移動速度
    private float horizontakSpeed = 0.05f;

   

    // Start is called before the first frame update
    void Start()
    {
        tf_Cloud = GetComponent<Transform>();

        //rb_Cloud = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //拡大・縮小させる
        this.transform.localScale = 
            new Vector3(this.min + Mathf.Cos(Time.time * this.spreadSpeed) * this.magnification,
            this.transform.localScale.y, 
            this.min + Mathf.Cos(Time.time * this.spreadSpeed) * this.magnification);

        //tf_Cloud.position += new Vector3(horizontakSpeed, 0f, 0f);

        if(this.transform.position.x < 5) {
            tf_Cloud.position += new Vector3(horizontakSpeed, 0f, 0f);

            if (this.transform.position.x == 5)
            {
                tf_Cloud.position += new Vector3(-horizontakSpeed, 0f, 0f);
            }

        }
        

        //if(this.transform.position.x < )

    }
}
