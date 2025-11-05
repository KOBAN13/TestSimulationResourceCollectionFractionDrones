using UnityEngine;
using UnityEngine.Pool;

namespace Pool
{
    public class GenericObjectPool<T> : IGenericObjectPool<T> where T : Component
    {
        private T _comboEffectPrefab;
        private ObjectPool<T> _effectPool;

        public void Initialize(T effect)
        {
            _comboEffectPrefab = effect;
            
            _effectPool = new ObjectPool<T>(
                CreateComboEffect, 
                OnGetComboEffect, 
                OnReleaseComboEffect, 
                OnComboEffectDispose, 
                true, 
                3, 
                6
            );
        }
        private static void OnComboEffectDispose(T obj)
        {
            Object.Destroy(obj.gameObject);
        }
        
        private static void OnReleaseComboEffect(T obj)
        {
            obj.gameObject.SetActive(false);
        }
        
        private static void OnGetComboEffect(T obj)
        {
            obj.gameObject.SetActive(true);
        }
            
        private T CreateComboEffect()
        {
            return Object.Instantiate(_comboEffectPrefab);
        }
        
        public T GetObject()
        {
            var comboEffectMover = _effectPool.Get();
            return comboEffectMover;
        }

        public void ReturnObject(T comboEffectMover)
        {
            _effectPool.Release(comboEffectMover);
        }
    }
}