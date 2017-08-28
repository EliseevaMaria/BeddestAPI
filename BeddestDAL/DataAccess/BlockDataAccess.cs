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
            Block blockFound;
            using (var context = new BeddestModel())
            {
                context.Blocks.Load();
                blockFound = context.Blocks.Find(blockId);
            }
            return blockFound != null;
        }

        public static void AddBlocks(Block[] blocks)
        {
            //foreach (Blocks block in blocks)
            //{
            //    if (BlockRegistered(block.Id))
            //        return;
            //}
            using (var context = new BeddestModel())
            {
                context.Blocks.Load();
                context.Blocks.AddRange(blocks);
                context.SaveChanges();
            }
        }

        public static List<Block> GetBlocks(int bedId)
        {
            List<Block> result;
            using (var context = new BeddestModel())
            {
                context.Blocks.Load();
                result = (from block in context.Blocks
                          where block.BedId == bedId
                          select block).ToList();
            }
            return result;
        }

        public static Block GetBlock(int id)
        {
            Block result;
            using (var context = new BeddestModel())
            {
                context.Blocks.Load();
                result = (from block in context.Blocks
                          where block.BlockId == id
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

        public static void RemoveBlocks(int bedId)
        {
            using (var context = new BeddestModel())
            {
                context.Blocks.Load();
                List<Block> blocksToRemove = (from block in context.Blocks
                                              where block.BedId == bedId
                                              select block).ToList();

                context.Blocks.RemoveRange(blocksToRemove);
                context.SaveChanges();
            }
        }
    }
}
