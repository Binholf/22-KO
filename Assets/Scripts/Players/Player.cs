using Assets.Scripts.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enemies;

namespace Assets.Scripts.Players
{
    public abstract class Player : MonoBehaviour, Observavel
    {   
        protected float tempoPassado ;
        [SerializeField] protected float keyDelay;
        [SerializeField] protected float velocidade;
        [SerializeField] protected float forcaPulo;
        [SerializeField] protected bool noChão;
        protected bool pulando;
        public bool vivo;
        protected Rigidbody2D rigid;
        protected SpriteRenderer spriteP;
        protected Animator animaP;
        protected BoxCollider2D boxP;
        [SerializeField] protected LayerMask groundLayer;
        [SerializeField] protected LayerMask voidLayer;
        protected List<Observador> observadores;
        [SerializeField] protected ParticleSystem particulas;

        protected void init(){
            //variáveis para pegar os componentes presentes no objeto "Player"
            vivo=true;
            animaP = GetComponent<Animator>();
            spriteP = GetComponent<SpriteRenderer>();
            rigid = GetComponent<Rigidbody2D>();
            boxP = GetComponent<BoxCollider2D>();
            observadores = new List<Observador>();
            animaP.SetBool("Vivo", vivo);
        }

        protected void OnCollisionStay2D(Collision2D other) {
            //colisão com qualquer objeto que possue a tag kill
            //necessário 'desligar' o player e todos seu componentes
            if(other.gameObject.CompareTag("Kill") || other.gameObject.CompareTag("Enemy")){
                rigid.velocity=Vector2.zero;
                boxP.enabled=false;
                rigid.bodyType=RigidbodyType2D.Kinematic;
                vivo=false;
                animaP.SetBool("Vivo", vivo);
                notifica(this, Eventos.PLAYER_MORTO);
            }
        }

        //Manipulando as colisões com o triger do inimigo, onde será possivel dar dano nele
        private void OnTriggerEnter2D(Collider2D other) {
            if(other.CompareTag("Enemy")){
                //jogar o player um pouco pra cima
                rigid.velocity=Vector2.zero;
                rigid.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
                //desativar o box colider temporariamente
                other.gameObject.GetComponent<CapsuleCollider2D>().enabled=false;
                other.gameObject.GetComponent<BoxCollider2D>().enabled=false;
                other.gameObject.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Kinematic;
                other.gameObject.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
                //chama a função do enemigo de tomar dano
                other.gameObject.GetComponent<Enemy>().tomarDano();
            }
        }

        
        public void cancelaRegistro(Observador obs) {
            observadores.Remove(obs);
        }

        public void notifica(object observavel, Eventos evento) {
            foreach (var observador in observadores) {
                observador.atualiza(this, evento);
            }
        }
        public void resgistraObs(Observador obs) {
            observadores.Add(obs);
        }

        //metodos abstratos que devem obrigatóriamente nas classes concretas
        public abstract void movimentação();
        public abstract void Pulo();
        public abstract void verificaChãoxParedes();

    }
}