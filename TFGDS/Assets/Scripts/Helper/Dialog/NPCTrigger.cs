using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public Story01 data;
    public DialogConfig assert;
    public DialogManager manager;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        PlayerInfo.instance_.CursoModeState = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        manager.StartDialog(data,assert);
    }

}
