
using authbackend.Entities;

namespace authbackend.Dtos; 

public class MyAccountResponseDto{
        public string User { get; set; } = string.Empty;
        
        // Puedes usar un array o una lista dependiendo de tus necesidades
        public int[] Roles { get; set; } = Array.Empty<int>();
        
        public ClaimDto[] Claims { get; set; } = Array.Empty<ClaimDto>();

}