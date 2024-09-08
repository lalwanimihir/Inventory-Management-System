using AutoMapper;
using Inventory_Management_System.Application.Dtos.InventoryDto.RequestDto;
using Inventory_Management_System.Application.Dtos.InventoryDto.ResponseDto;
using Inventory_Management_System.Application.Features.InventoryRequestDetailsFeature.Commands.Create;
using Inventory_Management_System.Application.Features.InventoryRequestDetailsFeature.Queries.GetAll;
using Inventory_Management_System.Application.Features.InventoryRequestDetailsFeature.Queries.GetById;
using Inventory_Management_System.Application.Features.ProductDetailsFeature.Commands.Create;
using Inventory_Management_System.Application.Features.ProductDetailsFeature.Commands.Update;
using Inventory_Management_System.Application.Features.ProductDetailsFeature.Queries.GetAll;
using Inventory_Management_System.Domain.Entities.InventoryDbEntities;

namespace Inventory_Management_System.Application.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles() {
            CreateMap<AddProductRequestDto, AddProductCommand>().ReverseMap();
            CreateMap<AddProductCommand, ProductDetails>();
            CreateMap<AddProductRequestDto, UpdateProductCommand>().ReverseMap();
            CreateMap<UpdateProductCommand, ProductDetails>();
            CreateMap<GetProductsResponseDto, ProductDetails>().ReverseMap();
            CreateMap<AddInventoryRequestDto, AddInventoryRequestCommand>().ReverseMap();
            CreateMap<AddInventoryRequestCommand, InventoryRequestDetails>().ReverseMap();
            CreateMap<GetInventoryRequestsResponseDto, InventoryRequestDetails>().ReverseMap();
            CreateMap<UpdateProductRequestDto, UpdateProductCommand>().ReverseMap();
            CreateMap<GetAllInventoryRequestsQuery, QueryRequestDto>().ReverseMap();
            CreateMap<GetInventoryRequestsByUserIdQuery, QueryRequestWithUserIdDto>().ReverseMap();
            CreateMap<GetAllProductQuery,QueryRequestDto>().ReverseMap();
        }
    }
}
