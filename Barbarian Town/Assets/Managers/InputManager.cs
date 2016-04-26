using UnityEngine;
using System.Collections;
using System;

public class InputManager : MonoBehaviour {

    float cameraCooldown = 0;

	void Update ()
    {
        CheckMouse();
        CheckKeyboard();
    }

    void CheckMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Global.gameManager.selectedObject = null;

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos = new Vector2((int)pos.x, (int)pos.y);

            ObjectData data = Global.mapManager.GetObject(pos);
            if (data != null && data.selectable)
                Global.gameManager.selectedObject = data;
        }
    }

    void CheckKeyboard()
    {
        if (cameraCooldown > 0)
        {
            cameraCooldown -= Time.deltaTime;
            return;
        }
        
        if (Global.gameManager.selectedObject != null)
        {
            ObjectData data = Global.gameManager.selectedObject;
            KeyListenerBehaviour listener = data.gameObject.GetComponent<KeyListenerBehaviour>();
            if (listener != null)
            {
                foreach (KeyBind bind in listener.bindings)
                {
                    if (Input.GetKeyDown(bind.code))
                    {
                        bind.listener.Perform(bind.code);
                    }
                }
            }
        }
        
        if (CheckCameraCommands())
            cameraCooldown = 1 / 10f;
    }

    bool CheckCameraCommands()
    {
        bool pressed = false;

        // zoom
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            Camera.main.orthographicSize = 12.5f;
            pressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Equals))
        {
            Camera.main.orthographicSize = 6.25f;
            pressed = true;
        }

        // movement
        int h = 0;
        int v = 0;

        if (Input.GetAxisRaw("Horizontal") > 0)
            h = 1;
        else if (Input.GetAxisRaw("Horizontal") < 0)
            h = -1;

        if (Input.GetAxisRaw("Vertical") > 0)
            v = 1;
        else if (Input.GetAxisRaw("Vertical") < 0)
            v = -1;

        if (h != 0 || v != 0)
            pressed = true;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            h *= 5;
            v *= 5;
        }

        Vector3 pos = Camera.main.transform.position;
        pos.x += h;
        pos.y += v;
        Camera.main.transform.position = pos;

        return pressed;
    }
}
