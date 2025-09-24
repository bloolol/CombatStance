// Decompiled with JetBrains decompiler
// Type: CombatStance.Stand
// Assembly: CombatStanceMovement, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 15398906-0FC5-4912-B95F-7B85C0671CB4
// Assembly location: C:\Users\clope\OneDrive\Desktop\CombatStanceMovement.dll

using GTA;
using GTA.Math;
using System;

namespace CombatStance
{
    public class Stand : Script
    {
        public static int goProneBack;
        public static int stopJump;

        public static void Stance_Stand() => Stand.StanceJump();

        public static string[] JumpName()
        {
            string[][] strArray1 = new string[5][]
            {
        new string[2]{ "move_jump", "dive_start_run" },
        new string[2]{ "mini@tennis", "dive_bh" },
        new string[2]{ "mini@tennis", "dive_fh" },
        new string[2]{ "missmic2@goon1", "goonfall_into_bin" },
        new string[2]
        {
          "move_climb",
          "clamberpose_to_dive_angled_20"
        }
            };
            foreach (string[] strArray2 in strArray1)
            {
                if (Game.Player.Character.IsAnimPlay(strArray2[0], strArray2[1]))
                    return strArray2;
            }
            return new string[2] { "null", "null" };
        }

        public static void JumpForce()
        {
            string[] strArray1 = new string[2]
            {
        "move_jump",
        "dive_start_run"
            };
            string[] strArray2 = new string[2]
            {
        "mini@tennis",
        "dive_bh"
            };
            string[] strArray3 = new string[2]
            {
        "mini@tennis",
        "dive_fh"
            };
            string[] strArray4 = new string[2]
            {
        "missmic2@goon1",
        "goonfall_into_bin"
            };
            string[] strArray5 = new string[2]
            {
        "move_climb",
        "clamberpose_to_dive_angled_20"
            };
            float[] numArray1;
            if (!(strArray1[1] == Stand.JumpName()[1]))
            {
                if (!(strArray2[1] == Stand.JumpName()[1]))
                {
                    if (!(strArray3[1] == Stand.JumpName()[1]))
                    {
                        if (!(strArray4[1] == Stand.JumpName()[1]))
                        {
                            if (!(strArray5[1] == Stand.JumpName()[1]))
                                numArray1 = new float[2];
                            else
                                numArray1 = new float[2]
                                {
                  Configuration.JumpCrouchForwardForceX,
                  Configuration.JumpCrouchForwardForceY
                                };
                        }
                        else
                            numArray1 = new float[2]
                            {
                Configuration.JumpBackForceX,
                Configuration.JumpBackForceY
                            };
                    }
                    else
                        numArray1 = new float[2]
                        {
              Configuration.JumpRightForceX,
              Configuration.JumpRightForceY
                        };
                }
                else
                    numArray1 = new float[2]
                    {
            Configuration.JumpLeftForceX,
            Configuration.JumpLeftForceY
                    };
            }
            else
                numArray1 = new float[2]
                {
          Configuration.JumpForwardForceX,
          Configuration.JumpForwardForceY
                };
            float[] numArray2 = numArray1;
            Vector3 direction = strArray1[1] == Stand.JumpName()[1] || strArray5[1] == Stand.JumpName()[1] ? (Game.Player.Character.ForwardVector * numArray2[0] + Game.Player.Character.UpVector * numArray2[1]) / Game.TimeScale : (strArray2[1] == Stand.JumpName()[1] ? (Game.Player.Character.RightVector * -numArray2[0] + Game.Player.Character.UpVector * numArray2[1]) / Game.TimeScale : (strArray3[1] == Stand.JumpName()[1] ? (Game.Player.Character.RightVector * numArray2[0] + Game.Player.Character.UpVector * numArray2[1]) / Game.TimeScale : (strArray4[1] == Stand.JumpName()[1] ? (Game.Player.Character.ForwardVector * -numArray2[0] + Game.Player.Character.UpVector * numArray2[1]) / Game.TimeScale : Vector3.Zero)));
            float[] numArray3;
            if (!(strArray1[1] == Stand.JumpName()[1]))
            {
                if (!(strArray2[1] == Stand.JumpName()[1]))
                {
                    if (!(strArray3[1] == Stand.JumpName()[1]))
                    {
                        if (!(strArray4[1] == Stand.JumpName()[1]))
                        {
                            if (!(strArray5[1] == Stand.JumpName()[1]))
                                numArray3 = new float[2];
                            else
                                numArray3 = new float[2] { 0.12f, 0.4f };
                        }
                        else
                            numArray3 = new float[2] { 0.01f, 0.35f };
                    }
                    else
                        numArray3 = new float[2] { 0.01f, 0.1f };
                }
                else
                    numArray3 = new float[2] { 0.01f, 0.1f };
            }
            else
                numArray3 = new float[2] { 0.25f, 0.45f };
            float[] numArray4 = numArray3;
            if ((double)Game.Player.Character.GetAnimTime(Stand.JumpName()[0], Stand.JumpName()[1]) < (double)numArray4[0] || (double)Game.Player.Character.GetAnimTime(Stand.JumpName()[0], Stand.JumpName()[1]) > (double)numArray4[1] || !Game.Player.Character.IsAnimPlay(Stand.JumpName()[0], Stand.JumpName()[1]))
                return;
            Game.Player.Character.ApplyForce(direction, Vector3.Zero, ForceType.InternalImpulse);
        }

