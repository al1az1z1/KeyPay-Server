using AutoMapper;
using KeyPay.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyPay.Presentation.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<KeyPay.Data.Models.User, KeyPay.Data.Dto.Site.Admin.Users.UserForDetailDto>()

                .ForMember(current => current.PhotoUrl, opt =>
                {

                    opt.MapFrom(src => src.Photoes.FirstOrDefault(p => p.IsMain).Url);

                }).ForMember(current => current.Age , opt=>
                
                {
                    // using extension methode from common service
                    opt.MapFrom(src => src.DateOfBirth.ToAge());

                });



            CreateMap<KeyPay.Data.Models.User, KeyPay.Data.Dto.Site.Admin.Users.UserForListDto>();

            CreateMap<KeyPay.Data.Models.Photo, KeyPay.Data.Dto.Site.Admin.Photos.PhotoForUserDetailDto>();

            CreateMap<KeyPay.Data.Models.BankCard, KeyPay.Data.Dto.Site.Admin.BankCards.BankCardForUserDetailDto>();

            //for update 
            CreateMap<KeyPay.Data.Dto.Site.Admin.Users.UserForUpdateDto, KeyPay.Data.Models.User>();
        }
    }
}
