using Assets.Scripts.Players;
using Assets.Scripts.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Players
{
    public class PlayerController : MonoBehaviour, Observavel
    {   
        //instanciando jogador na cena
        [SerializeField] Player jogador, jogadorUso;
        public Transform respawn;
        private float delay=0, delayAgr;
        private bool movendo;
        public bool jogoPausado=false;
        public int frutas;
        List<Observador> observadores;

        private void Awake() {
            observadores = new List<Observador>();
        }

        // Start is called before the first frame update
        void Start()
        {   
            jogadorUso = Instantiate(jogador,respawn.position, respawn.rotation);  
            notifica(jogadorUso, Eventos.PLAYER_CRIADO);        
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void FixedUpdate() {
            //como o jogador e instanciado junto do tranforme do objeto PlayerM
            //é necessario ter um delay no momento de seguir a posição do jogador
            //e começar a movimentar o player(dar tempo da animação de aparição terminar)
            delay+=Time.deltaTime;
            if(delay>0.6f && jogadorUso.vivo && !jogoPausado){
                transform.position=jogadorUso.transform.position;
                jogadorUso.movimentação();
                jogadorUso.Pulo();
                jogadorUso.verificaChãoxParedes(); 
                if(Input.GetKey(KeyCode.F)){
                    Debug.Log(frutas);
                }
            }
            //se o jogo não foi pausado e o player não está vivo, então é necessario destroir aquele objeto
            else if(!jogadorUso.vivo && !jogoPausado){
                Destroy(jogadorUso.gameObject, 0.40f);
                jogoPausado=true;
            }
            //mecânica de respaw
            //todo vez que o player morrer, será preciso instaciar um novo player caso o jogador queria continuar
            if(jogoPausado){
                if(Input.GetKey(KeyCode.Space) && !jogadorUso.vivo){
                    movendo =true;
                }
                //apenas para dar um movimento mais suave até a area de respawn do jogo 
                //também da um tempo para que o player seja instânciado(apenas estético)
                if(movendo){
                    transform.position= Vector3.Lerp(transform.position, respawn.transform.position, 0.3f);
                }
                if(transform.position==respawn.transform.position){
                    jogadorUso = Instantiate(jogador,respawn.position, respawn.rotation);
                    notifica(jogadorUso, Eventos.PLAYER_CRIADO); 
                    movendo=false;
                    jogoPausado=false;
                } 
            }
            
        }

        //setar um novo jogador do tipo Player
        public void setPlayer(Player jogador) {
            //pegar o local atual do player
            Transform local = jogadorUso.transform;
            //destroir e instânciar um novo jogador
            Destroy(jogadorUso.gameObject);
            jogadorUso = Instantiate(jogador,local.position, local.rotation);
            notifica(jogadorUso, Eventos.PLAYER_CRIADO); 
        }
        
        //Observavel
        public void notifica(object observavel, Eventos evento) {
            foreach (var observador in observadores) {
                observador.atualiza(observavel, evento);
            }
        }
        public void resgistraObs(Observador obs) {
           observadores.Add(obs);
        }
        public void cancelaRegistro(Observador obs) {
                observadores.Remove(obs);
        }

    }
}