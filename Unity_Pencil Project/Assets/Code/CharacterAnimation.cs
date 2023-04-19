using System;
using System.Collections;
using System.Collections.Generic;
using KinematicCharacterController.Examples;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
   [SerializeField]
   private Animator _animator;

   private static readonly int Jump = Animator.StringToHash("Jump");
   private static readonly int IsWalking = Animator.StringToHash("IsWalking");
   private static readonly int Attack = Animator.StringToHash("Attack");
   private static readonly int AttackPower = Animator.StringToHash("AttackPower");


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

      if (Input.GetMouseButtonDown(0))
      {
         PlayAttackAnimation();
      }
      
      if (Input.GetMouseButtonDown(1))
      {
         PlayAttackPowerAnimation();
      }
   }

   public void SetInputs(ref PlayerCharacterInputs inputs)
   {
      
   }

   private void PlayAttackPowerAnimation()
   {
      _animator.SetTrigger(AttackPower);
   }
   private void PlayAttackAnimation()
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

   
}
