using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Powerupslot : MonoBehaviour
{
    public void poof(){
        this.gameObject.SetActive(false);
    }
}
