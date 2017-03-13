using UnityEngine;

namespace Match3Test
{
    public class GameObjectFactory
    {
        public T CreatedGameObject<T> (GameObject prefab, Transform parent) where T : MonoBehaviour
        {
            var go = GameObject.Instantiate (prefab);
            go.transform.parent = parent;
            return go.GetComponent<T> ();
        }

    }
}