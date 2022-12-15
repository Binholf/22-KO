using Assets.Scripts.Observer;
using Assets.Scripts.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievment_5K : Achievment
{   
    public string nomeConquista;
    public float tempoLigado;
    public float tempo=0;
    //variaveis do painel de conquista
    [SerializeField]private GameObject AchievmentPainel;

    [SerializeField]private Text AchievmentText;
    [SerializeField]private Image AchievmentImage;
    [SerializeField]private Sprite AchievmentSprite;

    public int numMortes;
    //variavel para desativar o animator
    private bool acabou=false;

    private void Start() {
        FindObjectOfType<PlayerController>().resgistraObs(this);
        AchievmentPainel.SetActive(false);
    }

    private void Update() {
        tempo+= Time.deltaTime;
        //desligar o painel de conquista após o tempo determinado
        if(unlocked && !acabou){
            if(tempo>tempoLigado){
                AchievmentPainel.GetComponent<Animator>().SetBool("terminou",true);
                acabou=true;
            }
        }
        
    }

    public override void atualiza(object observavel, Eventos evento) {
            //"tranformação" do observavel em player
            Player player = (Player)observavel;
            //se o player foi criado é preciso registrar essa cosquista como observador
            if (evento == Eventos.PLAYER_CRIADO) {
                player.resgistraObs(this);
            }
            //se o player foi morto é preciso quantificar suas mortes para desbroquear essa consquista
            else if (evento == Eventos.PLAYER_MORTO) {
                numMortes++;
            if (numMortes >= 6)
                unlock();
            }
    }

    protected override void unlock(){
        if (!unlocked) {
            //setar a variavel terminou como false para que, caso um achievment ja tenha sido obtido, o painel possa aparecer novamente
            AchievmentPainel.GetComponent<Animator>().SetBool("terminou",false);

            //sinalizar que a conquista foi desbroqueada e passar os parametros dela(nome e imagem)
            unlocked = true;
            AchievmentText.text=nomeConquista;
            AchievmentImage.sprite=AchievmentSprite;
            AchievmentPainel.SetActive(true);
            
            tempo=0;
        }
    }
}
