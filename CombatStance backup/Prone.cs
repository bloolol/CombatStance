// Decompiled with JetBrains decompiler
// Type: CombatStance.Prone
// Assembly: CombatStanceMovement, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 15398906-0FC5-4912-B95F-7B85C0671CB4
// Assembly location: C:\Users\clope\OneDrive\Desktop\CombatStanceMovement.dll

using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CombatStance
{
    public class Prone : Script
    {
        private bool camhead;
        public static int ProneMleeAttackAction;
        public static int TimeLearpModifier;
        public static Camera ScopeOutCamera;
        public static int SOCDeltaTime;
        public static int SOCPosTransition;
        public static bool SOCEnable;
        public static bool SOCWaitControlEnable;
        public static bool SOCTexture;
        public static Scaleform sd;
        public static Ped pedVictum;
        public static Prop throwingObject;
        public static Camera AimOnBackCamera;
        public static Dictionary<Prop, int> throwingObjectDict;
        public static Dictionary<Ped, int> meleePedVictum;
        public static Timer ExplosionTimer;
        public static Timer TimerKeepThrowObj;
        public static int idleViewModeTransition;
        public static int ThrowAction;
        public static bool FlipChanger;
        public static bool goIdle;
        public static int goProneIdle;
        public static bool scopetriger;
        public static int ProneGrabVehicleAction;
        public static bool onCar;
        public static Vector3 speedVector;

        public static void Stance_Prone()
        {
            Prone.onCar = Function.Call<bool>(Hash.IS_PED_ON_VEHICLE, (InputArgument)(Entity)Game.Player.Character);
            Prone.speedVector = Function.Call<Vector3>(Hash.GET_ENTITY_SPEED_VECTOR, (InputArgument)(Entity)Game.Player.Character, (InputArgument)true);
            Main.blockScopeCameraPlay = !Prone.scopetriger && Prone.ScopeOutCamera == (Camera)null && Prone.SOCPosTransition == 0;
            Prone.Prone__Movement();
            Prone.Exp_Obj();
        }

        public static void Prone__Movement()
        {
            if (Main.currentStance != Main.Stance.Prone)
                return;
            if (Prone.idleViewModeTransition != 0)
                Prone.idleViewModeTransition = 0;
            Main.CameraCollisionDisable();
            Game.Player.Character.SetResetFlag(PedResetFlagToggles.PreferMeleeBodyIKHitReaction, true);
            Game.Player.Character.SetResetFlag(PedResetFlagToggles.DisableActionMode, true);
            Game.Player.Character.SetResetFlag(PedResetFlagToggles.DisableWallHitAnimation, true);
            Game.Player.Character.SetResetFlag(PedResetFlagToggles.DisablePedCollisionWithPedEvent, true);
            Game.Player.Character.SetResetFlag(PedResetFlagToggles.DisableAmbientMeleeMoves, true);
            Game.Player.Character.SetResetFlag(PedResetFlagToggles.DisableWallHitAnimation, true);
            ShapeTestHandle shapeTestHandle = ShapeTest.StartTestBox(new Vector3(Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z - 0.5f), new Vector3(0.7f, 1.8f, 0.1f), Game.Player.Character.Rotation, intersectFlags: IntersectFlags.Everything, excludeEntity: (Entity)Game.Player.Character);
            (ShapeTestStatus, ShapeTestResult) result = shapeTestHandle.GetResult();
            if (!result.Item2.DidHit)
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
                World.DrawMarker(MarkerType.Boxes, new Vector3(Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z - 0.5f), Vector3.Zero, Game.Player.Character.Rotation, new Vector3(0.7f, 1.8f, -0.1f), Color.Red);
                result = shapeTestHandle.GetResult();
                World.DrawMarker(MarkerType.Sphere, result.Item2.HitPosition, Vector3.Zero, Vector3.Zero, new Vector3(0.2f, 0.2f, 0.2f), Color.Red);
                object[] objArray = new object[3];
                result = shapeTestHandle.GetResult();
                objArray[0] = (object)("\nDidHit  = " + result.Item2.DidHit.ToString());
                objArray[1] = (object)("\nIsSwitchingWeapon = " + Game.Player.Character.GetConfigFlag(PedConfigFlagToggles.IsSwitchingWeapon).ToString());
                objArray[2] = (object)("\nHUD  = " + Function.Call<bool>(Hash.IS_HUD_COMPONENT_ACTIVE, (InputArgument)19).ToString());
                Screen.ShowSubtitle(string.Concat(objArray), 1000);
            }
            Main.WeaponChange();
            Prone.ProneCansel();
            Prone.ProneMovement();
            Prone.ProneGrabVehicle();
            if (Prone.FlipChanger)
                Prone.mf_aim();
            if (!Prone.FlipChanger && !Prone.onCar)
                Prone.mb_aim();
        }

        public static void ProneMovement()
        {
            bool flag1 = !Game.Player.Character.IsAnimPlay("cover@weapon@grenade", "hi_r_pinpull") && !Game.Player.Character.IsAnimPlay("cover@weapon@grenade", "hi_r_corner_cook") && !Game.Player.Character.IsAnimPlay("cover@weapon@grenade", "low_r_centre_throw") && !Game.Player.Character.IsAnimPlay("cover@weapon@grenade", "low_r_pinpull") && !Game.Player.Character.IsAnimPlay("cover@weapon@grenade", "low_r_centre_cook") && !Game.Player.Character.IsAnimPlay("cover@weapon@grenade", "low_r_throw_long");
            bool flag2 = !Game.Player.Character.IsAnimPlay("veh@driveby@first_person@passenger_left_handed@throw", "throw_90l") && !Game.Player.Character.IsAnimPlay("veh@driveby@first_person@passenger_left_handed@throw", "throw_90l") && !Game.Player.Character.IsAnimPlay("veh@drivebybike@sport@front@grenade", "throw_0") && !Game.Player.Character.IsAnimPlay("melee@small_wpn@streamed_core", "counter_attack_l") && !Game.Player.Character.IsAnimPlay("cover@weapon@grenade", "low_l_throw") && !Game.Player.Character.IsAnimPlay("cover@weapon@grenade", "low_r_throw");
            bool flag3 = !Game.Player.Character.IsAnimPlay("move_jump", "dive_start_run") && !Game.Player.Character.IsAnimPlay("anim@veh@btype@side_ds@base", "dead_fall_out") && !Game.Player.Character.IsAnimPlay("move_climb", "clamberpose_to_dive_angled_20") && !Game.Player.Character.IsAnimPlay("amb@world_human_sunbathe@male@front@enter", "enter") && !Game.Player.Character.IsAnimPlay("missfam5_yoga", "c6_fail_to_start");
            bool flag4 = !Game.Player.Character.IsAnimPlay("move_strafe@roll_fps", "combatroll_fwd_p1_00") && !Game.Player.Character.IsAnimPlay("move_strafe@roll", "combatroll_bwd_p1_180");
            bool flag5 = !Game.Player.Character.IsAnimPlay("veh@bike@chopper@front@ps", "jump_out") && !Game.Player.Character.IsAnimPlay("veh@bike@chopper@front@ds", "jump_out");
            bool flag6 = !Game.Player.Character.IsAnimPlay("veh@bicycle@bmx@front@ds", "jump_out") && !Game.Player.Character.IsAnimPlay("veh@bicycle@bmx@front@ps", "jump_out");
            bool flag7 = Game.Player.Character.IsAnimPlay("veh@bike@chopper@front@ps", "jump_out") || Game.Player.Character.IsAnimPlay("veh@bike@chopper@front@ds", "jump_out");
            bool flag8 = Game.IsControlPressed(Control.Jump) && !Game.IsControlPressed(Control.MoveUpOnly) && !Game.IsControlPressed(Control.MoveDownOnly);
            bool flag9 = Game.IsControlPressed(Control.Jump) && !Game.IsControlPressed(Control.MoveRightOnly) && !Game.IsControlPressed(Control.MoveLeftOnly);
            bool flag10 = !Function.Call<bool>(Hash.IS_HUD_COMPONENT_ACTIVE, (InputArgument)19) && !Game.Player.Character.IsAnimPlay("weapon@w_sp_jerrycan", "holster_crouch");
            if (flag3 && (Prone.goProneIdle == 2 || Prone.goIdle & flag4 & flag5 & flag6 || flag9 & flag7 || !Prone.FlipChanger && Game.IsControlJustReleased(Control.Aim) || Game.IsControlJustReleased(Control.MoveUpOnly) || Game.IsControlJustReleased(Control.MoveDownOnly) || Game.IsControlJustReleased(Control.Jump) && Game.IsControlJustReleased(Control.Aim)) && Main.blockScopeCameraPlay)
            {
                float blendInSpeed = Prone.goIdle ? 8f : 8f;
                string[] strArray1;
                if (!Prone.onCar || (double)Prone.speedVector.Y < 5.0)
                {
                    if (!Prone.FlipChanger)
                        strArray1 = new string[2]
                        {
              "move_crawlprone2crawlback",
              "back"
                        };
                    else
                        strArray1 = new string[2]
                        {
              "move_crawlprone2crawlfront",
              "front"
                        };
                }
                else
                    strArray1 = new string[2]
                    {
            "move_injured_ground",
            "front_intro"
                    };
                string[] strArray2 = strArray1;
                float playbackRate = Prone.goIdle ? 0.3f : (!Prone.onCar || (double)Prone.speedVector.Y < 5.0 ? (Prone.FlipChanger ? 0.8f : 0.5f) : 0.8f);
                if (Game.Player.Character.IsAnimPlay(strArray2[0], strArray2[1]))
                {
                    if (Main.TransitionAction != 0)
                        Main.TransitionAction = 0;
                    if (Prone.goProneIdle == 2)
                        Prone.goProneIdle = 0;
                    if (Prone.goIdle)
                        Prone.goIdle = false;
                }
                if (Game.Player.Character.IsAnimPlay(strArray2[0], strArray2[1]) || Game.Player.Character.IsAnimPlay("misstrevor2ig_11", "biker_jump_on"))
                    return;
                Game.Player.Character.Task.PlayAnimation(strArray2[0], strArray2[1], blendInSpeed, -4f, -1, AnimationFlags.StayInEndFrame | AnimationFlags.ForceStart, playbackRate);
                if (Main.TransitionAction != 0)
                    Main.TransitionAction = 0;
                if (Prone.goProneIdle == 2)
                    Prone.goProneIdle = 0;
                if (Prone.goIdle)
                    Prone.goIdle = false;
            }
            else if (Game.IsControlPressed(Control.MoveLeftOnly) && Prone.FlipChanger && Prone.goProneIdle == 0 && !Prone.scopetriger && !Prone.onCar)
            {
                Game.Player.Character.Heading += 0.75f;
                if (!flag8 || Game.IsControlPressed(Control.MoveRightOnly) || Game.Player.Character.IsAnimPlay("veh@bike@chopper@front@ps", "jump_out"))
                    return;
                Game.Player.Character.Task.PlayAnimation("veh@bike@chopper@front@ps", "jump_out", 8f, -4f, 800, AnimationFlags.StayInEndFrame, 0.6f);
                Prone.goIdle = true;
            }
            else if (Game.IsControlPressed(Control.MoveRightOnly) && Prone.FlipChanger && Prone.goProneIdle == 0 && !Prone.scopetriger && !Prone.onCar)
            {
                Game.Player.Character.Heading -= 0.75f;
                if (!flag8 || Game.IsControlPressed(Control.MoveLeftOnly) || Game.Player.Character.IsAnimPlay("veh@bike@chopper@front@ds", "jump_out"))
                    return;
                Game.Player.Character.Task.PlayAnimation("veh@bike@chopper@front@ds", "jump_out", 8f, -4f, 800, AnimationFlags.StayInEndFrame, 0.6f);
                Prone.goIdle = true;
            }
            else if (((!Game.IsControlPressed(Control.MoveUpOnly) ? 0 : (Main.blockScopeCameraPlay ? 1 : 0)) & (flag1 ? 1 : 0) & (flag4 ? 1 : 0)) != 0 && Prone.goProneIdle == 0)
            {
                string[] strArray3;
                if (!Prone.onCar || (double)Prone.speedVector.Y < 5.0)
                {
                    if (!Prone.FlipChanger)
                        strArray3 = new string[2]
                        {
              "move_crawl",
              "onback_fwd"
                        };
                    else
                        strArray3 = new string[2]
                        {
              "move_crawl",
              "onfront_fwd"
                        };
                }
                else
                    strArray3 = new string[2]
                    {
            "move_injured_ground",
            "sider_loop"
                    };
                string[] strArray4 = strArray3;
                if (Game.Player.Character.IsAnimPlay(strArray4[0], strArray4[1]) || Prone.ProneGrabVehicleAction != 0 && Prone.ProneGrabVehicleAction != 4)
                    return;
                Game.Player.Character.Task.PlayAnimation(strArray4[0], strArray4[1], 8f, -4f, -1, AnimationFlags.Loop, 0.0f);
            }
            else if (((!Game.IsControlPressed(Control.MoveDownOnly) || !Main.blockScopeCameraPlay ? 0 : (Prone.goProneIdle == 0 ? 1 : 0)) & (flag1 ? 1 : 0) & (flag4 ? 1 : 0)) != 0)
            {
                string[] strArray5;
                if (!Prone.onCar || (double)Prone.speedVector.Y < 5.0)
                {
                    if (!Prone.FlipChanger)
                        strArray5 = new string[2]
                        {
              "move_crawl",
              "onback_bwd"
                        };
                    else
                        strArray5 = new string[2]
                        {
              "move_crawl",
              "onfront_bwd"
                        };
                }
                else
                    strArray5 = new string[2]
                    {
            "move_crawl",
            "onfront_bwd"
                    };
                string[] strArray6 = strArray5;
                if (Game.Player.Character.IsAnimPlay(strArray6[0], strArray6[1]) || Prone.ProneGrabVehicleAction != 0 && Prone.ProneGrabVehicleAction != 4)
                    return;
                Game.Player.Character.Task.PlayAnimation(strArray6[0], strArray6[1], 8f, -4f, -1, AnimationFlags.Loop, 0.0f);
            }
            else if (((!Game.IsControlPressed(Control.MoveLeftOnly) || Prone.goProneIdle != 0 ? 0 : (!Prone.FlipChanger ? 1 : 0)) & (flag8 ? 1 : 0)) != 0 && !Game.Player.Character.IsAnimPlay("veh@bicycle@bmx@front@ds", "jump_out"))
            {
                Game.Player.Character.Task.ClearAll();
                Game.Player.Character.Heading -= 2f;
                Game.Player.Character.Task.PlayAnimation("veh@bicycle@bmx@front@ds", "jump_out", 8f, -4f, 1000, AnimationFlags.StayInEndFrame, 0.5f);
                Prone.goIdle = true;
            }
            else if (((!Game.IsControlPressed(Control.MoveRightOnly) || Prone.goProneIdle != 0 ? 0 : (!Prone.FlipChanger ? 1 : 0)) & (flag8 ? 1 : 0)) != 0 && !Game.Player.Character.IsAnimPlay("veh@bicycle@bmx@front@ps", "jump_out"))
            {
                Game.Player.Character.Task.ClearAll();
                Game.Player.Character.Heading += 2f;
                Game.Player.Character.Task.PlayAnimation("veh@bicycle@bmx@front@ps", "jump_out", 8f, -4f, 1000, AnimationFlags.StayInEndFrame, 0.5f);
                Prone.goIdle = true;
            }
            else
            {
                if (!Game.IsControlPressed(Control.Jump) && !Prone.FlipChanger)
                {
                    if (Game.IsControlPressed(Control.MoveRightOnly))
                        Game.Player.Character.Heading -= 0.75f;
                    if (Game.IsControlPressed(Control.MoveLeftOnly))
                        Game.Player.Character.Heading += 0.75f;
                }
                if (((!Game.IsControlJustPressed(Control.Sprint) || Prone.goProneIdle != 0 || Prone.onCar ? 0 : (Main.blockScopeCameraPlay ? 1 : 0)) & (flag4 ? 1 : 0) & (flag10 ? 1 : 0) & (flag2 ? 1 : 0)) == 0 || Game.Player.Character.IsRunning)
                    return;
                string[] strArray7;
                if (!Prone.FlipChanger)
                    strArray7 = new string[2]
                    {
            "move_strafe@roll",
            "combatroll_bwd_p1_180"
                    };
                else
                    strArray7 = new string[2]
                    {
            "move_strafe@roll_fps",
            "combatroll_fwd_p1_00"
                    };
                string[] strArray8 = strArray7;
                Game.Player.Character.Task.PlayAnimation(strArray8[0], strArray8[1], 8f, -4f, 600, AnimationFlags.StayInEndFrame, 0.0f);
                Prone.FlipChanger = !Prone.FlipChanger;
                Prone.goIdle = true;
            }
        }

        public static void ProneGrabVehicle()
        {
            if (!Prone.onCar && Prone.ProneGrabVehicleAction > 0)
            {
                Prone.ProneGrabVehicleAction = 0;
                if (!Game.Player.Character.CanRagdoll)
                    Game.Player.Character.CanRagdoll = true;
                if (Game.Player.Character.IsAnimPlay("misstrevor2ig_11", "biker_jump_on"))
                    Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("misstrevor2ig_11", "biker_jump_on"));
            }
            if (Prone.onCar)
            {
                if (!Game.Player.Character.IsAnimPlay("misstrevor2ig_11", "biker_jump_on"))
                {
                    if (Game.IsControlPressed(Control.MoveRightOnly))
                        Game.Player.Character.Heading -= 0.75f;
                    if (Game.IsControlPressed(Control.MoveLeftOnly))
                        Game.Player.Character.Heading += 0.75f;
                }
                if (Game.IsControlPressed(Control.Jump) && Prone.ProneGrabVehicleAction != 1)
                    Prone.ProneGrabVehicleAction = 1;
                if (Game.IsControlJustReleased(Control.Jump) && Prone.ProneGrabVehicleAction == 1)
                    Prone.ProneGrabVehicleAction = 2;
            }
            switch (Prone.ProneGrabVehicleAction)
            {
                case 1:
                    if (Game.Player.Character.IsAnimPlay("misstrevor2ig_11", "biker_jump_on"))
                        break;
                    Game.Player.Character.CanRagdoll = false;
                    Game.Player.Character.Task.PlayAnimation("misstrevor2ig_11", "biker_jump_on", 8f, -4f, -1, AnimationFlags.StayInEndFrame, 0.8f);
                    break;
                case 2:
                    Game.Player.Character.Task.PlayAnimation("misstrevor2ig_11", "biker_jump_on", 8f, -4f, -1, AnimationFlags.StayInEndFrame, (double)Prone.speedVector.Y < 5.0 ? 0.8f : 0.4f);
                    Prone.ProneGrabVehicleAction = 3;
                    break;
                case 3:
                    if ((double)Game.Player.Character.GetAnimTime("misstrevor2ig_11", "biker_jump_on") <= 0.10000000149011612)
                        break;
                    Prone.ProneGrabVehicleAction = 4;
                    break;
                case 4:
                    if (Game.Player.Character.IsAnimPlay("misstrevor2ig_11", "biker_jump_on"))
                        break;
                    Game.Player.Character.CanRagdoll = true;
                    Prone.ProneGrabVehicleAction = 0;
                    break;
            }
        }

        public static void ProneCansel()
        {
            if (!Game.Player.Character.IsFalling && !Game.Player.Character.IsRagdoll && (double)Game.Player.Character.SubmersionLevel <= 0.20000000298023224)
                return;
            if ((double)Game.Player.Character.SubmersionLevel > 0.20000000298023224)
            {
                Game.Player.Character.Ragdoll(2000);
                Game.Player.Character.SetConfigFlag(PedConfigFlagToggles.DisablePedConstraints, false);
                Main.SetStance(Main.Stance.Standing);
            }
            else
            {
                Game.Player.Character.SetConfigFlag(PedConfigFlagToggles.DisablePedConstraints, false);
                Game.Player.Character.Task.StandStill(1);
                Main.SetStance(Main.Stance.Standing);
            }
        }

        public static void mf_aim()
        {
            if (Game.IsControlJustReleased(Control.Aim) && !Prone.scopetriger && Prone.ThrowAction == 0 && !Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.Thrown) && !Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.Melee) && !Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.Unarmed))
                Game.Player.Character.TaskAimGunScripted("SCRIPTED_GUN_TASK_PLANE_WING");
            if (Utilits.IsFollowPedCamViewMode(Utilits.ViewMode.FIRST_PERSON) && Game.Player.Character.GetScriptTaskStatus(ScriptTaskNameHash.AimGunScripted) == ScriptTaskStatus.Performing)
            {
                if ((double)GameplayCamera.RelativeHeading > 84.0 && (double)Game.GetControlValueNormalized(Control.LookLeftRight) < 0.0)
                {
                    Game.DisableControlThisFrame(Control.LookLeftRight);
                    Game.DisableControlThisFrame(Control.LookLeftOnly);
                }
                if ((double)GameplayCamera.RelativeHeading < -84.0 && (double)Game.GetControlValueNormalized(Control.LookLeftRight) > 0.0)
                {
                    Game.DisableControlThisFrame(Control.LookLeftRight);
                    Game.DisableControlThisFrame(Control.LookRightOnly);
                }
            }
            Prone.mf_aim_WeaponSniper();
            Prone.aim_WeaponMelee("veh@driveby@first_person@passenger_left_handed@throw", "throw_90l", "veh@driveby@first_person@passenger_left_handed@throw", "throw_90l", "veh@drivebybike@sport@front@grenade", "throw_0");
            Prone.aim_WeaponThrown("cover@weapon@grenade", "hi_r_pinpull", "cover@weapon@grenade", "hi_r_corner_cook", "cover@weapon@grenade", "low_r_centre_throw");
            Prone.aim_WeaponThrown_PetrolCan("move_crawlprone2crawlfront", "front", 0.8f);
        }

        public static void mb_aim()
        {
            if (Game.IsControlJustPressed(Control.Aim) && !Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.Melee) && !Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.Thrown) && !Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.PetrolCan))
            {
                if (!Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.Sniper) && !Utilits.IsFollowPedCamViewMode(Utilits.ViewMode.FIRST_PERSON))
                {
                    Prone.AimOnBackCamera = World.CreateCamera(GameplayCamera.GetOffsetPosition(new Vector3(0.0f, -4f, 0.0f)), GameplayCamera.Rotation, GameplayCamera.FieldOfView);
                    World.RenderingCamera = Prone.AimOnBackCamera;
                }
                Game.Player.Character.TaskAimGunScripted(Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.Pistol) || Game.Player.Character.IsUsingWeaponMicroSMG() ? "SCRIPTED_GUN_TASK_PRONE_BACK" : "SCRIPTED_GUN_TASK_PRONE_BACK_RIFLE");
            }
            if (Prone.AimOnBackCamera == (Camera)null && Game.Player.Character.GetScriptTaskStatus(ScriptTaskNameHash.AimGunScripted) == ScriptTaskStatus.Performing)
            {
                if ((double)GameplayCamera.RelativeHeading > 84.0 && (double)Game.GetControlValueNormalized(Control.LookLeftRight) < 0.0)
                {
                    Game.DisableControlThisFrame(Control.LookLeftRight);
                    Game.DisableControlThisFrame(Control.LookLeftOnly);
                }
                if ((double)GameplayCamera.RelativeHeading < -84.0 && (double)Game.GetControlValueNormalized(Control.LookLeftRight) > 0.0)
                {
                    Game.DisableControlThisFrame(Control.LookLeftRight);
                    Game.DisableControlThisFrame(Control.LookRightOnly);
                }
            }
            if (Prone.AimOnBackCamera != (Camera)null && Prone.AimOnBackCamera.Exists())
            {
                Hud.ShowComponentThisFrame(HudComponent.Reticle);
                float y = Utilits.IsFollowPedCamViewMode(Utilits.ViewMode.THIRD_PERSON_NEAR) ? -2f : (Utilits.IsFollowPedCamViewMode(Utilits.ViewMode.THIRD_PERSON_MEDIUM) ? -1f : (Utilits.IsFollowPedCamViewMode(Utilits.ViewMode.THIRD_PERSON_FAR) ? -0.5f : 0.0f));
                Prone.AimOnBackCamera.Rotation = GameplayCamera.Rotation;
                Prone.AimOnBackCamera.Position = GameplayCamera.GetOffsetPosition(new Vector3(0.0f, y, 0.0f));
                GameplayCamera.SetThirdPersonCameraRelativeHeadingLimitsThisUpdate(-125f, 140f);
                if (Game.IsControlJustReleased(Control.Aim))
                {
                    Prone.AimOnBackCamera.Delete();
                    Prone.AimOnBackCamera = (Camera)null;
                    World.RenderingCamera = (Camera)null;
                }
            }
            Prone.aim_WeaponMelee("melee@small_wpn@streamed_core", "counter_attack_l", "cover@weapon@grenade", "low_l_throw", "cover@weapon@grenade", "low_r_throw");
            Prone.aim_WeaponThrown("cover@weapon@grenade", "low_r_pinpull", "cover@weapon@grenade", "low_r_centre_cook", "cover@weapon@grenade", "low_r_throw_long");
            Prone.aim_WeaponThrown_PetrolCan("move_crawlprone2crawlback", "back", 0.5f);
        }

        public static void mf_aim_WeaponSniper()
        {
            if (Prone.SOCTexture)
            {
                int num = (int)Utilits.LerpTime(0.0f, (float)byte.MaxValue, Prone.TimeLearpModifier, 10f);
                Function.Call(Hash.DRAW_SCALEFORM_MOVIE, new InputArgument[10]
                {
          (InputArgument) Prone.sd,
          (InputArgument) 0.5f,
          (InputArgument) 0.5f,
          (InputArgument) 1f,
          (InputArgument) 1f,
          (InputArgument) 5,
          (InputArgument) (int) byte.MaxValue,
          (InputArgument) 155,
          (InputArgument) 100,
          (InputArgument) 0
                });
            }
            if (Prone.ScopeOutCamera != (Camera)null)
            {
                Prone.ScopeOutCamera.FarDepthOfField = Function.Call<float>(Hash.GET_FINAL_RENDERED_CAM_FAR_DOF);
                Prone.ScopeOutCamera.NearDepthOfField = Function.Call<float>(Hash.GET_FINAL_RENDERED_CAM_NEAR_DOF);
                Prone.ScopeOutCamera.FarClip = Function.Call<float>(Hash.GET_FINAL_RENDERED_CAM_FAR_CLIP);
                Prone.ScopeOutCamera.NearClip = Function.Call<float>(Hash.GET_FINAL_RENDERED_CAM_NEAR_CLIP);
                Prone.ScopeOutCamera.MotionBlurStrength = Function.Call<float>(Hash.GET_FINAL_RENDERED_CAM_MOTION_BLUR_STRENGTH);
                Prone.ScopeOutCamera.FieldOfView = GameplayCamera.FieldOfView;
                Prone.ScopeOutCamera.Rotation = Game.Player.Character.Rotation;
                Function.Call(Hash.FORCE_ALL_HEADING_VALUES_TO_ALIGN, (InputArgument)(Entity)Game.Player.Character);
                Function.Call(Hash.SET_CAM_CONTROLS_MINI_MAP_HEADING, (InputArgument)Prone.ScopeOutCamera, (InputArgument)true);
            }
            if (Prone.SOCEnable)
            {
                float min = Utilits.IsFollowPedCamViewMode(Utilits.ViewMode.FIRST_PERSON) ? 1f : 1.5f;
                float num1 = Utilits.IsFollowPedCamViewMode(Utilits.ViewMode.FIRST_PERSON) ? 0.6f : 1.2f;
                if (Prone.scopetriger)
                    Prone.scopetriger = false;
                float num2 = Utilits.LerpTime(min, 0.005f, Prone.TimeLearpModifier, 10f * Game.TimeScale);
                double num3 = (double)Function.Call<float>(Hash.SET_TIMECYCLE_MODIFIER_STRENGTH, (InputArgument)num2);
                if (Prone.ScopeOutCamera == (Camera)null && !Game.Player.Character.IsVisible)
                {
                    Prone.ScopeOutCamera = World.CreateCamera(Game.Player.Character.GetOffsetPosition(new Vector3(0.16f, 0.7f, -0.45f)), Game.Player.Character.Rotation, GameplayCamera.FieldOfView);
                    World.RenderingCamera = Prone.ScopeOutCamera;
                }
                if ((double)num2 <= (double)num1 && !Game.Player.Character.IsVisible)
                {
                    Game.Player.Character.IsVisible = true;
                    Prone.SOCDeltaTime = Environment.TickCount;
                    Prone.SOCPosTransition = 1;
                }
                if ((double)num2 <= 0.004999999888241291)
                {
                    Function.Call(Hash.CLEAR_TIMECYCLE_MODIFIER);
                    Prone.TimeLearpModifier = Environment.TickCount;
                    Prone.SOCEnable = false;
                }
            }
            switch (Prone.SOCPosTransition)
            {
                case 1:
                    float num4 = Utilits.IsFollowPedCamViewMode(Utilits.ViewMode.FIRST_PERSON) ? 0.78f : 0.78f;
                    Prone.ScopeOutCamera.NearClip = Function.Call<float>(Hash.GET_FINAL_RENDERED_CAM_NEAR_CLIP) - 0.01f;
                    float y1 = Utilits.LerpTime(0.86f, num4, Prone.SOCDeltaTime, 10f * Game.TimeScale);
                    Prone.ScopeOutCamera.Position = Game.Player.Character.GetOffsetPosition(new Vector3(0.164f, y1, -0.66f));
                    if (Prone.SOCTexture)
                        Prone.SOCTexture = false;
                    if ((double)y1 > (double)num4)
                        return;
                    Prone.ScopeOutCamera.Position = Game.Player.Character.GetOffsetPosition(new Vector3(0.164f, num4, -0.66f));
                    Prone.SOCDeltaTime = Environment.TickCount;
                    Prone.SOCPosTransition = Utilits.IsFollowPedCamViewMode(Utilits.ViewMode.FIRST_PERSON) ? 2 : 3;
                    break;
                case 2:
                    float x = Utilits.LerpTime(0.164f, 0.1f, Prone.SOCDeltaTime, 10f * Game.TimeScale);
                    float y2 = Utilits.LerpTime(0.78f, 0.73f, Prone.SOCDeltaTime, 10f * Game.TimeScale);
                    float z = Utilits.LerpTime(-0.66f, -0.64f, Prone.SOCDeltaTime, 10f * Game.TimeScale);
                    Prone.ScopeOutCamera.Position = Game.Player.Character.GetOffsetPosition(new Vector3(x, y2, z));
                    if ((double)x > 0.10000000149011612)
                        return;
                    Prone.ScopeOutCamera.Position = Game.Player.Character.GetOffsetPosition(new Vector3(0.1f, 0.73f, 0.64f));
                    Prone.SOCDeltaTime = Environment.TickCount;
                    Prone.ScopeOutCamera.Delete();
                    World.RenderingCamera = (Camera)null;
                    Prone.ScopeOutCamera = (Camera)null;
                    Prone.SOCPosTransition = 0;
                    break;
                case 3:
                    Utilits.SetFollowPedCamViewMode(Utilits.ViewMode.FIRST_PERSON);
                    Vector3 offset = Utilits.Slerp(new Vector3(0.164f, 0.78f, -0.692f), new Vector3(0.5f, 0.65f, -0.66f), new Vector3(0.164f, -0.45f, -0.34f), Prone.SOCDeltaTime, 10f * Game.TimeScale);
                    Prone.ScopeOutCamera.Position = Game.Player.Character.GetOffsetPosition(offset);
                    if ((double)offset.Y > -0.2199999988079071)
                        return;
                    Utilits.SetFollowPedCamViewMode(Utilits.ViewMode.THIRD_PERSON_MEDIUM);
                    Prone.ScopeOutCamera.Position = Game.Player.Character.GetOffsetPosition(new Vector3(0.164f, -0.45f, -0.36f));
                    Prone.SOCDeltaTime = Environment.TickCount;
                    Prone.SOCPosTransition = 4;
                    break;
                case 4:
                    Prone.ScopeOutCamera.Position = Vector3.Lerp(Game.Player.Character.GetOffsetPosition(new Vector3(0.164f, -0.45f, -0.36f)), GameplayCamera.GetOffsetPosition(new Vector3(0.0f, 0.0f, 0.0f)), (float)((double)(Environment.TickCount - Prone.SOCDeltaTime) / 10000.0 * 10.0) * Game.TimeScale);
                    if ((double)(Environment.TickCount - Prone.SOCDeltaTime) / 10000.0 * 10.0 * (double)Game.TimeScale < 1.0)
                        return;
                    Prone.SOCDeltaTime = Environment.TickCount;
                    Prone.ScopeOutCamera.Delete();
                    World.RenderingCamera = (Camera)null;
                    Prone.ScopeOutCamera = (Camera)null;
                    Prone.SOCPosTransition = 0;
                    break;
            }
            if (Prone.SOCWaitControlEnable)
                Game.DisableAllControlsThisFrame();
            if (Prone.SOCWaitControlEnable && Prone.SOCPosTransition == 0)
                Prone.SOCWaitControlEnable = false;
            if (Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.Sniper) && Game.Player.Character.GetConfigFlag(PedConfigFlagToggles.IsAimingGun) && Game.IsControlJustPressed(Control.Aim))
            {
                Prone.scopetriger = true;
                Game.Player.Character.IsVisible = false;
                Game.Player.Character.TaskAimGunScripted("SCRIPTED_GUN_TASK_PRONE_BACK_RIFLE");
            }
            if (Prone.scopetriger && !Game.Player.Character.IsAnimPlay("missfbi3_sniping", "prone_michael"))
                Game.Player.Character.SetConfigFlag(PedConfigFlagToggles.IsStanding, false);
            if (Game.Player.Character.Weapons.Current.AmmoInClip == 0 && Prone.scopetriger)
            {
                Game.Player.Character.Task.PlayAnimation("misstrevor2ig_9b", "reload", 8f, -4f, -1, AnimationFlags.UpperBodyOnly | AnimationFlags.Secondary, 0.0f);
                Game.Player.Character.Task.PlayAnimation("move_crawlprone2crawlfront", "front", 8f, -4f, -1, AnimationFlags.StayInEndFrame, 0.8f);
                Game.Player.Character.IsVisible = true;
                Script.Wait(100);
                Prone.scopetriger = false;
            }
            if (!Prone.scopetriger || !Game.IsControlJustReleased(Control.Aim) || Prone.SOCPosTransition != 0 || Prone.SOCEnable)
                return;
            Game.Player.Character.FacePosition(World.Raycast(GameplayCamera.Position, GameplayCamera.GetOffsetPosition(new Vector3(0.0f, 1000f, -1000f)), (IntersectFlags)(-1), (Entity)Game.Player.Character).HitPosition);
            Game.Player.Character.Task.ClearAllImmediately();
            Game.Player.Character.Task.PlayAnimation("missfbi3_sniping", "prone_michael", 1000f, -4f, -1, AnimationFlags.StayInEndFrame, 0.0f);
            if (!Utilits.IsFollowPedCamViewMode(Utilits.ViewMode.FIRST_PERSON))
            {
                Function.Call(Hash.SET_TIMECYCLE_MODIFIER, (InputArgument)"rply_vignette");
            }
            else
            {
                Function.Call(Hash.SET_TIMECYCLE_MODIFIER, (InputArgument)"BarryFadeOut");
                Prone.SOCTexture = true;
            }
            Prone.TimeLearpModifier = Environment.TickCount;
            Prone.SOCWaitControlEnable = true;
            Prone.SOCEnable = true;
        }

        public static void Exp_Obj()
        {
            foreach (KeyValuePair<Prop, int> keyValuePair in Prone.throwingObjectDict.ToList<KeyValuePair<Prop, int>>())
            {
                Prop key = keyValuePair.Key;
                int num1 = keyValuePair.Value;
                bool flag1 = key.Model == (Model)"prop_gas_grenade" || key.Model == (Model)"w_ex_grenadefrag" || key.Model == (Model)"w_ex_pe" || key.Model == (Model)"w_ex_pipebomb" || key.Model == (Model)"w_ex_apmine" || key.Model == (Model)"w_ex_grenadesmoke";
                bool flag2 = key.Model == (Model)"w_ex_molotov";
                bool flag3 = key.Model == (Model)"w_ch_jerrycan" || key.Model == (Model)"w_am_jerrycan" || key.Model == (Model)"w_am_jerrycan_sf";
                bool flag4 = key.Model == (Model)"w_am_flare";
                bool flag5 = key.Model == (Model)"w_am_fire_exting";
                bool flag6 = key.Model == (Model)"w_ex_snowball";
                Model model = key.Model;
                bool flag7 = model.Hash == -197610887;
                bool flag8 = key.Model == (Model)"w_am_baseball";
                ExplosionType type = key.Model == (Model)"prop_gas_grenade" ? ExplosionType.BZGas : (key.Model == (Model)"w_ex_grenadefrag" ? ExplosionType.Grenade : (key.Model == (Model)"w_ex_pe" ? ExplosionType.StickyBomb : (key.Model == (Model)"w_ex_pipebomb" ? ExplosionType.PipeBomb : (key.Model == (Model)"w_ex_apmine" ? ExplosionType.ProxMine : ExplosionType.SmokeG))));
                if (key.Exists())
                {
                    if (num1 == 1)
                    {
                        if (flag3)
                            Function.Call(Hash.ADD_PETROL_TRAIL_DECAL_INFO, (InputArgument)key.Position.X, (InputArgument)key.Position.Y, (InputArgument)World.GetGroundHeight(key.Position), (InputArgument)1f);
                        if (flag1)
                        {
                            if (!Prone.ExplosionTimer.Enabled)
                                Prone.ExplosionTimer.Start();
                            if (Prone.ExplosionTimer.Enabled && Game.GameTime > Prone.ExplosionTimer.Waiter + 3000)
                            {
                                World.AddExplosion(key.Position, type, 5f, 1f, Game.Player.Character);
                                key.Delete();
                                Prone.throwingObjectDict.Remove(key);
                                Prone.ExplosionTimer.Enabled = false;
                            }
                        }
                        if (!flag1 && !key.IsAttached() && !Game.Player.Character.IsAnimPlay("cover@weapon@grenade", "hi_r_corner_cook"))
                        {
                            int num2;
                            if (key.HasCollided)
                            {
                                model = key.Model;
                                if (model.Hash != Game.GenerateHash("w_ch_jerrycan"))
                                {
                                    model = key.Model;
                                    if (model.Hash != Game.GenerateHash("w_am_jerrycan"))
                                    {
                                        model = key.Model;
                                        num2 = model.Hash != Game.GenerateHash("w_am_jerrycan_sf") ? 1 : 0;
                                        goto label_18;
                                    }
                                }
                            }
                            num2 = 0;
                        label_18:
                            if (num2 != 0)
                            {
                                if (flag6)
                                    World.AddExplosion(key.Position, ExplosionType.SnowBall, 0.5f, 0.0f, Game.Player.Character);
                                if (flag2)
                                    World.AddExplosion(key.Position, ExplosionType.Molotov1, 7f, 1f, Game.Player.Character);
                                key.Delete();
                                Prone.throwingObjectDict.Remove(key);
                                break;
                            }
                            if (flag5)
                            {
                                Function.Call(Hash.STOP_FIRE_IN_RANGE, (InputArgument)key.Position.X, (InputArgument)key.Position.Y, (InputArgument)key.Position.Z, (InputArgument)3f);
                                Game.Player.Character.Weapons.CurrentWeaponObject.IsVisible = true;
                                Prone.throwingObjectDict[key] = 2;
                                break;
                            }
                            if (flag4)
                            {
                                ParticleEffectAsset asset = new ParticleEffectAsset("core");
                                asset.Request();
                                World.CreateParticleEffect(asset, "exp_grd_flare", (Entity)key);
                                asset.MarkAsNoLongerNeeded();
                                Prone.throwingObjectDict[key] = 2;
                                break;
                            }
                            if (flag8 | flag7)
                            {
                                key.MarkAsNoLongerNeeded();
                                Prone.throwingObjectDict.Remove(key);
                                break;
                            }
                            if (flag3 && (double)key.Position.DistanceTo(Game.Player.Character.Position) > 2.0)
                            {
                                if ((double)key.Speed > 0.05000000074505806 && (double)key.Speed < 0.10000000149011612)
                                    Function.Call(Hash.ADD_PETROL_DECAL, new InputArgument[6]
                                    {
                    (InputArgument) key.Position.X,
                    (InputArgument) key.Position.Y,
                    (InputArgument) World.GetGroundHeight(key.Position),
                    (InputArgument) 1f,
                    (InputArgument) 3f,
                    (InputArgument) 1f
                                    });
                                if ((double)key.Speed <= 0.5)
                                {
                                    Function.Call(Hash.END_PETROL_TRAIL_DECALS);
                                    Function.Call(Hash.ADD_PETROL_DECAL, new InputArgument[6]
                                    {
                    (InputArgument) (key.Position.X + Game.Player.Character.ForwardVector.X * 1f),
                    (InputArgument) (key.Position.Y + Game.Player.Character.ForwardVector.Y * 1f),
                    (InputArgument) World.GetGroundHeight(key.Position + Game.Player.Character.ForwardVector * 1f),
                    (InputArgument) 4f,
                    (InputArgument) 3f,
                    (InputArgument) 1f
                                    });
                                    Game.Player.Character.Weapons.CurrentWeaponObject.IsVisible = true;
                                    Prone.throwingObjectDict[key] = 2;
                                    break;
                                }
                            }
                        }
                    }
                    if (num1 == 2)
                    {
                        if (!Prone.TimerKeepThrowObj.Enabled)
                            Prone.TimerKeepThrowObj.Start();
                        if (Prone.TimerKeepThrowObj.Enabled && Game.GameTime > Prone.TimerKeepThrowObj.Waiter + 28000)
                        {
                            key.Delete();
                            Prone.throwingObjectDict.Remove(key);
                            Prone.TimerKeepThrowObj.Enabled = false;
                        }
                        if (key.HasBeenDamagedByAnyWeapon() && Prone.TimerKeepThrowObj.Enabled)
                        {
                            if (flag3)
                                World.AddExplosion(key.Position, ExplosionType.HiOctane, 0.5f, 1f, Game.Player.Character);
                            if (flag5)
                            {
                                World.AddExplosion(key.Position, ExplosionType.Extinguisher, 10f, 1f, Game.Player.Character, false, true);
                                Vector3 position = key.Position;
                                ParticleEffectAsset asset = new ParticleEffectAsset("core");
                                asset.Request();
                                while (!asset.IsLoaded)
                                    Script.Wait(0);
                                World.CreateParticleEffectNonLooped(asset, "exp_extinguisher", position);
                                Function.Call(Hash.STOP_FIRE_IN_RANGE, (InputArgument)position.X, (InputArgument)position.Y, (InputArgument)position.Z, (InputArgument)8f);
                                asset.MarkAsNoLongerNeeded();
                            }
                            if (flag4)
                            {
                                World.RemoveAllParticleEffectsInRange(key.Position, 1f);
                                Vector3 position = key.Position;
                                ParticleEffectAsset asset = new ParticleEffectAsset("scr_bike_adversary");
                                asset.Request();
                                while (!asset.IsLoaded)
                                    Script.Wait(0);
                                Function.Call(Hash.SET_PARTICLE_FX_NON_LOOPED_COLOUR, (InputArgument)40f, (InputArgument)0, (InputArgument)0);
                                World.CreateParticleEffectNonLooped(asset, "scr_adversary_gunsmith_weap_change", position);
                                asset.MarkAsNoLongerNeeded();
                            }
                            key.Delete();
                            Prone.throwingObjectDict.Remove(key);
                            Prone.TimerKeepThrowObj.Enabled = false;
                            break;
                        }
                    }
                }
                else if (!key.Exists())
                    Prone.throwingObjectDict.Remove(key);
            }
        }

        public static void aim_WeaponThrown(
          string pinpullDisc,
          string pinpullName,
          string cookDisc,
          string cookName,
          string throwDisc,
          string throwName)
        {
            bool flag1 = Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.Thrown) || Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.PetrolCan) || Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.FireExtinguisher);
            bool flag2 = !Game.Player.Character.IsUsingWeapon(WeaponHash.Ball) && !Game.Player.Character.IsUsingWeapon(WeaponHash.AcidPackage) && !Game.Player.Character.IsUsingWeapon(WeaponHash.Snowball) && !Game.Player.Character.IsUsingWeapon(WeaponHash.Flare) && !Game.Player.Character.IsUsingWeapon(WeaponHash.Molotov) && !Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.PetrolCan) && !Game.Player.Character.IsUsingWeapon(WeaponHash.FireExtinguisher);
            bool flag3 = !Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.PetrolCan) && !Game.Player.Character.IsUsingWeapon(WeaponHash.FireExtinguisher) && !Game.Player.Character.IsUsingWeapon(WeaponHash.Ball);
            if (Game.IsControlJustReleased(Control.Aim) && Prone.ThrowAction != 0)
                Prone.ThrowAction = !Game.Player.Character.IsAnimPlay(cookDisc, cookName) ? 0 : 4;
            if (Game.IsControlPressed(Control.Aim) & flag1 && Prone.ThrowAction == 0 && !Game.Player.Character.IsAnimPlay(throwDisc, throwName) && Game.Player.Character.Weapons.Current.Ammo != 0)
                Prone.ThrowAction = 5;
            if (Game.IsControlJustReleased(Control.Attack) && (Prone.ThrowAction == 2 || Prone.ThrowAction == 4))
                Prone.ThrowAction = 3;
            if (Game.IsControlJustPressed(Control.Attack) && Prone.ThrowAction == 5 && Game.Player.Character.Weapons.Current.Ammo != 0)
                Prone.ThrowAction = 1;
            if (Prone.ThrowAction > 0)
                Hud.ShowComponentThisFrame(HudComponent.Reticle);
            switch (Prone.ThrowAction)
            {
                case 1:
                    Game.Player.Character.Weapons.CurrentWeaponObject.IsVisible = false;
                    Prone.throwingObject = World.CreateProp(Game.Player.Character.IsUsingWeapon(WeaponHash.BZGas) ? (Model)"prop_gas_grenade" : Game.Player.Character.Weapons.CurrentWeaponObject.Model, Game.Player.Character.Position, true, false);
                    Prone.throwingObject.AttachTo((Entity)Game.Player.Character.Weapons.CurrentWeaponObject, new Vector3(0.0f, 0.0f, 0.0f), Game.Player.Character.IsUsingWeapon(WeaponHash.BZGas) ? new Vector3(0.0f, 0.0f, 180f) : new Vector3(0.0f, 0.0f, 0.0f));
                    Prone.throwingObject.IsVisible = true;
                    if (flag2)
                        Game.Player.Character.Task.PlayAnimation(pinpullDisc, pinpullName, 4f, -4f, -1, AnimationFlags.UpperBodyOnly | AnimationFlags.Secondary, 0.0f);
                    else
                        Game.Player.Character.Task.PlayAnimation(cookDisc, cookName, 4f, -4f, -1, AnimationFlags.Loop | AnimationFlags.UpperBodyOnly | AnimationFlags.Secondary, 0.0f);
                    if (Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.PetrolCan))
                    {
                        Function.Call(Hash.START_PETROL_TRAIL_DECALS, (InputArgument)0.5f);
                        Utilits.CreateParticleFX("core", "ent_sht_petrol", (Entity)Prone.throwingObject, -1, boneFX: false);
                    }
                    if (Game.Player.Character.IsUsingWeapon(WeaponHash.Molotov))
                        Utilits.CreateParticleFX("scr_agencyheist", "sp_fire_trail", (Entity)Prone.throwingObject, 1, 0.3f);
                    Prone.ThrowAction = 2;
                    break;
                case 2:
                    if (!Game.Player.Character.IsAnimPlay(cookDisc, cookName) && (double)Game.Player.Character.GetAnimTime(pinpullDisc, pinpullName) > 0.30000001192092896)
                    {
                        Game.Player.Character.Task.PlayAnimation(cookDisc, cookName, 4f, -4f, -1, AnimationFlags.Loop | AnimationFlags.UpperBodyOnly | AnimationFlags.Secondary, 0.0f);
                        Prone.ThrowAction = 4;
                        break;
                    }
                    if (!Game.Player.Character.IsAnimPlay(cookDisc, cookName) || Game.Player.Character.IsAnimPlay(pinpullDisc, pinpullName))
                        break;
                    Prone.ThrowAction = 4;
                    break;
                case 3:
                    if (!Game.Player.Character.IsAnimPlay(cookDisc, cookName))
                        break;
                    Prone.throwingObjectDict.Add(Prone.throwingObject, 1);
                    Game.Player.Character.Task.PlayAnimation(throwDisc, throwName, 8f, -4f, -1, AnimationFlags.UpperBodyOnly | AnimationFlags.Secondary, 0.0f);
                    Function.Call(Hash.DETACH_ENTITY, (InputArgument)(Entity)Prone.throwingObject, (InputArgument)true, (InputArgument)false);
                    Prone.throwingObject.ApplyForce((GameplayCamera.ForwardVector * 15f + GameplayCamera.UpVector * 15f) / Game.TimeScale);
                    if (Game.Player.Character.IsUsingWeapon(WeaponHash.Ball))
                        Game.Player.Character.Weapons.Remove(Game.Player.Character.Weapons.Current);
                    if (Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.PetrolCan))
                        Game.Player.Character.Weapons.Remove(Game.Player.Character.Weapons.Current);
                    if (Game.Player.Character.IsUsingWeapon(WeaponHash.FireExtinguisher))
                    {
                        Utilits.CreateParticleFX("core", "ent_sht_extinguisher", (Entity)Prone.throwingObject, 1);
                        Game.Player.Character.Weapons.Remove(Game.Player.Character.Weapons.Current);
                    }
                    if (Game.Player.Character.IsUsingWeapon(WeaponHash.Molotov))
                        Utilits.CreateParticleFX("scr_agencyheist", "sp_fire_trail", (Entity)Prone.throwingObject, 1, 0.3f);
                    if (!Game.Player.Character.IsAnimPlay(throwDisc, throwName) && flag3)
                        Game.Player.Character.Weapons.CurrentWeaponObject.IsVisible = true;
                    if (Game.Player.Character.Weapons.Current.Ammo != 0)
                        --Game.Player.Character.Weapons.Current.Ammo;
                    if (Game.Player.Character.Weapons.Current.Ammo == 0)
                        Game.Player.Character.Weapons.Select(WeaponHash.Unarmed, true);
                    Prone.ThrowAction = 0;
                    break;
            }
        }

        public static void aim_WeaponThrown_PetrolCan(string animDist, string animname, float animrate)
        {
            Control control = Game.LastInputMethod == InputMethod.GamePad ? Control.Attack : Control.Attack;
            if (Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.PetrolCan) && !Game.IsControlPressed(Control.Aim))
            {
                if (Game.IsControlJustPressed(Control.Attack))
                {
                    Game.DisableControlThisFrame(Control.Aim);
                    Game.Player.Character.Task.PlayAnimation("weapons@first_person@aim_rng@generic@misc@jerrycan", "fire_intro_med", 8f, -4f, -1, AnimationFlags.StayInEndFrame | AnimationFlags.UpperBodyOnly | AnimationFlags.Secondary, 0.0f);
                    Utilits.CreateParticleFX("core", "ent_sht_petrol", (Entity)Game.Player.Character.Weapons.CurrentWeaponObject, 0, boneFX: false);
                    return;
                }
                if (Game.IsControlJustPressed(control))
                {
                    Game.DisableControlThisFrame(Control.Aim);
                    Game.Player.Character.Task.PlayAnimation("weapons@first_person@aim_rng@generic@misc@jerrycan", "fire_intro_high", 8f, -4f, -1, AnimationFlags.StayInEndFrame | AnimationFlags.UpperBodyOnly | AnimationFlags.Secondary, 0.0f);
                    Utilits.CreateParticleFX("core", "ent_sht_petrol", (Entity)Game.Player.Character.Weapons.CurrentWeaponObject, 0, 1.3f, false);
                    return;
                }
                if (Game.IsControlJustReleased(control))
                {
                    Game.Player.Character.Weapons.CurrentWeaponObject.RemoveParticleEffects();
                    Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("weapons@first_person@aim_rng@generic@misc@jerrycan", "fire_intro_med"));
                    Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("weapons@first_person@aim_rng@generic@misc@jerrycan", "fire_intro_high"));
                    Game.Player.Character.Task.PlayAnimation(animDist, animname, 8f, -4f, -1, AnimationFlags.StayInEndFrame, animrate);
                }
            }
            if (!Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.PetrolCan) || Game.IsControlPressed(Control.Aim) || !Game.IsControlJustPressed(Control.ThrowGrenade) || Game.Player.Character.Weapons.Current.Ammo == 0)
                return;
            Game.DisableControlThisFrame(Control.Aim);
            Game.Player.Character.Task.PlayAnimation("weapons@first_person@aim_rng@generic@misc@jerrycan", "fire_intro_med", 8f, -4f, -1, AnimationFlags.UpperBodyOnly | AnimationFlags.Secondary, 0.0f);
            Function.Call(Hash.ADD_PETROL_DECAL, new InputArgument[6]
            {
        (InputArgument) (Game.Player.Character.Position.X + Game.Player.Character.ForwardVector.X * 1f),
        (InputArgument) (Game.Player.Character.Position.Y + Game.Player.Character.ForwardVector.Y * 1f),
        (InputArgument) World.GetGroundHeight(Game.Player.Character.Position + Game.Player.Character.ForwardVector * 1f),
        (InputArgument) 1f,
        (InputArgument) 2f,
        (InputArgument) 1f
            });
            --Game.Player.Character.Weapons.Current.Ammo;
        }

        public static void aim_WeaponMelee(
          string attackDic,
          string attackName,
          string melleAttacke1Dic,
          string melleAttacke1Name,
          string melleAttacke2Dic,
          string melleAttacke2Name)
        {
            if (!Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.Melee) && !Game.Player.Character.IsUsingWeaponGroup(WeaponGroup.Unarmed))
                return;
            bool flag = !Game.Player.Character.IsAnimPlay(attackDic, attackName) && !Game.Player.Character.IsAnimPlay(melleAttacke1Dic, melleAttacke1Name) && !Game.Player.Character.IsAnimPlay(melleAttacke2Dic, melleAttacke2Name);
            Prone.pedVictum = Utilits.GetPedInFrontOfPlayer();
            Control control = Game.LastInputMethod == InputMethod.GamePad ? Control.VehicleExit : Control.MeleeAttack2;
            if (Game.IsControlJustPressed(Control.Attack) & flag)
            {
                if ((Entity)Prone.pedVictum != (Entity)null && Prone.pedVictum.Exists() && !Prone.pedVictum.IsPlayer && !Prone.meleePedVictum.ContainsKey(Prone.pedVictum))
                    Prone.meleePedVictum.Add(Prone.pedVictum, 1);
                Game.Player.Character.Task.PlayAnimation(attackDic, attackName, 4f, -4f, -1, AnimationFlags.UpperBodyOnly | AnimationFlags.Secondary, 0.0f);
                Prone.ProneMleeAttackAction = 1;
            }
            else if (Game.IsControlJustPressed(Control.MeleeAttack1) & flag)
            {
                if ((Entity)Prone.pedVictum != (Entity)null && Prone.pedVictum.Exists() && !Prone.pedVictum.IsPlayer && !Prone.meleePedVictum.ContainsKey(Prone.pedVictum))
                    Prone.meleePedVictum.Add(Prone.pedVictum, 1);
                Game.Player.Character.Task.PlayAnimation(melleAttacke1Dic, melleAttacke1Name, 4f, -4f, -1, AnimationFlags.UpperBodyOnly | AnimationFlags.Secondary, 0.0f);
                Prone.ProneMleeAttackAction = 2;
            }
            else
            {
                if (!(Game.IsControlJustPressed(control) & flag))
                    return;
                if ((Entity)Prone.pedVictum != (Entity)null && Prone.pedVictum.Exists() && !Prone.pedVictum.IsPlayer && !Prone.meleePedVictum.ContainsKey(Prone.pedVictum))
                    Prone.meleePedVictum.Add(Prone.pedVictum, 1);
                Game.Player.Character.Task.PlayAnimation(melleAttacke2Dic, melleAttacke2Name, 4f, -4f, -1, AnimationFlags.UpperBodyOnly | AnimationFlags.Secondary, 0.0f);
                Prone.ProneMleeAttackAction = 3;
            }
        }
    }
}
