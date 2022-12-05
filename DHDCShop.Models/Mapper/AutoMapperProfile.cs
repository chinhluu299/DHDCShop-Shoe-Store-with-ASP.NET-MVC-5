using AutoMapper;
using DHDCShop.Models.Model;
using DHDCShop.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHDCShop.Models.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Message, MessageViewModel>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dst => dst.Username, opt => opt.MapFrom(x => x.FromUser.Username))
                .ForMember(dst => dst.Avatar, opt => opt.MapFrom(x => x.FromUser.AvatarPath))
                .ForMember(dst => dst.Content, opt => opt.MapFrom(x => x.Content))
                .ForMember(dst => dst.Timestamp, opt => opt.MapFrom(x => new DateTime(long.Parse(x.Timestamp)).ToLongTimeString()))
                .ForMember(dst => dst.Stick, opt => opt.MapFrom(x => x.Stick));
            CreateMap<MessageViewModel, Message>();
            CreateMap<Customer, ChatViewModel>()
                .ForMember(dst => dst.Avatar, opt => opt.MapFrom(x => x.AvatarPath))
                .ForMember(dst => dst.DisplayName, opt => opt.MapFrom(x => x.FullName));
               
                
        }
    }
}
