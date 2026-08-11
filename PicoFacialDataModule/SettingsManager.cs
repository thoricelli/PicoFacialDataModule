using System.Reflection;
using System.Text.Json;

namespace PicoFacialDataModule
{
    public class ModuleSettings {
        public bool DisableEyeTracking { get; set; }
        public bool DisableFaceTracking { get; set; }
    }

    public class SettingsManager
    {
        private const string configFileName = "PicoFacialDataModule.json";
        
        public static ModuleSettings GetOrCreate()
        {
            string configPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, configFileName);

            if (!File.Exists(configPath))
            {
                ModuleSettings moduleSettings = new ModuleSettings();

                string defaultJson = JsonSerializer.Serialize(moduleSettings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configPath, defaultJson);

                return moduleSettings;
            }

            return JsonSerializer.Deserialize<ModuleSettings>(File.ReadAllText(configPath))!;
        }
    }
}
