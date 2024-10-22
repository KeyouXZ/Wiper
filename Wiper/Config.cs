using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using TShockAPI;

namespace ItemClear {
    public class Config {
		public bool enabled { get; set; } = true;

        [JsonProperty("Wiper Name")]
        public string wiperName { get; set; } = "Wiper";

		[JsonProperty("Wiper Color")]
		public string wiperColor { get; set; } = "Red";

        [JsonProperty("Wiper Color List (Doc)")]
		private string wiperColorList { get;  set; } = "Example: Black, Red, Green, Gray, White, Orange, Yellow, Purple, Brown";

        [JsonProperty("Check Interval in Second")]
        public int checkIntervalSecond { get; set; } = 3;

        [JsonProperty("Time Until Clear in Second")]
        public int timeUntilClearSecond { get; set; } = 10;

        [JsonProperty("Number of Items to Activate The Wiper")]
        public int numberOfItemsToActivateTheWiper { get; set; } = 150;

        public void Write() {
			string path = Path.Combine(TShock.SavePath, "Wiper.json");
			File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
		}
		public static Config Read() {
			string filepath = Path.Combine(TShock.SavePath, "Wiper.json");

			try {
				Config config = new Config();

				if (!File.Exists(filepath)) {
					File.WriteAllText(filepath, JsonConvert.SerializeObject(config, Formatting.Indented));
				}
				config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(filepath));


				return config;
			}
			
			catch (Exception ex) {
				TShock.Log.ConsoleError(ex.ToString());
				return new Config();
			}
		}
	}
}