using KodaStore.Api.DTOs;

namespace KodaStore.Api.Repositories;

public interface IOrderRepository
{
    Task<IReadOnlyList<OrderSummaryResponse>> GetPageAsync(int page, int pageSize, CancellationToken ct);

    Task<OrderDetailResponse?> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<OrderDetailResponse?> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken);

    Task<OrderDetailResponse?> ConfirmAsync(long id, CancellationToken cancellationToken);

}