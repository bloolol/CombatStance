// Decompiled with JetBrains decompiler
// Type: CombatStance.Timer
// Assembly: CombatStanceMovement, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 15398906-0FC5-4912-B95F-7B85C0671CB4
// Assembly location: C:\Users\clope\OneDrive\Desktop\CombatStanceMovement.dll

using GTA;

namespace CombatStance
{
    public class Timer
    {
        private bool enabled;
        private int interval;
        private int waiter;

        public bool Enabled
        {
            get => this.enabled;
            set => this.enabled = value;
        }

        public int Interval
        {
            get => this.interval;
            set => this.interval = value;
        }

        public int Waiter
        {
            get => this.waiter;
            set => this.waiter = value;
        }

        public Timer(int interval)
        {
            this.interval = interval;
            this.waiter = 0;
            this.enabled = false;
        }

        public Timer()
        {
            this.interval = 0;
            this.waiter = 0;
            this.enabled = false;
        }

        public void Start()
        {
            this.waiter = Game.GameTime + this.interval;
            this.enabled = true;
        }

        public void Reset() => this.waiter = Game.GameTime + this.interval;
    }
}
