using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Entity;

namespace BeddestDAL
{
    public class BlockDataAccess
    {
        public static bool BlockRegistered(int blockId)
        {
            Blocks blockFound;
            using (var context = new BeddestModel())
            {
                context.Blocks.Load();
                blockFound = context.Blocks.Find(blockId);
            }
            return blockFound != null;
        }

        public static void AddBlocks(Blocks[] blocks)
        {
            foreach (Blocks block in blocks)
            {
                if (BlockRegistered(block.Id))
                    return;
            }
            using (var context = new BeddestModel())
            {
                context.Blocks.Load();
                context.Blocks.AddRange(blocks);
                context.SaveChanges();
            }
        }

        public static List<Blocks> GetBlocks(int bedId)
        {
            List<Blocks> result;
            using (var context = new BeddestModel())
            {
                context.Blocks.Load();
                result = (from block in context.Blocks
                          where block.BedId == bedId
                          select block).ToList();
            }
            return result;
        }

        public static Blocks GetBlock(int id)
        {
            Blocks result;
            using (var context = new BeddestModel())
            {
                context.Blocks.Load();
                result = (from block in context.Blocks
                          where block.Id == id
                          select block).FirstOrDefault();
            }
            return result;
        }

        public static void ChangeBlock(int blockId, int height, int tilt, int hardness)
        {
            using (var context = new BeddestModel())
            {
                context.Blocks.Load();
                var blockToChange = context.Blocks.Find(blockId);

                if (blockToChange != null)
                {
                    blockToChange.Height = height;
                    blockToChange.TiltAngle = tilt;
                    blockToChange.Hardness = hardness;
                    context.SaveChanges();
                }
            }
        }
    }
}
