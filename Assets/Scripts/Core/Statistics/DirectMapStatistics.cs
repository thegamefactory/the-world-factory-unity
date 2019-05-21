namespace TWF
{
    public class DirectMapStatistics : IMapStatistics
    {
        IWorldView worldView;

        public DirectMapStatistics(IWorldView worldView)
        {
            this.worldView = worldView;
        }

        //public int GetCapacity(UsageType usageType)
        //{
        //    return MapBuidlings(b => b.GetCapacity(usageType));
        //}

        //public int GetOccupation(UsageType usageType)
        //{
        //    return MapBuidlings(b => b.GetOccupation(usageType));
        //}

        //public int MapBuidlings(Func<IBuildingModel, int> mapper)
        //{
        //    return worldView.ToAllPositions()
        //        .ToBuildings()
        //        .Select(b => b.GetOccupation(UsageType.HOUSING))
        //        .Sum();
        //}
    }
}
