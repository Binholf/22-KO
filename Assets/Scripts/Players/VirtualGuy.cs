using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Players{

    public class VirtualGuy : Player
    {     
        private float tempoPlanagem;

        public bool naParede;
        public bool puloDuplo;

        public void Awake() {
            init();
        }

        // Update is called once per frame
        public void Update()
        {
            
        }

        //Fixed update é utilizado para garantir a constância em diferentes hardwares
        public void FixedUpdate() 
        {
            
        }

        public override void verificaChãoxParedes(){
            //cria um "caixa", utilizando os parametros do boxCollider do Player, para verificar colisão
            //se está "caixa" tiver contato com a layer especificada ela retorna true
            noChão = Physics2D.BoxCast(boxP.bounds.center, boxP.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
            naParede = (Physics2D.BoxCast(boxP.bounds.center, boxP.bounds.size, 0, Vector2.right, 0.05f, groundLayer) || Physics2D.BoxCast(boxP.bounds.center, boxP.bounds.size, 0, Vector2.left, 0.05f, groundLayer));
            animaP.SetBool("NaParede", naParede);
        }

        public override void movimentação(){
            //mecânica para evitar que o jogador escale parede apertando infinitamente pra pular
            //adiona um temporizador que desativa os controles horizontais do player após um pulo feito sobre uma parede
            if(tempoPlanagem>0f){
                rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y);
            }
            else{
                tempoPlanagem=0f;
                //Movimentação do personagem conforme as teclas atribuidas
                //O valor atribuido ao eixo Y é referente a "gravidade" sobre o personagem 
                if(Input.GetKey(KeyCode.D)){
                    if(noChão){
                        particulas.Play();
                    }
                    if(noChão && !particulas.isPlaying ){
                        particulas.Play();
                    }
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
                    rigid.velocity = new Vector2(velocidade, rigid.velocity.y);
                    spriteP.flipX=false;
                    animaP.SetBool("Andando", true);      
                }
                else if(Input.GetKey(KeyCode.A)){
                    if(noChão){
                        particulas.Play();
                    }
                    if(noChão && !particulas.isPlaying ){
                        particulas.Play();
                    }
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
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
            //rotação do sprite para qualquer movimento
            if(rigid.velocity.x>0){
                spriteP.flipX=false;
            }else if(rigid.velocity.x<0){
                spriteP.flipX=true;
            }
        }

        public override void Pulo(){
            tempoPassado += Time.deltaTime;
            tempoPlanagem-=Time.deltaTime;
            animaP.SetFloat("Sob_Cai", rigid.velocity.y);
            animaP.SetBool("Pulando", pulando);
            animaP.SetBool("NoChao", noChão);
            //Pulo do personagem, adionando uma força vertical(eixo Y) no player 
            //Adionado um delay na tecla 
            if(Input.GetKey(KeyCode.W) && tempoPassado >= keyDelay){
                tempoPassado = 0f ;
                //mecânica de pulo duplo
                if(!noChão && puloDuplo && !naParede){
                    pulando=false;
                    rigid.velocity = new Vector2(0, 0);
                    rigid.AddForce(new Vector2(0, forcaPulo), ForceMode2D.Impulse);
                    puloDuplo = false;
                    animaP.SetBool("DoubleJump", puloDuplo);
                }

                //pulo estando no chão 
                else if(noChão){
                    rigid.velocity = new Vector2(0, 0);
                    rigid.AddForce(new Vector2(0, forcaPulo), ForceMode2D.Impulse);  
                    puloDuplo = true;
                    pulando =true;
                    animaP.SetBool("DoubleJump", puloDuplo);
                    new WaitForSeconds(3f);
                }

                //pulo estando na parade
                else if(naParede){
                    tempoPlanagem=0.28f;
                    rigid.velocity = new Vector2(0, 0);
                    if(!(spriteP.flipX)){
                        rigid.AddForce(new Vector2(-1.6f, forcaPulo), ForceMode2D.Impulse); 
                    }
                    else{
                        rigid.AddForce(new Vector2(1.6f, forcaPulo), ForceMode2D.Impulse); 
                    }
                    pulando=true;
                    animaP.SetBool("DoubleJump", puloDuplo);
                    new WaitForSeconds(3f);
                }
            }
        }

        //Manipula as colisões do personagem dentro do jogo
        private void OnCollisionEnter2D(Collision2D collision){
            //Se houver colisão com o objeto com nome "Ground" é verificado que o player está no chão
            //Necessário para a mecânica de pulo
            if(collision.gameObject.name == "Ground" && noChão){
                //verifica se mesmo colidindo com o "Ground/Chão" ele está realmente no chão para poder dar o pulo duplo
                puloDuplo = true; 
                pulando = false;
                animaP.SetBool("DoubleJump", puloDuplo);
                tempoPlanagem=0f;
            }
        
        }

    }
}