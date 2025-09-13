// Decompiled with JetBrains decompiler
// Type: CombatStance.Utilits
// Assembly: CombatStanceMovement, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 15398906-0FC5-4912-B95F-7B85C0671CB4
// Assembly location: C:\Users\clope\OneDrive\Desktop\CombatStanceMovement.dll

using GTA;
using GTA.Math;
using GTA.Native;
using GTA.NaturalMotion;
using System;

namespace CombatStance
{
    public static class Utilits
    {
        public static int dodgeAction;

        public static bool IsFacingPed(this Ped ped, Ped targetPed, float angular)
        {
            return Function.Call<bool>(Hash.IS_PED_FACING_PED, (InputArgument)(Entity)ped, (InputArgument)(Entity)targetPed, (InputArgument)angular);
        }

        public static bool IsUsingActionMod(this Ped ped)
        {
            return Function.Call<bool>(Hash.IS_PED_USING_ACTION_MODE, (InputArgument)(Entity)ped);
        }

        public static void SetUsingActionMod(this Ped ped, bool value)
        {
            Function.Call(Hash.SET_PED_USING_ACTION_MODE, (InputArgument)(Entity)ped, (InputArgument)value, (InputArgument)(-1), (InputArgument)"DEFAULT_ACTION");
        }

        public static void CollisionCapsule(this Entity entity, float value)
        {
            Function.Call(Hash.SET_PED_CAPSULE, new InputArgument[2]
            {
        (InputArgument) entity,
        (InputArgument) value
            });
        }

        public static void SetPedCanPlayAmbientAnims(this Entity entity, bool value)
        {
            Function.Call(Hash.SET_PED_CAN_PLAY_AMBIENT_ANIMS, new InputArgument[2]
            {
        (InputArgument) entity,
        (InputArgument) value
            });
        }

        public static void SetPedCanPlayAmbientBaseAnims(this Entity entity, bool value)
        {
            Function.Call(Hash.SET_PED_CAN_PLAY_AMBIENT_BASE_ANIMS, new InputArgument[2]
            {
        (InputArgument) entity,
        (InputArgument) value
            });
        }

        public static float GetAnimTime(this Ped ped, string animDict, string animName)
        {
            return Function.Call<float>(Hash.GET_ENTITY_ANIM_CURRENT_TIME, (InputArgument)(Entity)ped, (InputArgument)animDict, (InputArgument)animName);
        }

        public static bool IsAnimPlay(this Ped ped, string animDict, string animName)
        {
            return Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, (InputArgument)(Entity)ped, (InputArgument)animDict, (InputArgument)animName, (InputArgument)3);
        }

        public static bool IsTaskActive(this Ped ped, int taskIndex)
        {
            return Function.Call<bool>(Hash.GET_IS_TASK_ACTIVE, (InputArgument)(Entity)ped, (InputArgument)taskIndex);
        }

        public static void FacePosition(this Ped ped, Vector3 pos)
        {
            ped.Heading = Function.Call<float>(Hash.GET_HEADING_FROM_VECTOR_2D, new InputArgument[2]
            {
        (InputArgument) (pos.X - ped.Position.X),
        (InputArgument) (pos.Y - ped.Position.Y)
            });
        }

        public static void SetDisableAmbientMeleeMove(this Entity entity, bool value)
        {
            Function.Call(Hash.SET_DISABLE_AMBIENT_MELEE_MOVE, new InputArgument[2]
            {
        (InputArgument) entity,
        (InputArgument) value
            });
        }

        public static void ResetPedMovementClipset(this Entity entity, float transitionSpeed = 1.048576E+09f)
        {
            Function.Call(Hash.RESET_PED_MOVEMENT_CLIPSET, new InputArgument[2]
            {
        (InputArgument) entity,
        (InputArgument) transitionSpeed
            });
        }

        public static void ResetPedStrafeClipset(this Entity entity)
        {
            Function.Call(Hash.RESET_PED_STRAFE_CLIPSET, new InputArgument[1]
            {
        (InputArgument) entity
            });
        }

        public static void SetPedMaxMoveBlendRatio(this Entity entity, float speed = 1f)
        {
            Function.Call(Hash.SET_PED_MAX_MOVE_BLEND_RATIO, new InputArgument[2]
            {
        (InputArgument) entity,
        (InputArgument) speed
            });
        }

