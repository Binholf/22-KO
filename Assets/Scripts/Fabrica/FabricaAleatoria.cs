using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Fabrica
{
    public abstract class FabricaAleatoria : MonoBehaviour
    {
        //lista de possiveis spawnPoints
        [SerializeField]protected Transform[] spawnPoints;
        protected int randSpawn;
        protected bool[] jaTem;

        //Metodo interno para obter posicao
        protected int definirSpawn() {
            randSpawn= Random.Range(0, spawnPoints.Length);
            return randSpawn;
        }

        protected void Awake() {
            //iniciar o vetor de posições ja spawnadas
            jaTem = new bool[spawnPoints.Length];
            //setar todos como false
            for(int i=0; i<spawnPoints.Length; i++){
                jaTem[i]=false;
            }
        }

        public abstract void criaInstancia();  
    }
}