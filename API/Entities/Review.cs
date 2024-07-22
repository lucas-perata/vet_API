using API.Entities.Identity;

namespace API.Entities
{
    // TODO: idea de agregar a las reviews un campo para que sean revisadas por el administrador
    public class Review
    {
        public int Id { get; set; }
        public ReviewStar Stars { get; set; }
        public string Body { get; set; }
        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }
        public string VetId { get; set; }

        // public bool Pending {get; set;}
        public AppUser Vet { get; set; }
    }

    public enum ReviewStar
    {
        AwfulService,
        BadService,
        GoodService,
        ExcellentService,
        PerfectService
    }
}

