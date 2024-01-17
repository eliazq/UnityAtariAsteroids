using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public float VerticalInput()
    {
        return Mathf.Clamp01(Input.GetAxis("Vertical"));
    }
    public float HorizontalInput()
    {
        return Input.GetAxis("Horizontal");
    }

    public bool ShootInput()
    {
        return Input.GetButtonDown("Jump");
    }
}
