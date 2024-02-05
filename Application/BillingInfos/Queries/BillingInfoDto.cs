namespace Application.BillingInfos.Queries
{
    public class BillingInfoDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidIn { get; set; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<BillingInfo, BillingInfoDto>();
            }
        }
    }
}
