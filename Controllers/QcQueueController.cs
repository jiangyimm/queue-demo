using Coravel.Queuing.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace queue_demo.Controllers;

[ApiController]
[Route("[controller]")]
public class QcQueueController : ControllerBase
{
    IQueue _queue;

    public QcQueueController(IQueue queue)
    {
        _queue = queue;
    }

    [HttpPost]
    public async Task<IActionResult> NewJob()
    {
        for (var index = 0; index < 3000; index++)
        {
            var qcQueueModel = new QcQueueModel("0101", $"{index}");
            _queue.QueueInvocableWithPayload<QcQueue, QcQueueModel>(qcQueueModel);
        }

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> QueryJob()
    {
        var jobs = _queue.GetMetrics();
        return Ok(new
        {
            RunningCount = jobs.RunningCount(),
            WaitingCount = jobs.WaitingCount()
        });
    }
}
