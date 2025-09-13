// Decompiled with JetBrains decompiler
// Type: CombatStance.Intime
// Assembly: CombatStanceMovement, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 15398906-0FC5-4912-B95F-7B85C0671CB4
// Assembly location: C:\Users\clope\OneDrive\Desktop\CombatStanceMovement.dll

using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CombatStance
{
    public class Intime : Script
    {
        public Intime()
        {
            this.Tick += new EventHandler(this.OnTick);
            this.KeyDown += new KeyEventHandler(this.OnKeyDown);
            this.KeyUp += new KeyEventHandler(this.OnKeyUp);
        }

        private void OnTick(object sender, EventArgs e) => this.___MelleWeaponAttack();

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
        }

        private void ___MelleWeaponAttack()
        {
            bool flag1 = Game.Player.Character.IsUsingWeapon(WeaponHash.Unarmed) || Game.Player.Character.IsUsingWeapon(WeaponHash.Flashlight) || Game.Player.Character.IsUsingWeapon(WeaponHash.KnuckleDuster);
            bool flag2 = Game.Player.Character.IsUsingWeapon(WeaponHash.Knife) || Game.Player.Character.IsUsingWeapon(WeaponHash.Bottle) || Game.Player.Character.IsUsingWeapon(WeaponHash.Dagger) || Game.Player.Character.IsUsingWeapon(WeaponHash.SwitchBlade);
            bool flag3 = Game.Player.Character.IsUsingWeapon(WeaponHash.Nightstick) || Game.Player.Character.IsUsingWeapon(WeaponHash.Hammer) || Game.Player.Character.IsUsingWeapon(WeaponHash.Crowbar) || Game.Player.Character.IsUsingWeapon(WeaponHash.Wrench);
            bool flag4 = Game.Player.Character.IsUsingWeapon(WeaponHash.Machete) || Game.Player.Character.IsUsingWeapon(WeaponHash.BattleAxe) || Game.Player.Character.IsUsingWeapon(WeaponHash.Hatchet) || Game.Player.Character.IsUsingWeapon(WeaponHash.StoneHatchet);
            bool flag5 = Game.Player.Character.IsUsingWeapon(WeaponHash.Bat) || Game.Player.Character.IsUsingWeapon(WeaponHash.GolfClub) || Game.Player.Character.IsUsingWeapon(WeaponHash.PoolCue);
            foreach (KeyValuePair<Ped, int> keyValuePair in Prone.meleePedVictum.ToList<KeyValuePair<Ped, int>>())
            {
                Ped key = keyValuePair.Key;
                int num1 = keyValuePair.Value;
                if (key.Exists() && key.IsAlive)
                {
                    Vector3 offsetPosition1 = Game.Player.Character.Bones[Bone.SkelRightHand].GetOffsetPosition(new Vector3(0.0f, 0.0f, 0.0f));
                    Vector3 offsetPosition2 = key.Bones[Bone.SkelRightFoot].GetOffsetPosition(new Vector3(0.0f, 0.0f, 0.0f));
                    Vector3 offsetPosition3 = key.Bones[Bone.SkelLeftFoot].GetOffsetPosition(new Vector3(0.0f, 0.0f, 0.0f));
                    Function.Call<int>(Hash.FLOOR, (InputArgument)(offsetPosition2.DistanceTo(offsetPosition1) * 1000f));
                    Function.Call<int>(Hash.FLOOR, (InputArgument)(offsetPosition3.DistanceTo(offsetPosition1) * 1000f));
                    if ((num1 == 1 || num1 == 2 || num1 == 3) && !key.IsInCombatAgainst(Game.Player.Character) && (double)Game.Player.Character.Position.DistanceTo(key.Position) > 10.0)
                    {
                        if (!key.IsFleeing)
                        {
                            Function.Call(Hash.SET_PED_RAGDOLL_ON_COLLISION, (InputArgument)(Entity)key, (InputArgument)true);
                            key.Task.FleeFrom(Game.Player.Character, 8000);
                            key.MaxSpeed = 4.5f;
                            key.KeepTaskWhenMarkedAsNoLongerNeeded = true;
                        }
                        key.MarkAsNoLongerNeeded();
                        Prone.meleePedVictum.Remove(key);
                    }
                    if (num1 == 1 && !key.IsFleeing)
                    {
                        int num2 = Function.Call<int>(Hash.GET_HASH_KEY, (InputArgument)"COP");
                        int num3 = Function.Call<int>(Hash.GET_HASH_KEY, (InputArgument)"SECURITY_GUARD");
                        int num4 = Function.Call<int>(Hash.GET_HASH_KEY, (InputArgument)"PRIVATE_SECURITY");
                        if (key.RelationshipGroup == (RelationshipGroup)num2 || key.RelationshipGroup == (RelationshipGroup)num3 || key.RelationshipGroup == (RelationshipGroup)num4)
                        {
                            Utilits.dodgeAction = 6;
                            Prone.meleePedVictum[key] = 4;
                            break;
                        }
                        if (key.RelationshipGroup != (RelationshipGroup)num2 && key.RelationshipGroup != (RelationshipGroup)num3 && key.RelationshipGroup != (RelationshipGroup)num4 && Utilits.RandomINT(0, 10) < 5)
                        {
                            key.Weapons.Give(WeaponHash.Unarmed, 100, true, true);
                            Utilits.RandomINT(1, 101);
                            Utilits.dodgeAction = 6;
                            Prone.meleePedVictum[key] = 4;
                            break;
                        }
                        Prone.meleePedVictum[key] = 2;
                    }
                    if (num1 == 2 && !key.IsFleeing)
                    {
                        Function.Call(Hash.SET_PED_RAGDOLL_ON_COLLISION, (InputArgument)(Entity)key, (InputArgument)true);
                        key.Task.FleeFrom(Game.Player.Character, 8000);
                        key.MaxSpeed = 4.5f;
                        key.KeepTaskWhenMarkedAsNoLongerNeeded = true;
                    }
                    if ((num1 == 1 || num1 == 2) && (double)key.HeightAboveGround < 0.5 && !key.GetConfigFlag(PedConfigFlagToggles.WasStanding))
                    {
                        switch (Utilits.RandomINT(0, 3))
                        {
                            case 0:
                                key.GiveNMMessage(Utilits.NMMessage.bodyWrithe, duration: 8000);
                                return;
                            case 1:
                                key.GiveNMMessage(Utilits.NMMessage.pedalLegs, duration: 8000);
                                break;
                            case 2:
                                key.GiveNMMessage(Utilits.NMMessage.injuredOnGround, duration: 8000);
                                break;
                            case 3:
                                key.Ragdoll(8000);
                                Function.Call(Hash.SET_ENABLE_BOUND_ANKLES, (InputArgument)(Entity)key, (InputArgument)true);
                                break;
                        }
                        Function.Call(Hash.PLAY_PAIN, (InputArgument)(Entity)key, (InputArgument)8, (InputArgument)0, (InputArgument)0);
                        Function.Call(Hash.SET_PED_RAGDOLL_ON_COLLISION, (InputArgument)(Entity)key, (InputArgument)true);
                        key.Task.FleeFrom(Game.Player.Character, 8000);
                        key.MaxSpeed = 4.5f;
                        key.KeepTaskWhenMarkedAsNoLongerNeeded = true;
                        Prone.meleePedVictum[key] = 3;
                    }
                    if (num1 == 4)
                    {
                        key.SetResetFlag(PedResetFlagToggles.DisableAmbientMeleeMoves, true);
                        key.SetConfigFlag(PedConfigFlagToggles.PreventAllMeleeTaunts, true);
                        Utilits.Random_Anim_DodgeWariation(key);
                        switch (Prone.ProneMleeAttackAction)
                        {
                            case 1:
                                if (Game.Player.Character.IsFacingPed(key, 45f))
                                {
                                    if (flag1)
                                        Utilits.MelleWeaponAttack(key, "core", "bul_stungun", "core", "ent_anim_dusty_hands", Utilits.MeleeAttackType.Light, 1.2f, damage: 5);
                                    if (flag2)
                                        Utilits.MelleWeaponAttack(key, "scr_solomon3", "scr_trev4_747_blood_impact", "core", "blood_entry_shotgun", Utilits.MeleeAttackType.Light, 1.4f, 1.35f, 15, 0.125f);
                                    if (flag3)
                                        Utilits.MelleWeaponAttack(key, "core", "bang_blood_car", "core", "bang_blood", Utilits.MeleeAttackType.Light, 1.5f, 2.2f, ScaleFx: 1.3f, groundScaleFx: 1.3f);
                                    if (flag4)
                                        Utilits.MelleWeaponAttack(key, "scr_solomon3", "scr_trev4_747_blood_impact", "core", "blood_entry_shotgun", Utilits.MeleeAttackType.Light, 1.5f, 1.8f, 20, 0.15f, 1.4f);
                                    if (flag5)
                                        Utilits.MelleWeaponAttack(key, "core", "bang_blood_car", "core", "bang_blood", Utilits.MeleeAttackType.Light, 1.7f, 2.5f, 15, 1.5f, 1.5f);
                                }
                                Prone.ProneMleeAttackAction = 0;
                                break;
                            case 2:
                                if (Game.Player.Character.IsFacingPed(key, 45f))
                                {
                                    if (flag2)
                                        Utilits.MelleWeaponAttack(key, "scr_solomon3", "scr_trev4_747_blood_impact", "core", "blood_entry_shotgun", Utilits.MeleeAttackType.Light, 1.4f, 1.35f, 15, 0.125f);
                                    if (flag5)
                                        Utilits.MelleWeaponAttack(key, "core", "bang_blood_car", "core", "bang_blood", Utilits.MeleeAttackType.Light, 1.7f, 2.5f, 15, 1.5f, 1.5f);
                                    if (flag3)
                                        Utilits.MelleWeaponAttack(key, "core", "bang_blood_car", "core", "bang_blood", Utilits.MeleeAttackType.Light, 1.5f, 2.2f, ScaleFx: 1.3f, groundScaleFx: 1.3f);
                                    if (flag4)
                                        Utilits.MelleWeaponAttack(key, "scr_solomon3", "scr_trev4_747_blood_impact", "core", "blood_entry_shotgun", Utilits.MeleeAttackType.Light, 1.5f, 1.8f, 20, 0.15f, 1.4f);
                                    if (flag1)
                                        Utilits.MelleWeaponAttack(key, "core", "bul_stungun", "core", "ent_anim_dusty_hands", Utilits.MeleeAttackType.Light, 1.2f, damage: 5);
                                }
                                Prone.ProneMleeAttackAction = 0;
                                break;
                            case 3:
                                Prone.ProneMleeAttackAction = 0;
                                if (Game.Player.Character.IsFacingPed(key, 45f))
                                {
                                    if (flag2)
                                    {
                                        Utilits.MelleWeaponAttack(key, "scr_solomon3", "scr_trev4_747_blood_impact", "core", "blood_entry_shotgun", Utilits.MeleeAttackType.Power, 1.4f, 1.35f, 15, 0.125f);
                                        return;
                                    }
                                    if (flag5)
                                    {
                                        Utilits.MelleWeaponAttack(key, "core", "bang_blood_car", "core", "bang_blood", Utilits.MeleeAttackType.Power, 1.7f, 2.5f, 15, 1.5f, 1.5f);
                                        return;
                                    }
                                    if (flag3)
                                    {
                                        Utilits.MelleWeaponAttack(key, "core", "bang_blood_car", "core", "bang_blood", Utilits.MeleeAttackType.Power, 1.5f, 2.2f, ScaleFx: 1.3f, groundScaleFx: 1.3f);
                                        return;
                                    }
                                    if (flag4)
                                    {
                                        Utilits.MelleWeaponAttack(key, "scr_solomon3", "scr_trev4_747_blood_impact", "core", "blood_entry_shotgun", Utilits.MeleeAttackType.Power, 1.5f, 1.8f, 20, 0.15f, 1.4f);
                                        return;
                                    }
                                    if (!flag1)
                                        return;
                                    Utilits.MelleWeaponAttack(key, "core", "bul_stungun", "core", "ent_anim_dusty_hands", Utilits.MeleeAttackType.Power, 1.3f, damage: 5);
                                    return;
                                }
                                break;
                        }
                        if (key.IsInCombatAgainst(Game.Player.Character) && (double)Game.Player.Character.Position.DistanceTo(key.Position) > 35.0)
                        {
                            key.MarkAsNoLongerNeeded();
                            Prone.meleePedVictum.Remove(key);
                        }
                    }
                }
                if (!key.Exists() || key.IsDead)
                    Prone.meleePedVictum.Remove(key);
            }
        }
    }
}
