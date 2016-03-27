using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    [SerializeField]
    GameObject title;
    [SerializeField]
    GameObject game;
    [SerializeField]
    GameObject result;

    public void StartButtonPush()
    {
        title.SetActive(false);
        game.SetActive(true);
    }

    public void TitleBackButtonPush()
    {
        title.SetActive(true);
        result.SetActive(false);
    }
}
