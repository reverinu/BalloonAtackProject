using UnityEngine;
using System.Collections;

public class BalloonScript : MonoBehaviour {

    [SerializeField]
    GameObject balloon;

    static readonly Vector3 BALLOON_MAX_SIZE = new Vector3(1f,1f,1f);// バルーンの最大サイズ。これを超えるとDestroyする

    public bool balloon_red_check = false;// バルーンが赤くし始めるときにTRUEとなる。得点5倍
    
	void Update () {
        BallonIncrease();
        // バルーンのサイズをベクトルの長さに変換し比較
        if (balloon.transform.localScale.sqrMagnitude >= BALLOON_MAX_SIZE.sqrMagnitude)
        {
            BallonColorChange();
            balloon_red_check = true;
            // 1秒後にDestroy
            StartCoroutine(iBalloonExplosion(1f, () =>
            {
                Destroy(balloon);
            }));
        }
	}
    
    // バルーンのサイズUP
    void BallonIncrease()
    {
        balloon.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
    }

    // 一定のサイズになった時に白から赤へ（赤に達した時は白へ戻る）
    void BallonColorChange()
    {
        balloon.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.red,Mathf.PingPong(Time.time, 1));
    }

    IEnumerator iBalloonExplosion(float wait_time, System.Action action)
    {
        yield return new WaitForSeconds(wait_time);
        action();
    }
}
