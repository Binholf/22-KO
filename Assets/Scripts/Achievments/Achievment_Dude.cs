using Assets.Scripts.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Scenary;

public class Achievment_Dude : Achievment
{   
    public string nomeConquista;
    public float tempoLigado;
    public float tempo=0;
    //variaveis do painel de conquista
    [SerializeField]private GameObject AchievmentPainel;

    [SerializeField]private Text AchievmentText;
    [SerializeField]private Image AchievmentImage;
    [SerializeField]private Sprite AchievmentSprite;

    //variavel para desativar o animator
    private bool acabou=false;

    private void Start() {
        FindObjectOfType<MaskDude>().resgistraObs(this);
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
            //"tranformação" do observavel em MaskDuDe
            MaskDude mask = (MaskDude)observavel;
            //se o maskDude foi criado é preciso registrar essa cosquista como observador
            if (evento == Eventos.DUDE_CRIADO) {
                mask.resgistraObs(this);
            }
            //se o maskDude foi descoberto ja é possivel liberar a conquista
            else if (evento == Eventos.DUDE_ENCONTRADO) {
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
