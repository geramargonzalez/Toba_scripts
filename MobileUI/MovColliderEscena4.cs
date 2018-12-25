using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovColliderEscena4 : MonoBehaviour {

    [SerializeField]
    private PolygonCollider2D[] colliders;
    private int currentColliderIndex = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetColliderForSprite(int spriteNum)
    {
        colliders[currentColliderIndex].enabled = false;
        currentColliderIndex = spriteNum;
        colliders[currentColliderIndex].enabled = true;
    }
}
