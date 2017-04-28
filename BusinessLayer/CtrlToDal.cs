using BeddestDAL;
//using IotMessagesReceiver;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class CtrlToDal
    {
        #region Users
        public static UserDTO LogIn(string name, string password)
        {
            Users user = UserDataAccess.FindUser(name, password);
            if (user != null)
                return user.ToDto();
            else
                return null;
        }

        public static UserDTO Register(string newUserName, string password)
        {
            UserDTO result = null;
            bool userExists = UserDataAccess.CheckUserExists(newUserName);
            if (!userExists)
            {
                UserDataAccess.AddUser(newUserName, password);
                result = LogIn(newUserName, password);
            }
            return result;
        }
        #endregion

        #region Beds
        public static BedDTO GetBed(int bedId)
        {
            Beds bed = BedDataAccess.GetBed(bedId);
            if (bed == null)
                return null;
            List<Blocks> blocks = BlockDataAccess.GetBlocks(bedId);
            int[] blocksDTO = new int[blocks.Count];
            for (int j = 0; j < blocks.Count; j++)
                blocksDTO[j] = blocks[j].Id;
            return bed.ToDto(blocksDTO);
        }

        public static List<BedDTO> GetBeds(int userId)
        {
            List<Beds> userBeds = BedDataAccess.GetBeds(userId);

            int bedsCount = userBeds.Count();
            List<BedDTO> result = new List<BedDTO>();
            foreach (Beds bed in userBeds)
            {
                List<Blocks> blocks = BlockDataAccess.GetBlocks(bed.Id);
                int[] blocksDTO = new int[blocks.Count];
                for (int j = 0; j < blocks.Count; j++)
                    blocksDTO[j] = blocks[j].Id;
                result.Add(bed.ToDto(blocksDTO));
            }
            return result;
        }

        public static async void SetTemp(int bedId, int newTemp)
        {
            //await Transfer.SendAsync(bedId == 1, "SetTemp", newTemp);
            BedDataAccess.UpdateTemperature(bedId, newTemp);
        }

        public static async void SetHeatingTime(int bedId, int newTime)
        {
            //await Transfer.SendAsync(bedId == 1, "SetTime", newTime);
            BedDataAccess.UpdateHeatingTime(bedId, newTime);
        }

        public static void AddBed(int userId, int bedId, int[] blockIds, int[] hardnessLevels)
        {
            int blocksCount = blockIds.Length;
            Blocks[] blocks = new Blocks[blocksCount];
            if (BedDataAccess.BedRegistered(bedId))
                return;
            for (int i = 0; i < blocksCount; i++)
            {
                if (BlockDataAccess.BlockRegistered(blockIds[i]))
                    return;
                blocks[i] = new Blocks(blockIds[i], bedId, hardnessLevels[i]);
            }

            Beds newBed = new Beds(bedId, userId, bedId.ToString());
            BedDataAccess.AddBed(newBed);
            BlockDataAccess.AddBlocks(blocks);
        }
        #endregion
        
        #region Modes
        public static List<ModeDTO> GetModes(int userId)
        {
            List<Modes> userModes = ModeDataAccess.GetModes(userId),
                commonModes = ModeDataAccess.GetModes(0);

            int userModesCount = userModes.Count,
                commonModesCount = commonModes.Count;
            List<ModeDTO> result = new List<ModeDTO>();
            for (int i = 0; i < userModesCount; i++)
            {
                result.Add(userModes[i].ToDto());
            }
            for (int i = 0; i < commonModesCount; i++)
            {
                result.Add(commonModes[i].ToDto());
            }
            return result;
        }

        public static async void SelectModeForIotAsync(int modeId, int bedId)
        {
            Modes mode = ModeDataAccess.GetMode(modeId);
            //await Transfer.SendAsync(bedId == 1, "SelectMode", mode);
        }
        public static void SelectModeForDB(int modeId, int bedId, int headIndex, int legsIndex)
        {
            Modes mode = ModeDataAccess.GetMode(modeId);
            List<Blocks> bedBlocks = BlockDataAccess.GetBlocks(bedId);
            int blocksCount = bedBlocks.Count;
            for (int i = 0; i < blocksCount; i++)
            {
                if (i == headIndex - 1)
                {
                    BlockDataAccess.ChangeBlock(bedBlocks[i].Id, mode.HeadHeight, mode.HeadTilt, mode.HeadHardness);
                }
                else if (i == legsIndex - 1)
                {
                    BlockDataAccess.ChangeBlock(bedBlocks[i].Id, mode.LegsHeight, mode.LegsTilt, mode.LegsHardness);
                }
                else
                {
                    BlockDataAccess.ChangeBlock(bedBlocks[i].Id, mode.OtherHeight, 0, mode.OtherHardness);
                }
            }
        }

        public static async void AddModeForIotAsync(int userId, int bedId, string name)
        {
            //await Transfer.SendAsync(bedId == 1, "AddMode", name);
        }
        public static void AddModeForDB(int userId, int bedId, string name, int head, int legs)
        { 
            List<Blocks> blocks = BlockDataAccess.GetBlocks(bedId);
            Modes newMode = Services.CreateMode(userId, head - 1, legs - 1, name, blocks.ToArray());
            ModeDataAccess.CreateMode(newMode);
        }
        #endregion

        #region Blocks

        public static BlockDTO GetBlockData(int blockId)
        {
            Blocks result = BlockDataAccess.GetBlock(blockId);
            if (result == null)
                return null;
            return result.ToDto();
        }

        public static async void ChangeBlockAsync(int blockId, int height, int tiltAngle, int hardness)
        {
            BlockDTO dto = new BlockDTO(blockId, height, tiltAngle, hardness);
            Blocks block = new Blocks(dto);
            //await Transfer.SendAsync(blockId == 1 || blockId == 2 || blockId == 3 || blockId == 4, "ChangeBlock", block);
            BlockDataAccess.ChangeBlock(blockId, height, tiltAngle, block.Hardness);
        }

        #endregion
    }
}
