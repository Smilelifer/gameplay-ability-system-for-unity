using System;
using GAS.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GAS.Runtime
{
    /// <summary>
    /// 授予能力的激活策略
    /// </summary>
    public enum GrantedAbilityActivationPolicy
    {
        None,       // 不激活, 等待用户调用ASC激活
        WhenAdded,  // 能力添加时激活（GE添加时激活）
        SyncWithEffect, // 同步GE激活时激活
    }
    
    /// <summary>
    /// 授予能力的取消激活策略
    /// </summary>
    public enum GrantedAbilityDeactivationPolicy
    {
        None,       // 无相关取消激活逻辑, 需要用户调用ASC取消激活
        SyncWithEffect, // 同步GE，GE失活时取消激活
    }
    
    /// <summary>
    /// 授予能力的移除策略
    /// </summary>
    public enum GrantedAbilityRemovePolicy
    {
        None,       // 不移除
        SyncWithEffect, // 同步GE，GE移除时移除
        WhenEnd,    // 能力结束时自己移除
        WhenCancel, // 能力取消时自己移除
        WhenCancelOrEnd, // 能力结束或取消时自己移除
    }
    
    [Serializable]
    public struct GrantedAbilityConfig
    {
        [LabelWidth(100)]
        [LabelText(GASTextDefine.LABEL_GRANT_ABILITY)]
        [AssetSelector]
        public AbilityAsset AbilityAsset;
        
        [LabelWidth(100)]
        [LabelText(GASTextDefine.LABEL_GRANT_ABILITY_LEVEL)]
        public int AbilityLevel;
        
        [LabelWidth(100)]
        [LabelText(GASTextDefine.LABEL_GRANT_ABILITY_ACTIVATION_POLICY)]
        [Tooltip(GASTextDefine.TIP_GRANT_ABILITY_ACTIVATION_POLICY)]
        public GrantedAbilityActivationPolicy ActivationPolicy;    
        
        [LabelWidth(100)]
        [LabelText(GASTextDefine.LABEL_GRANT_ABILITY_DEACTIVATION_POLICY)]
        [Tooltip(GASTextDefine.TIP_GRANT_ABILITY_DEACTIVATION_POLICY)]
        public GrantedAbilityDeactivationPolicy DeactivationPolicy;       
        
        [LabelWidth(100)]
        [LabelText(GASTextDefine.LABEL_GRANT_ABILITY_REMOVE_POLICY)]
        [Tooltip(GASTextDefine.TIP_GRANT_ABILITY_REMOVE_POLICY)]
        public GrantedAbilityRemovePolicy RemovePolicy;
    }
    
    public class GrantedAbilityFromEffect
    {
        public readonly AbstractAbility Ability;
        public readonly int AbilityLevel;
        public readonly GrantedAbilityActivationPolicy ActivationPolicy;
        public readonly GrantedAbilityDeactivationPolicy DeactivationPolicy;
        public readonly GrantedAbilityRemovePolicy RemovePolicy;
   
        public GrantedAbilityFromEffect(GrantedAbilityConfig config)
        {
            Ability = Activator.CreateInstance(config.AbilityAsset.AbilityType(), args: config.AbilityAsset) as AbstractAbility;
            AbilityLevel = config.AbilityLevel;
            ActivationPolicy = config.ActivationPolicy;
            DeactivationPolicy = config.DeactivationPolicy;
            RemovePolicy = config.RemovePolicy;
        }

        public GrantedAbilityFromEffect(
            AbstractAbility ability,
            int abilityLevel,
            GrantedAbilityActivationPolicy activationPolicy,
            GrantedAbilityDeactivationPolicy deactivationPolicy,
            GrantedAbilityRemovePolicy removePolicy)
        {
            Ability = ability;
            AbilityLevel = abilityLevel;
            ActivationPolicy = activationPolicy;
            DeactivationPolicy = deactivationPolicy;
            RemovePolicy = removePolicy;
        }

        public GrantedAbilitySpecFromEffect CreateSpec(GameplayEffectSpec sourceEffectSpec)
        {
            var grantedAbility = new GrantedAbilitySpecFromEffect(this,sourceEffectSpec);
            return grantedAbility;
        } 
    }

    public class GrantedAbilitySpecFromEffect
    {
        public readonly GrantedAbilityFromEffect GrantedAbility;
        public readonly GameplayEffectSpec SourceEffectSpec;
        public readonly AbilitySystemComponent Owner;

        public readonly string AbilityName;
        public int AbilityLevel => GrantedAbility.AbilityLevel;
        public GrantedAbilityActivationPolicy ActivationPolicy => GrantedAbility.ActivationPolicy;
        public GrantedAbilityDeactivationPolicy DeactivationPolicy => GrantedAbility.DeactivationPolicy;
        public GrantedAbilityRemovePolicy RemovePolicy => GrantedAbility.RemovePolicy;
        public AbilitySpec AbilitySpec => Owner.AbilityContainer.AbilitySpecs()[AbilityName];

        public GrantedAbilitySpecFromEffect(GrantedAbilityFromEffect grantedAbility,
            GameplayEffectSpec sourceEffectSpec)
        {
            GrantedAbility = grantedAbility;
            SourceEffectSpec = sourceEffectSpec;
            AbilityName = GrantedAbility.Ability.Name;
            Owner = SourceEffectSpec.Owner;
            if (Owner.AbilityContainer.HasAbility(AbilityName))
            {
                Debug.LogError($"GrantedAbilitySpecFromEffect: {Owner.name} already has ability {AbilityName}");
            }
            Owner.GrantAbility(GrantedAbility.Ability);
            AbilitySpec.SetLevel(AbilityLevel);
            
            // 是否添加时激活
            if(ActivationPolicy == GrantedAbilityActivationPolicy.WhenAdded)
            {
                Owner.TryActivateAbility(AbilityName);
            }
            
            switch (RemovePolicy)
            {
                case GrantedAbilityRemovePolicy.WhenEnd:
                    AbilitySpec.RegisterEndAbility(RemoveSelf);
                    break;
                case GrantedAbilityRemovePolicy.WhenCancel:
                    AbilitySpec.RegisterCancelAbility(RemoveSelf);
                    break;
                case GrantedAbilityRemovePolicy.WhenCancelOrEnd:
                    AbilitySpec.RegisterEndAbility(RemoveSelf);
                    AbilitySpec.RegisterCancelAbility(RemoveSelf);
                    break;
            }
                
        }
        
        private void RemoveSelf()
        {
            Owner.RemoveAbility(AbilityName);
        }
    }
}