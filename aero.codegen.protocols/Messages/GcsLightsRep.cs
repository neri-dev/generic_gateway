namespace infrastructure
{
    public class GcsLightsRep
	{
        public GcsLightsRep(bool isNavLightsOn, bool iStrobLightsOn)
        {
            IsNavLightsOn = isNavLightsOn;
            IStrobLightsOn = iStrobLightsOn;
        }

        public bool IsNavLightsOn { get; set; }
        public bool IStrobLightsOn { get; set; }
	}
}