    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using DG.Tweening;

public class BomberBeAI : MonoBehaviour
{

    public float beeDestroydelay;
    public float beeSpeed;
    bool unaVez;

    void Start()
    {
        unaVez = true;
    }



    public void ActivatedBee(Vector3 playerpos)
    {
        transform.DOMove(playerpos, beeSpeed, false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (unaVez)
            {
                Destroy(this.gameObject, beeDestroydelay);




            }

            if (other.gameObject.CompareTag("Player"))
            {
                if (unaVez)
                {
                    SistemaDejuego.instance.PlayerDies(other.gameObject);
                    unaVez = false;
                }

            }

        }

    }

}


