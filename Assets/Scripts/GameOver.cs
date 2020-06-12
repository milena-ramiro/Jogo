using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    public Text best;
    public Text score;


    // Start is called before the first frame update
    void Start()
    {
        best.text = PlayerPrefs.GetInt("best").ToString();
        score.text = PlayerPrefs.GetInt("score").ToString();
    }

    public void JogarNovamente()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.btnPlaySound);

        SceneManager.LoadScene("Game");
    }
}
