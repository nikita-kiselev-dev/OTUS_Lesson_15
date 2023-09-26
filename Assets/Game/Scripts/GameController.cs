using System.Collections;
using System.Linq;
using UnityEngine;

namespace Game.Scripts
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private Character[] _players;

        [SerializeField]
        private Character[] _enemies;

        [SerializeField]
        private Weapon _sniperRifle;

        [SerializeField] private GameMenu gameMenu;

        [SerializeField] private PlaySound _playSound;

        private Queue _turns = new Queue();

        private Character _selectedTarget;
        private bool _isTargetSelectionConfirmed;
        

        private void Start()
        {
            foreach (var player in _players)
            {
                _turns.Enqueue(player);
            }

            foreach (var enemy in _enemies)
            {
                _turns.Enqueue(enemy);
            }

            StartCoroutine(LevelLoop());
        }

        private IEnumerator LevelLoop()
        {
            foreach (var turn in GetTurns())
            {
                if (turn is Character character)
                {
                    if (character.IsAlive)
                    {
                        if (_players.Contains(character))
                        {
                            _isTargetSelectionConfirmed = false;

                            SelectRandomEnemy();

                            yield return new WaitUntil(() => _isTargetSelectionConfirmed);

                            yield return character.Attack(_selectedTarget);
                        }
                        else
                        {
                            var opponent = GetTarget(_players);

                            yield return character.Attack(opponent);
                        }

                        yield return new WaitForSeconds(2f);

                        _turns.Enqueue(character);
                    }
                }
                else if (turn is Weapon weapon)
                {
                    var enemy = GetTarget(_enemies);
                    if (enemy != null)
                    {
                        enemy.TakeDamage(weapon.Damage);
                    }

                    yield return new WaitForSeconds(2f);
                }

                if (!AnyCharactersAlive(_enemies))
                {
                    GameWon();
                    yield break;
                }

                if (!AnyCharactersAlive(_players))
                {
                    GameLost();
                    yield break;
                }
            }
        }

        private void GameWon()
        {
            gameMenu.ShowGameResult("win");
            _playSound.PlaySoundEffect("Win");
            Debug.Log($"{GetType().Name}.GameWon:");
        }

        private void GameLost()
        {
            gameMenu.ShowGameResult("lose");
            _playSound.PlaySoundEffect("Lose");
            Debug.Log($"{GetType().Name}.GameLost:");
        }

        private IEnumerable GetTurns()
        {
            while (true)
            {
                var attacker = _turns.Dequeue();
                yield return attacker;
            }
        }

        private bool AnyCharactersAlive(Character[] characters)
        {
            return characters.Any(character => character.IsAlive);
        }

        private bool IsAlive(Character character)
        {
            return character.IsAlive;
        }

        private Character GetTarget(Character[] characters)
        {
            return characters.First(character => character.IsAlive);
        }

        public void CallSniper()
        {
            if (_turns.Contains(_sniperRifle))
            {
                return;
            }

            _turns.Enqueue(_sniperRifle);
        }

        public void SelectRandomEnemy()
        {
            var nextEnemies = _enemies.Where(character =>
            {
                return character.IsAlive && character != _selectedTarget;
            });

            if (nextEnemies.Count() > 0)
            {
                _selectedTarget = nextEnemies.ToArray()[Random.Range(0, nextEnemies.Count())];
            }
        }
        
        public void ConfirmTargetSelection()
        {
            _isTargetSelectionConfirmed = true;
        }
        
    }
}