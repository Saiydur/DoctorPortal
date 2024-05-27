using Autofac;
using Infrastructure.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MainServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MedicineRequest
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }

    public class HospitalRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class HealthPlanRequest
    {
        public string Name { get; set; }
        public int Prize { get; set; }
        public string Details { get; set; }
        public int Duration { get; set; }

    }
    public class MicroController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        private readonly IRabitMQProducer _rabbitmqProducer;

        public MicroController(ILifetimeScope scope,IRabitMQProducer rabitMQProducer)
        {
            _scope = scope;
            _rabbitmqProducer = rabitMQProducer;
        }

        [HttpGet("/medicines")]
        public IActionResult GetMedicine()
        {
            try
            {
                _rabbitmqProducer.SendMessage("microservice_queue", "get-medicine", 1);
                var result = _rabbitmqProducer.RecivedMessage("medicine-response");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/medicines/{id}")]
        public IActionResult GetMedicineById(int id)
        {
            try
            {
                _rabbitmqProducer.SendMessage("microservice_queue", "get-medicine-by-id", id);
                var result = _rabbitmqProducer.RecivedMessage("medicine-response");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("/medicines")]
        public IActionResult PostMessage([FromBody]MedicineRequest data)
        {
            try
            {
                _rabbitmqProducer.SendMessage("microservice_queue", "post-medicine", data);
                var result = _rabbitmqProducer.RecivedMessage("medicine-response");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("/medicines/{id}")]
        public IActionResult PutMessage(int id, [FromBody] MedicineRequest data)
        {
            try
            {
                _rabbitmqProducer.SendMessage("microservice_queue", "put-medicine", new { id = id, data = data });
                var result = _rabbitmqProducer.RecivedMessage("medicine-response");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("/medicines/{id}")]
        public IActionResult DeleteMessage(int id)
        {
            try
            {
                _rabbitmqProducer.SendMessage("microservice_queue", "delete-medicine", id);
                var result = _rabbitmqProducer.RecivedMessage("medicine-response");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/hospitals")]
        public IActionResult GetHospital()
        {
            try
            {
                _rabbitmqProducer.SendMessage("microservice_queue", "get-hospitals", 1);
                var result = _rabbitmqProducer.RecivedMessage("hospital-response");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/hospitals/{id}")]
        public IActionResult GetHospitalById(int id)
        {
            try
            {
                _rabbitmqProducer.SendMessage("microservice_queue", "get-hospitals-by-id", id);
                var result = _rabbitmqProducer.RecivedMessage("hospital-response");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("/hospitals")]
        public IActionResult PostHospital([FromBody] HospitalRequest data)
        {
            try
            {
                _rabbitmqProducer.SendMessage("microservice_queue", "post-hospitals", data);
                var result = _rabbitmqProducer.RecivedMessage("hospital-response");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("/hospitals/{id}")]
        public IActionResult PutHospital(int id, [FromBody] MedicineRequest data)
        {
            try
            {
                _rabbitmqProducer.SendMessage("microservice_queue", "put-hospitals", new { id = id, data = data });
                var result = _rabbitmqProducer.RecivedMessage("hospital-response");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("/hospitals/{id}")]
        public IActionResult DeleteHospital(int id)
        {
            try
            {
                _rabbitmqProducer.SendMessage("microservice_queue", "delete-hospitals", id);
                var result = _rabbitmqProducer.RecivedMessage("hospital-response");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/health-plans")]
        public IActionResult GetHealthPlan()
        {
            try
            {
                _rabbitmqProducer.SendMessage("microservice_queue", "get-healthPlans", 1);
                var result = _rabbitmqProducer.RecivedMessage("health-response");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/health-plans/{id}")]
        public IActionResult GetHealthPlanById(int id)
        {
            try
            {
                _rabbitmqProducer.SendMessage("microservice_queue", "get-healthPlans-by-id", id);
                var result = _rabbitmqProducer.RecivedMessage("health-response");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("/health-plans")]
        public IActionResult PostHealthPlan([FromBody] HealthPlanRequest data)
        {
            try
            {
                _rabbitmqProducer.SendMessage("microservice_queue", "post-healthPlans", data);
                var result = _rabbitmqProducer.RecivedMessage("health-response");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("/health-plans/{id}")]
        public IActionResult PutHealthPlan(int id, [FromBody] HealthPlanRequest data)
        {
            try
            {
                _rabbitmqProducer.SendMessage("microservice_queue", "put-healthPlans", new { id = id, data = data });
                var result = _rabbitmqProducer.RecivedMessage("health-response");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("/health-plans/{id}")]
        public IActionResult DeleteHealthPlan(int id)
        {
            try
            {
                _rabbitmqProducer.SendMessage("microservice_queue", "delete-healthPlans", id);
                var result = _rabbitmqProducer.RecivedMessage("health-response");
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
