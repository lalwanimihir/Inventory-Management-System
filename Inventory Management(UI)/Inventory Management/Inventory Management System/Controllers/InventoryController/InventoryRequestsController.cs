using AutoMapper;
using Inventory_Management_System.Application.Dtos.InventoryDto.RequestDto;
using Inventory_Management_System.Application.Features.InventoryRequestDetailsFeature.Commands.Create;
using Inventory_Management_System.Application.Features.InventoryRequestDetailsFeature.Commands.Update;
using Inventory_Management_System.Application.Features.InventoryRequestDetailsFeature.Queries.GetAll;
using Inventory_Management_System.Application.Features.InventoryRequestDetailsFeature.Queries.GetById;
using Inventory_Management_System.Application.IUnitOfWork;
using Inventory_Management_System.Application.Services.IMailSendService;
using Inventory_Management_System.Application.Validations.InventoryRequestValidation;
using Inventory_Management_System.Application.Validations.QueryValidation;
using Inventory_Management_System.Domain.Entities.IdentityDbEntity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Inventory_Management_System.Controllers.InventoryController
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly InventoryRequestDetailsValidation _validationRules;
        private readonly UserManager<ApplicationUser> _userManager;

        public InventoryRequestsController(IMediator mediator, IMapper mapper, IMailSender mailSender, UserManager<ApplicationUser> userManager, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userManager = userManager;
            _validationRules = new InventoryRequestDetailsValidation(unitOfWorkRepository);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [Route("AddInventoryRequest")]
        public async Task<IActionResult> AddInventoryRequest(AddInventoryRequestDto addInventoryRequestDto)
        {
            var result = _validationRules.Validate(addInventoryRequestDto);
            if (result.IsValid)
            {
                var validData = _mapper.Map<AddInventoryRequestCommand>(addInventoryRequestDto);

                ClaimsPrincipal currentUser = HttpContext.User;
                ApplicationUser? user = await _userManager.FindByNameAsync(currentUser.Identity.Name);

                validData.UserId = user?.Id;

                var product = await _mediator.Send(validData);
                if (product.IsSucceeded == true)
                {
                    return Ok(product);
                }
                else
                {
                    return BadRequest(product);
                }
            }
            var errorMessage = result.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(errorMessage);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllInventoryRequests([FromQuery] QueryRequestDto queryRequest)
        {
            var validator = new QueryFilterValidation();
            var result = validator.Validate(queryRequest);

            if (!result.IsValid)
            {
                var error = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(error);
            }
            var queryRequests = _mapper.Map<GetAllInventoryRequestsQuery>(queryRequest);
            var products = await _mediator.Send(queryRequests);

            if (products != null)
            {
                return Ok(products);
            }
            return BadRequest(products);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("GetAllByUserId")]
        public async Task<IActionResult> GetAllByUserId([FromQuery] QueryRequestWithUserIdDto queryRequestWithUser)
        {
            var queryRequests = _mapper.Map<GetInventoryRequestsByUserIdQuery>(queryRequestWithUser);
            var products = await _mediator.Send(queryRequests);

            if (products != null)
            {
                return Ok(products);
            }
            return BadRequest(products);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("UpdateInventoryRequestStatus")]
        public async Task<IActionResult> UpdateInventoryRequestStatus(UpdateInventoryRequestDto updateInventoryRequestDto)
        {
            var data = await _mediator.Send(new UpdateInventoryRequestStatusCommand { Id = updateInventoryRequestDto.Id, Status = updateInventoryRequestDto.Status });
            if (data == null)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }
    }
}