        public static void SetPedMovementClipset(
          this Entity entity,
          string clipSet,
          float transitionSpeed)
        {
            Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, new InputArgument[3]
            {
        (InputArgument) entity,
        (InputArgument) clipSet,
        (InputArgument) transitionSpeed
            });
        }

        public static void SetPedStrafeClipset(this Entity entity, string clipSet)
        {
            Function.Call(Hash.SET_PED_STRAFE_CLIPSET, new InputArgument[2]
            {
        (InputArgument) entity,
        (InputArgument) clipSet
            });
        }

        public static void SetPedStealthMovement(this Entity entity, bool toggle, string action = "DEFAULT_ACTION")
        {
            Function.Call(Hash.SET_PED_STEALTH_MOVEMENT, new InputArgument[3]
            {
        (InputArgument) entity,
        (InputArgument) toggle,
        (InputArgument) action
            });
        }

        public static void TaskAimGunScripted(this Ped ped, string GunTaskMetadata)
        {
            Function.Call(Hash.TASK_AIM_GUN_SCRIPTED, new InputArgument[4]
            {
        (InputArgument) (Entity) ped,
        (InputArgument) Game.GenerateHash(GunTaskMetadata),
        (InputArgument) 1,
        (InputArgument) 1
            });
        }

        public static bool IsUsingWeaponMicroSMG(this Ped ped)
        {
            WeaponHash hash = ped.Weapons.Current.Hash;
            int num;
            switch (hash)
            {
                case WeaponHash.MicroSMG:
                case WeaponHash.StunGun:
                case WeaponHash.MiniSMG:
                case WeaponHash.MachinePistol:
                    num = 1;
                    break;
                default:
                    num = hash == WeaponHash.CompactGrenadeLauncher ? 1 : 0;
                    break;
            }
            return num != 0;
        }

        public static bool IsUsingWeaponGroup(this Ped ped, WeaponGroup weaponGroup)
        {
            return ped.Weapons.Current.Group == weaponGroup;
        }

        public static bool IsUsingWeapon(this Ped ped, WeaponHash weapon)
        {
            return ped.Weapons.Current.Hash == weapon;
        }

        public static void StartShotBaseNmMessages(Ped ped)
        {
            ped.Euphoria.BalancerCollisionsReaction.Start();
            FallOverWallHelper fallOverWall = ped.Euphoria.FallOverWall;
            fallOverWall.MoveLegs = true;
            fallOverWall.MoveArms = true;
            fallOverWall.BendSpine = true;
            fallOverWall.RollingPotential = 0.3f;
            fallOverWall.RollingBackThr = 0.5f;
            fallOverWall.ForceTimeOut = 2f;
            fallOverWall.MagOfForce = 0.5f;
            fallOverWall.BodyTwist = 0.54f;
            fallOverWall.Start();
            ped.Euphoria.Shot.Start();
            ShotConfigureArmsHelper shotConfigureArms = ped.Euphoria.ShotConfigureArms;
            shotConfigureArms.PointGun = true;
            shotConfigureArms.Start();
            ConfigureBalanceHelper configureBalance = ped.Euphoria.ConfigureBalance;
            configureBalance.FallMult = 80f;
            configureBalance.Update();
        }

        public static void GiveNMMessage(
          this Ped ped,
          Utilits.NMMessage nmMessage,
          int ragdoll_ON = 1,
          int nmMessage_ON = 1,
          int duration = -1)
        {
            int num = 0;
            if (num < ragdoll_ON)
                Function.Call(Hash.SET_PED_TO_RAGDOLL, new InputArgument[7]
                {
          (InputArgument) (Entity) ped,
          (InputArgument) duration,
          (InputArgument) duration,
          (InputArgument) 1,
          (InputArgument) 1,
          (InputArgument) 1,
          (InputArgument) 0
                });
            if (num >= nmMessage_ON)
                return;
            Function.Call(Hash.CREATE_NM_MESSAGE, new InputArgument[2]
            {
        (InputArgument) true,
        (InputArgument) (int) nmMessage
            });
            Function.Call(Hash.GIVE_PED_NM_MESSAGE, new InputArgument[1]
            {
        (InputArgument) (Entity) ped
            });
        }

        public static bool DicValueBelong(this Ped pedcheck, int valuecheck)
        {
            int num = valuecheck;
            bool flag = Prone.meleePedVictum.TryGetValue(pedcheck, out num);
            return Prone.meleePedVictum.ContainsKey(pedcheck) & flag && num == valuecheck;
        }

        public static void Settimera(int value)
        {
            Function.Call(Hash.TIMERA, new InputArgument[1]
            {
        (InputArgument) value
            });
        }

        public static void AnimPostFX(
          string effectName,
          string modifierName,
          int effectduration = 500,
          bool effectlooped = false,
          float modifierstrength = 1f)
        {
            Function.Call(Hash.ANIMPOSTFX_PLAY, new InputArgument[3]
            {
        (InputArgument) effectName,
        (InputArgument) effectduration,
        (InputArgument) effectlooped
            });
            Function.Call(Hash.SET_TIMECYCLE_MODIFIER, new InputArgument[1]
            {
        (InputArgument) modifierName
            });
            for (float strength = 1.2f; (double)strength > 1.0 / 1000.0; strength -= 1f / 1000f)
            {
                Utilits.SetTimecycleModifierStrength(strength);
                if ((double)strength == 1.0 / 1000.0)
                {
                    Function.Call(Hash.ANIMPOSTFX_STOP_ALL, new InputArgument[0]);
                    Function.Call(Hash.CLEAR_TIMECYCLE_MODIFIER, new InputArgument[0]);
                }
            }
        }

        public static void SetFollowPedCamViewMode(Utilits.ViewMode viewMode)
        {
            Function.Call<int>(Hash.SET_FOLLOW_PED_CAM_VIEW_MODE, new InputArgument[1]
            {
        (InputArgument) (Enum) viewMode
            });
        }

        public static bool IsFollowPedCamViewMode(Utilits.ViewMode viewMode)
        {
            return (Utilits.ViewMode)Function.Call<int>(Hash.GET_FOLLOW_PED_CAM_VIEW_MODE, new InputArgument[0]) == viewMode;
        }

        private static float calculateZero(float start, float decrement, int count)
        {
            for (int index = 0; index < count; ++index)
                start -= decrement;
            return start;
        }

        private static void SetTimecycleModifierStrength(float strength, int delay = 10)
        {
            double num = (double)Function.Call<float>(Hash.SET_TIMECYCLE_MODIFIER_STRENGTH, new InputArgument[1]
            {
        (InputArgument) strength
            });
        }

        public static Ped GetPedInFrontOfPlayer()
        {
            return (Entity)World.GetClosestPed(Game.Player.Character.Position + Game.Player.Character.ForwardVector * 1f, 1f) == (Entity)null || World.GetClosestPed(Game.Player.Character.Position + Game.Player.Character.ForwardVector * 1f, 1f).IsPlayer ? World.GetClosestPed(Game.Player.Character.Position + Game.Player.Character.ForwardVector * 1f, 2f) : World.GetClosestPed(Game.Player.Character.Position + Game.Player.Character.ForwardVector * 1f, 1f);
        }

        public static float RandomFloat(float min, float max)
        {
            return (float)(new System.Random().NextDouble() * ((double)max - (double)min)) + min;
        }

        public static void AddDecal(
          Vector3 pos,
          Utilits.DecalTypes decalType,
          float width = 1f,
          float height = 1f,
          float rCoef = 0.1f,
          float gCoef = 0.1f,
          float bCoef = 0.1f,
          float opacity = 1f,
          float timeout = 20f)
        {
            Function.Call<int>(Hash.ADD_DECAL, (InputArgument)(int)decalType, (InputArgument)pos.X, (InputArgument)pos.Y, (InputArgument)World.GetGroundHeight(pos), (InputArgument)0, (InputArgument)0, (InputArgument)(-1.0), (InputArgument)0, (InputArgument)1.0, (InputArgument)0, (InputArgument)width, (InputArgument)height, (InputArgument)rCoef, (InputArgument)gCoef, (InputArgument)bCoef, (InputArgument)opacity, (InputArgument)timeout, (InputArgument)0, (InputArgument)0, (InputArgument)0);
        }

        private static float GetGroundZ(Vector3 pos)
        {
            OutputArgument outputArgument = new OutputArgument();
            Function.Call<bool>(Hash.GET_GROUND_Z_FOR_3D_COORD, (InputArgument)pos.X, (InputArgument)pos.Y, (InputArgument)pos.Z, (InputArgument)outputArgument, (InputArgument)false);
            return outputArgument.GetResult<float>();
        }

        public static void CreateParticleFX(
          string nameAsset,
          string particleFx,
          Entity entity,
          int BoneIndex,
          float scaleFx = 1f,
          bool boneFX = true)
        {
            Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, new InputArgument[1]
            {
        (InputArgument) nameAsset
            });
            Function.Call(Hash.USE_PARTICLE_FX_ASSET, new InputArgument[1]
            {
        (InputArgument) nameAsset
            });
            if (boneFX)
                Function.Call(Hash.START_PARTICLE_FX_NON_LOOPED_ON_PED_BONE, new InputArgument[13]
                {
          (InputArgument) particleFx,
          (InputArgument) entity,
          (InputArgument) 0.0,
          (InputArgument) 0.0,
          (InputArgument) 0.0,
          (InputArgument) 0.0,
          (InputArgument) 0.0,
          (InputArgument) 0.0,
          (InputArgument) BoneIndex,
          (InputArgument) scaleFx,
          (InputArgument) false,
          (InputArgument) false,
          (InputArgument) false
                });
            if (boneFX)
                return;
            Function.Call(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, new InputArgument[11]
            {
        (InputArgument) particleFx,
        (InputArgument) entity,
        (InputArgument) 0.0,
        (InputArgument) 0.0,
        (InputArgument) 0.0,
        (InputArgument) 0.0,
        (InputArgument) 0.0,
        (InputArgument) scaleFx,
        (InputArgument) false,
        (InputArgument) false,
        (InputArgument) false
            });
        }

        public static void MelleWeaponAttack(
          Ped ped,
          string nameAsset,
          string particleFx,
          string groundnameAsset,
          string groundparticleFx,
          Utilits.MeleeAttackType attackType,
          float hitdistance = 1f,
          float force = 1f,
          int damage = 10,
          float ScaleFx = 1f,
          float groundScaleFx = 1f,
          float hitdistanceGround = 1.5f)
        {
            bool flag1 = ped.IsGettingUp || ped.IsProne;
            Vector3 offsetPosition1 = Game.Player.Character.Bones[Bone.SkelRightHand].GetOffsetPosition(new Vector3(0.0f, 0.0f, 0.0f));
            Vector3 offsetPosition2 = ped.Bones[Bone.SkelRightFoot].GetOffsetPosition(new Vector3(0.0f, 0.0f, 0.0f));
            Vector3 offsetPosition3 = ped.Bones[Bone.SkelLeftFoot].GetOffsetPosition(new Vector3(0.0f, 0.0f, 0.0f));
            int num1 = Function.Call<int>(Hash.FLOOR, (InputArgument)(offsetPosition2.DistanceTo(offsetPosition1) * 1000f));
            int num2 = Function.Call<int>(Hash.FLOOR, (InputArgument)(offsetPosition3.DistanceTo(offsetPosition1) * 1000f));
            bool flag2 = num1 < num2;
            bool flag3 = num1 > num2;
            bool flag4 = num1 == num2;
            Vector3 position;
            int num3;
            if ((double)ped.HeightAboveGround < 0.5 && (ped.IsGettingUp || ped.IsProne) && !ped.GetConfigFlag(PedConfigFlagToggles.WasStanding))
            {
                position = Game.Player.Character.Position;
                num3 = (double)position.DistanceTo(ped.Position) < (double)hitdistanceGround ? 1 : 0;
            }
            else
                num3 = 0;
            if (num3 != 0)
            {
                if (attackType == Utilits.MeleeAttackType.Light)
                {
                    Utilits.CreateParticleFX(groundnameAsset, groundparticleFx, (Entity)ped, 23553, groundScaleFx);
                    ped.Ragdoll(800, RagdollType.ScriptControl);
                    ped.ApplyRelativeForceRelativeOffset(new Vector3(0.0f, -1.5f, 0.0f), Vector3.Zero, ForceType.ExternalImpulse, RagdollComponent.Spine3, true, scaleByTimeScale: false);
                    Utilits.StartShotBaseNmMessages(ped);
                    Function.Call(Hash.APPLY_DAMAGE_TO_PED, (InputArgument)(Entity)ped, (InputArgument)damage, (InputArgument)false, (InputArgument)(Entity)Game.Player.Character);
                    ped.PlayAmbientSpeech("GENERIC_FRIGHTENED_HIGH", SpeechModifier.Force);
                    Utilits.dodgeAction = 6;
                }
                if (attackType == Utilits.MeleeAttackType.Power)
                {
                    Utilits.CreateParticleFX(groundnameAsset, groundparticleFx, (Entity)ped, 23553, groundScaleFx);
                    ped.Ragdoll(800, RagdollType.ScriptControl);
                    ped.ApplyRelativeForceRelativeOffset(new Vector3(0.0f, -1.5f, 0.0f), Vector3.Zero, ForceType.ExternalImpulse, RagdollComponent.Spine2, true, scaleByTimeScale: false);
                    Utilits.StartShotBaseNmMessages(ped);
                    Function.Call(Hash.APPLY_DAMAGE_TO_PED, (InputArgument)(Entity)ped, (InputArgument)damage, (InputArgument)false, (InputArgument)(Entity)Game.Player.Character);
                    ped.PlayAmbientSpeech("GENERIC_FRIGHTENED_HIGH", SpeechModifier.Force);
                    Utilits.dodgeAction = 6;
                }
            }
            int num4;
            if ((double)ped.HeightAboveGround > 0.5 && !ped.IsGettingUp && !ped.IsProne)
            {
                position = Game.Player.Character.Position;
                num4 = (double)position.DistanceTo(ped.Position) < (double)hitdistance ? 1 : 0;
            }
            else
                num4 = 0;
            if (num4 == 0)
                return;
            Utilits.MoveMeleeMovement moveMeleeMovement = Utilits.RandomINT(0, 5) <= 2 ? Utilits.MoveMeleeMovement.Dodge : Utilits.MoveMeleeMovement.Hit;
            if (attackType == Utilits.MeleeAttackType.Power)
            {
                if (ped.IsInCombatAgainst(Game.Player.Character))
                {
                    if (moveMeleeMovement == Utilits.MoveMeleeMovement.Hit)
                    {
                        Utilits.CreateParticleFX(nameAsset, particleFx, (Entity)ped, 52301, ScaleFx);
                        Utilits.CreateParticleFX(nameAsset, particleFx, (Entity)ped, 14201, ScaleFx);
                        ped.Ragdoll(800, RagdollType.ScriptControl);
                        ped.ApplyRelativeForceRelativeOffset(new Vector3(0.0f, -0.1f, 0.0f), Vector3.Zero, ForceType.ExternalImpulse, RagdollComponent.FootLeft, true, scaleByTimeScale: false);
                        ped.ApplyRelativeForceRelativeOffset(new Vector3(0.0f, -0.1f, 0.0f), Vector3.Zero, ForceType.ExternalImpulse, RagdollComponent.FootRight, true, scaleByTimeScale: false);
                        Utilits.StartShotBaseNmMessages(ped);
                        Function.Call(Hash.APPLY_DAMAGE_TO_PED, (InputArgument)(Entity)ped, (InputArgument)damage, (InputArgument)false, (InputArgument)(Entity)Game.Player.Character);
                        ped.PlayAmbientSpeech("FIGHT", SpeechModifier.Force);
                        Utilits.dodgeAction = 6;
                        return;
                    }
                    if (moveMeleeMovement == Utilits.MoveMeleeMovement.Dodge)
                    {
                        if (Utilits.dodgeAction == 0)
                            Utilits.dodgeAction = Utilits.RandomINT(1, 6);
                        Utilits.Random_Anim_DodgeWariation(ped);
                        return;
                    }
                }
                else if (!ped.IsInCombatAgainst(Game.Player.Character))
                {
                    Utilits.CreateParticleFX(nameAsset, particleFx, (Entity)ped, 52301, ScaleFx);
                    Utilits.CreateParticleFX(nameAsset, particleFx, (Entity)ped, 14201, ScaleFx);
                    ped.Ragdoll(700, RagdollType.ScriptControl);
                    ped.ApplyRelativeForceRelativeOffset(new Vector3(0.0f, -0.3f, 0.0f), Vector3.Zero, ForceType.ExternalImpulse, RagdollComponent.FootLeft, true, scaleByTimeScale: false);
                    ped.ApplyRelativeForceRelativeOffset(new Vector3(0.0f, -0.3f, 0.0f), Vector3.Zero, ForceType.ExternalImpulse, RagdollComponent.FootRight, true, scaleByTimeScale: false);
                    Utilits.StartShotBaseNmMessages(ped);
                    ped.PlayAmbientSpeech("GUN_BEG", SpeechModifier.Force);
                    Utilits.dodgeAction = 6;
                    return;
                }
            }
            if (attackType == Utilits.MeleeAttackType.Light)
            {
                if (flag3)
                {
                    if (!ped.IsInCombatAgainst(Game.Player.Character))
                    {
                        Utilits.CreateParticleFX(nameAsset, particleFx, (Entity)ped, 14201, ScaleFx);
                        ped.Ragdoll(800, RagdollType.ScriptControl);
                        ped.ApplyRelativeForceRelativeOffset(new Vector3(0.0f, -1.5f, 0.0f), Vector3.Zero, ForceType.ExternalImpulse, RagdollComponent.FootLeft, true, scaleByTimeScale: false);
                        Utilits.StartShotBaseNmMessages(ped);
                        Function.Call(Hash.APPLY_DAMAGE_TO_PED, (InputArgument)(Entity)ped, (InputArgument)damage, (InputArgument)false, (InputArgument)(Entity)Game.Player.Character);
                        ped.PlayAmbientSpeech("GUN_BEG", SpeechModifier.Force);
                        Utilits.dodgeAction = 6;
                    }
                    if (ped.IsInCombatAgainst(Game.Player.Character))
                    {
                        if (moveMeleeMovement == Utilits.MoveMeleeMovement.Hit)
                        {
                            Utilits.CreateParticleFX(nameAsset, particleFx, (Entity)ped, 14201, ScaleFx);
                            ped.Ragdoll(800, RagdollType.ScriptControl);
                            ped.ApplyRelativeForceRelativeOffset(new Vector3(0.0f, -1.5f, 0.0f), Vector3.Zero, ForceType.ExternalImpulse, RagdollComponent.FootLeft, true, scaleByTimeScale: false);
                            Utilits.StartShotBaseNmMessages(ped);
                            Function.Call(Hash.APPLY_DAMAGE_TO_PED, (InputArgument)(Entity)ped, (InputArgument)damage, (InputArgument)false, (InputArgument)(Entity)Game.Player.Character);
                            ped.PlayAmbientSpeech("FIGHT", SpeechModifier.Force);
                            Utilits.dodgeAction = 6;
                        }
                        if (moveMeleeMovement == Utilits.MoveMeleeMovement.Dodge && Utilits.dodgeAction == 0)
                            Utilits.dodgeAction = Utilits.RandomINT(1, 6);
                    }
                }
                else if (flag2)
                {
                    if (!ped.IsInCombatAgainst(Game.Player.Character))
                    {
                        Utilits.CreateParticleFX(nameAsset, particleFx, (Entity)ped, 52301, ScaleFx);
                        ped.Ragdoll(800, RagdollType.ScriptControl);
                        ped.ApplyRelativeForceRelativeOffset(new Vector3(0.0f, -1.5f, 0.0f), Vector3.Zero, ForceType.ExternalImpulse, RagdollComponent.FootRight, true, scaleByTimeScale: false);
                        Utilits.StartShotBaseNmMessages(ped);
                        Function.Call(Hash.APPLY_DAMAGE_TO_PED, (InputArgument)(Entity)ped, (InputArgument)damage, (InputArgument)false, (InputArgument)(Entity)Game.Player.Character);
                        ped.PlayAmbientSpeech("FIGHT", SpeechModifier.Force);
                        Utilits.dodgeAction = 6;
                    }
                    if (ped.IsInCombatAgainst(Game.Player.Character))
                    {
                        if (moveMeleeMovement == Utilits.MoveMeleeMovement.Hit)
                        {
                            Utilits.CreateParticleFX(nameAsset, particleFx, (Entity)ped, 52301, ScaleFx);
                            ped.Ragdoll(800, RagdollType.ScriptControl);
                            ped.ApplyRelativeForceRelativeOffset(new Vector3(0.0f, -1.5f, 0.0f), Vector3.Zero, ForceType.ExternalImpulse, RagdollComponent.FootRight, true, scaleByTimeScale: false);
                            Utilits.StartShotBaseNmMessages(ped);
                            Function.Call(Hash.APPLY_DAMAGE_TO_PED, (InputArgument)(Entity)ped, (InputArgument)damage, (InputArgument)false, (InputArgument)(Entity)Game.Player.Character);
                            Utilits.dodgeAction = 6;
                        }
                        if (moveMeleeMovement == Utilits.MoveMeleeMovement.Dodge && Utilits.dodgeAction == 0)
                            Utilits.dodgeAction = Utilits.RandomINT(1, 6);
                    }
                }
            }
        }

        public static int RandomINT(int min, int max)
        {
            return new System.Random(DateTime.Now.Millisecond).Next(min, max);
        }

        public static void Random_Anim_DodgeWariation(Ped ped)
        {
            switch (Utilits.dodgeAction)
            {
                case 1:
                    if (!ped.IsAnimPlay("melee@unarmed@streamed_core_fps", "dodge_generic_r"))
                    {
                        ped.Task.ClearAllImmediately();
                        ped.Task.PlayAnimation("melee@unarmed@streamed_core_fps", "dodge_generic_r", 8f, -4f, 1300, AnimationFlags.None, 0.0f);
                    }
                    if ((double)ped.GetAnimTime("melee@unarmed@streamed_core_fps", "dodge_generic_r") <= 0.10000000149011612)
                        break;
                    Utilits.dodgeAction = 6;
                    break;
                case 2:
                    if (!ped.IsAnimPlay("melee@unarmed@streamed_core_fps", "dodge_generic_r") && !ped.IsAnimPlay("melee@unarmed@streamed_core_fps", "dodge_generic_l"))
                    {
                        ped.Task.ClearAllImmediately();
                        ped.Task.PlayAnimation("melee@unarmed@streamed_core_fps", "dodge_generic_r", 8f, -4f, 1300, AnimationFlags.None, 0.0f);
                    }
                    if ((double)ped.GetAnimTime("melee@unarmed@streamed_core_fps", "dodge_generic_r") <= 0.89999997615814209)
                        break;
                    ped.Task.PlayAnimation("melee@unarmed@streamed_core_fps", "dodge_generic_l", 8f, -4f, 1500, AnimationFlags.None, 0.0f);
                    Utilits.dodgeAction = 6;
                    break;
                case 3:
                    if (!ped.IsAnimPlay("melee@unarmed@streamed_core_fps", "dodge_generic_l"))
                    {
                        ped.Task.ClearAllImmediately();
                        ped.Task.PlayAnimation("melee@unarmed@streamed_core_fps", "dodge_generic_l", 8f, -4f, 1500, AnimationFlags.None, 0.0f);
                        Utilits.dodgeAction = 6;
                        break;
                    }
                    if ((double)ped.GetAnimTime("melee@unarmed@streamed_core_fps", "dodge_generic_l") <= 0.10000000149011612)
                        break;
                    Utilits.dodgeAction = 6;
                    break;
                case 4:
                    if (!ped.IsAnimPlay("melee@unarmed@streamed_core_fps", "dodge_generic_l") && !ped.IsAnimPlay("melee@unarmed@streamed_core_fps", "dodge_generic_r"))
                    {
                        ped.Task.ClearAllImmediately();
                        ped.Task.PlayAnimation("melee@unarmed@streamed_core_fps", "dodge_generic_l", 8f, -4f, 1500, AnimationFlags.None, 0.0f);
                    }
                    if ((double)ped.GetAnimTime("melee@unarmed@streamed_core_fps", "dodge_generic_l") <= 0.89999997615814209)
                        break;
                    ped.Task.PlayAnimation("melee@unarmed@streamed_core_fps", "dodge_generic_r", 8f, -4f, 1300, AnimationFlags.None, 0.0f);
                    Utilits.dodgeAction = 6;
                    break;
                case 5:
                    if (!ped.IsAnimPlay("melee@unarmed@streamed_core_fps", "dodge_generic_r"))
                    {
                        ped.Task.ClearAllImmediately();
                        ped.Task.PlayAnimation("melee@unarmed@streamed_core_fps", "dodge_generic_r", 8f, -4f, 1300, AnimationFlags.None, 0.0f);
                    }
                    if ((double)ped.GetAnimTime("melee@unarmed@streamed_core_fps", "dodge_generic_r") <= 0.89999997615814209)
                        break;
                    Utilits.dodgeAction = 6;
                    break;
                case 6:
                    if (ped.IsAnimPlay("melee@unarmed@streamed_core_fps", "dodge_generic_l") || ped.IsAnimPlay("melee@unarmed@streamed_core_fps", "dodge_generic_r"))
                        break;
                    ped.IsEnemy = true;
                    ped.BlockPermanentEvents = true;
                    Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, (InputArgument)(Entity)ped, (InputArgument)5, (InputArgument)1);
                    Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, (InputArgument)(Entity)ped, (InputArgument)46, (InputArgument)1);
                    ped.Task.FightAgainst(Game.Player.Character);
                    ped.KeepTaskWhenMarkedAsNoLongerNeeded = true;
                    Utilits.dodgeAction = 0;
                    break;
            }
        }

        public static float LerpTime(float min, float max, int environmenttick, float smooth)
        {
            return Utilits.Lerp(min, max, (float)(Environment.TickCount - environmenttick) / 10000f * smooth);
        }

        public static float Lerp(float value1, float value2, float ammount)
        {
            return value1 + (value2 - value1) * Utilits.Clamp01(ammount);
        }

        public static float Clamp01(float value)
        {
            if ((double)value < 0.0)
                return 0.0f;
            return (double)value > 1.0 ? 1f : value;
        }

        public static Vector3 Slerp(
          Vector3 a,
          Vector3 b,
          Vector3 c,
          int environmenttick,
          float smooth)
        {
            return Vector3.Lerp(Vector3.Lerp(a, b, (float)(Environment.TickCount - environmenttick) / 10000f * smooth), Vector3.Lerp(b, c, (float)(Environment.TickCount - environmenttick) / 10000f * smooth), (float)(Environment.TickCount - environmenttick) / 10000f * smooth);
        }

        public static float SmoothStep(float from, float to, float t)
        {
            t = Utilits.Clamp01(t);
            t = (float)(-2.0 * (double)t * (double)t * (double)t + 3.0 * (double)t * (double)t);
            return (float)((double)to * (double)t + (double)from * (1.0 - (double)t));
        }

        public enum MeleeAttackType
        {
            Light,
            Power,
        }

        public enum MoveMeleeMovement
        {
            Hit,
            Dodge,
        }

        public enum ViewMode
        {
            THIRD_PERSON_NEAR,
            THIRD_PERSON_MEDIUM,
            THIRD_PERSON_FAR,
            CINEMATIC,
            FIRST_PERSON,
        }

        public enum DecalTypes
        {
            splatters_blood = 1010, // 0x000003F2
            splatters_blood_dir = 1015, // 0x000003F7
            splatters_blood_mist = 1017, // 0x000003F9
            splatters_mud = 1020, // 0x000003FC
            splatters_paint = 1030, // 0x00000406
            splatters_water = 1040, // 0x00000410
            splatters_water_hydrant = 1050, // 0x0000041A
            splatters_blood2 = 1110, // 0x00000456
            weapImpact_metal = 4010, // 0x00000FAA
            weapImpact_concrete = 4020, // 0x00000FB4
            weapImpact_mattress = 4030, // 0x00000FBE
            weapImpact_mud = 4032, // 0x00000FC0
            weapImpact_cardboard = 4040, // 0x00000FC8
            weapImpact_wood = 4050, // 0x00000FD2
            weapImpact_sand = 4053, // 0x00000FD5
            weapImpact_melee_glass = 4100, // 0x00001004
            weapImpact_glass_blood = 4102, // 0x00001006
            weapImpact_glass_blood2 = 4104, // 0x00001008
            weapImpact_shotgun_paper = 4200, // 0x00001068
            weapImpact_shotgun_mattress = 4201, // 0x00001069
            weapImpact_shotgun_metal = 4202, // 0x0000106A
            weapImpact_shotgun_wood = 4203, // 0x0000106B
            weapImpact_shotgun_dirt = 4204, // 0x0000106C
            weapImpact_shotgun_tvscreen = 4205, // 0x0000106D
            weapImpact_shotgun_tvscreen2 = 4206, // 0x0000106E
            weapImpact_shotgun_tvscreen3 = 4207, // 0x0000106F
            weapImpact_melee_concrete = 4310, // 0x000010D6
            weapImpact_melee_wood = 4312, // 0x000010D8
            weapImpact_melee_metal = 4314, // 0x000010DA
            burn1 = 4421, // 0x00001145
            burn2 = 4422, // 0x00001146
            burn3 = 4423, // 0x00001147
            burn4 = 4424, // 0x00001148
            burn5 = 4425, // 0x00001149
            bang_concrete_bang = 5000, // 0x00001388
            bang_concrete_bang2 = 5001, // 0x00001389
            bang_bullet_bang = 5002, // 0x0000138A
            bang_bullet_bang2 = 5004, // 0x0000138C
            bang_glass = 5031, // 0x000013A7
            bang_glass2 = 5032, // 0x000013A8
            solidPool_water = 9000, // 0x00002328
            solidPool_blood = 9001, // 0x00002329
            solidPool_oil = 9002, // 0x0000232A
            solidPool_petrol = 9003, // 0x0000232B
            solidPool_mud = 9004, // 0x0000232C
            porousPool_water = 9005, // 0x0000232D
            porousPool_blood = 9006, // 0x0000232E
            porousPool_oil = 9007, // 0x0000232F
            porousPool_petrol = 9008, // 0x00002330
            porousPool_mud = 9009, // 0x00002331
            porousPool_water_ped_drip = 9010, // 0x00002332
            liquidTrail_water = 9050, // 0x0000235A
        }

        public enum NMMessage
        {
            stopAllBehaviours = 0,
            armsWindmill = 372, // 0x00000174
            bodyBalance = 466, // 0x000001D2
            bodyFoetal = 507, // 0x000001FB
            bodyWrithe = 526, // 0x0000020E
            braceForImpact = 548, // 0x00000224
            catchFall = 576, // 0x00000240
            dragged = 597, // 0x00000255
            highFall = 715, // 0x000002CB
            injuredOnGround = 787, // 0x00000313
            pedalLegs = 816, // 0x00000330
            rollDownStairs = 941, // 0x000003AD
            shot = 983, // 0x000003D7
            staggerFall = 1151, // 0x0000047F
            stumble = 1195, // 0x000004AB
            teeter = 1221, // 0x000004C5
            yanked = 1249, // 0x000004E1
        }
    }
}
