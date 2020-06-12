using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private SpriteRenderer sprite;
    private Animator _animator;
    private string ladoAtual = "E"; // D = Direita, E = Esquerda

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.gameOver)
        {
            sprite.flipX = false;
            _animator.Play("Die");
        }
       
    }

    void TrocarPersonagemLado(string novaPosicao)
    {
        if(ladoAtual != novaPosicao)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z );
            sprite.flipX = !sprite.flipX;
            ladoAtual = novaPosicao;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.instance.gameOver = true;
        GameManager.instance.SalvarPontuacao();
    }

    public void TouchPlayer(string ladoToque)
    {
        if (!GameManager.instance.gameOver)
        {
            TrocarPersonagemLado(ladoToque);
            _animator.Play("Cut");
        }
    }

}
