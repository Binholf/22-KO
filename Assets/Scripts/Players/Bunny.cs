using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Players
{
    public class Bunny : Player
    {

        public float contPulo;

        // Start is called before the first frame update
        void Awake()
        {
            init();
        }

        // Update is called once per frame
        void Update()
        {
            //codigo para verificar os tipos de pula e implementar o pulo variavel
            //pulo normal
            if(Input.GetKeyDown(KeyCode.W) && noChão){
                pulando=true;
            }
            //manter a tecla de pulo precionada, contador que ira determinar o tempo que o player ganhar velocidade
            //vai diminuindo, evitando ele pular infinitamente
            if(Input.GetKey(KeyCode.W)){
                contPulo -=Time.deltaTime;
            }
            //soltar a tecla de pulo, contador restaurado ao padrão
            if(Input.GetKeyUp(KeyCode.W)){
                pulando=false;
                contPulo=0.5f;
            }
        }

        public override void verificaChãoxParedes(){
            //cria um "caixa", utilizando os parametros do boxCollider do Player, para verificar colisão
            //se está "caixa" tiver contato com a layer especificada ela retorna true
            noChão = Physics2D.BoxCast(boxP.bounds.center, boxP.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
            
        }

        public override void movimentação(){
            //Movimentação do personagem conforme as teclas atribuidas
            //O valor atribuido ao eixo Y é referente a "gravidade" sobre o personagem 
            if(Input.GetKey(KeyCode.D)){
                if(noChão && particulas.isPlaying || noChão && !particulas.isPlaying){
                    particulas.Play();
                }
                rigid.velocity = new Vector2(0, rigid.velocity.y);
                rigid.velocity = new Vector2(velocidade, rigid.velocity.y);
                //Se o player se move para direita é necessario mudar a direção do seu sprite
                spriteP.flipX=true;
                animaP.SetBool("Andando", true);
            }

            else if(Input.GetKey(KeyCode.A)){
                if(noChão && particulas.isPlaying || noChão && !particulas.isPlaying){
                    particulas.Play();
                }
                rigid.velocity = new Vector2(0, rigid.velocity.y);
                rigid.velocity = new Vector2(-velocidade, rigid.velocity.y);
                spriteP.flipX=false;
                animaP.SetBool("Andando", true);
            }

            //Se nenhuma tecla é precionada, o player é parado
            else{
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
                    animaP.SetBool("Andando", false);
                    
            }
            
            //rotação do sprite para qualquer movimento
            if(rigid.velocity.x>0){
                spriteP.flipX=true;
            }else if(rigid.velocity.x<0){
                spriteP.flipX=false;
            }
        }

        public override void Pulo(){
            tempoPassado += Time.deltaTime;
            animaP.SetFloat("Sob_Cai", rigid.velocity.y);
            animaP.SetBool("Pulando", pulando);
            animaP.SetBool("NoChao", noChão);
            //mecanica de pulo variavel, enquanto o contador estiver maior que zero o player pode ganhar velocidade (pular)
            //o contador é diminuido pelos metodos auxiliares do pulo na função update  
            if(pulando){
                if(contPulo>0){
                    rigid.velocity=new Vector2(rigid.velocity.x, forcaPulo);
                }
                else{
                    pulando=false;
                }
            }
        }

        //Manipula as colisões do personagem dentro do jogo
        private void OnCollisionEnter2D(Collision2D collision){
            //Se houver colisão com o objeto com nome "Ground" é verificado que o player está no chão
            //Necessário para a mecânica de pulo
            if(collision.gameObject.name == "Ground" && noChão){
                //verifica se mesmo colidindo com o "Ground/Chão" ele está realmente no chão para poder dar o pulo duplo
                pulando = false;
            }
        
        }

    }
}
