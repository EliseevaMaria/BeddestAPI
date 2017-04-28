using BusinessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BeddestAPI.Controllers
{
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
        public void ChangeBlock(int blockId, int height, int tiltAngle, int hardness)
        {
            CtrlToDal.ChangeBlockAsync(blockId, height, tiltAngle, hardness);
        }
    }
}
