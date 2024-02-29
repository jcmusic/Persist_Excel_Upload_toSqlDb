using AutoMapper;
using DAL.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL
{
    public class JCM_Repo : IJCM_Repo
    {
        #region Fields/Ctor

        private readonly JcmContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public JCM_Repo(JcmContext context, ILogger<JCM_Repo> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        #endregion

        public async Task Test()
        {
            var o = await GetOrderAsync(1);

            var ord = new Order();
            ord.Name = "Name";
            ord.OrderNumber = "678";
            ord.OrderDate = DateTime.Now;
            ord.Phone = "1234567";
            ord.Total = 55.07M;

            await _context.AddAsync(ord);
            var result = await SaveChangesAsync();
        }

        public async Task<OrderDto> AddOrderAsync(OrderImportDto dto)
        {
            var entity = _mapper.Map<Order>(dto);
            await _context.Orders.AddAsync(entity);

            await SaveChangesAsync();

            return _mapper.Map<OrderDto>(entity);
        }

        public async Task AddOrdersAsync(List<OrderImportDto> orderList)
        {
            var entityList = _mapper.Map<List<Order>>(orderList);
            await _context.AddRangeAsync(entityList);
            var result = await SaveChangesAsync();

            return;
        }

        public async Task<OrderDto> GetOrderAsync(int id)
        {
            var entity = await _context.Orders
                .SingleOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<OrderDto>(entity);
        }

        public async Task<(List<OrderDto>, PaginationMetadata)> GetOrdersAsync(int pageNumber = 0, int pageSize = 15)
        {
            int totalRecords = await _context.Orders.CountAsync();

            var entityList = await _context.Orders.AsNoTracking()
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mappedList = _mapper.Map<List<OrderDto>>(entityList);

            var paginationMetadata = new PaginationMetadata(totalRecords, pageSize, pageNumber);

            return (mappedList, paginationMetadata);
        }

        public async Task<bool> OrderExistsAsync(int orderId)
        {
            return await _context.Orders.AnyAsync(x => x.Id == orderId);
        }

        public async Task<bool> RemoveOrderAsync(OrderDto dto)
        {
            var entity = await _context.Orders
                .SingleOrDefaultAsync(x => x.Id == dto.Id);

            if (entity == null)
                throw new ArgumentException($"Order '{dto.OrderNumber}' not found.");

            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveOrderAsync(int id)
        {
            var entity = await _context.Orders
                .SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new ArgumentException("Order not found.");

            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
