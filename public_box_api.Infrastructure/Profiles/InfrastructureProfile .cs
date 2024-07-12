using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
//using YourProject.Entities; // นำเข้า Entities จาก Domain Layer
//using YourProject.Dtos; // นำเข้า DTOs สำหรับการสื่อสารกับ Use Cases

namespace public_box_api.Infrastructure.Profiles
{
    public  class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {
            //CreateMap<UserDto, User>();
            //CreateMap<OrderDto, Order>();
            // กำหนดการแม็พข้อมูลอื่น ๆ ตามต้องการ
        }
    }
}
