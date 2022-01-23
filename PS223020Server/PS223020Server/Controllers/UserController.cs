using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PS223020Server.BusinessLogic.Core.Interfaces;
using PS223020Server.BusinessLogic.Core.Models;
using PS223020Server.Core.Models;
using PS223020Server.DataAccess.Core.Interfaces.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PS223020Server.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("registration")]
        public async Task<ActionResult<UserInformationDto>> Register(UserIdentityDto userIdentityDto)
        {
            UserInformationBlo userInformationBlo = await _userService.RegisterWithPhone(userIdentityDto.NumberPrefix, userIdentityDto.Number, userIdentityDto.Password);

            return await ConvertToUserInformationAsync(userInformationBlo);
        }

        private async Task<UserInformationDto> ConvertToUserInformationAsync(UserInformationBlo userInformationBlo)
        {
            if (userInformationBlo == null) throw new ArgumentNullException(nameof(userInformationBlo));

            UserInformationDto userInformationDto = _mapper.Map<UserInformationDto>(userInformationBlo);

            return userInformationDto;
        }
    }
}
