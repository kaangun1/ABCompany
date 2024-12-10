using AB.Entities.Models.Concrete;
using AB.MVC.Models.VMS.AccountVM;
using AutoMapper;

namespace AB.MVC.AutoMapperConfig
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User,EditVM>().ReverseMap();
            CreateMap<User,LoginVM>().ReverseMap();
        }
    }
}
