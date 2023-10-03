using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransform : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _playerVisual;
    private Rigidbody2D rb;
    private Animator anim;
    private bool lookRight = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float x = _joystick.Horizontal;
        float y = _joystick.Vertical;

        rb.velocity = new Vector2(x, y) * _speed;
        if (x + y != 0)
        {
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
        if (x < 0 && lookRight)
        {
            Flip();
        }
        else if (x > 0 && !lookRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        lookRight = !lookRight;

        Vector3 scale = _playerVisual.localScale;
        scale.x *= -1;
        _playerVisual.localScale = scale;
    }
}