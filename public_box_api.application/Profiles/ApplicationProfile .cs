using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
//using YourProject.Entities; // นำเข้า Entities จาก Domain Layer
//using YourProject.Dtos; // นำเข้า DTOs สำหรับการสื่อสารกับ Interface Adapters

namespace public_box_api.application.Profiles
{
  
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            //CreateMap<User, UserDto>();
            //CreateMap<Order, OrderDto>();
            // กำหนดการแม็พข้อมูลอื่น ๆ ตามต้องการ
        }
    }

}
