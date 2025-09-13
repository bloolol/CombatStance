// Decompiled with JetBrains decompiler
// Type: CombatStance.Crouch
// Assembly: CombatStanceMovement, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 15398906-0FC5-4912-B95F-7B85C0671CB4
// Assembly location: C:\Users\clope\OneDrive\Desktop\CombatStanceMovement.dll

using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;
using System.Drawing;

namespace CombatStance
{
    public class Crouch : Script
    {
        public static bool crouchtriger;

        public static void Stance_Crouch() => Crouch.Crouch__Movement();

        public static void Crouch__Movement()
        {
            if (Main.currentStance != Main.Stance.Crouching)
                return;
            ShapeTestHandle shapeTestHandle = ShapeTest.StartTestBox(new Vector3(Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z - 0.5f), new Vector3(0.7f, 0.8f, 0.6f), Game.Player.Character.Rotation, intersectFlags: IntersectFlags.Everything, excludeEntity: (Entity)Game.Player.Character);
            if (!shapeTestHandle.GetResult().result.DidHit && !Game.Player.Character.IsInWater)
            {
                Game.Player.Character.CollisionCapsule(0.0001f);
                Game.Player.Character.SetConfigFlag(PedConfigFlagToggles.DisablePedConstraints, true);
            }
            else
            {
                Game.Player.Character.CollisionCapsule(0.7f);
                Game.Player.Character.SetConfigFlag(PedConfigFlagToggles.DisablePedConstraints, false);
            }
            if (Main.debugger)
            {
                World.DrawMarker(MarkerType.Boxes, new Vector3(Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z - 0.5f), Vector3.Zero, Game.Player.Character.Rotation, new Vector3(0.7f, 0.8f, 0.6f), Color.Red);
                World.DrawMarker(MarkerType.Sphere, shapeTestHandle.GetResult().result.HitPosition, Vector3.Zero, Vector3.Zero, new Vector3(0.2f, 0.2f, 0.2f), Color.Red);
                Screen.ShowSubtitle("\nDidHit  = " + shapeTestHandle.GetResult().result.DidHit.ToString(), 1000);
            }
            Game.Player.Character.SetConfigFlag(PedConfigFlagToggles.UsingCrouchedPedCapsule, true);
            Game.Player.Character.SetResetFlag(PedResetFlagToggles.DisableActionMode, true);
            Game.Player.Character.SetResetFlag(PedResetFlagToggles.PreferMeleeBodyIKHitReaction, true);
            Game.Player.Character.SetResetFlag(PedResetFlagToggles.NoAutoRunWhenFiring, true);
            Game.Player.Character.SetResetFlag(PedResetFlagToggles.DisablePlayerVaulting, true);
            Game.Player.Character.SetResetFlag(PedResetFlagToggles.DisablePlayerJumping, true);
            Game.Player.Character.SetResetFlag(PedResetFlagToggles.DisablePlayerAutoVaulting, true);
            if (Game.Player.Character.IsFalling || Game.Player.Character.IsRagdoll || (double)Game.Player.Character.SubmersionLevel > 0.40000000596046448)
            {
                if ((double)Game.Player.Character.SubmersionLevel > 0.40000000596046448)
                {
                    Game.Player.Character.Ragdoll(2000);
                    Game.Player.Character.SetConfigFlag(PedConfigFlagToggles.DisablePedConstraints, false);
                    Main.SetStance(Main.Stance.Standing);
                }
                else
                {
                    Game.Player.Character.SetConfigFlag(PedConfigFlagToggles.DisablePedConstraints, false);
                    Main.SetStance(Main.Stance.Standing);
                }
            }
            else
            {
                Stand.JumpForce();
                Crouch.Crouch__Movement_roll();
                if (Game.Player.Character.IsInMeleeCombat)
                {
                    Game.Player.Character.SetConfigFlag(PedConfigFlagToggles.DisablePedConstraints, false);
                    Main.SetStance(Main.Stance.Standing);
                }
                else
                {
                    if (!Game.Player.Character.IsInMeleeCombat)
                    {
                        Game.Player.Character.SetDisableAmbientMeleeMove(true);
                        if (Utilits.IsFollowPedCamViewMode(Utilits.ViewMode.FIRST_PERSON))
                        {
                            Crouch.AnimIdleClear();
                            if (Game.IsControlPressed(Control.Sprint))
                                Game.Player.Character.SetPedMaxMoveBlendRatio(2f);
                            if (!Game.IsControlPressed(Control.Sprint))
                                Game.Player.Character.SetPedMaxMoveBlendRatio();
                            Game.Player.Character.SetConfigFlag(PedConfigFlagToggles.IsStanding, false);
                            Function.Call(Hash.SET_PED_USING_ACTION_MODE, (InputArgument)(Entity)Game.Player.Character, (InputArgument)false, (InputArgument)(-1), (InputArgument)0);
                            Game.Player.Character.SetPedStealthMovement(true);
                            if (Prone.idleViewModeTransition != 2)
                                Prone.idleViewModeTransition = 2;
                        }
                        if (!Utilits.IsFollowPedCamViewMode(Utilits.ViewMode.FIRST_PERSON))
                        {
                            Game.Player.Character.Weapons.Select(Game.Player.Character.Weapons.Current.Hash, true);
                            if (Prone.idleViewModeTransition == 1)
                            {
                                Game.DisableControlThisFrame(Control.MoveDownOnly);
                                Game.DisableControlThisFrame(Control.MoveUpOnly);
                                Game.DisableControlThisFrame(Control.MoveLeftOnly);
                                Game.DisableControlThisFrame(Control.MoveRightOnly);
                                Game.DisableAllControlsThisFrame();
                            }
                            if (Game.Player.IsAiming && Game.IsControlJustPressed(Control.Aim))
                            {
                                Crouch.AnimIdleClear();
                                if (Function.Call<bool>(Hash.IS_PED_ARMED, (InputArgument)(Entity)Game.Player.Character, (InputArgument)4))
                                    Game.Player.Character.SetConfigFlag(PedConfigFlagToggles.IsStanding, false);
                            }
                            if ((((double)Game.Player.Character.Speed <= 0.0099999997764825821 || Game.Player.Character.IsStopped) && !Crouch.crouchtriger || Prone.idleViewModeTransition > 0) && Main.TransitionAction == 0 && !Game.Player.Character.IsAnimPlay("get_up@directional@transition@prone_to_knees@crawl", "front") && !Game.Player.Character.IsAnimPlay("get_up@directional@movement@from_seated@action", "getup_l_0") && !Game.Player.Character.IsAnimPlay("missexile2", "enter_crouch_a") && !Game.Player.IsAiming && !Game.IsControlPressed(Control.SelectWeapon))
                            {
                                Crouch.Crouch__Movement_idl();
                                Crouch.crouchtriger = true;
                                if (Prone.idleViewModeTransition == 1)
                                    Prone.idleViewModeTransition = 0;
                                if (Prone.idleViewModeTransition == 2)
                                {
                                    Game.Player.Character.SetPedMovementClipset("move_ped_crouched", 1f);
                                    Game.Player.Character.SetPedStrafeClipset("move_ped_crouched_strafing");
                                    Game.Player.Character.SetPedStealthMovement(false);
                                    Prone.idleViewModeTransition = 0;
                                }
                            }
                            if (((double)Game.Player.Character.Speed >= 0.0099999997764825821 || Game.Player.Character.IsStopped) && Crouch.crouchtriger)
                                Crouch.crouchtriger = false;
                            Function.Call(Hash.SET_PED_USING_ACTION_MODE, (InputArgument)(Entity)Game.Player.Character, (InputArgument)false, (InputArgument)( - 1), (InputArgument)0);
                            Game.Player.Character.SetPedStealthMovement(false);
                        }
                    }
                    Main.CameraCollisionDisable();
                    Game.DisableControlThisFrame(Control.Duck);
                }
            }
        }

