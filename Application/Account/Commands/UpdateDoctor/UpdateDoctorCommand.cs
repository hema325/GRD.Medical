namespace Application.Account.Commands.UpdateDoctor
{
    public class UpdateDoctorCommand: IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public int Experience { get; set; }
        public decimal ConsultFee { get; set; }
        public int SpecialityId { get; set; }
        public IEnumerable<int> LanguageIds { get; set; }
    }
}
