// Decompiled with JetBrains decompiler
// Type: CombatStance.Main
// Assembly: CombatStanceMovement, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 15398906-0FC5-4912-B95F-7B85C0671CB4
// Assembly location: C:\Users\clope\OneDrive\Desktop\CombatStanceMovement.dll

using GTA;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace CombatStance
{
    public class Main : Script
    {
        public static int TransitionAction;
        public static bool debugger;
        public static Main.Stance currentStance;
        private Stopwatch buttonPressTimer;
        public static bool banButton;
        private bool fallofcar;
        private int fallofcartimer;
        public static bool blockScopeCameraPlay;
        public static Entity IgnoreEntity;

        public Main()
        {
            this.Tick += new EventHandler(this.OnTick);
            this.KeyDown += new KeyEventHandler(this.OnKeyDown);
            this.KeyUp += new KeyEventHandler(this.OnKeyUp);
            this.Requests();
            Prone.TimerKeepThrowObj = new Timer(175);
            Prone.ExplosionTimer = new Timer(80);
            Prone.sd = new Scaleform("observatory_scope");
            this.buttonPressTimer = new Stopwatch();
            Prone.throwingObjectDict = new Dictionary<Prop, int>();
            Prone.meleePedVictum = new Dictionary<Ped, int>();
            Prone.FlipChanger = true;
            Prone.idleViewModeTransition = 0;
            Prone.AimOnBackCamera = (Camera)null;
            Prone.ScopeOutCamera = (Camera)null;
            Prone.SOCPosTransition = 0;
            Main.debugger = false;
        }

        private void TransitionStanse()
        {
            bool flag1 = (double)Game.Player.Character.GetAnimTime("move_jump", "dive_start_run") > 0.0099999997764825821 || (double)Game.Player.Character.GetAnimTime("anim@veh@btype@side_ds@base", "dead_fall_out") > 0.0099999997764825821 || (double)Game.Player.Character.GetAnimTime("move_climb", "clamberpose_to_dive_angled_20") > 0.0099999997764825821 || (double)Game.Player.Character.GetAnimTime("amb@world_human_sunbathe@male@front@enter", "enter") > 0.0099999997764825821 || (double)Game.Player.Character.GetAnimTime("missfam5_yoga", "c6_fail_to_start") > 0.0099999997764825821;
            bool flag2 = (double)Game.Player.Character.GetAnimTime("get_up@directional@transition@prone_to_knees@crawl", "front") > 0.0099999997764825821 || (double)Game.Player.Character.GetAnimTime("get_up@directional@movement@from_seated@action", "getup_l_0") > 0.0099999997764825821 || (double)Game.Player.Character.GetAnimTime("missexile2", "enter_crouch_a") > 0.0099999997764825821;
            switch (Main.TransitionAction)
            {
                case 1:
                    if (Game.Player.Character.IsSprinting || Game.Player.Character.IsRunning)
                    {
                        Game.Player.Character.Task.ClearAll();
                        Game.Player.Character.Task.PlayAnimation("move_jump", "dive_start_run", 8f, -4f, 1100, AnimationFlags.None, 0.0f);
                        Main.TransitionAction = 8;
                        break;
                    }
                    if (Game.Player.Character.IsWalking)
                    {
                        Game.Player.Character.Task.ClearAll();
                        Game.Player.Character.Task.PlayAnimation("anim@veh@btype@side_ds@base", "dead_fall_out", 8f, -4f, 700, AnimationFlags.None, 0.2f);
                        Prone.goProneIdle = 1;
                        Main.SetStance(Main.Stance.Prone);
                        Main.TransitionAction = 7;
                        break;
                    }
                    if (Game.Player.Character.IsWalking || Game.Player.Character.IsSprinting || Game.Player.Character.IsRunning)
                        break;
                    Game.Player.Character.Task.ClearAll();
                    Game.Player.Character.Task.PlayAnimation("amb@world_human_sunbathe@male@front@enter", "enter", 8f, -4f, 3200, AnimationFlags.None, 0.0f);
                    Prone.goProneIdle = 1;
                    Main.SetStance(Main.Stance.Prone);
                    Main.TransitionAction = 7;
                    break;
                case 2:
                    if (Game.Player.Character.IsUsingActionMod())
                    {
                        Game.Player.Character.SetResetFlag(PedResetFlagToggles.DisableActionMode, true);
                        Game.Player.Character.Task.PlayAnimation("missexile2", "enter_crouch_a", 4f, -4f, 600, AnimationFlags.StayInEndFrame, 0.0f);
                        Main.SetStance(Main.Stance.Crouching);
                        Main.TransitionAction = 7;
                        break;
                    }
                    Prone.idleViewModeTransition = 1;
                    Main.SetStance(Main.Stance.Crouching);
                    Main.TransitionAction = 0;
                    break;
                case 3:
                    if (Game.Player.Character.IsSprinting || Game.Player.Character.IsRunning)
                    {
                        Game.Player.Character.Task.ClearAll();
                        Game.Player.Character.Task.PlayAnimation("move_climb", "clamberpose_to_dive_angled_20", 8f, -4f, 700, AnimationFlags.None, 0.08f);
                        Main.TransitionAction = 8;
                        break;
                    }
                    if (Game.Player.Character.IsSprinting || Game.Player.Character.IsRunning)
                        break;
                    Game.Player.Character.Task.ClearAllImmediately();
                    Game.Player.Character.Task.PlayAnimation("missfam5_yoga", "c6_fail_to_start", 8f, -4f, 800, AnimationFlags.None, 0.0f);
                    Prone.goProneIdle = 1;
                    Main.SetStance(Main.Stance.Prone);
                    Main.TransitionAction = 7;
                    break;
                case 4:
                    Game.Player.Character.Task.ClearAll();
                    Main.SetStance(Main.Stance.Standing);
                    Main.TransitionAction = 0;
                    break;
                case 5:
                    string[] strArray1;
                    if (!Prone.FlipChanger)
                        strArray1 = new string[2]
                        {
              "get_up@first_person@directional@movement@from_seated@standard",
              "get_up_l_0"
                        };
                    else
                        strArray1 = new string[2]
                        {
              "get_up@first_person@directional@movement@from_knees@standard",
              "getup_l_0"
                        };
                    string[] strArray2 = strArray1;
                    int duration1 = Prone.FlipChanger ? 930 : 1000;
                    float blendInSpeed1 = Prone.FlipChanger ? 1000f : 16f;
                    Game.Player.Character.Task.ClearAll();
                    Game.Player.Character.Task.PlayAnimation(strArray2[0], strArray2[1], blendInSpeed1, -4f, duration1, AnimationFlags.None, 0.0f);
                    if (!Prone.FlipChanger)
                    {
                        Prone.FlipChanger = true;
                        Game.Player.ForcedAim = false;
                    }
                    Main.SetStance(Main.Stance.Standing);
                    Main.TransitionAction = 0;
                    break;
                case 6:
                    string[] strArray3;
                    if (!Prone.FlipChanger)
                        strArray3 = new string[2]
                        {
              "get_up@directional@movement@from_seated@action",
              "getup_l_0"
                        };
                    else
                        strArray3 = new string[2]
                        {
              "get_up@directional@transition@prone_to_knees@crawl",
              "front"
                        };
                    string[] strArray4 = strArray3;
                    int duration2 = Prone.FlipChanger ? 800 : 800;
                    float blendInSpeed2 = Prone.FlipChanger ? 1000f : 16f;
                    Game.Player.Character.Task.ClearAll();
                    Game.Player.Character.Task.PlayAnimation(strArray4[0], strArray4[1], blendInSpeed2, -4f, duration2, AnimationFlags.None, 0.0f);
                    if (Game.Player.Character.IsUsingActionMod())
                    {
                        Game.Player.Character.SetResetFlag(PedResetFlagToggles.DisableActionMode, true);
                        Function.Call(Hash.FORCE_PED_MOTION_STATE, (InputArgument)(Entity)Game.Player.Character, (InputArgument)4000413475U, (InputArgument)true, (InputArgument)0, (InputArgument)false);
                    }
                    if (!Prone.FlipChanger)
                    {
                        Prone.FlipChanger = true;
                        Game.Player.ForcedAim = false;
                    }
                    Main.SetStance(Main.Stance.Crouching);
                    Main.TransitionAction = 7;
                    break;
                case 7:
                    if (Game.Player.Character.IsRagdoll || Game.Player.Character.IsFalling || (double)Game.Player.Character.SubmersionLevel > 0.20000000298023224)
                    {
                        Game.Player.Character.Task.ClearAll();
                        Main.SetStance(Main.Stance.Standing);
                        Main.TransitionAction = 0;
                        break;
                    }
                    if (flag1)
                    {
                        Prone.goProneIdle = 2;
                        break;
                    }
                    if (!flag2)
                        break;
                    Prone.idleViewModeTransition = 1;
                    Main.TransitionAction = 0;
                    break;
                case 8:
                    if ((double)Game.Player.Character.SubmersionLevel > 0.20000000298023224 && ((double)Game.Player.Character.GetAnimTime("move_jump", "dive_start_run") > 0.43999999761581421 || (double)Game.Player.Character.GetAnimTime("move_climb", "clamberpose_to_dive_angled_20") > 0.38999998569488525))
                    {
                        Game.Player.Character.Ragdoll(2000);
                        Main.SetStance(Main.Stance.Standing);
                        Main.TransitionAction = 0;
                        break;
                    }
                    if (Game.Player.Character.IsRagdoll || Game.Player.Character.IsFalling)
                    {
                        Game.Player.Character.Task.ClearAll();
                        Main.SetStance(Main.Stance.Standing);
                        Main.TransitionAction = 0;
                        break;
                    }
                    if (Game.Player.Character.IsAnimPlay("move_jump", "dive_start_run") || Game.Player.Character.IsAnimPlay("move_climb", "clamberpose_to_dive_angled_20") || Game.Player.Character.IsRagdoll || Game.Player.Character.IsFalling || (double)Game.Player.Character.SubmersionLevel > 0.20000000298023224)
                        break;
                    Main.SetStance(Main.Stance.Prone);
                    Prone.goProneIdle = 2;
                    break;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
        }

        private void OnTick(object sender, EventArgs e)
        {
            this.Сuts();
            this.Controller();
            Stand.Stance_Stand();
            Crouch.Stance_Crouch();
            Prone.Stance_Prone();
            this.TransitionStanse();
            this.FalloffCar();
            if (!Game.Player.Character.IsDead && !Function.Call<bool>(Hash.IS_PLAYER_SWITCH_IN_PROGRESS) && !Function.Call<bool>(Hash.IS_PLAYER_BEING_ARRESTED) || Main.currentStance == 0)
                return;
            Main.SetStance(Main.Stance.Standing);
        }

        private void Controller()
        {
            GTA.Control control = Game.LastInputMethod == InputMethod.GamePad ? Configuration.PAD_Stance : Configuration.Key_Stance;
            if (Game.IsControlJustPressed(control) && Main.banButton)
                this.buttonPressTimer.Start();
            if (!this.buttonPressTimer.IsRunning)
                return;
            if (Main.currentStance == Main.Stance.Standing && Main.blockScopeCameraPlay)
            {
                if (Game.IsControlJustReleased(control) && Main.TransitionAction == 0)
                {
                    if (this.buttonPressTimer.ElapsedMilliseconds >= 250L)
                    {
                        if (Main.TransitionAction == 0)
                            Main.TransitionAction = 1;
                    }
                    else if (Main.TransitionAction == 0)
                        Main.TransitionAction = 2;
                    this.buttonPressTimer.Stop();
                    this.buttonPressTimer.Reset();
                }
            }
            else if (Main.currentStance == Main.Stance.Crouching && Main.TransitionAction == 0 && Main.blockScopeCameraPlay)
            {
                if (Game.IsControlJustReleased(control))
                {
                    if (this.buttonPressTimer.ElapsedMilliseconds >= 250L)
                    {
                        if (Main.TransitionAction == 0)
                            Main.TransitionAction = 3;
                    }
                    else if (Main.TransitionAction == 0)
                        Main.TransitionAction = 4;
                    this.buttonPressTimer.Stop();
                    this.buttonPressTimer.Reset();
                }
            }
            else if (Main.currentStance == Main.Stance.Prone && Main.TransitionAction == 0 && Game.IsControlJustReleased(control) && Main.blockScopeCameraPlay)
            {
                if (this.buttonPressTimer.ElapsedMilliseconds >= 250L)
                {
                    if (Main.TransitionAction == 0)
                        Main.TransitionAction = 5;
                }
                else if (Main.TransitionAction == 0)
                    Main.TransitionAction = 6;
                this.buttonPressTimer.Stop();
                this.buttonPressTimer.Reset();
            }
        }

        private void Requests()
        {
            Utilits.Settimera(0);
            Function.Call(Hash.REQUEST_ANIM_SET, (InputArgument)"move_ped_crouched");
            Function.Call(Hash.REQUEST_ANIM_SET, (InputArgument)"move_ped_crouched_strafing");
        }

        public static void SetStance(Main.Stance stance)
        {
            switch (stance)
            {
                case Main.Stance.Standing:
                    Game.Player.Character.CanPlayGestures = true;
                    Game.Player.Character.SetPedCanPlayAmbientAnims(true);
                    Game.Player.Character.SetPedCanPlayAmbientBaseAnims(true);
                    Game.Player.Character.SetDisableAmbientMeleeMove(false);
                    Game.Player.Character.SetPedStealthMovement(false);
                    Game.Player.Character.ResetPedMovementClipset(0.2f);
                    Game.Player.Character.ResetPedStrafeClipset();
                    Main.currentStance = Main.Stance.Standing;
                    break;
                case Main.Stance.Crouching:
                    Function.Call(Hash.SET_PED_USING_ACTION_MODE, (InputArgument)(Entity)Game.Player.Character, (InputArgument)false, (InputArgument)(-1), (InputArgument)0);
                    Game.Player.Character.CanPlayGestures = false;
                    Game.Player.Character.SetPedCanPlayAmbientAnims(false);
                    Game.Player.Character.SetPedCanPlayAmbientBaseAnims(false);
                    Game.Player.Character.SetDisableAmbientMeleeMove(true);
                    Game.Player.Character.SetResetFlag(PedResetFlagToggles.DisableActionMode, true);
                    if (Utilits.IsFollowPedCamViewMode(Utilits.ViewMode.FIRST_PERSON))
                    {
                        Function.Call(Hash.SET_PED_USING_ACTION_MODE, (InputArgument)(Entity)Game.Player.Character, (InputArgument)false, (InputArgument)(-1), (InputArgument)0);
                        Game.Player.Character.SetPedMaxMoveBlendRatio(1.5f);
                        Game.Player.Character.ResetPedMovementClipset(0.2f);
                        Game.Player.Character.ResetPedStrafeClipset();
                        Game.Player.Character.SetPedStealthMovement(true);
                    }
                    if (!Utilits.IsFollowPedCamViewMode(Utilits.ViewMode.FIRST_PERSON))
                    {
                        Function.Call(Hash.SET_PED_USING_ACTION_MODE, (InputArgument)(Entity)Game.Player.Character, (InputArgument)false, (InputArgument)(-1), (InputArgument)0);
                        Game.Player.Character.SetPedMovementClipset("move_ped_crouched", 0.2f);
                        Game.Player.Character.SetPedStrafeClipset("move_ped_crouched_strafing");
                        Game.Player.Character.SetPedStealthMovement(false);
                    }
                    Main.currentStance = Main.Stance.Crouching;
                    break;
                case Main.Stance.Prone:
                    Main.currentStance = Main.Stance.Prone;
                    break;
            }
        }

        public static void WeaponChange()
        {
            Function.Call<int>(Hash.FLOOR, (InputArgument)(Function.Call<float>(Hash.GET_ANIM_DURATION, (InputArgument)"weapon@w_sp_jerrycan", (InputArgument)"holster_crouch") * 1000f));
            Game.Player.Character.Weapons.Select(Game.Player.Character.Weapons.Current.Hash, true);
            if (!Game.IsControlJustPressed(GTA.Control.SelectWeapon) || Game.Player.Character.IsAnimPlay("weapon@w_sp_jerrycan", "holster_crouch"))
                return;
            Game.DisableControlThisFrame(GTA.Control.SelectWeapon);
            Game.DisableControlThisFrame(GTA.Control.WeaponWheelNext);
            Game.DisableControlThisFrame(GTA.Control.WeaponWheelPrev);
            Game.Player.Character.Task.PlayAnimation("weapon@w_sp_jerrycan", "holster_crouch", 8f, -4f, -1, AnimationFlags.UpperBodyOnly | AnimationFlags.Secondary, 0.0f);
            switch (Main.currentStance)
            {
                case Main.Stance.Crouching:
                    Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("move_crouch_proto", "idle"));
                    Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("move_aim_strafe_crouch_2h", "idle"));
                    Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("move_weapon@rifle@generic", "idle_crouch"));
                    Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("missfbi2", "franklin_sniper_crouch"));
                    Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("cover@first_person@weapon@rpg", "blindfire_low_r_aim_med_edge"));
                    Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("weapons@first_person@aim_idle@remote_clone@heavy@minigun@", "aim_low_loop"));
                    Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("weapons@first_person@aim_rng@p_m_zero@misc@jerrycan@aim_trans@unholster_to_rng", "aim_trans_low"));
                    Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("move_crouch_proto", "idle"));
                    Game.Player.Character.Task.StopScriptedAnimationTask(new CrClipAsset("anim@amb@business@weed@weed_inspecting_lo_med_hi@", "weed_spraybottle_crouch_spraying_01_inspector"));
                    break;
                case Main.Stance.Prone:
                    if (!Prone.goIdle)
                        Prone.goIdle = true;
                    if (!Prone.scopetriger)
                        return;
                    Prone.scopetriger = false;
                    Game.Player.Character.IsVisible = true;
                    return;
            }
            Prone.idleViewModeTransition = 1;
        }

        private void FalloffCar()
        {
            if ((double)Game.Player.Character.Speed <= 10.0)
                return;
            if (Game.Player.Character.IsStandingOnVehicle())
            {
                Utilits.Settimera(0);
                this.fallofcar = true;
                this.fallofcartimer = Function.Call<int>(Hash.TIMERA);
            }
            if (!Game.Player.Character.IsStandingOnVehicle() && Game.Player.Character.IsFalling && this.fallofcar && this.fallofcartimer + 100 < Function.Call<int>(Hash.TIMERA))
            {
                Game.Player.Character.Task.ClearAll();
                Game.Player.Character.GiveNMMessage(Utilits.NMMessage.rollDownStairs, duration: 4000);
                this.fallofcar = false;
            }
        }

        private void Сuts()
        {
            Main.banButton = !Game.Player.Character.IsGettingUp && !Game.Player.Character.IsInjured && !Game.Player.Character.IsFalling && !Game.Player.Character.IsRagdoll && !Game.Player.Character.IsInAir && !Game.Player.Character.IsSwimming && !Game.Player.Character.IsInVehicle() && (double)Game.Player.Character.SubmersionLevel < 0.5;
        }

        public static void CameraCollisionDisable()
        {
            Entity entity;
            if (Game.Player.Character.TryGetPhysicalEntityFromLastCollisionRecord(out entity))
                Main.IgnoreEntity = entity;
            if (Main.IgnoreEntity != (Entity)null)
            {
                Function.Call(Hash.SET_GAMEPLAY_CAM_IGNORE_ENTITY_COLLISION_THIS_UPDATE, (InputArgument)Main.IgnoreEntity);
                Function.Call(Hash.DISABLE_CAM_COLLISION_FOR_OBJECT, (InputArgument)Main.IgnoreEntity);
            }
            if (!(Main.IgnoreEntity != (Entity)null) || Game.Player.Character.IsInRange(Main.IgnoreEntity.Position, 2f))
                return;
            Main.IgnoreEntity = (Entity)null;
        }

        public enum Stance
        {
            Standing,
            Crouching,
            Prone,
        }
    }
}