        public static void Crouch__Movement_idl()
        {
            string[] strArray1 = new string[2]
            {
        "move_crouch_proto",
        "idle"
            };
            string[] strArray2 = new string[2]
            {
        "move_aim_strafe_crouch_2h",
        "idle"
            };
            string[] strArray3 = new string[2]
            {
        "move_weapon@rifle@generic",
        "idle_crouch"
            };
            string[] strArray4 = new string[2]
            {
        "missfbi2",
        "franklin_sniper_crouch"
            };
            string[] strArray5 = new string[2]
            {
        "cover@first_person@weapon@rpg",
        "blindfire_low_r_aim_med_edge"
            };
            string[] strArray6 = new string[2]
            {
        "weapons@first_person@aim_idle@remote_clone@heavy@minigun@",
        "aim_low_loop"
            };
            string[] strArray7 = new string[2]
            {
        "weapons@first_person@aim_rng@p_m_zero@misc@jerrycan@aim_trans@unholster_to_rng",
        "aim_trans_low"
            };
            string[] strArray8 = new string[2]
            {
        "move_crouch_proto",
        "idle"
            };
            string[] strArray9 = Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.Melee) ? strArray1 : (Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.Pistol) || Game.Player.Character.IsUsingWeaponMicroSMG() ? strArray2 : (Game.Player.Character.IsUsingWeapon(WeaponHash.Railgun) || Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.AssaultRifle) || Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.MG) || Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.SMG) && !Game.Player.Character.IsUsingWeaponMicroSMG() || Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.Shotgun) ? strArray3 : (Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.Sniper) ? strArray4 : (Game.Player.Character.IsUsingWeapon(WeaponHash.RPG) || Game.Player.Character.IsUsingWeapon(WeaponHash.Firework) || Game.Player.Character.IsUsingWeapon(WeaponHash.HomingLauncher) ? strArray5 : (Game.Player.Character.IsUsingWeapon(WeaponHash.Minigun) || Game.Player.Character.IsUsingWeapon(WeaponHash.RailgunXmas3) ? strArray6 : (Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.PetrolCan) ? strArray7 : strArray8))))));
            AnimationFlags animationFlags = strArray9 == strArray5 || strArray9 == strArray7 ? AnimationFlags.UpperBodyOnly : AnimationFlags.None;
            if (Game.Player.Character.IsAnimPlay(strArray9[0], strArray9[1]))
                return;
            Game.Player.Character.Task.PlayAnimation(strArray9[0], strArray9[1], 2f, -2f, -1, animationFlags | AnimationFlags.StayInEndFrame | AnimationFlags.AbortOnPedMovement, 0.0f);
            if (strArray9 == strArray6)
            {
                Game.Player.Character.Task.PlayAnimation("anim@amb@business@weed@weed_inspecting_lo_med_hi@", "weed_spraybottle_crouch_spraying_01_inspector", 2f, -2f, -1, AnimationFlags.StayInEndFrame | AnimationFlags.AbortOnPedMovement, 0.0f);
                Game.Player.Character.Task.PlayAnimation(strArray9[0], strArray9[1], 2f, -2f, -1, AnimationFlags.StayInEndFrame | AnimationFlags.UpperBodyOnly | AnimationFlags.Secondary | AnimationFlags.AbortOnPedMovement, 0.0f);
            }
        }

        public static string[] RollName()
        {
            string[][] strArray1 = new string[4][]
            {
        new string[2]
        {
          "move_strafe@roll_fps",
          "combatroll_fwd_p1_00"
        },
        new string[2]
        {
          "move_strafe@roll_fps",
          "combatroll_fwd_p1_-45"
        },
        new string[2]
        {
          "move_strafe@roll_fps",
          "combatroll_fwd_p1_45"
        },
        new string[2]
        {
          "move_strafe@roll_fps",
          "combatroll_bwd_p1_180"
        }
            };
            foreach (string[] strArray2 in strArray1)
            {
                if (Game.Player.Character.IsAnimPlay(strArray2[0], strArray2[1]))
                    return strArray2;
            }
            return new string[2] { "null", "null" };
        }

        public static void RollForce()
        {
            string[] strArray1 = new string[2]
            {
        "move_strafe@roll_fps",
        "combatroll_fwd_p1_00"
            };
            string[] strArray2 = new string[2]
            {
        "move_strafe@roll_fps",
        "combatroll_fwd_p1_-45"
            };
            string[] strArray3 = new string[2]
            {
        "move_strafe@roll_fps",
        "combatroll_fwd_p1_45"
            };
            string[] strArray4 = new string[2]
            {
        "move_strafe@roll_fps",
        "combatroll_bwd_p1_180"
            };
            float[] numArray1;
            if (!(strArray1[1] == Crouch.RollName()[1]))
            {
                if (!(strArray2[1] == Crouch.RollName()[1]))
                {
                    if (!(strArray3[1] == Crouch.RollName()[1]))
                    {
                        if (!(strArray4[1] == Crouch.RollName()[1]))
                            numArray1 = new float[2];
                        else
                            numArray1 = new float[2]
                            {
                Configuration.RollBackForceX,
                Configuration.RollBackForceY
                            };
                    }
                    else
                        numArray1 = new float[2]
                        {
              Configuration.RollRightForceX,
              Configuration.RollRightForceY
                        };
                }
                else
                    numArray1 = new float[2]
                    {
            Configuration.RollLeftForceX,
            Configuration.RollLeftForceY
                    };
            }
            else
                numArray1 = new float[2]
                {
          Configuration.RollForwardForceX,
          Configuration.RollForwardForceY
                };
            float[] numArray2 = numArray1;
            Game.Player.Character.ApplyForce(strArray1[1] == Crouch.RollName()[1] ? (Game.Player.Character.ForwardVector * numArray2[0] + Game.Player.Character.UpVector * numArray2[1]) / Game.TimeScale : (strArray2[1] == Crouch.RollName()[1] ? (Game.Player.Character.RightVector * -numArray2[0] + Game.Player.Character.UpVector * numArray2[1]) / Game.TimeScale : (strArray3[1] == Crouch.RollName()[1] ? (Game.Player.Character.RightVector * numArray2[0] + Game.Player.Character.UpVector * numArray2[1]) / Game.TimeScale : (strArray4[1] == Crouch.RollName()[1] ? (Game.Player.Character.ForwardVector * -numArray2[0] + Game.Player.Character.UpVector * numArray2[1]) / Game.TimeScale : Vector3.Zero))), Vector3.Zero, ForceType.InternalImpulse);
        }

        public static void Crouch__Movement_roll()
        {
            bool flag1 = Configuration.roll_front && !Game.Player.Character.IsAnimPlay("move_strafe@roll_fps", "combatroll_fwd_p1_00");
            bool flag2 = Configuration.roll_left && !Game.Player.Character.IsAnimPlay("move_strafe@roll_fps", "combatroll_fwd_p1_-45");
            bool flag3 = Configuration.roll_right && !Game.Player.Character.IsAnimPlay("move_strafe@roll_fps", "combatroll_fwd_p1_45");
            bool flag4 = Configuration.roll_back && !Game.Player.Character.IsAnimPlay("move_strafe@roll_fps", "combatroll_bwd_p1_180");
            bool flag5 = flag3 & flag2 & flag1 & flag4;
            Crouch.RollForce();
            bool flag6 = Game.Player.Character.IsAnimPlay("move_strafe@roll_fps", "combatroll_fwd_p1_00") || Game.Player.Character.IsAnimPlay("move_strafe@roll_fps", "combatroll_fwd_p1_-45") || Game.Player.Character.IsAnimPlay("move_strafe@roll_fps", "combatroll_fwd_p1_45") || Game.Player.Character.IsAnimPlay("move_strafe@roll_fps", "combatroll_bwd_p1_180");
            if (GameplayCamera.GetCamViewModeForContext(CamViewModeContext.OnFoot) == CamViewMode.FirstPerson & flag6)
            {
                Game.DisableControlThisFrame(Control.LookLeftRight);
                Game.DisableControlThisFrame(Control.LookUpDown);
            }
            if (Game.IsControlPressed(Control.MoveLeftOnly) && !flag1 || Game.IsControlPressed(Control.MoveLeftOnly) && !flag3 || Game.IsControlPressed(Control.MoveLeftOnly) && !flag1 || Game.IsControlPressed(Control.MoveLeftOnly) && !flag4)
                Game.Player.Character.Heading += 2f;
            if (Game.IsControlPressed(Control.MoveRightOnly) && !flag2 || Game.IsControlPressed(Control.MoveRightOnly) && !flag3 || Game.IsControlPressed(Control.MoveRightOnly) && !flag1 || Game.IsControlPressed(Control.MoveRightOnly) && !flag4)
                Game.Player.Character.Heading -= 2f;
            if (((!Game.IsControlPressed(Control.Jump) || !Game.IsControlPressed(Control.MoveUpOnly) || Game.IsControlPressed(Control.MoveLeftOnly) ? 0 : (!Game.IsControlPressed(Control.MoveRightOnly) ? 1 : 0)) & (flag5 ? 1 : 0)) != 0)
                Game.Player.Character.Task.PlayAnimation("move_strafe@roll_fps", "combatroll_fwd_p1_00", 8f, -4f, -1, AnimationFlags.None, 0.0f);
            else if (((!Game.IsControlPressed(Control.Jump) || !Game.IsControlPressed(Control.MoveDownOnly) || Game.IsControlPressed(Control.MoveLeftOnly) ? 0 : (!Game.IsControlPressed(Control.MoveRightOnly) ? 1 : 0)) & (flag5 ? 1 : 0)) != 0)
                Game.Player.Character.Task.PlayAnimation("move_strafe@roll_fps", "combatroll_bwd_p1_180", 8f, -4f, -1, AnimationFlags.None, 0.0f);
            else if (((!Game.IsControlPressed(Control.Jump) ? 0 : (Game.IsControlPressed(Control.MoveLeftOnly) ? 1 : 0)) & (flag5 ? 1 : 0)) != 0)
            {
                Game.Player.Character.Task.PlayAnimation("move_strafe@roll_fps", "combatroll_fwd_p1_-45", 8f, -4f, -1, AnimationFlags.None, 0.0f);
            }
            else
            {
                if (((!Game.IsControlPressed(Control.Jump) ? 0 : (Game.IsControlPressed(Control.MoveRightOnly) ? 1 : 0)) & (flag5 ? 1 : 0)) == 0)
                    return;
                Game.Player.Character.Task.PlayAnimation("move_strafe@roll_fps", "combatroll_fwd_p1_45", 8f, -4f, -1, AnimationFlags.None, 0.0f);
            }
        }

        public static void AnimIdleClear()
        {
            Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("move_crouch_proto", "idle"));
            Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("move_aim_strafe_crouch_2h", "idle"));
            Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("move_weapon@rifle@generic", "idle_crouch"));
            Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("missfbi2", "franklin_sniper_crouch"));
            Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("cover@first_person@weapon@rpg", "blindfire_low_r_aim_med_edge"));
            Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("weapons@first_person@aim_idle@remote_clone@heavy@minigun@", "aim_low_loop"));
            Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("weapons@first_person@aim_rng@p_m_zero@misc@jerrycan@aim_trans@unholster_to_rng", "aim_trans_low"));
            Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("move_crouch_proto", "idle"));
            Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("anim@amb@business@weed@weed_inspecting_lo_med_hi@", "weed_spraybottle_crouch_spraying_01_inspector"));
        }
    }
}
