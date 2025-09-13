// Decompiled with JetBrains decompiler
// Type: CombatStance.Configuration
// Assembly: CombatStanceMovement, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 15398906-0FC5-4912-B95F-7B85C0671CB4
// Assembly location: C:\Users\clope\OneDrive\Desktop\CombatStanceMovement.dll

using GTA;

namespace CombatStance
{
    public class Configuration : Script
    {
        public static ScriptSettings IniCSMConfig;
        public static float RollLeftForceX;
        public static float RollLeftForceY;
        public static float RollRightForceX;
        public static float RollRightForceY;
        public static float RollBackForceX;
        public static float RollBackForceY;
        public static float JumpLeftForceX;
        public static float JumpLeftForceY;
        public static float JumpRightForceX;
        public static float JumpRightForceY;
        public static float JumpForwardForceX;
        public static float JumpForwardForceY;
        public static float JumpBackForceX;
        public static float JumpBackForceY;
        public static float RollForwardForceX;
        public static float RollForwardForceY;
        public static float JumpCrouchForwardForceX;
        public static float JumpCrouchForwardForceY;
        public static bool jump_left;
        public static bool jump_right;
        public static bool jump_front;
        public static bool jump_back;
        public static bool roll_left;
        public static bool roll_right;
        public static bool roll_front;
        public static bool roll_back;
        public static bool EnableProne;
        public static bool ControllerEnable;
        public static Control Key_Stance;
        public static Control PAD_Stance;

        public Configuration()
        {
            Configuration.IniCSMConfig = ScriptSettings.Load("scripts\\CombatStanceMovement.ini");
            Configuration.ControllerEnable = Configuration.IniCSMConfig.GetValue<bool>("Controller_Options", nameof(ControllerEnable), false);
            Configuration.JumpForwardForceX = Configuration.IniCSMConfig.GetValue<float>("Jump", "ForwadForceX", 0.1f);
            Configuration.JumpForwardForceY = Configuration.IniCSMConfig.GetValue<float>("Jump", "ForwardForceY", 0.0f);
            Configuration.JumpLeftForceX = Configuration.IniCSMConfig.GetValue<float>("Jump", "LeftForceX", 0.5f);
            Configuration.JumpLeftForceY = Configuration.IniCSMConfig.GetValue<float>("Jump", "LeftForceY", 0.0f);
            Configuration.JumpRightForceX = Configuration.IniCSMConfig.GetValue<float>("Jump", "RightForceX", 0.5f);
            Configuration.JumpRightForceY = Configuration.IniCSMConfig.GetValue<float>("Jump", "RightForceY", 0.0f);
            Configuration.JumpBackForceX = Configuration.IniCSMConfig.GetValue<float>("Jump", "BackForceX", 0.1f);
            Configuration.JumpBackForceY = Configuration.IniCSMConfig.GetValue<float>("Jump", "BackForceY", 0.0f);
            Configuration.JumpCrouchForwardForceX = Configuration.IniCSMConfig.GetValue<float>("Jump", "ForwadForceX", 0.1f);
            Configuration.JumpCrouchForwardForceY = Configuration.IniCSMConfig.GetValue<float>("Jump", "ForwardForceY", 0.0f);
            Configuration.RollForwardForceX = Configuration.IniCSMConfig.GetValue<float>("Roll", "ForwardForceX", 0.5f);
            Configuration.RollForwardForceY = Configuration.IniCSMConfig.GetValue<float>("Roll", "ForwardForceY", 0.0f);
            Configuration.RollLeftForceX = Configuration.IniCSMConfig.GetValue<float>("Roll", "LeftForceX", 0.5f);
            Configuration.RollLeftForceY = Configuration.IniCSMConfig.GetValue<float>("Roll", "LeftForceY", 0.0f);
            Configuration.RollRightForceX = Configuration.IniCSMConfig.GetValue<float>("Roll", "RightForceX", 0.5f);
            Configuration.RollRightForceY = Configuration.IniCSMConfig.GetValue<float>("Roll", "RightForceY", 0.0f);
            Configuration.RollBackForceX = Configuration.IniCSMConfig.GetValue<float>("Roll", "BackForceX", 0.5f);
            Configuration.RollBackForceY = Configuration.IniCSMConfig.GetValue<float>("Roll", "BackForceY", 0.0f);
            Configuration.Key_Stance = Configuration.IniCSMConfig.GetValue<Control>("Buttons", "Key", Control.VehicleHeadlight);
            Configuration.PAD_Stance = Configuration.IniCSMConfig.GetValue<Control>("Buttons", "PAD", Control.VehicleHeadlight);
            Configuration.jump_left = Configuration.IniCSMConfig.GetValue<bool>("Jump", "JumpLeftEnable", true);
            Configuration.jump_right = Configuration.IniCSMConfig.GetValue<bool>("Jump", "JumpRightEnable", true);
            Configuration.jump_front = Configuration.IniCSMConfig.GetValue<bool>("Jump", "JumpFrontEnable", true);
            Configuration.jump_back = Configuration.IniCSMConfig.GetValue<bool>("Jump", "JumpBackEnable", true);
            Configuration.roll_left = Configuration.IniCSMConfig.GetValue<bool>("Roll", "RollLeftEnable", true);
            Configuration.roll_right = Configuration.IniCSMConfig.GetValue<bool>("Roll", "RollRightEnable", true);
            Configuration.roll_front = Configuration.IniCSMConfig.GetValue<bool>("Roll", "RollFrontEnable", true);
            Configuration.roll_back = Configuration.IniCSMConfig.GetValue<bool>("Roll", "RollBackEnable", true);
            Configuration.EnableProne = Configuration.IniCSMConfig.GetValue<bool>("Prone", "ProneStanceEnable", true);
        }
    }
}
