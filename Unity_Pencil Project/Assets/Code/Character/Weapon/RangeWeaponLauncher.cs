using System;
using Code.Enemy;
using Code.StaticData.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Character
{
    public class RangeWeaponLauncher : MonoBehaviour, IRangeWeapon
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _arcRange = 1;
        
        private const float MaxProjectileDistance = 1000f;
        private Camera _camera;
        private Vector3 _destination;
        private GameObject _projectile;
        private float _projectileSpeed;
        private float _projectileDamage;


        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R) && _projectile != null)
            {
                ShootProjectile();
            }
        }

        public void ShootProjectile()
        {
            ShootDirectionRay();
            InitializeProjectile();
        }

        private void ShootDirectionRay()
        {
            Ray ray = new Ray(_firePoint.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                _destination = hit.point;
            }
            else
            {
                _destination = ray.GetPoint(MaxProjectileDistance);
            }
        }

        private void InitializeProjectile()
        {
            GameObject projectile = CreateProjectile(_projectile);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.Init(_projectileDamage);
            SetProjectileDirection(projectile);
            ProjectileFlyWithCurveTrajectory(projectile);
        }

        private GameObject CreateProjectile(GameObject projectile)
        {
            return Instantiate(projectile, _firePoint.position, Quaternion.LookRotation(transform.forward));
        }

        private void ProjectileFlyWithCurveTrajectory(GameObject projectile)
        {
            iTween.PunchPosition(projectile, RandomizeFireArc(), Random.Range(0.5f, 2f));
        }


        private Vector3 RandomizeFireArc()
        {
            return new Vector3(Random.Range(-_arcRange, _arcRange),Random.Range(-_arcRange, _arcRange),0);
        }

        private void SetProjectileDirection(GameObject projectile)
        {
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            Vector3 direction = (_destination - _firePoint.position).normalized * _projectileSpeed;
            projectileRb.velocity = direction;
            
        }


        public void LoadWeapon(RangeWeaponSO weapon)
        {
            _projectile = weapon.Projectile;
            _projectileSpeed = weapon.ProjectileSpeed;
            _projectileDamage = weapon.Damage;
        }
        
    }
}