using UnityEngine;
using System.Collections;

public class MouseClickScript : MonoBehaviour {

    public float distance = 10000f;// rayが届く範囲

    static readonly string BALLOON_NAME = "Balloon(Clone)";

    [SerializeField]
    GameManager game_manager;
    [SerializeField]
    GameObject game;
    
	void Update () {
        // 左クリックを取得
        if (Input.GetMouseButtonDown(0))
        {
            // クリックしたスクリーン座標をrayに変換
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Rayの当たったオブジェクトの情報を格納する
            RaycastHit hit = new RaycastHit();
            // オブジェクトにrayが当たった時
            if (Physics.Raycast(ray, out hit, distance))
            {
                // rayが当たったオブジェクトの名前を取得
                string objectName = hit.collider.gameObject.name;
                if(objectName == BALLOON_NAME)
                {
                    if (game.activeSelf)
                    {
                        // 赤く変化してるときは5得点加算。それ以外は1加算
                        if (hit.collider.gameObject.GetComponent<BalloonScript>().balloon_red_check)
                        {
                            game_manager.GetComponent<GameManager>().point += 5;
                        }
                        else
                        {
                            game_manager.GetComponent<GameManager>().point++;
                        }
                        
                    }
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
