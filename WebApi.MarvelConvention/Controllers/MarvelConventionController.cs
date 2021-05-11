using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationService.Interfaces;
using Domain.Model;
using Domain.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.MarvelConvention.Dto;

namespace WebApi.MarvelConvention.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarvelConventionController : ControllerBase
    {
        private readonly IConventionService _conventionService;
        private readonly IUserManagementService _userManagementService;
        private readonly IHttpContextAccessor _context;


        public MarvelConventionController(IConventionService conventionService, IUserManagementService userManagementService, IHttpContextAccessor context)
        {
            _conventionService = conventionService ?? throw new ArgumentNullException(nameof(conventionService));
            _userManagementService = userManagementService ?? throw new ArgumentNullException(nameof(userManagementService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        [Route("GetConventionRelatedData")]
        [Authorize(Policy = Policies.Admin)] 
        public async Task<IActionResult> GetConventionRelatedData()
        {
            var conventionRelatedData = await _conventionService.GetConventionRelatedData();
            return Ok(conventionRelatedData);
        }

        [HttpPost]
        [Route("CreateConvention")]
        [Authorize(Policy = Policies.Admin)] 
        public async Task<IActionResult> CreateConvention([FromBody]CreateNewConventionDto conventionDto)
        {
            
            Speaker speaker = null;
            if (conventionDto.SpeakerId != null)
            {
                speaker = await _userManagementService.GetSpeakerById(conventionDto.SpeakerId);
            }
            await _conventionService.CreateConvention(Convention.Create(Guid.NewGuid(), conventionDto.Name, conventionDto.Topic, conventionDto.MaxCap, 0, speaker));
            return Ok();
        }
        [HttpPost]
        [Route("AllocateConvention")]
        public IActionResult AllocateConvention([FromBody]AllocateConventionDto allocateConventionDtoconvention)
        {
            string userId = _context.HttpContext.User.Claims
                .First(i => i.Type == "userId").Value;
            var conventionParticipant = ConventionParticipant.Create(Guid.NewGuid(), Guid.Parse( userId),
                allocateConventionDtoconvention.ConventionId, allocateConventionDtoconvention.IsReserved);
            
            _conventionService.AllocateConvention(conventionParticipant);
            return Ok();
        }

        [HttpGet]
        [Route("GetConventions")]
        public async Task<IActionResult> GetConventions()
        {
         
            var conventions = await _conventionService.GetConventions();
            
            var conventionDtos = new List<ConventionDto>();
            foreach (var convention in conventions)
            {
                var conventionDto = new ConventionDto
                {
                    Id = convention.Id,
                    Topic = convention.Topic,
                    Name = convention.Name,
                    IsFull = convention.HasSpeaker() ? "YES" : "NO",
                    HasSpeaker = convention.HasSpeaker() ? "YES" : "NO",

                };
                conventionDtos.Add(conventionDto);
            }

            return Ok(conventionDtos);
        }

       
    }
}