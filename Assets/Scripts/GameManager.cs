using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public bool gameOver = false;


    //Variáveis da árvore

    public GameObject[] troncos;
    public List<GameObject> listaTroncos;

    private float alturaTronco = 2.43f;
    private float posicaoInicialY = -4.42f;
    private int maxTroncos = 6;
    private bool troncoSemGalho = false;


    //Variáveis de pontuação
    public Text pontuacao;
    public Text level;
    private int pontos = 0;

    //Variáveis de tempo
    public Image barraTempo;
    private float larguraBarraTempo = 330f;

    private float tempoJogo = 20f;
    private float tempoExtra = 0.20f; //Define nível do jogo
    private float tempoAtual;

    void Awake() // Necessário pois tenho variável estática;
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        tempoAtual = tempoJogo;
        InicializarTroncos();
        DefinirDificuldade();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            DiminuirBarra();
            DefinirDificuldade();
        }

    }

    void CriarTroncos(int position)
    {
        GameObject tronco = Instantiate(troncoSemGalho ? troncos[Random.Range(0, 3)] : troncos[0]);

        tronco.transform.localPosition = new Vector3(0f, posicaoInicialY + position * alturaTronco, 0f);

        listaTroncos.Add(tronco);

        troncoSemGalho = !troncoSemGalho;
    }

    void InicializarTroncos()
    {
        for (int position = 0; position <= maxTroncos; position++)
        {
            CriarTroncos(position);
        }
    }

    void CortarTronco()
    {
        //O tronco cortado sempre vai ser o primeiro (POSIÇÃO 0)
        Destroy(listaTroncos[0]);
        listaTroncos.RemoveAt(0);
        SoundManager.instance.PlaySound(SoundManager.instance.corteSound);
        SomarPontos();
        SomarTempo();
    }

    void ReposicionarTronco()
    {
        for (int position = 0; position < listaTroncos.Count; position++)
        {
            listaTroncos[position].transform.localPosition = new Vector3(0f, posicaoInicialY + position * alturaTronco, 0f);
        }
        CriarTroncos(maxTroncos);
    }

    void SomarPontos()
    {
        pontos++;
        pontuacao.text = pontos.ToString();
    }

    void DefinirDificuldade()
    {
        switch (pontos)
        {
            case 0:
                level.text = "Level 1";
                StartCoroutine(Destruir());
                break;

            case 20:
                level.gameObject.SetActive(true);
                tempoExtra = 0.15f;
                level.text = "Level 2";
                StartCoroutine(Destruir());
                break;
            case 50:
                level.gameObject.SetActive(true);
                tempoExtra = 0.10f;
                level.text = "Level 3";
                StartCoroutine(Destruir());
                break;
            case 80:
                level.gameObject.SetActive(true);
                tempoExtra = 0.05f;
                level.text = "Level 4";
                StartCoroutine(Destruir());
                break;
            case 100:
                level.gameObject.SetActive(true);
                tempoExtra = 0.02f;
                level.text = "Level 5";
                StartCoroutine(Destruir());
                break;
            case 120:
                level.gameObject.SetActive(true);
                tempoExtra = 0.01f;
                level.text = "Level 6";
                StartCoroutine(Destruir());
                break;
            default:
                break;
        }
    }

    IEnumerator Destruir()
    {
        yield return new WaitForSeconds(2);
        level.gameObject.SetActive(false);
    }

    void SomarTempo()
    {
        if (tempoAtual + tempoExtra < tempoJogo) //Verficar se não vou ultrapassar meu tempo máx. de jogo
        {
            tempoAtual += tempoExtra;
        }
    }

    void DiminuirBarra()
    {
        tempoAtual = tempoAtual - Time.deltaTime;

        float tempo = tempoAtual / tempoJogo;
        float position = larguraBarraTempo - (tempo * larguraBarraTempo);

        barraTempo.transform.localPosition = new Vector2(-position, barraTempo.transform.localPosition.y);

        if (tempoAtual <= 0) //Tempo acabou
        {
            gameOver = true;
            SalvarPontuacao();
        }
    }

    public void SalvarPontuacao()
    {
        if (PlayerPrefs.GetInt("best") < pontos)
        {
            PlayerPrefs.SetInt("best", pontos);
        }

        PlayerPrefs.SetInt("score", pontos);

        SoundManager.instance.PlaySound(SoundManager.instance.dieSound);

        Invoke("ChamarCenaGameOver", 2f);

    }

    public void ChamarCenaGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Touch()
    {
        if (!gameOver)
        {
            CortarTronco();
            ReposicionarTronco();
        }
    }

}
