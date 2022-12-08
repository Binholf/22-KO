using UnityEngine;
using System.Collections;
using Assets.Scripts.Observer;

namespace Assets.Scripts.Observer
{
    public interface Observador 
    {
        void atualiza(object observavel, Eventos evento);
    }
}