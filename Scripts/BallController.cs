using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour 
{
    //　ボールのTransform情報
    private Transform tf_Ball;

    //　ボールが見える可能性のあるz軸の最大値
    private float visiblePosZ = -13f;

    // ゲームオーバーを表示するテキスト
    private GameObject gameOverText;

    private GameObject retryText;


    //ゲームオーバー判定
    private bool isGameOver;

    //  スコアテキストを設定
    private GameObject scoreText;

    //  ゲート1テキストを設定
    private GameObject Gate_1_Text;
    //  ゲート2テキストを設定
    private GameObject Gate_2_Text;
    //  ゲート3テキストを設定
    private GameObject Gate_3_Text;

    
    //  スコア(シーン間でやりとりするためstatic)
    public static int score = 0;
    // スコアの保持
    public static int SaveScore()
    {
        return score;
    }

    

    //  プレートより上の状態でのフラグ
    private bool isUp1_Flag;
    private bool isUp2_Flag;
    private bool isUp3_Flag;


    //  プレートより下の状態でのフラグ
    private bool isBottom1_Flag;
    private bool isBottom2_Flag;
    private bool isBottom3_Flag;

    //  ゲートを通過したかの判定フラグ
    private bool isGate_1;
    private bool isGate_2;
    private bool isGate_3;

    //  ゲート上の板を通り過ぎたかのフラグ
    private bool isUp_Trriger;

    

    //  public変数
    public GameObject Gate_1_Plate;
    public GameObject Gate_2_Plate;
    public GameObject Gate_3_Plate;



    public GameObject Gate_1;
    public GameObject Gate_2;
    public GameObject Gate_3;

    //  ナンバーテキストを設定
    public GameObject Number_1;
    public GameObject Number_2;
    public GameObject Number_3;


    public GameObject TreasureBox;

    public GameObject TresureMessageText;

    public GameObject Gate_4_Bottom;

    public GameObject BgmObject;

    // ゲームオーバー音源を挿入
    public AudioClip GamOver_Jingle;

    public AudioClip ImpactTree;
    public AudioClip ImpactCat;
    public AudioClip ImpactFripper;
    public AudioClip ImpactDaikon;
    public AudioClip GetCoin;
    public AudioClip MushShot;

    //public AudioClip BreakGate;

    AudioSource audioSource;





   







    // Start is called before the first frame update
    void Start()
    {
        // フレームレートを設定
        //ゲームをできるだけ速く実行
        Application.targetFrameRate = 60;


        audioSource = GetComponent<AudioSource>();

       

      

       //
       tf_Ball = GetComponent<Transform>();

        // GameOverTextオブジェクトを取得
        this.gameOverText = GameObject.Find("GameOverText");

        this.retryText = GameObject.Find("RetryText");

        //  ScoreTextオブジェクトを取得
        this.scoreText = GameObject.Find("ScoreText");

        //  Gate_1_Textオブジェクトを取得
        this.Gate_1_Text = GameObject.Find("Gate_1_Text");
        //  Gate_2_Textオブジェクトを取得
        this.Gate_2_Text = GameObject.Find("Gate_2_Text");
        //  Gate_3_Textオブジェクトを取得
        this.Gate_3_Text = GameObject.Find("Gate_3_Text");

        // Number_Xオブジェクトを取得
        this.Number_1 = GameObject.Find("Number_1");
        this.Number_2 = GameObject.Find("Number_2");
        this.Number_3 = GameObject.Find("Number_3");

        this.TreasureBox = GameObject.Find("TreasureBox");
        this.Gate_4_Bottom = GameObject.Find("Gate_4_Bottom");

        this.TresureMessageText = GameObject.Find("TresureMessageText");

        //  一度,非アクティブにする
        this.TreasureBox.SetActive(false);
      //  this.Gate_4_Bottom.SetActive(false);




         Gate_1_Plate = GameObject.Find("Gate_1_Plate");
        Gate_2_Plate = GameObject.Find("Gate_2_Plate");
        Gate_3_Plate = GameObject.Find("Gate_3_Plate");

       


    }

    // Update is called once per frame
    void Update()
    {
        // ボールが画面外に消えたとき、ゲームオーバーにする
        if (this.transform.position.z < this.visiblePosZ)
        {
            GameOver();

            //GameOverTextを表示
            this.gameOverText.GetComponent<Text>().text = "GameOver";


            //  Spaceキーを押したらたらシーンをロードする
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                //GameSceneを読み込む
                SceneManager.LoadScene("GameScene");

            }
        }

        if(isUp1_Flag && isBottom1_Flag)
        {
            //  ゲート1を通過したことを知らせる

            //audioSource.PlayOneShot(BreakGate);

            this.Gate_1_Text.GetComponent<Text>().text = "ゲート1通過！";

            this.Number_1.SetActive(false);

            Destroy(Gate_1);

            this.isGate_1 = true;

            
        }
       if(isUp2_Flag && isBottom2_Flag)
        {
            //  ゲート2を通過したことを知らせる

            //audioSource.PlayOneShot(BreakGate);

            this.Gate_2_Text.GetComponent<Text>().text = "ゲート2通過！";

            this.Number_2.SetActive(false);

            Destroy(Gate_2);

            this.isGate_2 = true;
        }
        if (isUp3_Flag && isBottom3_Flag)
        {
            //  ゲート3を通過したことを知らせる
            //audioSource.PlayOneShot(BreakGate);
            this.Gate_3_Text.GetComponent<Text>().text = "ゲート3通過！";


            this.Number_3.SetActive(false);

            Destroy(Gate_3);

            this.isGate_3 = true;
        }

        //　宝箱出現！
        ReleaseTreasureBox();

        // タイトルに戻る
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // スコアを保持させないため、0で初期化
            score = 0;

            SceneManager.LoadScene("TitleScene");
        }


    }


    

    

    //  ゲームオーバー処理
    public void GameOver()
    {
        this.isGameOver = true;
        this.gameOverText.GetComponent<Text>().text = "GameOver";
        this.retryText.GetComponent<Text>().text = "Spaceキーでリトライ！";

        // スコアを保持させないため、0で初期化
        score = 0;

        
        //  後で入れる
        //audioSource.PlayOneShot(GamOver_Jingle);
        

    }

    //  すり抜けた状態で衝突を検知(Istrrigerが付いた状態)
    public void OnTriggerEnter(Collider other)
    {

        //  コインに衝突したとき
        if (other.gameObject.tag == "CoinTag")
        {
            audioSource.PlayOneShot(GetCoin);

            //  スコア加算
            score += 50;

            //  獲得したスコアをScoreTextに表示
            this.scoreText.GetComponent<Text>().text = "Score:" + score;

            //  パーティクルの再生（生成）
            GetComponent<ParticleSystem>().Play();

            //  コイン破壊
            Destroy(other.gameObject);
            //Debug.Log("destroy!");
        }


       /* else if(other.gameObject.tag == "Up_Trriger_Tag")
        {
            this.isUp_Trriger = true;
            //  Debug.Log(other.gameObject);

            this.Gate_4_Bottom.SetActive(true);

            if (other.gameObject.tag == "Bottom_Trriger_Tag")
            {

            }

        }*/


        //  ボールの座標がプレート1より上ならフラグをTrueにする
        if (other.gameObject.tag == "Plate_1_Tag"&& this.tf_Ball.transform.position.z >= 4.24)
        {
            isUp1_Flag = true;
            Debug.Log("a1"); 
        }
        //  ボールの座標がプレート2より上ならフラグをTrueにする
        else if (other.gameObject.tag == "Plate_2_Tag" && this.tf_Ball.transform.position.z >= 2.14 &&
            this.tf_Ball.transform.position.z < 4.24)
        {
            isUp2_Flag = true;
            Debug.Log("a2");
        }
        else if (other.gameObject.tag == "Plate_3_Tag" && this.tf_Ball.transform.position.z >= -0.67 &&
            this.tf_Ball.transform.position.z < 2.14)
        {
            isUp3_Flag = true;
            Debug.Log("a3");
        }


        





        //  ゲートをくぐったとき
        /*else if (other.gameObject.tag == "Gate_1")
        {
            Debug.Log("Gate_1!");
            

            Destroy(other.gameObject);

        }*/

    }

    //  すり抜けた状態で衝突し、離れたことを検知(Istrrigerが付いた状態)
    public void OnTriggerExit(Collider other)
    {
        if (this.tf_Ball.transform.position.z < 4.24)
        {
            isBottom1_Flag = true;
            Debug.Log("b1");

            isBottom2_Flag = true;
            Debug.Log("b2");

            isBottom3_Flag = true;
            Debug.Log("b3");
        }

    }





    //  衝突を検知（Istrrigerはナシ）
    public void OnCollisionEnter(Collision other)
    {
        //  フリッパーに当たったとき
        if(other.gameObject.tag == "RightFripperTag" || other.gameObject.tag == "LeftFripperTag")
        {
            audioSource.PlayOneShot(ImpactFripper);
        }
        // 木に衝突したとき
        else if (other.gameObject.tag == "TreeTag")
        {
            audioSource.PlayOneShot(ImpactTree);

            //  スコア加算
            score += 10;

            //  獲得したスコアをScoreTextに表示
            this.scoreText.GetComponent<Text>().text = "Score:" + score;

        }
        //  星に衝突したとき
        else if (other.gameObject.tag == "LargeStarTag")
        {
            //  スコア加算
            score += 30;

            //  獲得したスコアをScoreTextに表示
            this.scoreText.GetComponent<Text>().text = "Score:" + score;
        }
        //  大根に衝突したとき
        else if (other.gameObject.tag == "DaikonTag")
        {
            audioSource.PlayOneShot(ImpactDaikon);

            //  スコア加算
            score += Random.Range(40, 60);

            //  獲得したスコアをScoreTextに表示
            this.scoreText.GetComponent<Text>().text = "Score:" + score;
        }
        //  ヌコに衝突したとき
        else if (other.gameObject.tag == "CatTag")
        {
            audioSource.PlayOneShot(ImpactCat);

            //  スコアをランダムで加算
            score += Random.Range(10, 20);

            //  獲得したスコアをScoreTextに表示
            this.scoreText.GetComponent<Text>().text = "Score:" + score;
        }
        //  キノコに衝突したとき
        else if (other.gameObject.tag == "MushRoomTag")
        {
            audioSource.PlayOneShot(MushShot);

            //  スコア加算
            score += Random.Range(20, 35);

            //  獲得したスコアをScoreTextに表示
            this.scoreText.GetComponent<Text>().text = "Score:" + score;
        }
        //  宝箱に衝突したとき
        else if(other.gameObject.tag == "TreasureBoxTag")
        {
            score *= 10;

            this.scoreText.GetComponent<Text>().text = "Score:" + score;

            this.TresureMessageText.GetComponent<Text>().text = "Clear！！";

            

            Destroy(this.TreasureBox);

            //ResultSceneを読み込む
            SceneManager.LoadScene("ResultScene");

        }
        

    }

    

    //  宝箱解放関数
    public void ReleaseTreasureBox()
    {
        if(isGate_1 && isGate_2 && isGate_3)
        {
            this.TreasureBox.SetActive(true);
            this.TresureMessageText.GetComponent<Text>().text = "お宝があらわれた！";
           

            
        }

    }

}
