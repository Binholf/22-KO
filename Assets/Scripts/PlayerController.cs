using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public float keyDelay ;  
    private float tempoPassado ;

    public float velocidade;
    public float forcaPulo;
    public bool noChão;
    public bool puloDuplo;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteP;
    private Animator animaP;

    // Start is called before the first frame update
    void Start()
    {
        //variaveis para pegar os componentes presentes no player
        animaP = GetComponent<Animator>();
        spriteP = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Fixed update é utilizado para garantir a constância em diferentes hardwares
    void FixedUpdate() {
        movimentação();
        Pulo();
    }

    void movimentação(){

        //Movimentação do personagem conforme as teclas atribuidas
        //O valor atribuido ao eixo Y é referente a "gravidade" sobre o personagem 
        if(Input.GetKey(KeyCode.D)){
            rigid.velocity = new Vector2(velocidade, rigid.velocity.y);
            spriteP.flipX=false;
            animaP.SetBool("Andando", true);
        }
        else if(Input.GetKey(KeyCode.A)){
            rigid.velocity = new Vector2(-velocidade, rigid.velocity.y);
            //Se o player se move para esquerda é necessario mudar a direção do seu sprite
            spriteP.flipX=true;
            animaP.SetBool("Andando", true);
        }
        //Se nenhuma tecla é precionada, o player é parado
        else{
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            animaP.SetBool("Andando", false);
        }



    }

    void Pulo(){
        tempoPassado += Time.deltaTime;
        animaP.SetFloat("Sob_Cai", rigid.velocity.y);
        //Pulo do personagem, adionando uma força vertical(eixo Y) no player
        if(Input.GetKey(KeyCode.W) && tempoPassado >= keyDelay){
            tempoPassado = 0f ;
            //mecânica de pulo duplo
            if(!noChão && puloDuplo){
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(new Vector2(0, forcaPulo), ForceMode2D.Impulse);
            noChão=false;
            puloDuplo = false;
            animaP.SetBool("DoubleJump", puloDuplo);
            animaP.SetBool("NoChao", noChão);
            }

            if(noChão){
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(new Vector2(0, forcaPulo), ForceMode2D.Impulse);
            noChão=false;
            puloDuplo = true;
            animaP.SetBool("NoChao", noChão);
            animaP.SetBool("DoubleJump", puloDuplo);
            new WaitForSeconds(3f);
            }
        }   
    }

    //Manipula as colisões do personagem dentro do jogo
    private void OnCollisionEnter2D(Collision2D collision){
        //Se houver colisão com o objeto com nome "Ground" é verificado que o player está no chão
        //Necessário para a mecânica de pulo
        if(collision.gameObject.name == "Ground"){
            noChão=true;
            puloDuplo = false;
            animaP.SetBool("NoChao", noChão);
            animaP.SetBool("DoubleJump", puloDuplo);
        }
    }



   

}
