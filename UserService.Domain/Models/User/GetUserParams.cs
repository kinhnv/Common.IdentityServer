using i3rothers.Domain.Models;

namespace UserService.Domain.Models.User
{
    public class GetUserParams : GetPaginationParams
    {
        public GetUserParams()
        {
            IsDeleted = false;
        }
    }
}
