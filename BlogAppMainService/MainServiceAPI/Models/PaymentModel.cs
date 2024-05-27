using Autofac;
using AutoMapper;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;

namespace MainServiceAPI.Models
{
    public class PaymentModel
    {
        public Guid Id { get; set; }
        public string PaymentType { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid UserId { get; set; }
        public Guid DoctorId { get; set; }

        public IPaymentHistoryService _paymentHistoryService { get; set; }
        public IMapper _mapper { get; set; }

        public PaymentModel()
        {
            
        }

        public PaymentModel(IPaymentHistoryService paymentHistoryService, IMapper mapper)
        {
            _paymentHistoryService = paymentHistoryService;
            _mapper = mapper;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _paymentHistoryService = scope.Resolve<IPaymentHistoryService>();
            _mapper = scope.Resolve<IMapper>();
        }

        public void Add()
        {
            var paymentBO = _mapper.Map<PaymentHistory>(this);
            _paymentHistoryService.Add(paymentBO);
        }

        public void Remove()
        {
            var paymentBO = _mapper.Map<PaymentHistory>(this);
            _paymentHistoryService.Delete(paymentBO);
        }

        public void Update()
        {
            var paymentBO = _mapper.Map<PaymentHistory>(this);
            _paymentHistoryService.Edit(paymentBO);
        }

        public PaymentModel GetPaymentById(Guid id)
        {
            var paymentBO = _paymentHistoryService.GetById(id);
            var payment = _mapper.Map<PaymentModel>(paymentBO);
            return payment;
        }

        public IList<PaymentModel> GetPayments()
        {
            var paymentBOs = _paymentHistoryService.GetPaymentHistorys();
            var payments = new List<PaymentModel>();
            foreach (var paymentBO in paymentBOs)
            {
                payments.Add(_mapper.Map<PaymentModel>(paymentBO));
            }
            return payments;
        }
    }
}
