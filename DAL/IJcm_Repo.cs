using Domain.Models;

namespace DAL
{
    public interface IJCM_Repo
    {
        Task<OrderDto> AddOrderAsync(OrderImportDto dto);
        Task AddOrdersAsync(List<OrderImportDto> orderList);
        Task<OrderDto> GetOrderAsync(int id);
        Task<(List<OrderDto>, PaginationMetadata)> GetOrdersAsync(int pageNumber = 0, int pageSize = 10);
        Task<bool> OrderExistsAsync(int orderId);
        Task<bool> RemoveOrderAsync(int id);
        Task<bool> RemoveOrderAsync(OrderDto dto);
        Task<bool> SaveChangesAsync();
    }
}