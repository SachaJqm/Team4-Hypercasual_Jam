using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private bool canMove;
    private void Awake()
    {
        GameManager.OnGameStart += OnGameStart;
        GameManager.OnWin += OnGameWinOrLoose;
        GameManager.OnLoose += OnGameWinOrLoose;
        canMove = false;
    }

    void OnGameStart()
    {
        canMove = true;
    }

    void OnGameWinOrLoose()
    {
        canMove = false;
    }
    
    void Update()
    {
        if (!canMove)
            return;
        
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var dir = new Vector3(horizontal, 0, vertical);
        dir *= speed * 0.001f;

        transform.Translate(dir);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Objectif"))
        {
            GameManager.OnWin.Invoke();
        }
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
    }
}
