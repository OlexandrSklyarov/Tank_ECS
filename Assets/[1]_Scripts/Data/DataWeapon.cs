using UnityEngine;

namespace SA.Tanks.Data
{
    [CreateAssetMenu(fileName = "DataWeapon", menuName = "Data/DataWeapon")]
    public class DataWeapon : ScriptableObject
    {
        #region Properties

        public GameObject Prefab => prefab;
        public float Speed => speed;
        public int Damage => damage;
        public float ReloadTime => reloadTime;

        #endregion


        #region Var

        [SerializeField] GameObject prefab;
        [SerializeField] [Range(1f, 500f)] float speed;
        [SerializeField] [Range(1, 1000)] int damage;
        [SerializeReference] [Range(0.5f, 5f)] float reloadTime;

        #endregion

    }
}
