using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    float keyboardCooldown = 0;

	void Update ()
    {
        if (keyboardCooldown > 0)
        {
            keyboardCooldown -= Time.deltaTime;
            return;
        }

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

        Debug.Log(h);

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

        if (pressed)
            keyboardCooldown = 1 / 10f;
    }
}
