using LiveSplit.Memory;
using System;
using System.Diagnostics;
namespace LiveSplit.LightFall {
	public partial class SplitterMemory {
		private static ProgramPointer Manager = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.Steam, "558BEC83EC088B05????????83EC086A0050E8????????83C41085C00F84????????8B05", 8));
		public Process Program { get; set; }
		public bool IsHooked { get; set; } = false;
		public DateTime LastHooked;

		public SplitterMemory() {
			LastHooked = DateTime.MinValue;
		}
		public string RAMPointers() {
			return Manager.GetPointer(Program).ToString("X");
		}
		public string RAMPointerVersion() {
			return Manager.Version.ToString();
		}
		public Level CurrentLevel() {
			return (Level)Manager.Read<int>(Program, -0x38, 0x30);
		}
		public Level NextLevel() {
			return (Level)Manager.Read<int>(Program, -0x38, 0x28);
		}
		public Level LevelLoading() {
			return (Level)Manager.Read<int>(Program, -0x38, 0x2c);
		}
		public bool ReachedEnd() {
			return Manager.Read<bool>(Program, -0x38, 0x34);
		}
		public float GameTime() {
			IntFloat time = default(IntFloat);
			time.FloatVal = Manager.Read<float>(Program, -0x24, 0x18);
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