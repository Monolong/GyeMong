using playerCharacter;
using Unity.Mathematics;
using UnityEngine;

namespace Creature.Player.Component.Collider
{
    public class AttackCollider : MonoBehaviour
    {
        public float attackDamage;
        private PlayerSoundController _soundController;
        private EventObject _eventObject;
        private ParticleSystem _particleSystem;
        private ParticleSystem.ShapeModule _shape;
        private void Start()
        {
            _particleSystem = GetComponentInChildren<ParticleSystem>();
            _shape = _particleSystem.shape;//.GetComponent<ParticleSystem.ShapeModule>();
            _eventObject = GetComponent<EventObject>();
            var player = PlayerCharacter.Instance;
            attackDamage = player.stat.AttackPower;
        }

        public void Init(PlayerSoundController soundController)
        {
            _soundController = soundController;
        }

        public void SetDamage(float damage)
        {
            attackDamage = damage;
        }
  
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
        
            var creature = collision.GetComponent<Creature>();
            if (creature != null)
            {
                SetParticleSystemTexture(collision);

                _eventObject.Trigger();
                _soundController.Trigger(PlayerSoundType.SWORD_ATTACK);
                creature.OnAttacked(attackDamage);
                PlayerCharacter.Instance.AttackIncreaseGauge();
            } else
            {
                IAttackable[] attackableObjects = collision.GetComponents<IAttackable>();
                if (attackableObjects.Length != 0)
                {
                    SetParticleSystemTexture(collision);
                    _eventObject.Trigger();
                    _soundController.Trigger(PlayerSoundType.SWORD_ATTACK);
                    foreach (IAttackable @object in attackableObjects)
                    {
                        @object.OnAttacked(attackDamage);
                    }
                }
            }
        
        }

        private void SetParticleSystemTexture(Collider2D collision)
        {
            try
            {
                Sprite sprite;
                try
                {
                    sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
                }
                catch (MissingComponentException)
                {
                    sprite = collision.GetComponentInChildren<SpriteRenderer>().sprite;
                }
                _shape.texture = sprite.texture;
            } catch
            {
                Debug.Log("No SpriteRenderer found");
            }
        }
    }
}
