using UnityEngine;

public class WShotgun : WeaponControllerBase
{
    // [추가] 우리가 업그레이드한 '공격 도구' 참조
    private Entity_TakeDamage entityAttack;

    public override void Initialize(Weapon weapon, Entity_Stat stats)
    {
        base.Initialize(weapon, stats);

        // [추가] '공격 도구'를 미리 찾아둡니다.
        entityAttack = weapon.GetComponent<Entity_TakeDamage>();
        if (entityAttack == null)
        {
            Debug.LogError("샷건을 사용하려면 Entity_Attack 컴포넌트가 필요합니다!");
        }
    }

    public override void OnAttackInput()
    {
        base.OnAttackInput(); // weapon.RequestAttack() 호출
    }

    public override void PerformAttack()
    {
        // 1. 총구 이펙트 생성 (총알은 생성 안 함)
        GameObject muzzleflash = weapon.muzzleflashPrefab;
        if (muzzleflash != null)
        {
            Object.Instantiate(muzzleflash, weapon.firePoint.position, weapon.firePoint.rotation);
        }

        if (entityAttack == null) return; // 도구가 없으면 중단

        // 2. 샷건 스탯 값을 가져옵니다.
        float range = stats.GetShotgunRadius(); // 샷건 '거리'
        float angle = stats.GetShotgunRange();  // 샷건 '각도'
        float damage = stats.GetDamage();

        // 3. '공격 도구'에게 스탯을 넘겨주며 실행을 '요청'합니다.
        entityAttack.PerformFanAttack(range, angle, damage);
    }
}
