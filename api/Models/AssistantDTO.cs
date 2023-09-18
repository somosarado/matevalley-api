namespace api.Models
{
    public class AssistantDTO : BaseDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }

        public bool PayCash { get; set; }

        public bool PayQr { get; set; }

        public bool PrintedLabel { get; set; }

        public int Calification { get; set; }

        public int PrintedSuccesful { get; set; }
    }
}
