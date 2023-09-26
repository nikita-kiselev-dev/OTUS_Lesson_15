using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Game.Scripts
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private Weapon _weapon;

        [SerializeField]
        private Health _health;

        [SerializeField]
        private float _speed = 2f;

        [SerializeField]
        private Transform _anchor;

        [SerializeField] private CharacterUI characterUI;

        [SerializeField] private PlaySound playSound;
        
        [SerializeField] private TextMeshProUGUI unitName;

        public bool IsAlive => _health.IsAlive;

        public Transform Anchor => _anchor;


        private void Start()
        {
            _health.OnDeath += OnHealthDeath;
            unitName.text = gameObject.name;
            characterUI.UpdateHealthBar(_health.CurrentHealth);
        }

        private void OnDestroy()
        {
            _health.OnDeath -= OnHealthDeath;
        }

        private void OnHealthDeath()
        {
            Debug.Log($"{GetType().Name}.OnHealthDeath:");

            _animator.SetTrigger("Die");
            playSound.PlaySoundEffect("Dead");
        }


        public IEnumerator Attack(Character attackedCharacter)
        {
            Debug.Log($"{GetType().Name}.Attack: gameObject.name = {gameObject.name} => {attackedCharacter.gameObject.name}");

            var originalPosition = transform.position;
            if (_weapon.Type == WeaponType.Bat)
            {
                yield return MoveTo(attackedCharacter.Anchor.position);
                playSound.PlaySoundEffect("Bat");
            }
            else if (_weapon.Type == WeaponType.Gun)
            {
                playSound.PlaySoundEffect("Shoot");
            }

            var animationName = WeaponHelpers.GetAnimationNameFor(_weapon.Type);
            _animator.SetTrigger(animationName);

            yield return new WaitForSeconds(2f);

            attackedCharacter.TakeDamage(_weapon.Damage);

            if (_weapon.Type == WeaponType.Bat)
            {
                yield return MoveTo(originalPosition);
            }
        }

        private IEnumerator MoveTo(Vector3 position)
        {
            _animator.SetFloat("Speed", _speed);
            playSound.PlaySoundEffectLoop("Move");
            var step = _speed * Time.deltaTime;
            float distance;
            do
            {
                distance = Vector3.Distance(transform.position, position);
                transform.position = Vector3.MoveTowards(transform.position, position, step);
                yield return null;
            } while (distance > 0.5f);

            _animator.SetFloat("Speed", 0f);
        }


        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
            characterUI.UpdateHealthBar(_health.CurrentHealth);
            playSound.PlaySoundEffect("Damaged");
        }
    }
}