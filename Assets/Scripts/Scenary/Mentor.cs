using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Players;

namespace Assets.Scripts.Scenary
{
    public class Mentor : MonoBehaviour
    {
        //variaveis para controlar o painel de dialogo

        //controlar a string de fala do NPC
        public string[] falaNPC;
        public int falaIndex=0;

        //controlar o canvas com o painel do dialogo
        [SerializeField]private GameObject dialoguePainel;
        [SerializeField]private Text dialogueText;

        //controlar as informação referentes ao npc especifico que serão mostrada no painel
        [SerializeField]private Text nomeNPC;
        [SerializeField]private Image imageNPC;
        [SerializeField]private Sprite spriteNPC;

        //variavel de controle da fala 
        private bool falou=false;
        //private bool comecou=false;

        [SerializeField]private Player jogador;

        private void Start() {
            //desativar o painel de dialogo
            dialoguePainel.SetActive(false);
        }

        private void Update() {
            //pular as falas com o botao space
            if(!falou){
                if(Input.GetKeyDown(KeyCode.Space) && (dialogueText.text == falaNPC[falaIndex])){
                    nextDialogue();
                }
            }
        }

        //comecar o dialogo
        //setar as variaveis e ativar o painel
        void startDialogue(){
            nomeNPC.text = "Mentor";
            imageNPC.sprite = spriteNPC;
            falaIndex=0;
            dialoguePainel.SetActive(true);
        }
        
        //codigo para passar para o proximo dialogo, andar com o index sobre o vetor de falas
        private void nextDialogue(){
            falaIndex++;

            if(falaIndex < falaNPC.Length){
                StartCoroutine(showDialogue());
            }
            //não possue mais falar
            else{
                dialoguePainel.GetComponent<Animator>().SetBool("Falou",true);
                falou=true;
                falaIndex=0;
                FindObjectOfType<PlayerController>().jogoPausado=false;
            }

        }

        IEnumerator showDialogue(){
            //zerando o dialogo para apagar qualquer fala subjacente
            dialogueText.text="";
            //para cada letra dentro da string falaNPC será mostrado no dialogueText com um tempo
            //deixar a fala mostrando letra por letra 
            foreach (char letra in falaNPC[falaIndex])
            {
                dialogueText.text+=letra;
                yield return new WaitForSeconds(0.05f);
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.CompareTag("Player")){
                if(!falou){
                    //comecou=true;
                    //zerar a velocidade do player
                    other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, other.GetComponent<Rigidbody2D>().velocity.y);
                    //pausar o jogo dentro do dialogo, faz com que o player nao possa se mexer
                    FindObjectOfType<PlayerController>().jogoPausado=true;
                    other.GetComponent<Animator>().SetBool("Andando", false);
                    startDialogue();
                    StartCoroutine(showDialogue());
                }
            }
        }

        //instanciar um novo player assim que sair do colisor do NPC
        private void OnTriggerExit2D(Collider2D other) {
            if(other.CompareTag("Player")){
                if(other.gameObject.name!="Bunny(Clone)"){
                    FindObjectOfType<PlayerController>().setPlayer(jogador);
                }
                
            }
        }
    }
}
