using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const float speed = 0.08f; // 移動速度 

    public Text TimeText; // 残り時間のテキスト
    public GameObject resultPanal; // リザルト
    string playTime;
    public float time = 30; // 残り時間

    public Text scoreText; // スコアのテキスト
    int score; // スコア
    public Text finalText; // 合計スコア

    public AudioClip soundEffect; // にんじん用効果音
    public AudioClip soundEffect2; // ばい菌用効果音
    private AudioSource audioSource;

    void Start()
    {
        scoreText.text = "得点：" + score;

        // AudioSourceコンポーネントを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        playTime = System.DateTime.Now.ToString();
        time -= Time.deltaTime;
        if (time < 0)
        {
            time = 0;
        }
        TimeText.text = "残り時間：" + ((int)time).ToString();
        // 残り時間が0になったら
        if (time == 0)
        {
            // リザルトを表示する
            resultPanal.SetActive(true);
            finalText.text = scoreText.text;
            Debug.Log("時間になった");
        }

        // 残り時間が0じゃなかったら
        if (time != 0)
        {
            if (transform.position.x >= -6.8f && transform.position.x <= 6.8f)
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    this.transform.localPosition = new Vector3
                        (this.transform.localPosition.x - speed,
                        this.transform.localPosition.y);
                }
                else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    this.transform.localPosition = new Vector3
                        (this.transform.localPosition.x + speed,
                        this.transform.localPosition.y);
                }
            }
            else if (transform.position.x < -6.8f)
            {
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    this.transform.localPosition = new Vector3
                        (this.transform.localPosition.x + speed,
                        this.transform.localPosition.y);
                }
            }
            else if (transform.position.x > 6.8f)
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    this.transform.localPosition = new Vector3
                        (this.transform.localPosition.x - speed,
                        this.transform.localPosition.y);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // にんじんに当たったら
        if (collision.gameObject.CompareTag("ninjin"))
        {
            // オブジェクト削除
            GameObject.Destroy(collision.gameObject);
            // SEを再生
            audioSource.PlayOneShot(soundEffect);
            // スコアを増やす
            score += 10;
            scoreText.text = "得点:" + score;
            Debug.Log("qqq");
        }
        // ばい菌に当たったら
        if (collision.gameObject.CompareTag("baikin"))
        {
            // オブジェクト削除
            GameObject.Destroy(collision.gameObject);
            // SEを再生
            audioSource.PlayOneShot(soundEffect2);
            // スコアを減らす
            score -= 10;
            scoreText.text = "得点:" + score;
            Debug.Log("aaa");
        }
    }

    void InitScore()
    {
        //スコア初期化
        score = 0;
    }

    public void OnTitleButton()
    {
        // タイトルに戻る
        SceneManager.LoadScene("Title");
        InitScore();
    }

    public void OnRetryButton()
    {
        // もう一回遊ぶ
        SceneManager.LoadScene("Main");
        InitScore();
    }
}
