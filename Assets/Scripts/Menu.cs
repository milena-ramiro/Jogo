using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void IniciarJogo()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.btnPlaySound);

        SceneManager.LoadScene("Game");
    }
}
