🎮 Adaptive Performance Manager for Unity

This project provides an automatic performance adaptation system for Unity using the Adaptive Performance API.
It dynamically adjusts graphics quality based on the device’s thermal and performance state — helping reduce overheating, maintain stable FPS, and optimize battery usage on mobile devices.

🚀 Features

🔥 Thermal-based quality adjustment:
Automatically lowers quality when the device is about to overheat.

⚙️ CPU / GPU bottleneck detection:
Reduces quality when performance bottlenecks occur.

🌡️ Dynamic quality increase:
Gradually restores higher quality when the device cools down.

🧩 Integrated with Unity’s QualitySettings:
Automatically updates LOD bias, shadow distance, and target FPS.

🧠 How It Works

Every 2 seconds, the script checks the device’s thermal and performance status.
It then adjusts the quality level between Low (0), Medium (1), and High (2) automatically.

Condition	Action	Description
⚠️ Overheating (Throttling Imminent)	Decrease quality	Prevents further thermal throttling
⚙️ CPU / GPU Bottleneck	Decrease quality	Maintains smooth gameplay
🌡️ Cool & Stable	Increase quality	Improves visual fidelity
🧩 Quality Levels
Level	Target FPS	LOD Bias	Shadow Distance	Description
0 (Low)	30	0.5	20	Lowest quality, best stability
1 (Medium)	30	0.75	40	Balanced performance
2 (High)	30	1.0	80	Highest quality for powerful devices
🛠️ Setup

Install the Adaptive Performance package via Unity Package Manager:

Window → Package Manager → Adaptive Performance


Add the AdaptivePerformanceManager.cs file to your project.

Create an empty GameObject and attach the script:

GameObject → Create Empty → Add Component → AdaptivePerformanceManager


Run your scene and monitor adaptive quality changes in the Console.

🧾 Example Log Output
Adaptive Performance active!
[Adaptive] Quality: 2 | FPS: 60
[Adaptive] Quality: 1 | FPS: 30 (Thermal warning detected)
[Adaptive] Quality: 2 | FPS: 30 (Device cooled down)
