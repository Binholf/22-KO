using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Players;
using Assets.Scripts.Dialogue;
using Assets.Scripts.Observer;

namespace Assets.Scripts.Scenary
{
    public class MaskDude : Falador, Observavel
    {
        List<Observador> observadores;

        private void Awake() {
            observadores = new List<Observador>();
            notifica(this, Eventos.DUDE_CRIADO); 
        }

        //Informar os observadores que o dude foi encontrado
        //Gerar conquista
        private void OnTriggerExit2D(Collider2D other) {
            if(other.CompareTag("Player")){
                notifica(this, Eventos.DUDE_ENCONTRADO);
            }
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
