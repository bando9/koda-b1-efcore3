using System.Drawing;
using KodaStore.Api.DTOs;
using KodaStore.Api.Repositories;

namespace KodaStore.Api.Services;

public sealed class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }
    public Task<OrderDetailResponse> ConfirmAsync(long id, CancellationToken cancellationToken) => _repository.ConfirmAsync(id,cancellationToken);

    public Task<OrderDetailResponse> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        if(request.Items is null || request.Items.Count == 0) throw new ArgumentException("Order minimal 1 item.");

        if(request.Items.Any(i => string.IsNullOrWhiteSpace(i.ProductCode) || i.Quantity <= 0 )) throw new ArgumentException("ProductCode & Quantity harus valid");

        var codes = request.Items.Select(i => i.ProductCode.Trim().ToUpperInvariant().ToArray());

        // if(codes.Distinct().Count()!=codes.Length) throw new ArgumentException("ProductCode hanya boleh muncul sekali per order");

        return _repository.CreateAsync(request,cancellationToken);
    }

    public Task<OrderDetailResponse> GetByIdAsync(long id, CancellationToken cancellationToken) => _repository.GetByIdAsync(id,cancellationToken);

    public Task<IReadOnlyList<OrderSummaryResponse>> GetPageAsync(int page, int pageSize, CancellationToken ct)
    {
        page = Math.Max(1,page);
        pageSize = Math.Clamp(pageSize,1,50);

        if(page > 10_000) throw new ArgumentException("Page terlalu besar untuk pagination");

        return _repository.GetPageAsync(page,pageSize,ct);
    }
}