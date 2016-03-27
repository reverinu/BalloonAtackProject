using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    GameObject title;
    [SerializeField]
    GameObject game;
    [SerializeField]
    GameObject result;

    [SerializeField]
    GameObject balloon;

    [SerializeField]
    Text point_text;
    [SerializeField]
    Text result_text;

    float latency_time;// バルーンの発動時間
    float balloon_x, balloon_z;// バルーンの発生地点
    static readonly float balloon_y = 1f;
    int balloon_count = 0;// バルーンの数
    float balloon_rand = 0f;// バルーンが発生する値
    static readonly float BALLOON_CHECK = 4.8f;// バルーン発生の閾値
    public int point = 0;//　得点
    
	void Update () {
        // シーンごとに分けている
        if (title.activeSelf)
        {
            GameStart();
        }
        else if (!title.activeSelf && balloon_count <= 100)
        {
            InGame();
            PointSet();
        }
        else if (balloon_count > 100)
        {
            GameEnd();
        }
	}

    // ゲーム開始処理
    void GameStart()
    {
        balloon_count = 0;
        point = 0;
        point_text.GetComponent<Text>().text = "Point: " + point;
    }

    // ゲーム中処理
    void InGame()
    {
        RandomBalloonNum();
        if (balloon_rand > BALLOON_CHECK)
        {
            // latency_time秒後にバルーン生成
            StartCoroutine(iBalloonSet(latency_time, () =>
            {
                // バルーン生成
                Instantiate(balloon, new Vector3(balloon_x, balloon_y, balloon_z), Quaternion.identity);
                balloon_count++;
            }));
        }
    }

    // ゲーム終了処理
    void GameEnd()
    {
        game.SetActive(false);
        result.SetActive(true);
        PointSet();
    }

    // バルーンのランダム要素処理
    void RandomBalloonNum()
    {
        latency_time = Random.Range(1f, 3f);
        balloon_x = Random.Range(-3f, 3f);
        balloon_z = Random.Range(-3f, 3f);
        balloon_rand = Random.Range(0f, 5f);
    }

    // ゲーム中、リザルトの得点表示
    void PointSet()
    {
        if (game.activeSelf)
        {
            point_text.GetComponent<Text>().text = "Point: " + point;
        }
        else if (result.activeSelf)
        {
            result_text.GetComponent<Text>().text = "Your Point：" + point;
        }
        
    }

    IEnumerator iBalloonSet(float wait_time, System.Action action)
    {
        yield return new WaitForSeconds(wait_time);
        action();
    }
}
