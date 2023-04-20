using System;
using Code.Data;
using Code.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class PlayerSaver : MonoBehaviour , ISavedProgress
    {

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (CurrentLevel() == playerProgress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = playerProgress.WorldData.PositionOnLevel.Position;

                if (playerProgress.WorldData.PositionOnLevel.Position != null)
                {
                    Warp(savedPosition);
                }
                
            }
            
        }

        private void Warp(Vector3Data to)
        {
            transform.position = to.AsUnityVector3(); //.AddY(1f);
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            playerProgress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVector3Data()) ;
        }

        private static string CurrentLevel()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}