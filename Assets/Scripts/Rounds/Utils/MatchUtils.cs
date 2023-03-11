public static class MatchUtils
{
    static Room GetMatchRoom(Match match)
    {
        switch (match)
        {
            case Match.Match1:
                return Room.GamingRoom;
            case Match.Match2:
                return Room.CinemaRoom;
            case Match.Match3:
                return Room.GamingRoom;
            case Match.Match4:
                return Room.CinemaRoom;
            case Match.Match5:
                return Room.GamingRoom;
            case Match.Match6:
                return Room.CinemaRoom;
            case Match.Match7:
                return Room.GamingRoom;
            default:
                return Room.GamingRoom;
        }
    }

    public const int TeamCountInMatch = 2;
}