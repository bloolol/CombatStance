// Decompiled with JetBrains decompiler
// Type: CombatStance.ModMenu
// Assembly: CombatStanceMovement, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 15398906-0FC5-4912-B95F-7B85C0671CB4
// Assembly location: C:\Users\clope\OneDrive\Desktop\CombatStanceMovement.dll

using GTA;
using GTA.Native;
using GTA.UI;
using LemonUI;
using LemonUI.Elements;
using LemonUI.Menus;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace CombatStance
{
    public class ModMenu : Script
    {
        private readonly ObjectPool pool = new ObjectPool();
        private readonly NativeMenu menu = new NativeMenu("CombatStance", "Setting", "Mod by BOPOHua");
        private readonly NativeMenu submenu_stance_stand = new NativeMenu("Stance Stand", "Stance Stand", "");
        private readonly NativeMenu submenu_stance_stand_jumps = new NativeMenu("Dive Jumps", "Dive Jumps", "");
        private static readonly NativeMenu submenu_stance_stand_jump_left = new NativeMenu("Jump Left", "Jump Left", "");
        private static readonly NativeMenu submenu_stance_stand_jump_right = new NativeMenu("Jump Right", "Jump Right", "");
        private static readonly NativeMenu submenu_stance_stand_jump_front = new NativeMenu("Jump Front", "Jump Front", "");
        private static readonly NativeMenu submenu_stance_stand_jump_back = new NativeMenu("Jump Back", "Jump Back", "");
        private static readonly NativeCheckboxItem item_stance_stand_jump_left = new NativeCheckboxItem("Enable/Disable", "Left", Configuration.jump_left);
        private static readonly NativeCheckboxItem item_stance_stand_jump_right = new NativeCheckboxItem("Enable/Disable", "Right", Configuration.jump_right);
        private static readonly NativeCheckboxItem item_stance_stand_jump_front = new NativeCheckboxItem("Enable/Disable", "Front", Configuration.jump_front);
        private static readonly NativeCheckboxItem item_stance_stand_jump_back = new NativeCheckboxItem("Enable/Disable", "Back", Configuration.jump_back);
        private static readonly NativeItem item_stance_stand_force_left_x = new NativeItem("X", "Force Left", Configuration.JumpLeftForceX.ToString());
        private static readonly NativeItem item_stance_stand_force_right_x = new NativeItem("X", "Force Right", Configuration.JumpRightForceX.ToString());
        private static readonly NativeItem item_stance_stand_force_front_x = new NativeItem("X", "Force Front", Configuration.JumpForwardForceX.ToString());
        private static readonly NativeItem item_stance_stand_force_back_x = new NativeItem("X", "Force Back", Configuration.JumpBackForceX.ToString());
        private static readonly NativeItem item_stance_stand_force_left_y = new NativeItem("Y", "Force Left", Configuration.JumpLeftForceY.ToString());
        private static readonly NativeItem item_stance_stand_force_right_y = new NativeItem("Y", "Force Right", Configuration.JumpRightForceY.ToString());
        private static readonly NativeItem item_stance_stand_force_front_y = new NativeItem("Y", "Force Front", Configuration.JumpForwardForceY.ToString());
        private static readonly NativeItem item_stance_stand_force_back_y = new NativeItem("Y", "Force Back", Configuration.JumpBackForceY.ToString());
        private readonly NativeMenu submenu_stance_crouch = new NativeMenu("Stance Сrouch", "Stance Сrouch", "");
        private readonly NativeMenu submenu_stance_crouch_roll = new NativeMenu("Rolling", "Rolling", "");
        private readonly NativeMenu submenu_stance_crouch_jump = new NativeMenu("Jump", "Jump", "");
        private static readonly NativeItem item_stance_crouch_jump_force_front_x = new NativeItem("X", "Force Front", Configuration.JumpCrouchForwardForceX.ToString());
        private static readonly NativeItem item_stance_crouch_jump_force_front_y = new NativeItem("Y", "Force Front", Configuration.JumpCrouchForwardForceY.ToString());
        private static readonly NativeMenu submenu_stance_crouch_roll_left = new NativeMenu("Roll Left", "Roll Left", "");
        private static readonly NativeMenu submenu_stance_crouch_roll_right = new NativeMenu("Roll Right", "Roll Right", "");
        private static readonly NativeMenu submenu_stance_crouch_roll_front = new NativeMenu("Roll Front", "Roll Front", "");
        private static readonly NativeMenu submenu_stance_crouch_roll_back = new NativeMenu("Roll Back", "Roll Back", "");
        private static readonly NativeCheckboxItem item_stance_crouch_roll_left = new NativeCheckboxItem("Enable/Disable", "Left", Configuration.roll_left);
        private static readonly NativeCheckboxItem item_stance_crouch_roll_right = new NativeCheckboxItem("Enable/Disable", "Right", Configuration.roll_right);
        private static readonly NativeCheckboxItem item_stance_crouch_roll_front = new NativeCheckboxItem("Enable/Disable", "Front", Configuration.roll_front);
        private static readonly NativeCheckboxItem item_stance_crouch_roll_back = new NativeCheckboxItem("Enable/Disable", "Back", Configuration.roll_back);
        private static readonly NativeItem item_stance_crouch_force_left_x = new NativeItem("X", "Force Left", Configuration.RollLeftForceX.ToString());
        private static readonly NativeItem item_stance_crouch_force_right_x = new NativeItem("X", "Force Right", Configuration.RollRightForceX.ToString());
        private static readonly NativeItem item_stance_crouch_force_front_x = new NativeItem("X", "Force Front", Configuration.RollForwardForceX.ToString());
        private static readonly NativeItem item_stance_crouch_force_back_x = new NativeItem("X", "Force Back", Configuration.RollBackForceX.ToString());
        private static readonly NativeItem item_stance_crouch_force_left_y = new NativeItem("Y", "Force Left", Configuration.RollLeftForceY.ToString());
        private static readonly NativeItem item_stance_crouch_force_right_y = new NativeItem("Y", "Force Right", Configuration.RollRightForceY.ToString());
        private static readonly NativeItem item_stance_crouch_force_front_y = new NativeItem("Y", "Force Front", Configuration.RollForwardForceY.ToString());
        private static readonly NativeItem item_stance_crouch_force_back_y = new NativeItem("Y", "Force Back", Configuration.RollBackForceY.ToString());
        public static bool WeitControlActivate = false;
        public static int WeitControlActivateTimer;
        private string nameMenuItem;
        private readonly NativeMenu submenu_stance_prone = new NativeMenu("Stance Prone", "Stance Prone", "");
        private static readonly NativeCheckboxItem item_stance_prone_enable = new NativeCheckboxItem("Enable/Disable", "", Configuration.EnableProne);
        private NativeMenu subMenuStanceButtons = new NativeMenu("Button", "Button");
        private NativeCheckboxItem MenuControlSetting_PAD = new NativeCheckboxItem("GamePad", "Enable/Disable");
        private static NativeItem iButton = new NativeItem("Button", "", "Input");
        private int saveSetting;

        private void Start_Menu_Stance_Stand()
        {
            this.menu.Add(this.submenu_stance_stand);
            this.submenu_stance_stand.Add(this.submenu_stance_stand_jumps);
            this.submenu_stance_stand_jumps.Add(ModMenu.submenu_stance_stand_jump_left);
            ModMenu.submenu_stance_stand_jump_left.Add((NativeItem)ModMenu.item_stance_stand_jump_left);
            ((NativeItem)ModMenu.item_stance_stand_jump_left).Activated += (EventHandler)((sender, item) =>
            {
                Configuration.jump_left = !Configuration.jump_left;
                ModMenu.item_stance_stand_jump_left.Checked = Configuration.jump_left;
                if (Configuration.jump_left)
                    Notification.PostTicker("~g~jump_left Enable~g~", false);
                else
                    Notification.PostTicker("~b~jump_left Disable~b~", false);
            });
            ModMenu.submenu_stance_stand_jump_left.Add(ModMenu.item_stance_stand_force_left_x);
            ModMenu.item_stance_stand_force_left_x.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.JumpLeftForceX = result;
                    ModMenu.item_stance_stand_force_left_x.AltTitle = Configuration.JumpLeftForceX.ToString();
                }
            });
            ModMenu.submenu_stance_stand_jump_left.Add(ModMenu.item_stance_stand_force_left_y);
            ModMenu.item_stance_stand_force_left_y.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.JumpLeftForceY = result;
                    ModMenu.item_stance_stand_force_left_y.AltTitle = Configuration.JumpLeftForceY.ToString();
                }
            });
            this.submenu_stance_stand_jumps.Add(ModMenu.submenu_stance_stand_jump_right);
            ModMenu.submenu_stance_stand_jump_right.Add((NativeItem)ModMenu.item_stance_stand_jump_right);
            ((NativeItem)ModMenu.item_stance_stand_jump_right).Activated += (EventHandler)((sender, item) =>
            {
                Configuration.jump_right = !Configuration.jump_right;
                ModMenu.item_stance_stand_jump_left.Checked = Configuration.jump_right;
                if (Configuration.jump_right)
                    Notification.PostTicker("~g~jump_right Enable~g~", false);
                else
                    Notification.PostTicker("~b~jump_right Disable~b~", false);
            });
            ModMenu.submenu_stance_stand_jump_right.Add(ModMenu.item_stance_stand_force_right_x);
            ModMenu.item_stance_stand_force_right_x.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.JumpRightForceX = result;
                    ModMenu.item_stance_stand_force_right_x.AltTitle = Configuration.JumpRightForceX.ToString();
                }
            });
            ModMenu.submenu_stance_stand_jump_right.Add(ModMenu.item_stance_stand_force_right_y);
            ModMenu.item_stance_stand_force_right_y.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.JumpRightForceY = result;
                    ModMenu.item_stance_stand_force_right_y.AltTitle = Configuration.JumpRightForceY.ToString();
                }
            });
            this.submenu_stance_stand_jumps.Add(ModMenu.submenu_stance_stand_jump_front);
            ModMenu.submenu_stance_stand_jump_front.Add((NativeItem)ModMenu.item_stance_stand_jump_front);
            ((NativeItem)ModMenu.item_stance_stand_jump_front).Activated += (EventHandler)((sender, item) =>
            {
                Configuration.jump_front = !Configuration.jump_front;
                ModMenu.item_stance_stand_jump_left.Checked = Configuration.jump_front;
                if (Configuration.jump_front)
                    Notification.PostTicker("~g~jump_front Enable~g~", false);
                else
                    Notification.PostTicker("~b~jump_front Disable~b~", false);
            });
            ModMenu.submenu_stance_stand_jump_front.Add(ModMenu.item_stance_stand_force_front_x);
            ModMenu.item_stance_stand_force_front_x.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.JumpForwardForceX = result;
                    ModMenu.item_stance_stand_force_front_x.AltTitle = Configuration.JumpForwardForceX.ToString();
                }
            });
            ModMenu.submenu_stance_stand_jump_front.Add(ModMenu.item_stance_stand_force_front_y);
            ModMenu.item_stance_stand_force_front_y.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.JumpForwardForceY = result;
                    ModMenu.item_stance_stand_force_front_y.AltTitle = Configuration.JumpForwardForceY.ToString();
                }
            });
            this.submenu_stance_stand_jumps.Add(ModMenu.submenu_stance_stand_jump_back);
            ModMenu.submenu_stance_stand_jump_back.Add((NativeItem)ModMenu.item_stance_stand_jump_back);
            ((NativeItem)ModMenu.item_stance_stand_jump_back).Activated += (EventHandler)((sender, item) =>
            {
                Configuration.jump_back = !Configuration.jump_back;
                ModMenu.item_stance_stand_jump_left.Checked = Configuration.jump_back;
                if (Configuration.jump_back)
                    Notification.PostTicker("~g~jump_back Enable~g~", false);
                else
                    Notification.PostTicker("~b~jump_back Disable~b~", false);
            });
            ModMenu.submenu_stance_stand_jump_back.Add(ModMenu.item_stance_stand_force_back_x);
            ModMenu.item_stance_stand_force_back_x.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.JumpBackForceX = result;
                    ModMenu.item_stance_stand_force_back_x.AltTitle = Configuration.JumpBackForceX.ToString();
                }
            });
            ModMenu.submenu_stance_stand_jump_back.Add(ModMenu.item_stance_stand_force_back_y);
            ModMenu.item_stance_stand_force_back_y.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.JumpBackForceY = result;
                    ModMenu.item_stance_stand_force_back_y.AltTitle = Configuration.JumpBackForceY.ToString();
                }
            });
            this.pool.Add((IProcessable)ModMenu.submenu_stance_stand_jump_left);
            this.pool.Add((IProcessable)ModMenu.submenu_stance_stand_jump_right);
            this.pool.Add((IProcessable)ModMenu.submenu_stance_stand_jump_front);
            this.pool.Add((IProcessable)ModMenu.submenu_stance_stand_jump_back);
            this.pool.Add((IProcessable)this.submenu_stance_stand);
            this.pool.Add((IProcessable)this.submenu_stance_stand_jumps);
        }

        private void Start_Menu_Stance_Crouch()
        {
            this.menu.Add(this.submenu_stance_crouch);
            this.submenu_stance_crouch.Add(this.submenu_stance_crouch_jump);
            this.submenu_stance_crouch_jump.Add(ModMenu.item_stance_crouch_jump_force_front_x);
            ModMenu.item_stance_crouch_jump_force_front_x.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.JumpCrouchForwardForceX = result;
                    ModMenu.item_stance_crouch_jump_force_front_x.AltTitle = Configuration.JumpCrouchForwardForceX.ToString();
                }
            });
            this.submenu_stance_crouch_jump.Add(ModMenu.item_stance_crouch_jump_force_front_y);
            ModMenu.item_stance_crouch_jump_force_front_y.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.JumpCrouchForwardForceY = result;
                    ModMenu.item_stance_crouch_jump_force_front_y.AltTitle = Configuration.JumpCrouchForwardForceY.ToString();
                }
            });
            this.submenu_stance_crouch.Add(this.submenu_stance_crouch_roll);
            this.submenu_stance_crouch_roll.Add(ModMenu.submenu_stance_crouch_roll_left);
            ModMenu.submenu_stance_crouch_roll_left.Add((NativeItem)ModMenu.item_stance_crouch_roll_left);
            ((NativeItem)ModMenu.item_stance_crouch_roll_left).Activated += (EventHandler)((sender, item) =>
            {
                Configuration.roll_left = !Configuration.jump_left;
                ModMenu.item_stance_crouch_roll_left.Checked = Configuration.roll_left;
                if (Configuration.jump_left)
                    Notification.PostTicker("~g~jump_left Enable~g~", false);
                else
                    Notification.PostTicker("~b~jump_left Disable~b~", false);
            });
            ModMenu.submenu_stance_crouch_roll_left.Add(ModMenu.item_stance_stand_force_left_x);
            ModMenu.item_stance_crouch_force_left_x.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.RollLeftForceX = result;
                    ModMenu.item_stance_crouch_force_left_x.AltTitle = Configuration.RollLeftForceX.ToString();
                }
            });
            ModMenu.submenu_stance_crouch_roll_left.Add(ModMenu.item_stance_crouch_force_left_y);
            ModMenu.item_stance_crouch_force_left_y.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.RollLeftForceY = result;
                    ModMenu.item_stance_crouch_force_left_y.AltTitle = Configuration.RollLeftForceY.ToString();
                }
            });
            this.submenu_stance_crouch_roll.Add(ModMenu.submenu_stance_crouch_roll_right);
            ModMenu.submenu_stance_crouch_roll_right.Add((NativeItem)ModMenu.item_stance_crouch_roll_right);
            ((NativeItem)ModMenu.item_stance_crouch_roll_right).Activated += (EventHandler)((sender, item) =>
            {
                Configuration.roll_right = !Configuration.roll_right;
                ModMenu.item_stance_crouch_roll_left.Checked = Configuration.roll_right;
                if (Configuration.roll_right)
                    Notification.PostTicker("~g~roll_right Enable~g~", false);
                else
                    Notification.PostTicker("~b~roll_right Disable~b~", false);
            });
            ModMenu.submenu_stance_crouch_roll_right.Add(ModMenu.item_stance_crouch_force_right_x);
            ModMenu.item_stance_crouch_force_right_x.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.RollRightForceX = result;
                    ModMenu.item_stance_crouch_force_right_x.AltTitle = Configuration.RollRightForceX.ToString();
                }
            });
            ModMenu.submenu_stance_crouch_roll_right.Add(ModMenu.item_stance_crouch_force_right_y);
            ModMenu.item_stance_crouch_force_right_y.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.RollRightForceY = result;
                    ModMenu.item_stance_crouch_force_right_y.AltTitle = Configuration.RollRightForceY.ToString();
                }
            });
            this.submenu_stance_crouch_roll.Add(ModMenu.submenu_stance_crouch_roll_front);
            ModMenu.submenu_stance_crouch_roll_front.Add((NativeItem)ModMenu.item_stance_crouch_roll_front);
            ((NativeItem)ModMenu.item_stance_crouch_roll_front).Activated += (EventHandler)((sender, item) =>
            {
                Configuration.roll_front = !Configuration.roll_front;
                ModMenu.item_stance_crouch_roll_left.Checked = Configuration.roll_front;
                if (Configuration.roll_front)
                    Notification.PostTicker("~g~roll_front Enable~g~", false);
                else
                    Notification.PostTicker("~b~roll_front Disable~b~", false);
            });
            ModMenu.submenu_stance_crouch_roll_front.Add(ModMenu.item_stance_crouch_force_front_x);
            ModMenu.item_stance_crouch_force_front_x.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.RollForwardForceX = result;
                    ModMenu.item_stance_crouch_force_front_x.AltTitle = Configuration.RollForwardForceX.ToString();
                }
            });
            ModMenu.submenu_stance_crouch_roll_front.Add(ModMenu.item_stance_crouch_force_front_y);
            ModMenu.item_stance_crouch_force_front_y.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.RollForwardForceY = result;
                    ModMenu.item_stance_crouch_force_front_y.AltTitle = Configuration.RollForwardForceY.ToString();
                }
            });
            this.submenu_stance_crouch_roll.Add(ModMenu.submenu_stance_crouch_roll_back);
            ModMenu.submenu_stance_crouch_roll_back.Add((NativeItem)ModMenu.item_stance_crouch_roll_back);
            ((NativeItem)ModMenu.item_stance_crouch_roll_back).Activated += (EventHandler)((sender, item) =>
            {
                Configuration.roll_back = !Configuration.roll_back;
                ModMenu.item_stance_crouch_roll_left.Checked = Configuration.roll_back;
                if (Configuration.roll_back)
                    Notification.PostTicker("~g~roll_back Enable~g~", false);
                else
                    Notification.PostTicker("~b~roll_back Disable~b~", false);
            });
            ModMenu.submenu_stance_crouch_roll_back.Add(ModMenu.item_stance_crouch_force_back_x);
            ModMenu.item_stance_crouch_force_back_x.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.RollBackForceX = result;
                    ModMenu.item_stance_crouch_force_back_x.AltTitle = Configuration.RollBackForceX.ToString();
                }
            });
            ModMenu.submenu_stance_crouch_roll_back.Add(ModMenu.item_stance_crouch_force_back_y);
            ModMenu.item_stance_crouch_force_back_y.Activated += (EventHandler)((sender, item) =>
            {
                float result;
                if (!float.TryParse(Game.GetUserInput(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                {
                    Notification.PostTicker("~r~Error~w~: string is invalid.", false);
                }
                else
                {
                    Configuration.RollBackForceY = result;
                    ModMenu.item_stance_crouch_force_back_y.AltTitle = Configuration.RollBackForceY.ToString();
                }
            });
            this.pool.Add((IProcessable)ModMenu.submenu_stance_crouch_roll_left);
            this.pool.Add((IProcessable)ModMenu.submenu_stance_crouch_roll_right);
            this.pool.Add((IProcessable)ModMenu.submenu_stance_crouch_roll_front);
            this.pool.Add((IProcessable)ModMenu.submenu_stance_crouch_roll_back);
            this.pool.Add((IProcessable)this.submenu_stance_crouch);
            this.pool.Add((IProcessable)this.submenu_stance_crouch_jump);
            this.pool.Add((IProcessable)this.submenu_stance_crouch_roll);
        }

        private void OnTick_Button()
        {
            if (!this.subMenuStanceButtons.Visible)
                return;
            this.ControlButtons(ref this.subMenuStanceButtons, ref ModMenu.iButton, ref Configuration.PAD_Stance, ref Configuration.Key_Stance, "Button");
        }

        private void Start_Menu_Stance_Prone()
        {
            this.menu.Add(this.submenu_stance_prone);
            this.submenu_stance_prone.Add((NativeItem)ModMenu.item_stance_prone_enable);
            ((NativeItem)ModMenu.item_stance_prone_enable).Activated += (EventHandler)((sender, item) =>
            {
                Configuration.EnableProne = !Configuration.EnableProne;
                ModMenu.item_stance_prone_enable.Checked = Configuration.EnableProne;
                if (Configuration.EnableProne)
                    Notification.PostTicker("~g~Prone Stance Enable~g~", false);
                else
                    Notification.PostTicker("~b~Prone Stance Disable~b~", false);
            });
            this.pool.Add((IProcessable)this.submenu_stance_prone);
        }

        private void ControlButtons(
          ref NativeMenu menu,
          ref NativeItem nativeItem,
          ref Control pad,
          ref Control key,
          string helptext)
        {
            Control control1 = Game.LastInputMethod == InputMethod.GamePad ? pad : key;
            if (!menu.Visible)
                return;
            this.MenuControlSetting_PAD.Checked = Game.LastInputMethod == InputMethod.GamePad;
            nativeItem.AltTitle = control1.ToString();
            if (menu.SelectedItem == nativeItem)
            {
                if (this.nameMenuItem != nativeItem.Title)
                {
                    this.nameMenuItem = nativeItem.Title;
                    if (ModMenu.WeitControlActivateTimer != 0)
                        ModMenu.WeitControlActivateTimer = 0;
                    if (ModMenu.WeitControlActivate)
                        ModMenu.WeitControlActivate = false;
                }
                if (ModMenu.WeitControlActivateTimer == 0)
                    GTA.UI.Screen.ShowSubtitle("~o~Activate Menu to Change " + helptext + " Button~w~", 1);
                nativeItem.AltTitle = control1.ToString();
                foreach (Control control2 in Enum.GetValues(typeof(Control)))
                {
                    if (ModMenu.WeitControlActivate && Function.Call<int>(Hash.TIMERA) - ModMenu.WeitControlActivateTimer > 1000)
                    {
                        if (Game.IsControlPressed(control2) && control2 != Control.ReplayRewind && control2 != Control.ReplayFfwd && control2 != Control.FrontendUp && control2 != Control.FrontendDown && control2 != Control.PhoneUp && control2 != Control.PhoneDown && control2 != Control.CursorX && control2 != Control.CursorY && control2 != Control.CursorAccept && control2 != Control.CursorCancel && control2 != Control.WeaponWheelLeftRight)
                        {
                            ScriptSound soundId = Audio.GetSoundId();
                            soundId.PlaySoundFrontend("CHALLENGE_UNLOCKED", "HUD_AWARDS");
                            soundId.Release();
                            if (Game.LastInputMethod == InputMethod.GamePad)
                            {
                                Function.Call(Hash.SET_CONTROL_SHAKE, (InputArgument)(Enum)control2, (InputArgument)300, (InputArgument)200);
                                pad = control2;
                            }
                            else
                                key = control2;
                            nativeItem.AltTitle = control1.ToString();
                            ModMenu.WeitControlActivateTimer = 0;
                            ModMenu.WeitControlActivate = false;
                            GTA.UI.Screen.ShowSubtitle("~b~ Button to Change" + helptext + " now is~w~   ~b~ " + control1.ToString() + " ~w~");
                        }
                        GTA.UI.Screen.ShowSubtitle("~b~Press Button to Change " + helptext + " Button~w~", 1);
                    }
                    if (ModMenu.WeitControlActivate && Function.Call<int>(Hash.TIMERA) - ModMenu.WeitControlActivateTimer > 8500)
                    {
                        GTA.UI.Screen.ShowSubtitle("~r~Time Out~w~ ~b~Button Is Not Changing~w~");
                        ModMenu.WeitControlActivateTimer = 0;
                        ModMenu.WeitControlActivate = false;
                    }
                }
            }
        }

        public ModMenu()
        {
            this.Start_Menu_Stance_Stand();
            this.Start_Menu_Stance_Crouch();
            this.Start_Menu_Stance_Prone();
            this.menu.Add(this.subMenuStanceButtons);
            this.subMenuStanceButtons.Add((NativeItem)this.MenuControlSetting_PAD);
            this.subMenuStanceButtons.Add(ModMenu.iButton);
            ModMenu.iButton.Activated += (EventHandler)((sender, item) =>
            {
                ModMenu.WeitControlActivate = !ModMenu.WeitControlActivate;
                if (ModMenu.WeitControlActivate)
                {
                    Notification.PostTicker("~g~Shake~g~: ~p~Enable~p~", false);
                    ModMenu.WeitControlActivateTimer = Function.Call<int>(Hash.TIMERA);
                }
                else
                    Notification.PostTicker("~g~Shaker~g~: ~r~Disable~r~", false);
            });
            this.pool.Add((IProcessable)this.subMenuStanceButtons);
            this.pool.Add((IProcessable)this.menu);
            this.Tick += new EventHandler(this.OnTick);
            foreach (NativeMenu nativeMenu in ((IEnumerable<IProcessable>)this.pool).ToList<IProcessable>())
            {
                nativeMenu.DisableControls = false;
                nativeMenu.RotateCamera = true;
                nativeMenu.MouseBehavior = MenuMouseBehavior.Disabled;
            }
            this.menu.Banner = (I2Dimensional)new ScaledTexture(PointF.Empty, new SizeF(512f, 128f), "foreclosures_signage", "emblem_bg_0_2");
            this.submenu_stance_stand.Banner = (I2Dimensional)new ScaledTexture(PointF.Empty, new SizeF(512f, 128f), "foreclosures_signage", "emblem_bg_1_4");
            this.submenu_stance_stand_jumps.Banner = (I2Dimensional)new ScaledTexture(PointF.Empty, new SizeF(512f, 128f), "foreclosures_signage", "emblem_bg_1_5");
            ModMenu.submenu_stance_stand_jump_left.Banner = (I2Dimensional)new ScaledTexture(PointF.Empty, new SizeF(512f, 128f), "foreclosures_signage", "emblem_bg_1_5");
            ModMenu.submenu_stance_stand_jump_right.Banner = (I2Dimensional)new ScaledTexture(PointF.Empty, new SizeF(512f, 128f), "foreclosures_signage", "emblem_bg_1_5");
            ModMenu.submenu_stance_stand_jump_front.Banner = (I2Dimensional)new ScaledTexture(PointF.Empty, new SizeF(512f, 128f), "foreclosures_signage", "emblem_bg_1_5");
            ModMenu.submenu_stance_stand_jump_back.Banner = (I2Dimensional)new ScaledTexture(PointF.Empty, new SizeF(512f, 128f), "foreclosures_signage", "emblem_bg_1_5");
            this.submenu_stance_crouch.Banner = (I2Dimensional)new ScaledTexture(PointF.Empty, new SizeF(512f, 128f), "foreclosures_signage", "emblem_bg_1_1");
            this.submenu_stance_crouch_jump.Banner = (I2Dimensional)new ScaledTexture(PointF.Empty, new SizeF(512f, 128f), "foreclosures_signage", "emblem_bg_1_8");
            this.submenu_stance_crouch_roll.Banner = (I2Dimensional)new ScaledTexture(PointF.Empty, new SizeF(512f, 128f), "foreclosures_signage", "emblem_bg_1_7");
            ModMenu.submenu_stance_crouch_roll_left.Banner = (I2Dimensional)new ScaledTexture(PointF.Empty, new SizeF(512f, 128f), "foreclosures_signage", "emblem_bg_1_7");
            ModMenu.submenu_stance_crouch_roll_right.Banner = (I2Dimensional)new ScaledTexture(PointF.Empty, new SizeF(512f, 128f), "foreclosures_signage", "emblem_bg_1_7");
            ModMenu.submenu_stance_crouch_roll_front.Banner = (I2Dimensional)new ScaledTexture(PointF.Empty, new SizeF(512f, 128f), "foreclosures_signage", "emblem_bg_1_7");
            ModMenu.submenu_stance_crouch_roll_back.Banner = (I2Dimensional)new ScaledTexture(PointF.Empty, new SizeF(512f, 128f), "foreclosures_signage", "emblem_bg_1_7");
            this.submenu_stance_prone.Banner = (I2Dimensional)new ScaledTexture(PointF.Empty, new SizeF(512f, 128f), "foreclosures_signage", "emblem_bg_1_3");
            this.subMenuStanceButtons.Banner = (I2Dimensional)new ScaledTexture(PointF.Empty, new SizeF(512f, 128f), "foreclosures_signage", "emblem_bg_1_2");
        }

        private void OnTick(object sender, EventArgs e)
        {
            this.pool.Process();
            this.OnTick_SaveSetting();
            this.OnTick_Button();
            this.OpenMenuButtons("csm", Control.Reload, false);
        }

        private void OnTick_SaveSetting()
        {
            if (this.menu.Visible && this.saveSetting == 0)
                this.saveSetting = 1;
            if (!this.menu.Visible && this.saveSetting == 1)
                this.saveSetting = 2;
            if (this.saveSetting != 2)
                return;
            Configuration.IniCSMConfig.SetValue<bool>("Controller_Options", "ControllerEnable", Configuration.ControllerEnable);
            Configuration.IniCSMConfig.SetValue<float>("Jump", "ForwadForceX", Configuration.JumpForwardForceX);
            Configuration.IniCSMConfig.SetValue<float>("Jump", "ForwardForceY", Configuration.JumpForwardForceY);
            Configuration.IniCSMConfig.SetValue<float>("Jump", "LeftForceX", Configuration.JumpLeftForceX);
            Configuration.IniCSMConfig.SetValue<float>("Jump", "LeftForceY", Configuration.JumpLeftForceY);
            Configuration.IniCSMConfig.SetValue<float>("Jump", "RightForceX", Configuration.JumpRightForceX);
            Configuration.IniCSMConfig.SetValue<float>("Jump", "RightForceY", Configuration.JumpRightForceY);
            Configuration.IniCSMConfig.SetValue<float>("Jump", "BackForceX", Configuration.JumpBackForceX);
            Configuration.IniCSMConfig.SetValue<float>("Jump", "BackForceY", Configuration.JumpBackForceY);
            double num1 = (double)Configuration.IniCSMConfig.GetValue<float>("Jump", "ForwadForceX", Configuration.JumpCrouchForwardForceX);
            double num2 = (double)Configuration.IniCSMConfig.GetValue<float>("Jump", "ForwardForceY", Configuration.JumpCrouchForwardForceY);
            Configuration.IniCSMConfig.SetValue<float>("Roll", "ForwardForceX", Configuration.RollForwardForceX);
            Configuration.IniCSMConfig.SetValue<float>("Roll", "ForwardForceY", Configuration.RollForwardForceY);
            Configuration.IniCSMConfig.SetValue<float>("Roll", "LeftForceX", Configuration.RollLeftForceX);
            Configuration.IniCSMConfig.SetValue<float>("Roll", "LeftForceY", Configuration.RollLeftForceY);
            Configuration.IniCSMConfig.SetValue<float>("Roll", "RightForceX", Configuration.RollRightForceX);
            Configuration.IniCSMConfig.SetValue<float>("Roll", "RightForceY", Configuration.RollRightForceY);
            Configuration.IniCSMConfig.SetValue<float>("Roll", "BackForceX", Configuration.RollBackForceX);
            Configuration.IniCSMConfig.SetValue<float>("Roll", "BackForceY", Configuration.RollBackForceY);
            Configuration.IniCSMConfig.SetValue<Control>("Buttons", "Key", Configuration.Key_Stance);
            Configuration.IniCSMConfig.SetValue<Control>("Buttons", "PAD", Configuration.PAD_Stance);
            Configuration.IniCSMConfig.SetValue<bool>("Jump", "JumpLeftEnable", Configuration.jump_left);
            Configuration.IniCSMConfig.SetValue<bool>("Jump", "JumpRightEnable", Configuration.jump_right);
            Configuration.IniCSMConfig.SetValue<bool>("Jump", "JumpFrontEnable", Configuration.jump_front);
            Configuration.IniCSMConfig.SetValue<bool>("Jump", "JumpBackEnable", Configuration.jump_back);
            Configuration.IniCSMConfig.SetValue<bool>("Roll", "RollLeftEnable", Configuration.roll_left);
            Configuration.IniCSMConfig.SetValue<bool>("Roll", "RollRightEnable", Configuration.roll_right);
            Configuration.IniCSMConfig.SetValue<bool>("Roll", "RollFrontEnable", Configuration.roll_front);
            Configuration.IniCSMConfig.SetValue<bool>("Roll", "RollBackEnable", Configuration.roll_back);
            Configuration.IniCSMConfig.SetValue<bool>("Prone", "ProneStanceEnable", Configuration.EnableProne);
            Configuration.IniCSMConfig.Save();
            this.saveSetting = 0;
        }

        private void OpenMenuButtons(string name, Control control, bool enableControl)
        {
            if (Game.WasCheatStringJustEntered(name))
                this.menu.Visible = true;
            if (!(Game.IsControlJustPressed(control) & enableControl))
                return;
            if (this.pool.AreAnyVisible)
                this.pool.HideAll();
            else
                this.menu.Visible = true;
        }
    }
}
