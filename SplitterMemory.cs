using LiveSplit.Memory;
using System;
using System.Diagnostics;
namespace LiveSplit.LightFall {
	public partial class SplitterMemory {
		private static ProgramPointer Singletons = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.Steam, "558BEC83EC088B05????????83EC086A0050E8????????83C41085C00F84????????8B05", 8));
		public Process Program { get; set; }
		public bool IsHooked { get; set; } = false;
		public DateTime LastHooked;

		public SplitterMemory() {
			LastHooked = DateTime.MinValue;
		}
		public string RAMPointers() {
			return Singletons.GetPointer(Program).ToString("X");
		}
		public string RAMPointerVersion() {
			return Singletons.Version.ToString();
		}
		public Level CurrentLevel() {
			//Singletons.levelManager.currentLevel
			return (Level)Singletons.Read<int>(Program, -0x3c, 0x30);
		}
		public Level NextLevel() {
			//Singletons.levelManager.nextLevel
			return (Level)Singletons.Read<int>(Program, -0x3c, 0x28);
		}
		public Level LevelLoading() {
			//Singletons.levelManager.levelLoading
			return (Level)Singletons.Read<int>(Program, -0x3c, 0x2c);
		}
		public bool ReachedEnd() {
			//Singletons.levelManager.hasReachedEnd
			return Singletons.Read<bool>(Program, -0x3c, 0x34);
		}
		public float GameTime() {
			//Singletons.gameMode.runTime
			IntFloat time = default(IntFloat);
			byte[] data = Singletons.ReadBytes(Program, 8, -0x28, 0x18);
			time.FloatVal = BitConverter.ToSingle(data, 0);
			if (time.IntVal == 230887) {
				time.FloatVal = BitConverter.ToSingle(data, 4);
			}
			time.IntVal ^= 230887;
			return time.FloatVal;
		}
		public bool HookProcess() {
			IsHooked = Program != null && !Program.HasExited;
			if (!IsHooked && DateTime.Now > LastHooked.AddSeconds(1)) {
				LastHooked = DateTime.Now;
				Process[] processes = Process.GetProcessesByName("LightFall");
				Program = processes != null && processes.Length > 0 ? processes[0] : null;

				if (Program != null && !Program.HasExited) {
					MemoryReader.Update64Bit(Program);
					IsHooked = true;
				}
			}

			return IsHooked;
		}
		public void Dispose() {
			if (Program != null) {
				Program.Dispose();
			}
		}
	}
}