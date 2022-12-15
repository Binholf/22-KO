using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Fabrica;

namespace Assets.Scripts.Enemies
{
    public class CriadorEnemy : FabricaAleatoria
    {
        [SerializeField] protected Enemy inimigo;
        public int quantidade;

        //gerar inimigo, seguindo ordem aleatória de spawn e quantidade especificada  
        public override void criaInstancia(){
            if(quantidade!=0){
                int pos =definirSpawn();
                if(!jaTem[pos]){
                    Instantiate(inimigo,spawnPoints[pos].position, spawnPoints[pos].rotation); 
                    jaTem[pos]=true;
                    quantidade--; 
                }
            }
        }


        private void Update() {
            criaInstancia();
        }

    }       
}
