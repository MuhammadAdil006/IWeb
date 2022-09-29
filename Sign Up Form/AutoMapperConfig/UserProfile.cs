using AutoMapper;
using Sign_Up_Form.Models.ViewModel;

namespace Sign_Up_Form.MapperConfiguration
{
    public class UserProfile:Profile
    {
        
       public UserProfile()
        {
            CreateMap<UserDatum,UserWithLessDetail > ();
        }
    }
}
