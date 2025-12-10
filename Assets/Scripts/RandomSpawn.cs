using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField] private GameObject item; // にんじん
    [SerializeField] private GameObject virus; // ばい菌
    [SerializeField] private Transform point; // アイテムを生成する場所

    private float time; // にんじん用
    private float time2; // ばい菌用

    private GameObject Player;

    private void Start()
    {
        Player = GameObject.Find("うさぎ");
    }

    void Update()
    {
        // 残り時間が0じゃなかったら
        if (Player.GetComponent<GameManager>().time != 0)
        {
            time += Time.deltaTime;
            if (time >= 0.7f)
            {
                Spawn(); // にんじんを生成
                time = 0;
            }
            time2 += Time.deltaTime;
            if (time2 >= 1.75f)
            {
                VirusSpawn(); // ばい菌を生成
                time2 = 0;
            }
        }
    }

    void Spawn()
    {
        var clone = Instantiate(item, point);
        float posX = Random.Range(-5f, 5f);
        clone.transform.localPosition = new Vector3(posX, clone.transform.localPosition.y);
    }

    void VirusSpawn()
    {
        var clone = Instantiate(virus, point);
        float posX = Random.Range(-5f, 5f);
        clone.transform.localPosition = new Vector3(posX, clone.transform.localPosition.y);
    }
}
