using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasePrimaryAttack", menuName = "JuliosFarm/Abilities/BasePrimaryAttack")]
public class BasePrimaryAttack : AttackAbility
{
    public override void StartAbility(AbilityCharacter character)
    {
        //conectamos con StartAbility de AttackAbility
        base.StartAbility(character);
        
    }

    public override void OnReceiveAnimationEvent(AbilityCharacter character)
    {
        

        //Vector3 rayOrigin = character.transform.position + new Vector3(0f, 0.5f, 0f);

        //RaycastHit[] sphereCastHitInfo = new RaycastHit[10];
        //Physics.SphereCastNonAlloc(rayOrigin, attackRadius, character.transform.forward, sphereCastHitInfo, attackRange, attackLayerMask);
        //if (sphereCastHitInfo.Length > 0)
        //{
        //    for (int i = 0; i < sphereCastHitInfo.Length; i++)
        //    {
        //        if (sphereCastHitInfo[i].collider != null)
        //        {
        //            IDamageable damageableObject = sphereCastHitInfo[i].collider.GetComponent<IDamageable>();
        //            if (damageableObject != null)
        //            {
        //                Debug.DrawRay(sphereCastHitInfo[i].collider.transform.position, Vector3.up, Color.red, 2f);
        //                damageableObject.TakeDamage(totalDamageAmount, damageEmiterType);
        //            }
        //        }
        //    }
        //}
    }
}
