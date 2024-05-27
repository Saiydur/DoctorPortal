using AutoMapper;
using Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentHistoryBO = Infrastructure.BusinessObjects.PaymentHistory;
using PaymentHistoryEO = Infrastructure.Entities.PaymentHistory;

namespace Infrastructure.Services
{
    public class PaymentHistoryService : IPaymentHistoryService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        public PaymentHistoryService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }

        public void Add(PaymentHistoryBO paymentHistory)
        {
            var paymentHistoryEO = _mapper.Map<PaymentHistoryEO>(paymentHistory);
            _applicationUnitOfWork.PaymentHistory.Add(paymentHistoryEO);
            _applicationUnitOfWork.Save();
        }

        public void Edit(PaymentHistoryBO paymentHistory)
        {
            var paymentHistoryEO = _applicationUnitOfWork.PaymentHistory.GetById(paymentHistory.Id);
            if (paymentHistoryEO == null)
                throw new InvalidOperationException("PaymentHistory Not Found");
            paymentHistoryEO = _mapper.Map(paymentHistory, paymentHistoryEO);
            _applicationUnitOfWork.Save();
        }

        public void Delete(PaymentHistoryBO paymentHistory)
        {
            _applicationUnitOfWork.PaymentHistory.Remove(paymentHistory.Id);
            _applicationUnitOfWork.Save();
        }

        public PaymentHistoryBO GetById(Guid id)
        {
            var paymentHistoryEO = _applicationUnitOfWork.PaymentHistory.GetById(id);
            if (paymentHistoryEO == null)
                throw new InvalidOperationException("PaymentHistory Not Found");
            var paymentHistoryBO = _mapper.Map<PaymentHistoryBO>(paymentHistoryEO);
            return paymentHistoryBO;
        }

        public IList<PaymentHistoryBO> GetPaymentHistorys()
        {
            var paymentHistoryEOs = _applicationUnitOfWork.PaymentHistory.GetAll();
            var paymentHistoryBOs = _mapper.Map<IList<PaymentHistoryBO>>(paymentHistoryEOs);
            return paymentHistoryBOs;
        }
    }
}
