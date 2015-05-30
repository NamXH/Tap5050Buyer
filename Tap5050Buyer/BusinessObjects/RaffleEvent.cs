using System;

namespace Tap5050Buyer
{
    public class RaffleEvent
    {
        public int Id {
            get;
            set;
        }

        public string Name {
            get;
            set;
        }

        public int UserId
        {
            get;
            set;
        }

        public string Organization
        {
            get;
            set;
        }

        public int locationId
        {
            get;
            set;
        }

        public string LocationName
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string ImageUrl
        {
            get;
            set;
        }

        public RaffleEvent()
        {
        }
    }
}