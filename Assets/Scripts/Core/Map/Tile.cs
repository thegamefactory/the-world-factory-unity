﻿namespace TWF.Map
{
    public class Tile
    {
        public enum Zone
        {
            EMPTY,
            RESIDENTIAL
        }

        public Zone Type { get; internal set; } = Zone.EMPTY;
    }
}