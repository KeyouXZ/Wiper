using Microsoft.Xna.Framework;
using System;
using System.Security.Cryptography;
using System.Threading;
using System.Timers;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace ItemClear {
    [ApiVersion(2, 1)]
    public class Wiper : TerrariaPlugin {
        public override string Author => "Keyou";
        public override string Name => "Wiper";
        public override string Description => "Clear trash items";
        public override Version Version => new Version(1, 0, 0);
        
        public static Config config;
        public static bool enabled;
        public static string wiperName;
        public static string wiperColor;
        public static int timeUntilClearSecond;
        public static int checkIntervalSecond;
        public static int numberOfItemsToActivateTheWiper;
        public static bool inprogress = false;
        public static DateTime LastCheck = DateTime.UtcNow;
        public static System.Timers.Timer Timer = new System.Timers.Timer();
        public static string tag;

		public Wiper(Main game) : base(game) { }

        public override void Initialize() {
			// Get Configs
			config = Config.Read();
			enabled = config.enabled;
			wiperName = config.wiperName;
            wiperColor = config.wiperColor;
			timeUntilClearSecond = config.timeUntilClearSecond;
			checkIntervalSecond = config.checkIntervalSecond;
			numberOfItemsToActivateTheWiper = config.numberOfItemsToActivateTheWiper;
			
            tag = GetWiperColorTag();

			// Timer
			Timer.Interval = checkIntervalSecond * 1000;
			Timer.Enabled = enabled;
			Timer.Elapsed += new ElapsedEventHandler(TimerElapsed);

			GeneralHooks.ReloadEvent += Reload;
        }

        public string GetWiperColorTag()
        {
            Color color = GetColorFromString(wiperColor);

            string tag = TShock.Utils.ColorTag($"{wiperName}:", color);
            return tag;
        }

        private Color GetColorFromString(string colorName)
        {
            switch (colorName.ToLower())
            {
                case "red":
                    return Color.Red;
                case "black":
                    return Color.Black;
                case "green":
                    return Color.Green;
                case "gray":
                    return Color.Gray;
                case "white":
                    return Color.White;
                case "orange":
                    return Color.Orange;
                case "yellow":
                    return Color.Yellow;
                case "purple":
                    return Color.Purple;
                case "brown":
                    return Color.Brown;
                default:
                    TShock.Log.ConsoleWarn("[Wiper] Invalid color '{0}' specified. Defaulting to Red.", colorName);
                    return Color.Red;
            }
        }


        public void Reload(ReloadEventArgs args)
        {
            args.Player.SendSuccessMessage("[Wiper] Configuration reloaded successfully!");
            
            // Get configs
            config = Config.Read();
			enabled = config.enabled;
			wiperName = config.wiperName;
            wiperColor = config.wiperColor;
            timeUntilClearSecond = config.timeUntilClearSecond;
			checkIntervalSecond = config.checkIntervalSecond;
			numberOfItemsToActivateTheWiper = config.numberOfItemsToActivateTheWiper;
			
            tag = GetWiperColorTag();

            Timer.Interval = checkIntervalSecond * 1000;
            Timer.Enabled = enabled;
            Timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {}
            base.Dispose(disposing);
        }
        
        public static void TimerElapsed(object sender, ElapsedEventArgs args) {
            bool flag = !inprogress;
            if (flag)
            {
                int num = 0;
                int num2;
                for (int i = 0; i < 400; i = num2 + 1)
                {
                    bool active = Main.item[i].active;
                    if (active)
                    {
                        num2 = num;
                        num = num2 + 1;
                    }
                    num2 = i;
                }
                //bool flag2 = num > 150;
                bool flag2 = num > numberOfItemsToActivateTheWiper;
                if (flag2)
                {
                    int num3 = timeUntilClearSecond;
                    inprogress = true;

                    TShock.Utils.Broadcast(string.Format("{0} Discovered {1} trash items. Removing in {2} seconds", tag, num, num3), Color.Silver);
                    Thread.Sleep(1000 * num3);
                    for (int j = 0; j < 400; j = num2 + 1)
                    {
                        bool active2 = Main.item[j].active;
                        if (active2)
                        {
                            Main.item[j].active = false;
                            TSPlayer.All.SendData(PacketTypes.UpdateItemDrop, "", j, 0f, 0f, 0f, 0);
                        }
                        num2 = j;
                    }
                    TShock.Utils.Broadcast(string.Format("{0} All trash items have been cleared", tag), Color.Silver);
                    inprogress = false;
                }
            }
        }
    }
}
