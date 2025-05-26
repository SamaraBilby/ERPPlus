using ERPPlus.Application.DTOs;
using ERPPlus.Domain.Entities;

namespace ERPPlus.Application.Mappers
{
    public static class MapperExtencions
    {

        public static ProductOrderSummaryDTO ToProductOrderSummaryDTO(this ProductOrder productOrder)
        {
            return new ProductOrderSummaryDTO
            {
                ProductId = productOrder.ProductId,
                ProductName = productOrder.Product.Name,
                Price = productOrder.Product.Price,
                Quantity = productOrder.Quantity,
                PriceTotal = productOrder.PriceTotal,
            };
        }

        public static OrderSummaryDTO ToOrderSummaryDTO(this Order order) {

            return new OrderSummaryDTO
            {
                Id = order.Id,
                Date = order.Date,
                ValueTotal = order.ValueTotal,
                ProductOrders = order.ProductOrders?.Select(po => po.ToProductOrderSummaryDTO()).ToList() ?? new(),
            };
        }

        public static OrderDTO ToOrderDTO(this Order order)
        {
            return new OrderDTO
            {
                Id = order.Id,
                Date = order.Date,
                ValueTotal = order.ValueTotal,
                CustomerId = order.CustomerId,
                CustomerName = order.Customer.Name,
                ProductOrders = order.ProductOrders?.Select(po => po.ToProductOrderSummaryDTO()).ToList() ?? new(),
            };
        }

        public static CustomerResponseDTO ToCustomerResponseDTO(this Customer customer)
        {
            return new CustomerResponseDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Orders = customer.Orders?.Select(o => o.ToOrderSummaryDTO()).ToList() ?? new(),
            };
        }

        public static CustomerDTO ToCustomerDTO(this Customer customer) 
        {

            return new CustomerDTO
            {
                Id= customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Orders = customer.Orders?.Select(o => o.ToOrderDTO()).ToList() ?? new(),
            };
        
        }

        public static Customer ToCustomerEntity(this CreateCustomerDTO createCustomer)
        {
            return new Customer
            {
                Id = Guid.NewGuid(),
                Name = createCustomer.Name,
                Email = createCustomer.Email,
                PasswordHash = createCustomer.PasswordHash,
            };
        }

        public static void UpadateCustomerFromDTO(this Customer customer, UpdateCustomerDTO updateCustomerDTO)
        {
            customer.Name = updateCustomerDTO.Name;
            customer.Email = updateCustomerDTO.Email;
        }

        public static ProductDTO ToProductDTO(this Product product)
        {
            return new ProductDTO
            {

                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
            };
        }

        public static Product ToProductEntity(this CreateProductDTO createProduct)
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = createProduct.Name,
                Price = createProduct.Price,
            };
        }

        public static void UpdateProductFromDTO(this Product product, ProductDTO productDTO) 
        {
            product.Name = productDTO.Name;
            product.Price = productDTO.Price;
        }

    }
}
