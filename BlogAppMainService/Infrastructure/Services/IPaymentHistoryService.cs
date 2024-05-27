using Infrastructure.BusinessObjects;

namespace Infrastructure.Services
{
    public interface IPaymentHistoryService
    {
        void Add(PaymentHistory paymentHistory);
        void Delete(PaymentHistory paymentHistory);
        void Edit(PaymentHistory paymentHistory);
        PaymentHistory GetById(Guid id);
        IList<PaymentHistory> GetPaymentHistorys();
    }
}