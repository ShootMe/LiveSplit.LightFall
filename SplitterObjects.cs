namespace LiveSplit.LightFall {
	public enum Level {
		NONE,
		SplashScreen = 10,
		A0Prologue,
		A1L01 = 20,
		A1L02,
		A1L03,
		A2L01_Sentinel = 30,
		A2L02_Hautes_Herbes,
		A2L03_Temple,
		A3L01_The_Ghostfields = 40,
		A3L02_The_Rogue_Friend,
		A3L03_The_Viperas_Forest = 43,
		A3L04_Sylveon,
		A4L01_Giant_Mouth = 50,
		A4L02_Hidden_Grotto,
		A4L03_Numbras_Core,
		A4L04_CelestialPalace,
		SpeedRun_AnimalKingdom = 60,
		SpeedRun_HotPursuit,
		SpeedRun_SylveonBackDoor,
		SpeedRun_InvertedWorld,
		SpeedRun_VoidHole,
		Max = 2147483647
	}
	public enum LogObject {
		CurrentSplit,
		Pointers,
		PointerVersion,
		CurrentLevel,
		NextLevel,
		LevelLoading,
		ReachedEnd
	}
}