        public static void StanceJump()
        {
            if (Stand.goProneBack != 0 && (Game.Player.Character.IsFalling || Game.Player.Character.IsRagdoll || (double)Game.Player.Character.SubmersionLevel > 0.20000000298023224 && !Game.Player.Character.IsAnimPlay("missmic2@goon1", "goonfall_into_bin")))
            {
                if ((double)Game.Player.Character.SubmersionLevel > 0.20000000298023224 && !Game.Player.Character.IsAnimPlay("missmic2@goon1", "goonfall_into_bin"))
                {
                    Game.Player.Character.Ragdoll(2000);
                    Stand.goProneBack = 0;
                }
                else
                    Stand.goProneBack = 0;
            }
            else if (Stand.stopJump != 0 && (double)Game.Player.Character.SubmersionLevel > 0.20000000298023224 && ((double)Game.Player.Character.GetAnimTime("mini@tennis", "dive_bh") > 0.10000000149011612 || (double)Game.Player.Character.GetAnimTime("mini@tennis", "dive_fh") > 0.10000000149011612))
            {
                Game.Player.Character.Ragdoll(2000);
                Stand.stopJump = 0;
            }
            else
            {
                if (Main.currentStance != Main.Stance.Standing || !Main.banButton)
                    return;
                Main.IgnoreEntity = (Entity)null;
                bool flag = !Game.Player.Character.IsAnimPlay("mini@tennis", "dive_bh") && !Game.Player.Character.IsAnimPlay("mini@tennis", "dive_fh") && !Game.Player.Character.IsAnimPlay("missmic2@goon1", "goonfall_into_bin");
                Stand.JumpForce();
                if (((!Configuration.jump_left || !Game.IsControlJustPressed(Control.Jump) || !Game.IsControlPressed(Control.MoveLeftOnly) || Game.IsControlPressed(Control.MoveUpOnly) ? 0 : (!Game.IsControlPressed(Control.MoveDownOnly) ? 1 : 0)) & (flag ? 1 : 0)) != 0)
                {
                    Game.Player.Character.Task.PlayAnimation("mini@tennis", "dive_bh", 8f, 2300, AnimationFlags.None);
                    Stand.stopJump = 1;
                }
                else if (((!Configuration.jump_right || !Game.IsControlJustPressed(Control.Jump) || !Game.IsControlPressed(Control.MoveRightOnly) || Game.IsControlPressed(Control.MoveUpOnly) ? 0 : (!Game.IsControlPressed(Control.MoveDownOnly) ? 1 : 0)) & (flag ? 1 : 0)) != 0)
                {
                    Game.Player.Character.Task.PlayAnimation("mini@tennis", "dive_fh", 8f, 2300, AnimationFlags.None);
                    Stand.stopJump = 1;
                }
                else if (((!Configuration.jump_back || !Game.IsControlJustPressed(Control.Jump) || Game.IsControlPressed(Control.MoveRightOnly) || Game.IsControlPressed(Control.MoveUpOnly) ? 0 : (Game.IsControlPressed(Control.MoveDownOnly) ? 1 : 0)) & (flag ? 1 : 0)) != 0 && Stand.goProneBack == 0)
                {
                    Game.Player.Character.Task.PlayAnimation("missmic2@goon1", "goonfall_into_bin", 8f, 4f, 1000, AnimationFlags.None, 0.0f);
                    Stand.goProneBack = 1;
                }
                else if (Stand.goProneBack == 1 && !Game.Player.Character.IsFalling && !Game.Player.Character.IsAnimPlay("missmic2@goon1", "goonfall_into_bin") && !Game.Player.Character.IsRagdoll)
                {
                    Stand.goProneBack = 0;
                    Prone.FlipChanger = false;
                    Prone.goProneIdle = 2;
                    Main.SetStance(Main.Stance.Prone);
                }
            }
        }
    }
}
