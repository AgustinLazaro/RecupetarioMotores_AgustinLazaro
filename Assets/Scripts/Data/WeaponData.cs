using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Estadísticas Combate")]
    public float damage = 25f;
    public float range = 100f;
    public GameObject bulletPrefab;

    [Header("Velocidades Animaciones precarias")]
    public float speedAim = 8f;
    public float recoilSpeed = 20f;
    public float returnSpeed = 5f;
}
