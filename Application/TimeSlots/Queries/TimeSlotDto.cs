namespace Application.TimeSlots.Queries
{
    public class TimeSlotDto
    {
        public int Id { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Day { get; set; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<TimeSlot, TimeSlotDto>();
            }
        }
    }
}
