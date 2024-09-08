using AutoMapper;
using Inventory_Management_System.Application.Dtos.InventoryDto.RequestDto;
using Inventory_Management_System.Application.Features.ProductDetailsFeature.Commands.Create;
using Inventory_Management_System.Application.Features.ProductDetailsFeature.Commands.Delete;
using Inventory_Management_System.Application.Features.ProductDetailsFeature.Commands.Update;
using Inventory_Management_System.Application.Features.ProductDetailsFeature.Queries.GetAll;
using Inventory_Management_System.Application.Features.ProductDetailsFeature.Queries.GetById;
using Inventory_Management_System.Application.IUnitOfWork;
using Inventory_Management_System.Application.Services.IMailSendService;
using Inventory_Management_System.Application.Validations.ProductDetailsValidations;
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
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IMailSender _mailSender;
        private readonly ProductDetailsValidation _validationRules;
        private readonly UpdateProductDetailsValidation _validationRulesUpdate;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(IMediator mediator, IMapper mapper,IMailSender mailSender, UserManager<ApplicationUser> userManager, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _mailSender = mailSender;
            _userManager = userManager;
            _validationRules = new ProductDetailsValidation();
            _validationRulesUpdate = new UpdateProductDetailsValidation();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("AddProducts")]
        public async Task<IActionResult> CreateProduct(AddProductRequestDto addProductRequestDto)
        {
            var result = _validationRules.Validate(addProductRequestDto);
            if (result.IsValid)
            {
                var validData = _mapper.Map<AddProductCommand>(addProductRequestDto);

                ClaimsPrincipal currentUser = HttpContext.User;
                ApplicationUser? user = await _userManager.FindByNameAsync(currentUser.Identity.Name);

                validData.AddedBy = user?.Id;

                var product = await _mediator.Send(validData);
                if(product.IsSucceeded == true)
                {
                    var usersInRole = await _userManager.GetUsersInRoleAsync("User");

                    var emailList = usersInRole
                                    .Select(user => user.Email)
                                    .ToList();

                    await _mailSender.SendMailToUser(emailList);
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
        public async Task<IActionResult> GetAllProducts([FromQuery] QueryRequestDto queryRequest)
        {
            var validator = new QueryFilterValidation();
            var result = validator.Validate(queryRequest);

            if (!result.IsValid)
            {
                var error = result.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(error);
            }
            var queryRequests = _mapper.Map<GetAllProductQuery>(queryRequest);
            var products = await _mediator.Send(queryRequests);

            if(products !=  null)
            {
                return Ok(products);
            }
            return BadRequest(products);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("GetAllActiveProducts")]
        public async Task<IActionResult> GetAllActiveProducts()
        {
            var productData = await _mediator.Send(new GetAllActiveProductsQuery());
            if (productData.IsSucceeded == true)
            {
                return Ok(productData);
            }
            return BadRequest(productData);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var productData = await _mediator.Send(new GetProductByIdQuery() { Id = id });

            if (productData == null)
            {
                return NotFound();
            }
            return Ok(productData);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequestDto updateProductRequestDto)
        {
            var result = _validationRulesUpdate.Validate(updateProductRequestDto);
            if (result.IsValid)
            {
                var validData = _mapper.Map<UpdateProductCommand>(updateProductRequestDto);
                var product = await _mediator.Send(validData);
                if (product == null) { return NotFound(product); }
                return Ok(product);
            }
            var errorMessage = result.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(errorMessage);
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("UpdateProductAvailability")]
        public async Task<IActionResult> UpdateProductAvailability(int id)
        {
            var data = await _mediator.Send(new UpdateProductAvailabilityCommand() { Id = id });
            if (data == null)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            ClaimsPrincipal currentUser = HttpContext.User;
            ApplicationUser? user = await _userManager.FindByNameAsync(currentUser.Identity.Name);
            
            var data = await _mediator.Send(new DeleteProductCommand { Id = id, UserId=user?.Id });
            if (data == null)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }
    }
}
