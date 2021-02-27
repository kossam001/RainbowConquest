using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerController : Character
{
    // Animator hashes
    private readonly int MoveXHash = Animator.StringToHash("MoveX");
    private readonly int MoveZHash = Animator.StringToHash("MoveZ");
    private readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");

    public float movementSpeed;
    public float rotationSpeed;
    public Camera cam;
    public GameObject character;
    private Movement movementComponent;
    private Attack attackComponent;
    public CharacterData characterData;

    private float lookDirection;
    
    private Animator characterAnimator;

    [SerializeField] private float forwardMagnitude;

    private Vector2 movementDirection;

    private bool isPaused = false;
    private float shootCooldown = 0.0f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        characterAnimator = character.GetComponent<Animator>();
        movementComponent = character.GetComponent<Movement>();
        attackComponent = character.GetComponent<Attack>();
    }

    // Used to handle physics
    void FixedUpdate()
    {
        if ((movementDirection.y != 0.0f || movementDirection.x != 0.0f))
        {
            Vector3 forwardForce = character.transform.forward * movementDirection.y;
            Vector3 rightForce = character.transform.right * movementDirection.x;
            movementComponent.Move(forwardForce + rightForce);

            Turn();
        }

        shootCooldown -= Time.deltaTime;
    }

    public void OnMovement(InputValue vector2)
    {
        if (isPaused) return;

        movementDirection = vector2.Get<Vector2>();

        characterAnimator.SetFloat(MoveXHash, movementDirection.x);
        characterAnimator.SetFloat(MoveZHash, movementDirection.y);
    }

    public override void Turn()
    {
        movementComponent.Turn(cam.transform.rotation);
    }

    public void OnAttack(InputValue button)
    {
        if (isPaused) return;
        if (shootCooldown >= 0.0f) return;

        shootCooldown = 0.5f;

        attackComponent.Shoot();
    }

    public void OnPause()
    {
        if (Time.timeScale == 1.0f)
        {
            UIManager.Instance.TogglePauseMenu();
            isPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 0.0f;
        }
        else
        {
            UIManager.Instance.TogglePauseMenu();
            isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Time.timeScale = 1.0f;
        }
    }
}
