using BusinessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BeddestWebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class BlockController : ApiController
    {
        [HttpGet]
        public BlockDTO GetBlockData(int blockId)
        {
            BlockDTO block = CtrlToDal.GetBlockData(blockId);
            return block;
        }

        [HttpGet]
        // [HttpPut]
        public async Task ChangeBlock(int blockId, int height, int tiltAngle, int hardness)
        {
            await CtrlToDal.ChangeBlockAsync(blockId, height, tiltAngle, hardness);
        }
    }
}
