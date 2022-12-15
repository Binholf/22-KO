using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Skull : Enemy
    {   

        [SerializeField]private int vida;
        //tempo para auxiliar nos movimentos
        private float tempo=3f;
        //varivel para definir o atack
        [SerializeField]private bool atacando=true;
        //variveis de auxilio
        private bool fraco=false, giro=false;
        private bool olhandoD = false;
        private int contT=0;

        //variveis do movimento circular
        public float velocidadeAngular = 1f;
        public float RaioCirculo = 1f;
 
        public Vector2 centroCirculo;
        private float angulo;

        //variaveis da verificação de parede
        public Transform wallCheck;
        public LayerMask groundLayer;
        private bool parede;

        private CircleCollider2D cirC;
        private bool nasceu=true;


        void FixedUpdate()
        {   
            if(nasceu){
                cirC = GetComponent<CircleCollider2D>();
                nasceu=false;
            }

            tempo-=Time.deltaTime;
            anima.SetBool("Atacando", atacando);

            //verificação das paredes
            parede = Physics2D.Linecast(wallCheck.position, transform.position, groundLayer);

            //trocar a velocidade 
            if(parede){
                velocidade*=-1;
            }

            //mudar os etados de atacando e parado
            if(contT<=5 && !fraco){
                atacando=true;
            }
            else if(contT<=6 && !fraco){
                atacando=false;
            }
            else if(contT<=7 && !fraco){
                contT=0;
            }

            if(atacando){    
                capC.enabled=true;
                anima.SetBool("Atacando", atacando);
                //tempo andando de um lado para o outro 
                if(tempo>1f && !giro){
                    rigid.velocity = new Vector2(velocidade, -velocidade*0.5f);

                }
                else if(tempo<1f && giro){
                    //movimento circular
                    angulo += velocidadeAngular * Time.deltaTime;
                    Vector2 deslocamento = new Vector2 (Mathf.Sin (angulo), Mathf.Cos (angulo)) * RaioCirculo;
                    this.transform.position = centroCirculo + deslocamento;
                }
            }
            //se não tiver atacando ele é parado
            else if(!atacando){
                capC.enabled=false;
                anima.SetBool("Atacando", atacando);
                rigid.velocity = new Vector2(0, 0);
            }

            //resetando o contador  
            if(tempo<0 && !fraco){
                tempo=3f;
                contT+=1;
            }
            else if(tempo<0 && fraco){
                capC.enabled=true;
                boxC.enabled=true;
                rigid.bodyType=RigidbodyType2D.Dynamic;
                fraco=false;
            }
            
            if(tempo==1f || tempo==3f){
                circular();
                rigid.velocity = new Vector2(0, 0);
            }
            
            //necessário mudar a direção transform do inimigo para melhor funcionamento da vereficação do chão
            if(rigid.velocity.x>0 && !olhandoD){
                Flip();
            }else if(rigid.velocity.x<0 && olhandoD){
                Flip();
            }
        }

        void circular(){
            giro=!giro;
            centroCirculo = this.transform.position;
        }

        //função para invertar a direção do angrypig
        //inverte o transform e não somente o sprite
        void Flip()
        {
            olhandoD=!olhandoD;
            //obter o scale do angrypig para poder invertelo
            Vector3 Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
        }

        public override void tomarDano(){
            if(vida<=0){
                Destroy(this.gameObject, 0.40f);
            }
            else{
                anima.SetBool("Dano", true);
                vida-=1;
                tempo=3f;
                fraco=true;
                atacando=false;
            }
            
        }

        //Manipula as colisões 
        private void OnCollisionEnter2D(Collision2D collision){
            if(collision.gameObject.name == "Wall"  ){
                //verifica se mesmo colidindo com o "Ground/Chão" ele está realmente no chão para poder dar o pulo duplo
                velocidade*=-1;
            }
        
        }
    }
}
