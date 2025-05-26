using ERPPlus.Application.DTOs;
using ERPPlus.Application.Exceptions;
using ERPPlus.Application.Mappers;
using ERPPlus.Application.Services.Interfaces;
using ERPPlus.Domain.Entities;
using ERPPlus.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPPlus.Application.Services
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        public OrderService(IOrderRepository orderRepository,ICustomerRepository customerRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository=customerRepository;
            _productRepository=productRepository;
        }

        public async Task<(IEnumerable<OrderDTO> Items, int TotalItems)> GetPagedAsync(int page, int pageSize) 
        {
            var orders = await _orderRepository.GetPagedAsync(page, pageSize);
            var totalItems = await _orderRepository.GetTotalCountAsync();

            return (orders.Select(o => o.ToOrderDTO()), totalItems);
        }

        public async Task<OrderDTO> GetByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order is null)
                throw new NotFoundException("Pedido não encontrado");

            return order.ToOrderDTO();
        }

        public async Task<OrderDTO> CreateAsync(CreateOrderDTO dto)
        {
            var customer = await _customerRepository.GetByIdAsync(dto.CustomerId) 
                ?? throw new NotFoundException("Cliente não encontrado");

            if(dto.ProductOrders is null || !dto.ProductOrders.Any())
                throw new BusinessRuleException("É necessário ao menos um produto no pedido.");

            var productOrders = new List<ProductOrder>();
            decimal valueTotal = 0;

            foreach (var item in dto.ProductOrders)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId) 
                    ?? throw new NotFoundException($"Produto com ID {item.ProductId} não encontrado");

                var priceTotal = product.Price * item.Quantity;

                var produtOrder = new ProductOrder
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    PriceTotal = priceTotal,
                };

                productOrders.Add(produtOrder);
                valueTotal += priceTotal;
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id,
                ProductOrders = productOrders,
                ValueTotal = valueTotal,
                Date = DateTime.UtcNow,
            };

            await _orderRepository.AddAsync(order);
            var savedOrder = await _orderRepository.GetByIdAsync(order.Id);

            return savedOrder.ToOrderDTO();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order != null)
                throw new NotFoundException("Pedido não encontrado");

            await _orderRepository.DeleteAsync(order);
            return true;
        }

    }
}
