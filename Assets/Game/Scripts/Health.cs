using System;
using UnityEngine;

namespace Game.Scripts
{
    [Serializable]
    public class Health
    {
        public event Action OnDeath;

        [SerializeField]
        private int _max;

        [SerializeField]
        private int _current;

        public bool IsAlive => _current > 0;
        
        public int CurrentHealth => _current;


        public void TakeDamage(int damage)
        {
            if (!IsAlive)
            {
                return;
            }

            _current -= damage;

            if (_current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log($"{GetType().Name}.Die:");

            OnDeath?.Invoke();
        }
    }
}