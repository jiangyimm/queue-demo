using Coravel.Invocable;

namespace queue_demo
{
    public class QcQueue : IInvocable, IInvocableWithPayload<QcQueueModel>
    {
        public QcQueueModel Payload { get; set; }

        private readonly ILogger<QcQueue> _logger;

        public QcQueue(ILogger<QcQueue> logger)
        {
            _logger = logger;
        }

        public async Task Invoke()
        {
            _logger.LogError($"begin... [{this.GetHashCode()}]{Payload.ToString()}");
            await Task.Delay(TimeSpan.FromSeconds(5));
            _logger.LogError($"end... [{this.GetHashCode()}]{Payload.ToString()}");
        }
    }

    public class QcQueueModel
    {
        public QcQueueModel(string hospCode, string inpatId)
        {
            HospCode = hospCode;
            InpatId = inpatId;
        }
        public string HospCode { get; set; }
        public string InpatId { get; set; }

        public override string ToString()
        {
            return $"HospCode:{HospCode} InpatId:{InpatId}";
        }
    }
}