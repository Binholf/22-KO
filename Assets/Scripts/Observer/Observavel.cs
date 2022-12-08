using UnityEngine;
using System.Collections;
using Assets.Scripts.Observer;

namespace Assets.Scripts.Observer
{
    public interface Observavel 
    {
        void resgistraObs(Observador obs);
        void cancelaRegistro(Observador obs);
        void notifica(object observavel, Eventos evento);
    }
}