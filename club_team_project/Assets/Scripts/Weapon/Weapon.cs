using UnityEditor;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Entity_Stat entitystat;
    private IWeaponStrategy currentStrategy;

    [Header("Weapon State")]
    [SerializeField] private int currentBullets;
    [SerializeField] private bool isReloading;
    [SerializeField] private bool isAttackCooldown;

    private void Awake()
    {
        entitystat = GetComponent<Entity_Stat>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1")) // '공격' 버튼 누름
        {
            currentStrategy?.OnAttackInput();
        }
        if (Input.GetButtonUp("Fire1")) // '공격' 버튼 뗌
        {
           // currentStrategy?.OnAttackInputReleased();
        }
    }

    public void EquipNewWeapon(WeaponStatSO stats, IWeaponStrategy strategy)
    {
        entitystat.EquipNewWeapon(stats);

        currentStrategy = strategy;
        currentStrategy.Initialize(this, entitystat);

        currentBullets = (int)entitystat.GetMaxBullets();
        isReloading = false;
        isAttackCooldown = false;
        StopAllCoroutines();

        Debug.Log($"{stats.weaponType} 장착! 전략: {strategy.GetType().Name}");
    }
}