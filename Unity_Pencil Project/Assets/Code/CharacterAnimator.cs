using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Logic;
using KinematicCharacterController.Examples;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
   [SerializeField]
   private Animator _animator;

   private static readonly int Jump = Animator.StringToHash("Jump");
   private static readonly int IsWalking = Animator.StringToHash("IsWalking");
   private static readonly int Attack = Animator.StringToHash("Attack");
   private static readonly int AttackPower = Animator.StringToHash("AttackPower");
   private static readonly int Die = Animator.StringToHash("Die");
   
   private readonly int _idleStateHash = Animator.StringToHash("Idle");
   private readonly int _attackStateHash = Animator.StringToHash("Attack");
   private readonly int _walkingStateHash = Animator.StringToHash("IsWalking");
   private readonly int _deathStateHash = Animator.StringToHash("Die");
   
   public event Action<AnimatorState> StateEntered;
   public event Action<AnimatorState> StateExited;

   public AnimatorState State { get; private set; }
   public bool IsAttacking => State == AnimatorState.Attack;


   private void Update()
   {
      if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
      {
         PlayWalkAnimation(true);
      }
      else
      {
         PlayWalkAnimation(false);
      }

      if (Input.GetKeyDown(KeyCode.Space))
      {
         PlayJumpAnimation();
      }

      
   }

   public void SetInputs(ref PlayerCharacterInputs inputs)
   {
      
   }

   public void PlayPowerAttackAnimation()
   {
      _animator.SetTrigger(AttackPower);
   }
   public void PlayAttackAnimation()
   {
      _animator.SetTrigger(Attack);
   }

   public void PlayJumpAnimation()
   {
      _animator.SetTrigger(Jump);
   }

   public void PlayWalkAnimation(bool isWalking)
   {
      _animator.SetBool(IsWalking, isWalking);
   }


   public void PlayDeathAnimation()
   {
      _animator.SetTrigger(Die);
   }
   public void EnteredState(int stateHash)
   {
      State = StateFor(stateHash);
      StateEntered?.Invoke(State);
   }

   public void ExitedState(int stateHash)
   {
      StateExited?.Invoke(StateFor(stateHash));
   }
   
   private AnimatorState StateFor(int stateHash)
   {
      AnimatorState state;
      if (stateHash == _idleStateHash)
      {
         state = AnimatorState.Idle;
      }
      else if (stateHash == _attackStateHash)
      {
         state = AnimatorState.Attack;
      }
      else if (stateHash == _walkingStateHash)
      {
         state = AnimatorState.Walking;
      }
      else if (stateHash == _deathStateHash)
      {
         state = AnimatorState.Died;
      }
      else
      {
         state = AnimatorState.Unknown;
      }

      return state;
   }
}
