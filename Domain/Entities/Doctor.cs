namespace Domain.Entities
{
    public class Doctor: EntityBase
    {
        public int Experience { get; set; }
        public string Biography { get; set; }
        public decimal ConsultFee { get; set; }
        
        public int UserId { get; set; }
        public int SpecialityId { get; set; }

        public User User { get; set; }
        public Speciality Speciality { get; set; }
        public ICollection<Language> Languages { get; set; }
    }
}
