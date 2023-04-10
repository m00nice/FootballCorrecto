class PointsComparer : IComparer<Team>
{
    public int Compare(Team x, Team y)
    {
        // Compare the ages of the two Person objects
        if (x.points > y.points)
        {
            return -1;
        }
        else if (x.points < y.points)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}