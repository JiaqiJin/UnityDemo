using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompasControl : MonoBehaviour
{
    public GameObject pointer;
    public GameObject target;
    public GameObject player;
    public RectTransform compasLine;
    
    RectTransform rect;
    private void Start()
    {
        rect = pointer.GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3[] v = new Vector3[4]; // los cornes de la barra de la brujula
        compasLine.GetLocalCorners(v);
        float pointerScale = Vector3.Distance(v[1], v[2]); 

        Vector3 direction = target.transform.position - player.transform.position;
        float angleToTarget = Vector3.SignedAngle(player.transform.forward, direction, player.transform.up);

        angleToTarget = Mathf.Clamp(angleToTarget, -90, 90) / 180.0f * pointerScale;
        rect.localPosition = new Vector3(angleToTarget, rect.localPosition.y, rect.localPosition.z);
    }
}
