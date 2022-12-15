using Assets.Scripts.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Achievment : MonoBehaviour, Observador
{
    [SerializeField]protected bool unlocked;
    protected abstract void unlock();
    public abstract void atualiza(object observavel, Eventos evento);
}
