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
    private readonly int CanCancelHash = Animator.StringToHash("CanCancel");
    private readonly int ComboHash = Animator.StringToHash("Combo");

    public float movementSpeed;
    public float rotationSpeed;
    public Camera cam;
    public GameObject character;
    private Movement movementComponent;
    public CharacterData characterData;

    private float lookDirection;
    
    private Animator characterAnimator;
    public AnimatorOverrideController animatorOverride;

    [SerializeField] private float forwardMagnitude;

    private Vector2 movementDirection;

    private void Awake()
    {
        characterAnimator = character.GetComponent<Animator>();
        characterAnimator.runtimeAnimatorController = animatorOverride;
        movementComponent = character.GetComponent<Movement>();
    }

    // Used to handle physics
    void FixedUpdate()
    {
        if ((movementDirection.y != 0.0f || movementDirection.x != 0.0f) 
            && (!characterAnimator.GetBool(IsAttackingHash) 
            || characterAnimator.GetBool(CanCancelHash)))
        {
            CancelAttack();

            Vector3 forwardForce = character.transform.forward * movementDirection.y;
            Vector3 rightForce = character.transform.right * movementDirection.x;
            movementComponent.Move(forwardForce + rightForce);

            Turn();
        }
    }

    private void CancelAttack()
    {
        if (characterAnimator.GetBool(CanCancelHash))
        {
            characterAnimator.SetBool(IsAttackingHash, false);
            characterAnimator.SetBool(CanCancelHash, false);
            characterAnimator.SetInteger(ComboHash, 0);
        }
    }

    public void OnMovement(InputValue vector2)
    {
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

    }
}
