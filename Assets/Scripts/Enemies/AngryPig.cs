using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class AngryPig : Enemy
    {   
        public Transform groundCheck, wallCheck;
        //tempo para auxiliar nos movimentos, parado e andando, do angrypig
        private float tempoPeA=4f;
        private bool raiva;
        private bool parede, noChão;
        private bool olhandoD = false;
        public LayerMask groundLayer;

        void FixedUpdate()
        {   
            //verificação do chão
            //traça uma linha entre os dois primeiros parametros(posições) e verifica se há colosão com a layer especificada 
            noChão = Physics2D.Linecast(groundCheck.position, transform.position, groundLayer);
            parede = Physics2D.Linecast(wallCheck.position, transform.position, groundLayer);
            tempoPeA-=Time.deltaTime;

            //garante que o movimento seja alterado sempre que o angrypig chegar na borda do chão
            if(noChão == false || parede){
                velocidade*=-1;
            }
            
            //fica 3 segundos andando e 1 parado 
            if(tempoPeA>1f){
                rigid.velocity = new Vector2(velocidade, rigid.velocity.y);
                anima.SetBool("Andando", true);
            }
            //tempo que ele fica parado
            else if(tempoPeA<1f){
                rigid.velocity = new Vector2(0, rigid.velocity.y);
                anima.SetBool("Andando", false);
            }

            //resetando o contador  
            if(tempoPeA<0){
                tempoPeA=4f;
            }
            
            //necessário mudar a direção transform do inimigo para melhor funcionamento da vereficação do chão
            if(rigid.velocity.x>0 && !olhandoD){
                Flip();
            }else if(rigid.velocity.x<0 && olhandoD){
                Flip();
            }
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
    }
}
