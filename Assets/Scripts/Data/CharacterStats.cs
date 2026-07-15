using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Scriptable Objects/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    [Header("Health")]
    public float maxHealth = 100f;

    [Header("Health")]
    public float damage = 25f;

    [Header("Movement")]
    public float walkSpeed = 4f;
    public float runSpeed = 10f;
    public float jumpForce = 2f;

    [Header("Crouched")]
    public float standHeight = 2f;
    public float crouchHeight = 1f;

    [Header("Enemy sense)")]
    public float viewDistance = 20f;
    public float rotationSpeed = 5f;
}
