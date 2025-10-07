using authbackend.Entities;

namespace authbackend.Dtos; 

public class ReactTestDto{
      public string User { get; set; } = string.Empty;
        
        // Puedes usar un array o una lista dependiendo de tus necesidades
        public string[] Roles { get; set; } = Array.Empty<string>();
        
        public ClaimDto[] Claims { get; set; } = Array.Empty<ClaimDto>();

        public string AccessToken = string.Empty;

         public string RefreshToken = string.Empty;

}